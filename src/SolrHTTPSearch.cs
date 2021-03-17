namespace SolrHTTP
{

    public class SolrHTTPSearch
    {

    }

    public class SolrHTTPSearchParms
    {
        public string q {get;set;}
        public string fq {get;set;}
        public string sort {get;set;}
        public long start {get;set;}
        public long rows {get;set;}
        public string fl {get;set;}
        public string df {get;set;}
        public string RawQueryParameters {get;set;}
        public bool indent {get;set;} = false;
        public bool DebugQuery {get;set;} = false;
        public bool DebugExplainStructured {get;set;} = false;
    }

}