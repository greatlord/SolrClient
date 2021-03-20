using System.Text.Json.Serialization;

namespace SolrHTTP.Docs.Error {
    public class SolrJsonDocumentError {
        public string msg {set;get;}
        public string trace {set;get;}
        public int code {set;get;}
        public SolrJsonDocumentErrorMetaData metadata {set;get;}
        
    }

    public class SolrJsonDocumentErrorMetaData {

        [JsonPropertyName("error-class")]
        public string errorClass {set;get;}

        [JsonPropertyName("result-set")]
        public string rootErrorClass {set;get;}

        [JsonPropertyName("result-set")]
        public long code {set;get;}
    }


}