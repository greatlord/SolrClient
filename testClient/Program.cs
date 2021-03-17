using System;
using solrClientTest;

namespace testClient
{
    
    class Program
    {
        static void Main(string[] args)
        {
            TestSolr testSolr = new TestSolr();

            Console.WriteLine("start: TestSolrConntecions");
            if (!testSolr.TestSolrConntecions()) {
                Console.WriteLine("TestSolrConntecions: fail");
            }
            Console.WriteLine("done: TestSolrConntecions");
            
            Console.WriteLine("start: TestSolrGetDocs");
            if (!testSolr.TestSolrGetDocs()) {
                Console.WriteLine("TestSolrGetDocs: fail");
            }
            Console.WriteLine("done: TestSolrGetDocs");

            
        }
    }
}
