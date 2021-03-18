using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;



namespace SolrHTTP
{

    public class SolrHTTPClient
    {
     
        private readonly HttpClient client = new HttpClient();
        public HttpResponseMessage status = new HttpResponseMessage();
        private Random rnd = new Random();

        public int commitWithin = 1000;

        private Config Cfg;

        private string solrServerUrlApi { get; set;}

        public SolrHTTPClient( Config cfg ) {

            if (cfg.solrServerUrl.Substring(cfg.solrServerUrl.Length - 5, 5)== "solr/") {
                cfg.solrServerUrl = cfg.solrServerUrl.Substring(0,cfg.solrServerUrl.Length - 5);
            }

            if (cfg.solrServerUrl.Substring(cfg.solrServerUrl.Length - 1, 1) == "/") {
                cfg.solrServerUrl = cfg.solrServerUrl.Substring(0,cfg.solrServerUrl.Length - 1);
            }
            cfg.solrServerUrl = cfg.solrServerUrl + "/solr/";
            solrServerUrlApi = cfg.solrServerUrl + "/api/";

            client.BaseAddress = new Uri( cfg.solrServerUrl );

            Cfg = cfg;

            if ( cfg.solrUseBaiscAuth ) {
                if (  !String.IsNullOrEmpty( cfg.solrUserName ) && !String.IsNullOrEmpty( cfg.solrPassword ) ) {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + getBase64BasicAuthUserAndPassword( cfg.solrUserName, cfg.solrPassword ) );
                }
            }
            
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");                        
        }
       
        
        public string Update( int coreIndexId, string strJsonData, bool overWrite ) {                      
            
            var solrUpdate = asyncUpdate( coreIndexId, strJsonData, overWrite);

            solrUpdate.Wait();

            return solrUpdate.Result;
        }

        /// <summary>
        /// Solr http Select interface see https://solr.apache.org/guide/8_8/query-syntax-and-parsing.html
        /// it is not 100% documemations how to use it.
        /// </summary>
        /// <param name="coreIndexId">index number which core/collections/database should be use</param>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <returns>raw solr response json string</returns>
        public string Select( int coreIndexId, solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSelect = this._asyncSelect( coreIndexId, httpQuery, strData);

            solrSelect.Wait();

            return solrSelect.Result; 
        }

        /// <summary>
        /// Solr http Select interface see https://solr.apache.org/guide/8_8/query-syntax-and-parsing.html
        /// it is not 100% documemations how to use it.
        /// </summary>
        /// <param name="coreIndexId">index number which core/collections/database should be use</param>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> SelectAsync( int coreIndexId, solrBuildHttpParms httpQuery, string strData ) {
        
