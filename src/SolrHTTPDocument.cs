
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolrHTTP {
    public class SolrJsonDocument {

        public solrResponseHeader responseHeader {set;get;}
        public SolrJsonDocumenterror error {set;get;}        
        public solrResponse response {set;get;}
        public solrDebug debug {set;get;}
    }

    public class SolrJsonDocumentSchema {
        public solrResponseHeader responseHeader {set;get;}

        public SolrJsonDocumenterror error {set;get;}
        public SolrJsonDocumentSchemaHeader schema {set;get;}

    }

    public class SolrJsonDocumenterror {
        public string msg {set;get;}
        public string trace {set;get;}
        public int code {set;get;}
        
    }


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



    public class solrResponseHeader {
        public bool? zkConnected {set;get;}
        public long? status {set;get;}
        public long? QTime  {set;get;}

        [JsonPropertyName("params")]
        public JsonDocument httpparms {set;get;}
    }

    public class solrResponse {
        public long? numFound {set;get;}
        public long? start  {set;get;}
        public double? maxScore  {set;get;}
        public bool? numFoundExact  {set;get;}
        public JsonDocument? docs {set;get;}
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
