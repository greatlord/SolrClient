using SolrHTTP.NET.Data;

using System.Text.Json;
using SolrHTTP;
using System;
using System.Data;

namespace solrClientTest {
    public class TestSolrADOConn {

        public bool TestDbConnection() {

            bool retValue = true;
            string connectionsting;
            SolrHTTPConnection conn;

            conn = null;
            connectionsting = "Host=http://srv5.greatlord.com:8983/;Database=techproducts";
            
            // Connections string test
            Console.WriteLine("start: ConnectionSting");
            retValue = true;
            try {
                conn = new SolrHTTPConnection(connectionsting);           
            } catch {
               retValue = false; 
               Console.WriteLine("fail: ConnectionSting");
            }
            Console.WriteLine("done: ConnectionSting");

            Console.WriteLine("start: state");
            if ( conn.State != ConnectionState.Closed) {
                 Console.WriteLine("fail: ConnectionState closed");
            }
            Console.WriteLine("done: state");
            
            conn.StateChange += HandleSqlConnectionDrop;

            // Method Open test
            Console.WriteLine("start: Open()");
            try {               
                conn.Open();            
            } catch {
               Console.WriteLine("fail: Open()");
            }
            Console.WriteLine("done: Open()");

            Console.WriteLine("start: state");
            if ( conn.State != ConnectionState.Open) {
                 Console.WriteLine("fail: ConnectionState open");
            }
            Console.WriteLine("done: state");

            // Method Close test
            Console.WriteLine("start: Close()");
            try {               
                conn.Close();            
            } catch {
               Console.WriteLine("fail: Close()");
            }
            Console.WriteLine("done: Close()");

            Console.WriteLine("start: state");
            if ( conn.State != ConnectionState.Closed) {
                 Console.WriteLine("fail: ConnectionState Closeed");
            }
            Console.WriteLine("done: state");

            
            
            return retValue;
        }
        public void HandleSqlConnectionDrop(object connection, StateChangeEventArgs args) {
            
        }
    }

}