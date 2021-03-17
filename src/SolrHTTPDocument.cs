
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SolrHTTP {
    public class SolrJsonDocument {

        public ResponseHeader responseHeader {set;get;}
        public Response response {set;get;}
    }

    public class ResponseHeader {
        public bool zkConnected {set;get;}
        public long status {set;get;}
        public long QTime  {set;get;}

        // params not suppored yet
    }

    public class Response {
        public long numFound {set;get;}
        public long start  {set;get;}
        public double maxScore  {set;get;}
        public bool numFoundExact  {set;get;}
        public JsonDocument docs {set;get;}
    }

   
}
