# Azure_DocumentDB_WebApiApp

---

Built using VS2015 Community

---

#####CRUD Features
|Feature|Database|Collection|Document|
|-------|--------|----------|--------|
|Create| CreateDatabaseAsync | CreateDocumentCollectionAsync | |
|Update| | | |
|Delete| DeleteDatabaseAsync | DeleteDocumentCollectionAsync | |
|Get / Read One | ReadDatabaseAsync | ReadDocumentCollectionAsync | |
|Get / Read All | CreateDatabaseQuery | ReadDocumentCollectionFeedAsync | |

---

####Database Http Requests
|Verb|Url|Data|Description|
|----|---|----|-----------|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Creates a new database|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Deletes a database|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Gets a single database|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db| |Gets a list of all databases|

---

####Collection Http Requests
|Verb|Url|Data|Description|
|----|---|----|-----------|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}| |Creates a new collection|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}| |Deletes a collection|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}| |Gets a single collection|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls| |Gets a list of collections for a specified database|

---

####Misc Code Features
|Feature|
|-------|
|DocumentClientException|
|DocumentClient| 
|UriFactory|
|IndexingPolicy|
|RequestOptions|

---
####Resources
|Title|Author|Publisher|
|-----|------|---------|
|[DocumentDB documentation](https://azure.microsoft.com/en-us/documentation/services/documentdb/)| | Microsoft Azure |
|[DocumentDB Tutorial](http://www.tutorialspoint.com/documentdb/index.htm)| | Tutorials Point |
|[azure-documentdb-dotnet](https://github.com/Azure/azure-documentdb-dotnet)| | Github |
|[Azure: New DocumentDB NoSQL Service](http://weblogs.asp.net/scottgu/azure-new-documentdb-nosql-service-new-search-service-new-sql-alwayson-vm-template-and-more)| Scott Guthrie | ScottGu's Blog |

