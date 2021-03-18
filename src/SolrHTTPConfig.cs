
using System;

namespace SolrHTTP
{
    public class Config {
        
        public  string solrServerUrl { get; set;} = string.Empty;        
        public  string solrUserName { get; set;} = string.Empty;
        public  string solrPassword { get; set;} = string.Empty;
        public  bool solrUseBaiscAuth { get; set;} = false;
        public CoreConfig[] solrCore { get; set;}
    }

    public class CoreConfig {
        public  string coreName { get; set;} = string.Empty;       
    }
    

}