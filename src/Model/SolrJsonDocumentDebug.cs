using System.Text.Json;
using System.Text.Json.Serialization;

// todo remove all JsonDocument

namespace SolrHTTP.Docs.Debug {
    public class SolrJsonDocumentDebug {

        public SolrJsonDocumentDebugTrack track {set;get;}
        public string rawquerystring {set;get;}
        public string querystring {set;get;}
        public string parsedquery {set;get;}
        public string parsedquery_toString {set;get;}
        public string QParser {set;get;}   
        public JsonDocument explain  {set;get;}   
    }

    public class SolrJsonDocumentDebugTrack {

        public string rid {set;get;}

        [JsonPropertyName("EXECUTE_QUERY")]
        public JsonDocument EexcuteQuery {set;get;}
       
        public JsonDocument timing  {set;get;}
       
        public JsonDocument process  {set;get;}

    }

}