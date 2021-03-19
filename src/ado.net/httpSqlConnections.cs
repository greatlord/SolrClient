
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;


using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;


using System.Threading.Channels;

using SolrHTTP.Docs;
using SolrHTTP.Helper;

namespace SolrHTTP.NET.Data
{
    public class SolrHTTPConnection : DbConnection, ICloneable
    {
        /// <summary>
        /// The physical connection to the solr.
        /// </summary>
        private SolrHTTPClient _solrClient = null;


        /// <summary>
        /// The config been autogenrated when connections string been set.
        /// </summary>
        private SolrHTTP.Config _solrConfig = null;

        /// <summary>
        /// The original connection string provided by the user, including the password.
        /// </summary>
        private string _connectionsString = string.Empty;
      
        /// <summary>
        /// The real internal status of connection, it method will set from change connections string, open, close connections
        /// </summary>
        private ConnectionState _state = ConnectionState.Closed;
  
        private bool _isDisposed = false; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolrHTTPConnection"/> class.
        /// </summary>
        public SolrHTTPConnection () {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="NpgsqlConnection"/> with the given connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the Solr database.</param>
        public SolrHTTPConnection (string conn) {    
            ConnectionString = conn;
            this._SetSolrConfig(conn);
        }

        public override string Database
        {
            get
            { return this._solrConfig.solrCore[0].coreName; }
        }

        /// <summary>
        /// Gets the string identifying the database server (host and port)
        /// </summary>
        /// <value>
        /// The name of the database server (host and port). the default value is the empty string.
        /// </value>
        public override string DataSource {           
            get { return this._solrConfig.solrServerUrl; }                 
        }


        /// <summary>
        /// Gets or sets the string used to connect to a Solr database. 
        /// </summary>
        /// <value>The connection string that includes the server url,
        /// the database name, and other parameters needed to establish
        /// the initial connection. The default value is an empty string.
        /// </value>
        public override string ConnectionString { 
            get { return _connectionsString; }
            set {                 
                this._SetStateChange(ConnectionState.Closed);
                this._SetSolrConfig(value); 
            }
        }

        /// <summary>
        /// Gets the current state of the connection.
        /// </summary>
        /// <value>A bitwise combination of the <see cref="System.Data.ConnectionState"/> values. The default is <b>Closed</b>.</value> 
        public override ConnectionState State {
            get { return _state; }             
        }

        /// <summary>
        /// Opens a database connection with the property settings specified by the <see cref="ConnectionString"/>.
        /// The http client will always close the connection direcly after open been call, we emulated it stay open.
        /// </summary>
        public override void Open()
        {                 
            this.OpenAsync().Wait();                           
        }

        /// <summary>
        /// Opens a database connection with the property settings specified by the <see cref="ConnectionString"/>.
        /// The http client will always close the connection direcly after open been call, we emulated it stay open.
        /// </summary>
        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            await Task.Run( async () => {
               
                if ( this._state == ConnectionState.Open ) {
                    // if it already open do not test same connections again
                    return;  
                }

                this._SetStateChange( ConnectionState.Connecting);
                
                if ( this._solrConfig == null ) {
                    this._SetStateChange( ConnectionState.Closed);                    
                    throw new NotSupportedException();
                }

                this._solrClient = new SolrHTTPClient( this._solrConfig );
                SolrJsonDocument SolrDocument = new SolrJsonDocument();
                solrBuildHttpParms parms = new solrBuildHttpParms();
                parms.Add("q","*:*");
                parms.Add("start","0");
                parms.Add("rows","1");                    

                var result = await this._solrClient.SelectAsync(0,parms,null);
                
                if ( this._solrClient.status.StatusCode != HttpStatusCode.OK ) {                
                    this._SetStateChange( ConnectionState.Closed);
                    throw new NotSupportedException();                
                }            

                SolrDocument = JsonSerializer.Deserialize<SolrJsonDocument>(result);                                        
                if (  
                      (SolrDocument == null) ||
                      (SolrDocument.responseHeader == null) ||
                      (!SolrDocument.responseHeader.zkConnected.HasValue) || 
                      (!SolrDocument.responseHeader.zkConnected.Value)) {
                    // Solr are not in cloud mode
                    // the sql interface are not vaild then
                    this._SetStateChange( ConnectionState.Closed);
                    throw new NotSupportedException();  
                }
                this._SetStateChange( ConnectionState.Open);

           });
        }


