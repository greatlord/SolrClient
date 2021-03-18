
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SolrHTTP.NET.Data
{
    public class SolrHTTPConnection : DbConnection, ICloneable
    {
        
        private string _database;
        private string _serverVersion;
        private ConnectionState _state = ConnectionState.Closed;
        private bool _isDisposed;
        

        
        public override string Database
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override string DataSource {           
            get { throw new NotSupportedException(); }                 
        }

        public override string ServerVersion
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override string ConnectionString
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
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