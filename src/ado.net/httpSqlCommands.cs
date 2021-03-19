// Note look at https://github.com/chequer-io/JDBC.NET/blob/master/JDBC.NET.Data/
// how they implement ado.net driver for java jdbc connections
 
using System;
using System.Data;
using System.Data.Common;
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

        internal SolrHTTPCommand(SolrHTTPConnection connection) {

            this._connection = connection;
        }

        internal SolrHTTPCommand(string commandText, SolrHTTPConnection connection) {

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
            if (_isPrepare) {
                throw new NotSupportedException();
            }
            throw new NotSupportedException();           
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