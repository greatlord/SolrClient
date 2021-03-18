using SolrHTTP.NET.Data;

using System.Text.Json;
using SolrHTTP;

namespace solrClientTest {
    public class TestSolrADOConn {

        public bool TestDbConnection() {

            string connectionsting;

            connectionsting = "Host=http://srv5.greatlord.com:8983/;Database=techproducts";
            SolrHTTPConnection conn = new SolrHTTPConnection(connectionsting);
            
            return false;
        }
    }

}