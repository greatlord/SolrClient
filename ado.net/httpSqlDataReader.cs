using System;
using System.Collections;
using System.Data;
using System.Data.Common;



namespace SolrHTTP.NET.Data
{
    public class SolrHTTPDataReader : DbDataReader
    {
 
 
        private bool _isClosed;
        
 

        public override int FieldCount {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override object this[int ordinal]
        {
            get
            {
               throw new NotSupportedException();
            }
        }

        public override object this[string name]
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override bool HasRows {
            get
            {
                throw new NotSupportedException();
            }
        }
        public override bool IsClosed {
            get
            {
                throw new NotSupportedException();
            }
        }        
        public override int RecordsAffected {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override int Depth => 0;
 

 
        internal SolrHTTPDataReader(SolrHTTPCommand command)
        {
            throw new NotSupportedException();
        }
 
        public override object GetValue(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public override DataTable GetSchemaTable()
        {
            throw new NotSupportedException();            
        }

        public override string GetName(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override int GetOrdinal(string name)
        {
            throw new NotSupportedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override short GetInt16(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override int GetInt32(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override long GetInt64(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override bool GetBoolean(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override string GetString(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override char GetChar(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override bool IsDBNull(int ordinal)
        {
            throw new NotSupportedException();
        }
       

        
        public override bool Read()
        {
            throw new NotSupportedException();
        }

        public override bool NextResult()
        {
            throw new NotSupportedException();
        }

        public override void Close()
        {
           throw new NotSupportedException();
        }
       
       
        protected override void Dispose(bool disposing)
        {
            throw new NotSupportedException();
        }
        
        public override IEnumerator GetEnumerator()
        {
            throw new NotSupportedException();
        }
        
    }
}