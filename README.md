# SolrHttpClient
A simple helper library for apache solr writen in .net 5 

This solr Client library commucate with apache solr with http/https interface
It support 
* collection/Update
* collection/Select
* collection/Sql (only if apache solr are in cloud mode, req zookeeper been config with apache solr)

#Do I need have other deepens ?
No you do not need have Zookeeper or Solr.Net library with this.
it does not support fail over to next solr server in zookeeper list. 
but it support distribution search with sql interface 

SQL interface support can read more here what solr support
https://solr.apache.org/guide/8_1/parallel-sql-interface.html

The SolrHttpClient are writen by Magnus Olsen 2020 (magnus@greatlord.com my new email address is olsen.sweden@gmail.com)

®Copyright By Magnus Olsen 2020 - 2021 (magnus@greatlord.com my new email address is olsen.sweden@gmail.com)
Licen is LGPL 2.1

## what diffent is it from other solr client for dotnet ?
* Support http basic authoring
* Support http interface command update/select/sql
* Support select core
* small overhead.

## If I want use it in close source or somthing that lgpl does not allown me todo ?
Contact me so we can agreed on the licen fee and terms for it.

## Ado.net support ?
Some basic support exists for zookeeper with solr.
Solr SQL interface are only activated if you got zookeeper setup
ado.net for solr http/https sql interface will check with normal post
if zkConnected == false it false it will throw NotSuppreted 

### SolrHTTPConnection
#### string and vars
* Database (done)
* DataSource (done)
* ConnectionString (done)
* State (done)

#### method
* SolrHTTPConnection constructor (done)
* State get ConnectionState (done)
* Open() method (done)
* Close() method (done)
* CreateCommand() (done)

#### events
* StateChange (done)

#### Will not be supported
* BeginDbTransaction (Not supported by solr)
* BeginTransactionAsync (Not supported by solr)
* EnlistTransaction (Not supported by solr)

#### ToDo
* ChangeDatabase
* ChangeDatabaseAsync
* Clone          
* CloseAsync
* ConnectionTimeout
* Container
* CreateCommand
* Dispose
* DisposeAsync
* Disposed
* GetSchema
* GetSchemaAsync
* GetType          
* OpenAsync
* ServerVersion
* Site


## Transform data from solr to object 
The solrHttpClient will return all data as string as it revcived it.
No helper exists yet to transform it to you own data struct.
in futuer it will be a helper with that maybe the client will accpect you model and transform it to a object

##example to use it 
using Solr;
using Solr.SolrConfig;

function myconnection() {
  Solr.SolrConfig.Config cfg = new Solr.SolrConfig.Config cfg();
  cfg.solrCore = new Solr.SolrConfig.CoreConfig[1];
  cfg.solrCore[0].coreName = "my core / collection / share name";
  cfg.solrServerUrl = "http://localhost:8983";
  cfg.solrUserName = "http basic authorizing name";
  cfg.solrPassword = "http basic authorizing password clear text";
  cfg.solrUseBaiscAuth = true;
  
  SolrHTTPClient solr = SolrHTTPClient(cfg);
  
  string docs = solr.Select(0,"q=*%3A*",null);
 }
as you can see you must self encode the parms correct solr does not under stand + as space (%20) the parm need be encode correct
to encode the q=*:* cars" correct you need use str = "q="+Uri.EscapeDataString("*:* cars"); now you got corret url encoding 

 
   
  
  
}