            return await this._asyncSelect( coreIndexId, httpQuery, strData);

        }

        private async Task <string> _asyncSelect( int coreIndexId, solrBuildHttpParms httpQuery, string strData) {                

            string strQuery;

            strQuery = null;
            if ( httpQuery != null ) {
                
                strQuery = httpQuery.GetUrlQuary();
                if (string.IsNullOrEmpty(strQuery)) {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() + "&wt=json";
                } else {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() + "&wt=json&" + strQuery;
                }

            }
            
            return await doPost(coreIndexId, "select", strQuery, strData, false);

        }

        public string Schema( int coreIndexId, solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSchema = this._asyncSchema( coreIndexId, httpQuery, strData);

            solrSchema.Wait();

            return solrSchema.Result; 
        }

        public async Task <string> SchemaAsync( int coreIndexId, solrBuildHttpParms httpQuery, string strData ) {
        
            return await this._asyncSchema( coreIndexId, httpQuery, strData);

        }

        private async Task <string> _asyncSchema( int coreIndexId, solrBuildHttpParms httpQuery, string strData) {                

            string strQuery;

            strQuery = null;
            if ( httpQuery != null ) {
                
                strQuery = httpQuery.GetUrlQuary();
                if (string.IsNullOrEmpty(strQuery)) {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() + "&wt=json";
                } else {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() + "&wt=json&" + strQuery;
                }

            }
            
            return await doPost(coreIndexId, "schema", strQuery, strData, false);

        }










        public string Sql(int coreIndexId, solrBuildHttpParms httpQuery, string strSqlQuery) {

            var solrSQL= asyncSQL( coreIndexId, httpQuery.GetUrlQuary(), strSqlQuery);

            solrSQL.Wait();

            return solrSQL.Result; 

        }

        public string Api(int coreIndexId, string type, string cmd, string data ) {

            var solrApi= this.asyncApi(coreIndexId, type, cmd, data );

            solrApi.Wait();

            return solrApi.Result; 

        }


        private async Task <string> asyncUpdate(  int coreIndexId, string strData,bool overWrite) {

            string parm;           
            List <string>strParms;

            strParms = new List<string>();

            strParms.Add( "?_=" + rnd.Next(1, 99999999).ToString() );

            strParms.Add("wt=json");
            
            if ( commitWithin > 0 ) {
                strParms.Add( "commitWithin="+commitWithin.ToString() );                 
            }

            if (overWrite ) {
                strParms.Add( "overwrite=true" );                 
            }

            parm = "?" + string.Join("&", strParms);
            
            return await doPost(coreIndexId, "update", parm, strData, false);

        }

         
        private async Task <string> asyncApi( int coreIndexId, string type, string cmd, string strData) {                

            string url;
            
            if ( Cfg.solrCore.Length == 0) {
                return null;
            }

            if ( string.IsNullOrEmpty( Cfg.solrCore[coreIndexId].coreName ) || string.IsNullOrWhiteSpace( Cfg.solrCore[coreIndexId].coreName ) ) {
                return null;
            }

            url =  this.solrServerUrlApi + type + "/" + Cfg.solrCore[coreIndexId].coreName + "/" + cmd ;

            return await doPostApi(url, strData);

        }

       

        private async Task <string> asyncSQL( int coreIndexId, string strQuery, string strSqlQuery) {    
       
            if (string.IsNullOrEmpty(strSqlQuery) ) {
                return null;
            }
            
            strSqlQuery = strSqlQuery.Trim();
            if (string.IsNullOrEmpty(strSqlQuery) ) {
                return null;
            }

            strSqlQuery = "stmt=" + strSqlQuery;

            if (string.IsNullOrEmpty(strQuery) ) {
                strQuery = "?" + strQuery;
            }
            
            return await doPost(coreIndexId, "sql", strQuery, strSqlQuery, true);

        }

        private async Task <string> doPost(  int coreIndexId, string cmd, string strQuery, string strData, bool doUrlEncodeData ) {
                        
            string url;
            StringContent data;

            if ( Cfg.solrCore.Length == 0) {
                return null;
            }

            if ( string.IsNullOrEmpty( Cfg.solrCore[coreIndexId].coreName ) || string.IsNullOrWhiteSpace( Cfg.solrCore[coreIndexId].coreName ) ) {
                return null;
            }
            
            url =  Cfg.solrServerUrl + Cfg.solrCore[coreIndexId].coreName + "/"+cmd+strQuery;
            data = null;            
            if (!string.IsNullOrEmpty(strData)) {

                if ( doUrlEncodeData ) {
                    strData = Uri.EscapeDataString(strData);
                }

                data = new StringContent(strData, Encoding.UTF8, "application/json");

                var postResult = await client.PostAsync( url,data); 

                var postContext = await postResult.Content.ReadAsStringAsync();    

                status  = postResult;

                return postContext;
            }

            // if no post data found we using Get url instead
            var getResult = await client.GetAsync(url);

            var getContext = await getResult.Content.ReadAsStringAsync(); 

            status = getResult;
                
            return getContext;
            
        }

        private async Task <string> doPostApi(  string url, string strData ) {
                        
            
            StringContent data;

            if (strData == null) {
                strData = " ";
            }
            data = null;
            if (!string.IsNullOrEmpty(strData)) {
                data = new StringContent(strData, Encoding.UTF8, "application/json");
            }

            var result = await client.PostAsync( url,data); 

            var context = await result.Content.ReadAsStringAsync();
            
            status  = result;
                        
            return context;
        }

        

        private string getBase64BasicAuthUserAndPassword ( string Username, string Password ) {
    
            byte[] credentials;
            string credentialsBase64;

            credentials = Encoding.UTF8.GetBytes( Username + ":" + Password ); 
            credentialsBase64 = Convert.ToBase64String(credentials);

            return credentialsBase64;
        }


    }
}
