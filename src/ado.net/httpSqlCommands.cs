// Note look at https://github.com/chequer-io/JDBC.NET/blob/master/JDBC.NET.Data/
// how they implement ado.net driver for java jdbc connections
 
using System;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using SolrHTTP.Docs;

namespace SolrHTTP.NET.Data
{
    public class SolrHTTPCommand : DbCommand
    {
        private SolrHTTPConnection _connection;
        private SolrHTTPDataReader _dataReader;
        

        public int FetchSize  {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override string CommandText { get; set; }

        public override int CommandTimeout  {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override CommandType CommandType  {
            get { return CommandType.Text; }
            set {
                if ( value != CommandType.Text ) {
                    throw new NotSupportedException();
                }                
            }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        protected override DbConnection DbConnection  {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
        protected override DbParameterCollection DbParameterCollection  {
            get => throw new NotSupportedException();          
        }
        

        protected override DbTransaction DbTransaction
        {     
            get => throw new NotSupportedException();       
            set => throw new NotSupportedException();
        }

        public override bool DesignTimeVisible
        {
            get => false;
            set => throw new NotSupportedException();
        }

        public SolrHTTPCommand(SolrHTTPConnection connection) {

            this._connection = connection;
        }

        public SolrHTTPCommand(string commandText, SolrHTTPConnection connection) {

            this._connection = connection;
            this.CommandText = commandText;
        }
        

        
        public override void Prepare()
        {
            throw new NotSupportedException();
        }

        public bool _isPrepare = false;

        public override int ExecuteNonQuery()
        {
            string strSQL;
            string jsonData;
            SolrJsonSQLDocument docs;

            strSQL = this.CommandText;

            if (_isPrepare) {
                throw new NotSupportedException();
            }
            
            if (this._connection.State != ConnectionState.Open ) {
                throw new NotSupportedException();
            }

            jsonData = this._connection._solrClient.Sql(0,null,strSQL); 

            try {
                docs = System.Text.Json.JsonSerializer.Deserialize<SolrJsonSQLDocument>(jsonData);
            } catch (Exception ex ) {
                 throw new ArgumentException( jsonData );                
            }
            
            docs.rawData = jsonData;
            
            if ( this._connection._solrClient.status.StatusCode != HttpStatusCode.OK ) {
                if ( ( docs.resultSet != null ) && 
                     ( docs.resultSet.docs != null ) &&
                     ( docs.resultSet.docs.Length > 0 ) ) {
                        
                    foreach ( var doc in docs.resultSet.docs ) {
                        if (!string.IsNullOrEmpty(doc.EXCEPTION)) {
                            throw new ArgumentException( doc.EXCEPTION );
                        }
                    }
                }

                if ( docs.error != null ) {
                    if (!string.IsNullOrEmpty( docs.error.msg )) {
                        throw new ArgumentException( docs.error.msg );
                    }

                    if (!string.IsNullOrEmpty( docs.error.metadata.errorClass )) {
                        throw new ArgumentException( docs.error.metadata.errorClass );
                    }

                     if (!string.IsNullOrEmpty( docs.error.metadata.rootErrorClass )) {
                        throw new ArgumentException( docs.error.metadata.rootErrorClass );
                    }
                }
                
                throw new NotSupportedException();
                //throw new ArgumentException("Index is out of range", nameof(index), ex);
            }

            return docs.resultSet.docs.Length-1;
        }

        public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
           throw new NotSupportedException();           
        }

        public override object ExecuteScalar()
        {
            throw new NotSupportedException();
        }

        public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotSupportedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotSupportedException();            
        }

        protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
           throw new NotSupportedException();
        }

        public override void Cancel()
        {
            throw new NotSupportedException();
        }
        
        protected override void Dispose(bool disposing)
        {            
            throw new NotSupportedException();           
        }
        
    }

}