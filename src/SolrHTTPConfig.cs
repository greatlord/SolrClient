
using System;
using SolrHTTP.Docs;

namespace SolrHTTP
{
    public class Config {
        
        public  string solrServerUrl { get; set;} = string.Empty;        
        public  string solrUserName { get; set;} = string.Empty;
        public  string solrPassword { get; set;} = string.Empty;
        public  bool solrUseBaiscAuth { get; set;} = false;
         public  string coreName { get; set;} = string.Empty;   
    }

  

}