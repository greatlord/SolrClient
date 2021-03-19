using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolrHTTP.Docs.Response {
    public class SolrJsonDocumentResponseHeader {
        public bool? zkConnected {set;get;}
        public long? status {set;get;}
        public long? QTime  {set;get;}

        [JsonPropertyName("params")]
        public JsonDocument httpparms {set;get;}
    }

    public class SolrJsonDocumentResponse {
        public long? numFound {set;get;}
        public long? start  {set;get;}
        public double? maxScore  {set;get;}
        public bool? numFoundExact  {set;get;}
        public JsonDocument docs {set;get;}
    }

}