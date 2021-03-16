using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Solr.SolrConfig;
using Solr.SolrHelpers;


namespace Solr
{

    public class SolrHTTPClient
    {
     
        private readonly HttpClient client = new HttpClient();
        private Random rnd = new Random();

        public int commitWithin = 1000;

        private Config Cfg;

        public SolrHTTPClient( Config cfg ) {

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

        public string Select( int coreIndexId, solrBuildHttpParms httpQuery, string strData ) {
        
            var solrSelect = asyncSelect( coreIndexId, httpQuery, strData);

            solrSelect.Wait();

            return solrSelect.Result; 
        }

        public string Sql(int coreIndexId, solrBuildHttpParms httpQuery, string strSqlQuery) {

            var solrSQL= asyncSQL( coreIndexId, httpQuery.GetUrlQuary(), strSqlQuery);

            solrSQL.Wait();

            return solrSQL.Result; 

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

         
        private async Task <string> asyncSelect( int coreIndexId, solrBuildHttpParms httpQuery, string strData) {                

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
                        
            StringContent data;

            if ( Cfg.solrCore.Length == 0) {
                return null;
            }

            if ( string.IsNullOrEmpty( Cfg.solrCore[coreIndexId].coreName ) || string.IsNullOrWhiteSpace( Cfg.solrCore[coreIndexId].coreName ) ) {
                return null;
            }

            data = null;
            if (!string.IsNullOrEmpty(strData)) {

                if ( doUrlEncodeData ) {
                    strData = Uri.EscapeDataString(strData);
                }

                data = new StringContent(strData, Encoding.UTF8, "application/json");
            }

            var result = await client.PostAsync( Cfg.solrCore[coreIndexId].coreName + "/"+cmd+strQuery,data); 
            return await result.Content.ReadAsStringAsync();
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
