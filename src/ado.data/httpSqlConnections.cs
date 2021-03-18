
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


namespace SolrHTTP.NET.Data
{
    public class SolrHTTPConnection : DbConnection, ICloneable
    {
        private SolrHTTPClient _solrClient;
        private SolrHTTP.Config _solrConfig = null;
        private string _connectionsString;


        
        
        private string _serverVersion;
        private ConnectionState _state = ConnectionState.Closed;
        private bool _isDisposed;

        

        public SolrHTTPConnection () {

        }

        public SolrHTTPConnection (string conn) {    
            ConnectionString = conn;
            this._SetSolrConfig(conn);
        }

        public override string Database
        {
            get
            { return this._solrConfig.solrCore[0].coreName; }
        }

        public override string DataSource {           
            get { return this._solrConfig.solrServerUrl; }                 
        }


        public override string ConnectionString { 
            get { return _connectionsString; }
            set {                 
                this._SetStateChange(ConnectionState.Closed);
                this._SetSolrConfig(value); 
            }
        }
                
        public override ConnectionState State {
            get { return _state; }             
        }

        public override void Open()
        {     
            
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

            var result = this._solrClient.Select(0,parms,null);

            if ( this._solrClient.status.StatusCode != HttpStatusCode.OK ) {                
                this._SetStateChange( ConnectionState.Closed);
                throw new NotSupportedException();                
            }            

            SolrDocument = JsonSerializer.Deserialize<SolrJsonDocument>(result);            
            if (!SolrDocument.responseHeader.zkConnected) {
                // Solr are not in cloud mode
                // the sql interface are not vaild then
                this._SetStateChange( ConnectionState.Closed);
                throw new NotSupportedException();  
            }
            this._SetStateChange( ConnectionState.Open);
               
        }

        public override void Close()
        {
            this._solrClient = null;
            this._SetStateChange( ConnectionState.Closed );            
        }

        public new SolrHTTPCommand CreateCommand()
        {
            return (SolrHTTPCommand)base.CreateCommand();
        }

        public SolrHTTPCommand CreateCommand(string commandText)
        {
            var command = CreateCommand();
            command.CommandText = commandText;

            return command;
        }

        protected override DbCommand CreateDbCommand()
        {
            
            return new SolrHTTPCommand(this);
        }

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

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
           throw new NotSupportedException();
        }

        public override async Task CloseAsync()
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
       
       
        protected override void Dispose(bool disposing)
        {
            
        }
       
        public object Clone()
        {
             throw new NotSupportedException();            
        }
        
    }
}