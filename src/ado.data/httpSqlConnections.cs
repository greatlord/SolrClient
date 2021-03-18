
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

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
            this.SetSolrConfig(conn);
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
            set { this.SetSolrConfig(value); }
        }
        

        private void SetSolrConfig(string conn ) {
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

                if ( string.IsNullOrEmpty( this._solrConfig.solrServerUrl ) ) {
                    throw new NotSupportedException();
                }

                if ( string.IsNullOrEmpty( this._solrConfig.solrCore[0].coreName ) ) {
                    throw new NotSupportedException();
                }
            }
        }

        public override string ServerVersion
        {
            get
            {
                throw new NotSupportedException();
            }
        }  
        public override ConnectionState State => _state;
        
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

        public override void Open()
        {     
            this._state = ConnectionState.Connecting;

            if ( this._solrConfig == null ) {
                this._state = ConnectionState.Closed;
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
                this._state = ConnectionState.Closed;
                throw new NotSupportedException();                
            }            

            SolrDocument = JsonSerializer.Deserialize<SolrJsonDocument>(result);            
            if (!SolrDocument.responseHeader.zkConnected) {
                // Solr are not in cloud mode
                // the sql interface are not vaild then
                this._state = ConnectionState.Closed;
                throw new NotSupportedException();  
            }

            this._state = ConnectionState.Open;
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
           throw new NotSupportedException();
        }

        public override void Close()
        {
            this._solrClient = null;
            this._state = ConnectionState.Closed;
        }

        public override async Task CloseAsync()
        {
            throw new NotSupportedException();
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