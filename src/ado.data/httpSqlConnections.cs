
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace SolrHTTP.NET.Data
{
    public class SolrHTTPConnection : DbConnection, ICloneable
    {
        
        private string _connectionsString;
        private SolrHTTP.Config Cfg;
        
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
            { return this.Cfg.solrCore[0].coreName; }
        }

        public override string DataSource {           
            get { return this.Cfg.solrServerUrl; }                 
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

            this.Cfg = new Config();
            this.Cfg.solrCore = new CoreConfig[1];
            this.Cfg.solrCore[0] = new CoreConfig();

            foreach(string prop in split) {
                string[] keyValue = prop.Split("=");
                switch( keyValue[0].ToLower() ) {
                    case "host":
                        this.Cfg.solrServerUrl = keyValue[1];                        
                        break;

                    case "user":
                        this.Cfg.solrUserName = keyValue[1];
                        this.Cfg.solrUseBaiscAuth = true;
                        break;

                    case "password":
                        this.Cfg.solrPassword = keyValue[1];
                        this.Cfg.solrUseBaiscAuth = true;
                        break;

                    case "database":                        
                        this.Cfg.solrCore[0].coreName = keyValue[1];
                        break;

                    default:
                        throw new NotSupportedException();                        
                }

                if ( string.IsNullOrEmpty( this.Cfg.solrServerUrl ) ) {
                    throw new NotSupportedException();
                }

                if ( string.IsNullOrEmpty( this.Cfg.solrCore[0].coreName ) ) {
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
            throw new NotSupportedException();
        }

        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
           throw new NotSupportedException();
        }

        public override void Close()
        {
           throw new NotSupportedException();
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