
using System;
using System.Collections.Generic;
using SolrHTTP.Docs;

namespace SolrHTTP {

    public class solrBuildHttpParms {

        private Dictionary<string,string> parms = new Dictionary<string, string>(); 
        public void Add(string key, string value) {

            string urlKey;
            
            urlKey = Uri.EscapeDataString(key);
            
            if ( parms.ContainsKey(urlKey) ) {
                parms[urlKey] = Uri.EscapeDataString(value);
            } else {
                parms.Add(urlKey, Uri.EscapeDataString(value));
            }

        }

        public string GetUrlQuary() {

            List<string> strParms;

            if (parms.Count == 0 ) {
                return null;
            }

            strParms = new List<string>();
            foreach( var row in parms ) {
                strParms.Add ( row.Key + "=" + row.Value );
            }

            if ( strParms.Count == 0) {
                return null;
            }

            return string.Join("&",strParms);
        }

        public void Clear() {
            parms.Clear();
        }
    }

}