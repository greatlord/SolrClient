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
I maybe will add ado support for sql support.
if some are willing help me write it so it is okay todo so

## Transform data from solr to object 
The solrHttpClient will return all data as string as it revcived it.
No helper exists yet to transform it to you own data struct.
in futuer it will be a helper with that maybe the client will accpect you model and transform it to a object

