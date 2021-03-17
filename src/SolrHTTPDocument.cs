
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolrHTTP {
    public class SolrJsonDocument {

        public solrResponseHeader responseHeader {set;get;}
        public solrResponse response {set;get;}
        public solrDebug debug {set;get;}
    }

    public class solrResponseHeader {
        public bool zkConnected {set;get;}
        public long status {set;get;}
        public long QTime  {set;get;}

        [JsonPropertyName("params")]
        public JsonDocument httpparms {set;get;}
    }

    public class solrResponse {
        public long numFound {set;get;}
        public long start  {set;get;}
        public double maxScore  {set;get;}
        public bool numFoundExact  {set;get;}
        public JsonDocument docs {set;get;}
    }

    public class solrDebug {

        public solrDebugTrack track {set;get;}
        public string rawquerystring {set;get;}
        public string querystring {set;get;}
        public string parsedquery {set;get;}
        public string parsedquery_toString {set;get;}
        public string QParser {set;get;}     
        public JsonDocument explain  {set;get;}   
    }

    public class solrDebugTrack {
        public string rid {set;get;}

        [JsonPropertyName("EXECUTE_QUERY")]
        public JsonDocument EexcuteQuery {set;get;}
        public JsonDocument timing  {set;get;}
        public JsonDocument process  {set;get;}

        
        
    }

    
    

   
}