        /// <summary>
        /// Releases the connection. 
        /// The http client will always close the connection direcly, we emulated it close
        /// </summary>
        public override void Close()
        {
            this.CloseAsync().Wait();            
        }

        /// <summary>
        /// Releases the connection. 
        /// The http client will always close the connection direcly, we emulated it close
        /// </summary>
        public override async Task CloseAsync()
        {
            await Task.Run(async () => {
                this._solrClient = null;
                this._SetStateChange( ConnectionState.Closed );
                await Task.Delay(5);
            });
           
        }

      


        
        /// <summary>
        /// Creates and returns a <see cref="SolrHTTPCommand"/> object associated with the <see cref="SolrHTTPCommand"/>.
        /// </summary>
        /// <returns>A <see cref="SolrHTTPCommand"/> object.</returns>
        public new SolrHTTPCommand CreateCommand()
        {
            return (SolrHTTPCommand)base.CreateCommand();
        }

        /// <summary>
        /// Creates and returns a <see cref="SolrHTTPCommand"/> object associated with the <see cref="SolrHTTPCommand"/>.
        /// </summary>
        /// <param name="commandText">Gets or sets the SQL statement, table name or stored procedure to execute at the data source.</param>
        /// <returns>A <see cref="SolrHTTPCommand"/> object.</returns>
        public SolrHTTPCommand CreateCommand(string commandText)
        {
            var command = CreateCommand();
            command.CommandText = commandText;

            return command;
        }


        /// <summary>
        /// Creates and returns a <see cref="System.Data.Common.DbCommand"/>
        /// object associated with the <see cref="System.Data.Common.DbConnection"/>.
        /// </summary>
        /// <returns>A <see cref="System.Data.Common.DbCommand"/> object.</returns>
        protected override DbCommand CreateDbCommand()
        {
            
            return new SolrHTTPCommand(this);
        }

        protected override void Dispose(bool disposing) {
            if (!_isDisposed) {
                this._isDisposed = true;
                base.Dispose(disposing);
            }
        }

        // private method here
       

        private void _SetStateChange( ConnectionState newState ) {
            
            StateChangeEventArgs changeParms;
            changeParms = new StateChangeEventArgs(this._state, newState );
            this._state = newState;
                    
            this.OnStateChange(changeParms);
        }

        private void _SetSolrConfig(string conn ) {
            string[] split;

            if ( string.IsNullOrEmpty(conn) ) {
                throw new NotSupportedException();
            }

            split = conn.Split(";");
            if (split.Length < 2 ) {
                throw new NotSupportedException();
            }

            this._solrConfig = new Config();
            this._solrConfig.solrCore = new CoreConfig[1];
            this._solrConfig.solrCore[0] = new CoreConfig();

            foreach(string prop in split) {
                string[] keyValue = prop.Split("=");
                switch( keyValue[0].ToLower() ) {
                    case "host":
                        this._solrConfig.solrServerUrl = keyValue[1];                        
                        break;

                    case "user":
                        this._solrConfig.solrUserName = keyValue[1];
                        this._solrConfig.solrUseBaiscAuth = true;
                        break;

                    case "password":
                        this._solrConfig.solrPassword = keyValue[1];
                        this._solrConfig.solrUseBaiscAuth = true;
                        break;

                    case "database":                        
                        this._solrConfig.solrCore[0].coreName = keyValue[1];
                        break;

                    default:
                        throw new NotSupportedException();                        
                }   
            }

            if ( string.IsNullOrEmpty( this._solrConfig.solrServerUrl ) ) {
                throw new NotSupportedException();
            }

            if ( string.IsNullOrEmpty( this._solrConfig.solrCore[0].coreName ) ) {
                throw new NotSupportedException();
            }

        }

        
        // Not suppred at all
       
        public override string ServerVersion
        {
            get
            {
                throw new NotSupportedException();
            }
        }  

        public override DataTable GetSchema()
        {
           throw new NotSupportedException();
        }
       
        public override DataTable GetSchema(string collectionName)
        {
            throw new NotSupportedException();
        }

        public override DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            throw new NotSupportedException();
        }

        

       

       


        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotSupportedException();
        }

        public override void ChangeDatabase(string databaseName)
        {            
           throw new NotSupportedException();
        }
             
        public object Clone()
        {
             throw new NotSupportedException();            
        }
        
    }
}