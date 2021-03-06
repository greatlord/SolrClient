
using System.Text.Json;
using SolrHTTP;
using SolrHTTP.Docs;

namespace solrClientTest {
    
    public class TestSolr {

        public bool TestSolrConntecions() {

            SolrHTTPClient solr;
            solrBuildHttpParms parms = new solrBuildHttpParms();
            SolrHTTP.Config cfg = new Config();

            // Setup config without basic auth;
            cfg.solrServerUrl = "http://srv5.greatlord.com:8983";


            cfg.coreName = "techproducts";

            // create solr http client
            solr = new SolrHTTPClient(cfg);

            // add parms you want for select
            parms.Add("q","*:*");
            parms.Add("start","0");
            parms.Add("rows","1");

            // execute the client
            var x = solr.Select(parms,null);

            if ( solr.status.StatusCode == System.Net.HttpStatusCode.OK ) {
                return true;
            }
        
            return false;
        }

        public bool TestSolrGetDocs() {

            SolrHTTPClient solr;
            SolrJsonDocument docs;
            solrBuildHttpParms parms = new solrBuildHttpParms();
            SolrHTTP.Config cfg = new Config();

            // Setup config without basic auth;
            cfg.solrServerUrl = "http://srv5.greatlord.com:8983";
         

            cfg.coreName = "techproducts";

            // create solr http client
            solr = new SolrHTTPClient(cfg);

            // add parms you want for select
            parms.Add("q","*:*");
            parms.Add("start","0");
            parms.Add("rows","10");

            // execute the client
            var x = solr.Select(parms,null);

            if ( solr.status.StatusCode != System.Net.HttpStatusCode.OK ) {
                return false;
            }

            docs = JsonSerializer.Deserialize<SolrJsonDocument>(x);
            if (docs.response.numFound < 1 )  {
                return false;
            }
            if ( docs.response.docs.RootElement.GetArrayLength() != 10 ) {
                return false;
            }

           
            
            return true;
        }

    } 
}