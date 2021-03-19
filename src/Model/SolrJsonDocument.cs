using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using SolrHTTP.Docs.Debug;
using SolrHTTP.Docs.Error;
using SolrHTTP.Docs.Response;
using SolrHTTP.Docs.Schema;

namespace SolrHTTP.Docs {
    public class SolrJsonDocument {
        public SolrJsonDocumentResponseHeader responseHeader {set;get;}               
        public SolrJsonDocumentResponse response {set;get;}
        public SolrJsonDocumentSchemaHeader schema {set;get;}
        public SolrJsonDocumentSchemaFields fields {set;get;}
        public SolrJsonDocumentError error {set;get;} 
        public SolrJsonDocumentDebug debug {set;get;}
    }

}