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

        public SolrHTTPClient( Config cfg ) {

            if (cfg.solrServerUrl.Substring(cfg.solrServerUrl.Length - 5, 5)== "solr/") {
                cfg.solrServerUrl = cfg.solrServerUrl.Substring(0,cfg.solrServerUrl.Length - 5);
            }

            if (cfg.solrServerUrl.Substring(cfg.solrServerUrl.Length - 1, 1) == "/") {
                cfg.solrServerUrl = cfg.solrServerUrl.Substring(0,cfg.solrServerUrl.Length - 1);
            }
            cfg.solrServerUrl = cfg.solrServerUrl + "/solr/";

            client.BaseAddress = new Uri( cfg.solrServerUrl );            

            Cfg = cfg;

            if ( cfg.solrUseBaiscAuth ) {
                if (  !String.IsNullOrEmpty( cfg.solrUserName ) && !String.IsNullOrEmpty( cfg.solrPassword ) ) {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + getBase64BasicAuthUserAndPassword( cfg.solrUserName, cfg.solrPassword ) );                    
                }
            }
            
            //clientSQL.DefaultRequestHeaders.Add("Accept", "*/*");

            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("DNT", "1");
            //client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");                        
        }
       


        /// <summary>
        /// Solr Update Select interface see https://solr.apache.org/guide/8_8/query-syntax-and-parsing.html
        /// it is not 100% documemations how to use it.
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public string Update(solrBuildHttpParms httpQuery, string strData, bool overWrite ) {
                     
            var solrUpdate = asyncUpdate(httpQuery, strData, overWrite);

            solrUpdate.Wait();

            return solrUpdate.Result; 
        }

        /// <summary>
        /// Solr http Update interface 
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> asyncUpdate(solrBuildHttpParms httpQuery, string strData, bool overWrite ) {
                
            return await Task.Run( async () => {

                string strQuery;

                httpQuery.Add("overwrite", overWrite.ToString().ToLower() );
                
                strQuery = this._getQuary(httpQuery);
                
                return await doPost("select", strQuery, strData, false, false);
                
            });

        }


        /// <summary>
        /// Solr http Select interface see https://solr.apache.org/guide/8_8/query-syntax-and-parsing.html
        /// it is not 100% documemations how to use it.
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public string Select(solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSelect = this.SelectAsync(httpQuery, strData);

            solrSelect.Wait();

            return solrSelect.Result; 
        }

        /// <summary>
        /// Solr http Select interface see https://solr.apache.org/guide/8_8/query-syntax-and-parsing.html
        /// it is not 100% documemations how to use it.
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> SelectAsync(solrBuildHttpParms httpQuery, string strData ) {
                
            return await Task.Run( async () => {

                string strQuery;

                strQuery = this._getQuary(httpQuery);
                
                return await doPost("select", strQuery, strData, false, false);
                
            });

        }

        /// <summary>
        /// Solr http Schema interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public string Schema(solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSchema = this.SchemaAsync(httpQuery, strData);

            solrSchema.Wait();

            return solrSchema.Result; 
        }

        /// <summary>
        /// Solr http Schema interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> SchemaAsync(solrBuildHttpParms httpQuery, string strData ) {
                 
            return await Task.Run( async () => {

                string strQuery;

                strQuery = strQuery = this._getQuary(httpQuery);
                
                return await doPost("schema", strQuery, strData, false, false);
            });
        }

        /// <summary>
        /// Solr http Schema/field interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public string SchemaFields(solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSchemaField = this.SchemaFieldsAsync(httpQuery, strData);

            solrSchemaField.Wait();

            return solrSchemaField.Result; 
        }

        /// <summary>
        /// Solr http Schema/field interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> SchemaFieldsAsync(solrBuildHttpParms httpQuery, string strData ) {
                  
            return await Task.Run( async () => {

                string strQuery;

                strQuery = this._getQuary(httpQuery);
                                
                return await doPost("schema/fields", strQuery, strData, false, false);
            });

        }

        /// <summary>
        /// Solr http SQL interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public string Sql(solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSQL = this.SqlAsync(httpQuery, strData);

            solrSQL.Wait();

            return solrSQL.Result; 
        }

        /// <summary>
        /// Solr http SQL interface
        /// </summary>
        /// <param name="httpQuery">The http Query parms or null <see cref="solrBuildHttpParms"/> </param>
        /// <param name="strData">post data to solr server</param>
        /// <returns>raw solr response json string</returns>
        public async Task <string> SqlAsync(solrBuildHttpParms httpQuery, string strData ) {
                  
            return await Task.Run( async () => {
                
                string strQuery;

                strQuery = this._getQuary(httpQuery);
              
                if (!string.IsNullOrEmpty(strData)) {
                strData = "stmt=" + Uri.EscapeDataString(strData);               
                }  
                
                return await doPost("sql", strQuery, strData, false, true);
            });
            
        }









       


        

        private string _getQuary( solrBuildHttpParms httpQuery ) {

            string strQuery;

            strQuery = null;
            
            if ( httpQuery != null ) {
                    
                strQuery = httpQuery.GetUrlQuary();
                if (string.IsNullOrEmpty(strQuery)) {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() ;
                } else {
                    strQuery = "?_=" + rnd.Next(1, 99999999).ToString() + "&" + strQuery;
                }
            
            }

            return strQuery;
        }

        private async Task <string> doPost(  
                string cmd, 
                string strQuery, 
                string strData, 
                bool doUrlEncodeData, 
                bool useSQLClient ) {

            return await Task.Run( async () => {         
                string url;
                StringContent data;
                
                if ( string.IsNullOrEmpty( Cfg.coreName ) || string.IsNullOrWhiteSpace( Cfg.coreName ) ) {
                    return null;
                }
                
                url =  Cfg.solrServerUrl + Cfg.coreName + "/"+cmd+strQuery;
                data = null;            
                if (!string.IsNullOrEmpty(strData)) {

                    if ( doUrlEncodeData ) {
                        strData = Uri.EscapeDataString(strData);
                    }

                    if ( useSQLClient ) {
                
                        data = new StringContent(strData, Encoding.UTF8, "application/x-www-form-urlencoded");
                        
                    } else {
                        data = new StringContent(strData, Encoding.UTF8, "application/json"); 
                        status = await client.PutAsync( url,data);                                            
                    }
                    
                    

                    var postContext = await status.Content.ReadAsStringAsync();    

                    

                    return postContext;
                }

                // if no post data found we using Get url instead
                var getResult = await client.GetAsync(url);

                var getContext = await getResult.Content.ReadAsStringAsync(); 

                status = getResult;
                    
                return getContext;
            });
            
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
