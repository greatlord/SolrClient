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
            
        }
    }
}
