using System.Text.Json.Serialization;

// todo only part are implment of Solr Schema

namespace SolrHTTP.Docs.Schema {

    public class SolrJsonDocumentSchemaHeader {

        public string name  {set;get;}
        public double? version  {set;get;}
        public string uniqueKey  {set;get;}
        public SolrJsonDocumentSchemaFieldTypes[] fieldTypes {set;get;}

    }

    public class SolrJsonDocumentSchemaFieldTypes {

        public string name  {set;get;}

        [JsonPropertyName("class")]
        public string solrclass  {set;get;}
        public string maxCharsForDocValues  {set;get;}
        public string geo {set;get;}
        public bool? omitNorms  {set;get;}
        public bool? omitTermFreqAndPositions  {set;get;}
        public bool? indexed  {set;get;}
        public bool? stored  {set;get;}
        public bool? multiValued  {set;get;}
        public bool? sortMissingLast  {set;get;}
        public bool? docValues {set;get;}
        public SolrJsonDocumentSchemaIndexAnalyzer indexAnalyzer {set;get;}
        public SolrJsonDocumentSchemaQueryAnalyzer queryAnalyzer {set;get;}
    }
    
    public class SolrJsonDocumentSchemaIndexAnalyzer {
        public SolrJsonDocumentSchemaTokenizer tokenizer {set;get;}
    }

    public class SolrJsonDocumentSchemaQueryAnalyzer {
        public SolrJsonDocumentSchemaTokenizer tokenizer {set;get;}
    }

    public class SolrJsonDocumentSchemaTokenizer {

        [JsonPropertyName("class")]
        public string solrclass  {set;get;}
        public string delimiter  {set;get;}
    }


}
