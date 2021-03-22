# SolrHttpClient
A simple helper library for apache solr writen in .net 5 

This solr Client library commucate with apache solr with http/https interface
It support 
* collection/update
* collection/select
* collection/schema
* collection/schema/fields
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

## SolrHTTPClient
### method
* Select() 
  - calling on solr url http://localhost:8393/solr/corename/select

* SelectAsync()
  - calling on solr url http://localhost:8393/solr/corename/select

* Schema()
  - calling on solr url http://localhost:8393/solr/corename/schema

* SchemaAsync()
  - calling on solr url http://localhost:8393/solr/corename/schema
 
* SchemaFields()
  - calling on solr url http://localhost:8393/solr/corename/schema/fields

* SchemaFieldsAsync()
  - calling on solr url http://localhost:8393/solr/corename/schema/fields

* Sql()
  - calling on solr url http://localhost:8393/solr/corename/sql

* SqlAsny()
  - calling on solr url http://localhost:8393/solr/corename/sql

## Ado.net support ?
Ado are not included in the release. 
Left todo is the datareader before it can be release

Some basic support exists for zookeeper with solr.
Solr SQL interface are only activated if you got zookeeper setup
ado.net for solr http/https sql interface will check with normal post
if zkConnected == false it false it will throw NotSuppreted. 

### SolrHTTPConnection
Ado.net.data Connections works same as SqlConnection
if behovir is diffent it contain a bug or not supported then
Please fill in a bug report then

#### string and vars
* Database (done)
* DataSource (done)
* ConnectionString (done)
* State (done)

#### method
* SolrHTTPConnection constructor (done)
* State (done)
* Open() method (done)
* OpenAsync (done)
* Close() method (done)
* CloseAsync method (done)
* CreateCommand() (done)


#### events
* StateChange (done)

#### Will not be supported
* BeginDbTransaction (Not supported by solr)
* BeginTransactionAsync (Not supported by solr)
* EnlistTransaction (Not supported by solr)

#### Ado.net Does not yet support follow solr datatype for datatable
 - _nest_path_
 - ancestor_path
 - binary
 - delimited_payloads_float
 - delimited_payloads_int
 - delimited_payloads_string
 - descendent_path
 - ignored
 - location
 - location_rpt
 - lowercase
 - pdate
 - pdates
 - phonetic_en
 - point
 - random
 - rank
 - text_en_splitting
 - text_en_splitting_tight
 - text_gen_sort
 - text_general_rev

#### ToDo
* ChangeDatabase
* ChangeDatabaseAsync
* Clone          
* ConnectionTimeout
* Container
* Dispose
* DisposeAsync
* Disposed
* GetSchema()
* GetSchema("corename")
* GetSchema("collectionName", restrictionValues)
* GetSchemaAsync("corename")
* GetSchemaAsync("corename", restrictionValues)
* GetType          
* ServerVersion
* Site

### SolrHTTPCommand
Ado.net.data Commands works same as SqlCommand
if behovir is diffent it contain a bug or not supported then
Please fill in a bug report then

#### string and vars
* CommandText
* CommandType
  - Only support CommandType.Text Apache solr does not have any CommandType.StoredProcedure

#### method
* ExecuteNonQuery()
* ExecuteNonQueryAsync()

#### events
#### Will not be supported
* DbTransaction

#### ToDO
* FetchSize
* CommandTimeout
* UpdatedRowSource
* DbConnection
* DbParameterCollection
* DesignTimeVisible
* Prepare()
* ExecuteScalar()
* ExecuteScalarAsync()
* CreateDbParameter()
* ExecuteDbDataReader()
* ExecuteDbDataReaderAsync()
* Cancel()
* Dispose()

## Transform data from Apache solr to object 
The solrHttpClient will return all data as string as it revcived it.
No helper exists yet to transform it to you own data struct.
in futuer it will be a helper with that maybe the client will accpect you model and transform it to a object

##example to use it 
using Solr;
using Solr.SolrConfig;

function myconnection() {
  Solr.SolrConfig.Config cfg = new Solr.SolrConfig.Config cfg();
  cfg.solrCore.coreName = "my core / collection / share name";
  cfg.solrServerUrl = "http://localhost:8983";
  cfg.solrUserName = "http basic authorizing name";
  cfg.solrPassword = "http basic authorizing password clear text";
  cfg.solrUseBaiscAuth = true;
  
  SolrHTTPClient solr = SolrHTTPClient(cfg);
  solrBuildHttpParms parms = new solrBuildHttpParms();
  parms.Add("q","*:*");
  parms.Add("start","0");
  parms.Add("rows","10");
  
  string docs = solr.Select(0,parms,null);
 }
as you can see you must self encode the parms correct Apache solr does not under stand + as space (%20) the parm need be encode correct
to encode the q=*:* cars" correct you need use str = "q="+Uri.EscapeDataString("*:* cars"); now you got corret url encoding 

 
   
  
  
}


