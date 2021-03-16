
using System;

namespace Solr.SolrConfig
{
    public class Config {
        
        public  string solrServerUrl { get; set;}
        public  string solrUserName { get; set;}
        public  string solrPassword { get; set;}
        public  bool solrUseBaiscAuth { get; set;}        
        public CoreConfig[] solrCore { get; set;}
    }

    public class CoreConfig {
        public  string coreName { get; set;}        
    }
    

}