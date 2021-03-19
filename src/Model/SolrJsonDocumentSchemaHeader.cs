using System.Text.Json.Serialization;

namespace SolrHTTP.Docs.Schema {

    public class SolrJsonDocumentSchemaHeader {

        public string name  {set;get;}
        public double? version  {set;get;}
        public string uniqueKey  {set;get;}
        public SolrJsonDocumentSchemaFieldTypes[] fieldTypes {set;get;}
        public SolrJsonDocumentSchemaFields[] fields {set;get;}
        public SolrJsonDocumentSchemaDynamicFields[] dynamicFields {set;get;}
        public SolrJsonDocumentSchemaCopyFields[] copyFields {set;get;}
        
    }

    public class SolrJsonDocumentSchemaCopyFields {

        public string source  {set;get;}
        public string dest  {set;get;}
        public long maxChars  {set;get;}

    }

     public class SolrJsonDocumentSchemaDynamicFields {
        public string name  {set;get;} 
        public string type  {set;get;} 

        [JsonPropertyName("default")]
        public string defaultValue  {set;get;} 
        
        public bool? indexed  {set;get;}
        public bool? docValues {set;get;}        
        public bool? multiValued  {set;get;}        
        public bool? omitNorms {set;get;}
        public bool? omitTermFreqAndPositions {set;get;}        
        public bool? omitPositions {set;get;}
        public bool? required {set;get;}
        public bool? stored {set;get;}                
        public bool? sortMissingFirst {set;get;}
        public bool? sortMissingLast {set;get;}                        
        public bool? termOffsets {set;get;} 
        public bool? termPayloads {set;get;}
        public bool? termPositions {set;get;}
        public bool? termVectors {set;get;}                
        public bool? uninvertible {set;get;}
        
        public bool? useDocValuesAsStored {set;get;}
        
    }

    public class SolrJsonDocumentSchemaFields {
        public string name  {set;get;} 
        public string type  {set;get;} 

        [JsonPropertyName("default")]
        public string defaultValue  {set;get;} 
        
        public bool? indexed  {set;get;}
        public bool? docValues {set;get;}        
        public bool? multiValued  {set;get;}        
        public bool? omitNorms {set;get;}
        public bool? omitTermFreqAndPositions {set;get;}        
        public bool? omitPositions {set;get;}
        public bool? required {set;get;}
        public bool? stored {set;get;}                
        public bool? sortMissingFirst {set;get;}
        public bool? sortMissingLast {set;get;}                        
        public bool? termOffsets {set;get;} 
        public bool? termPayloads {set;get;}
        public bool? termPositions {set;get;}
        public bool? termVectors {set;get;}                
        public bool? uninvertible {set;get;}                        
    }

    public class SolrJsonDocumentSchemaFieldTypes {

        public string name  {set;get;}

        [JsonPropertyName("class")]
        public string solrclass  {set;get;}
        public string maxCharsForDocValues  {set;get;}
        public bool? omitNorms  {set;get;}
        public string geo {set;get;}
        
        public bool? omitTermFreqAndPositions  {set;get;}
        public bool? indexed  {set;get;}
        public bool? stored  {set;get;}
        public bool? multiValued  {set;get;}
        public bool? sortMissingLast  {set;get;}
        public bool? docValues {set;get;}
        public string maxDistErr {set;get;}
        public bool? termOffsets {set;get;}
        public string distErrPct {set;get;}
        public string distanceUnits {set;get;}
        public bool? termPositions {set;get;}
        public bool? omitPositions {set;get;}
        public string positionIncrementGap {set;get;}
        public string subFieldSuffix {set;get;}
        public string dimension {set;get;}
        public string autoGeneratePhraseQueries  {set;get;}

      


        public SolrJsonDocumentSchemaAnalyzer analyzer {set;get;}
        public SolrJsonDocumentSchemaIndexAnalyzer indexAnalyzer {set;get;}
        public SolrJsonDocumentSchemaQueryAnalyzer queryAnalyzer {set;get;}

        
        
    }
    
    public class SolrJsonDocumentSchemaAnalyzer {
        public SolrJsonDocumentSchemaTokenizer tokenizer {set;get;}
        public SolrJsonDocumentSchemaFilters[] filters {set;get;}

    }

    public class SolrJsonDocumentSchemaIndexAnalyzer {
        public SolrJsonDocumentSchemaTokenizer tokenizer {set;get;}
        public SolrJsonDocumentSchemaFilters[] filters {set;get;}
    }

    public class SolrJsonDocumentSchemaQueryAnalyzer {
        public SolrJsonDocumentSchemaTokenizer tokenizer {set;get;}
        public SolrJsonDocumentSchemaFilters[] filters {set;get;}
    }

    public class SolrJsonDocumentSchemaTokenizer {

        [JsonPropertyName("class")]
        public string solrclass  {set;get;}
        public string delimiter  {set;get;}
        public string outputUnknownUnigrams  {set;get;}
        public string decompoundMode  {set;get;}
        public string mode {set;get;}

    }

    public class SolrJsonDocumentSchemaFilters {

        [JsonPropertyName("class")]
        public string solrclass  {set;get;}
        public string encoder  {set;get;}
        public string inject {set;get;}
        public string words {set;get;}
        public string ignoreCase {set;get;}
        public string expand {set;get;}
        public string generateNumberParts {set;get;}
        public string synonyms {set;get;}
        public string catenateNumbers {set;get;}
        public string generateWordParts {set;get;}
        public string catenateAll {set;get;}
        public string catenateWords {set;get;}

        [JsonPropertyName("protected")]
        public string solrProtected {set;get;}
        public string language {set;get;}
        public string articles {set;get;}
        public string format {set;get;}
        public string minimumLength {set;get;}
        public string stemDerivational {set;get;}
        public string maxPosQuestion {set;get;}
        public string maxFractionAsterisk {set;get;}
        public string maxPosAsterisk {set;get;}
        public string withOriginal {set;get;}
        
    }



}
