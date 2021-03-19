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
        
        public int FetchSize { get; set; }

        public override string CommandText { get; set; }

        public override int CommandTimeout { get; set; }

        public override CommandType CommandType { get; set; }

        public override UpdateRowSource UpdatedRowSource
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        protected override DbConnection DbConnection { get; set; }

        protected override DbParameterCollection DbParameterCollection => Parameters;

        

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
            throw new NotSupportedException();
        }


        
        public override void Prepare()
        {
            throw new NotSupportedException();
        }

        public override int ExecuteNonQuery()
        {
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