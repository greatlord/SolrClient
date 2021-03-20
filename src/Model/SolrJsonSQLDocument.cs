
using System.Text.Json.Serialization;
using SolrHTTP.Docs.Error;


namespace SolrHTTP.Docs {
    public class SolrJsonSQLDocument {

        [JsonPropertyName("result-set")]
        public SolrJsonSQLDocumentResultSet resultSet {get;set;}
        public SolrJsonDocumentError error {get;set;}
        public string rawData;
    }

    public class SolrJsonSQLDocumentResultSet {
        public SolrJsonSQLDocumentDocs[] docs  {get;set;}
    }

    public class SolrJsonSQLDocumentDocs {
        
        [JsonPropertyName("RESPONSE_TIME")]
        public long RESPONSETIME {get;set;}
        public string EXCEPTION {get;set;}
        public bool? EOF {get;set;}
        
    }
}
