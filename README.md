# NoSql_DocumentDB_Admin

---

A WebApi demonstrating how to manage DocumentDB Databases, Collections, Documents & Users/Permissions. Requires Fiddler or POSTMAN to test.

---

Built using VS2015 Community

---

#####CRUD Features
|Feature|Database|Collection|Document|Users|
|-------|--------|----------|--------|-----|
|Create| CreateDatabaseAsync | CreateDocumentCollectionAsync | CreateDocumentCollectionUri | CreateUserAsync |
|Update| | | ReplaceDocumentAsync | | 
|Delete| DeleteDatabaseAsync | DeleteDocumentCollectionAsync | DeleteDocumentAsync | DeleteUserAsync |
|Get / Read One | ReadDatabaseAsync | ReadDocumentCollectionAsync |ReadDocumentAsync | ReadUserAsync | 
|Get / Read All | CreateDatabaseQuery | ReadDocumentCollectionFeedAsync | ReadDocumentFeedAsync | |
|Set Permission | | CreatePermissionAsync | | CreatePermissionAsync |
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

####Document Http Requests
|Verb|Url|Data|Description|
|----|---|----|-----------|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}/docs| Document |Creates a new document|
|PUT|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}/docs| Document |Replaces a document|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}/docs/{docid}| |Deletes a document|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}/docs/{docid}| |Gets a single document|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}/docs| |Gets a list of documents for a specified collection|

---

####User Http Requests
|Verb|Url|Data|Description|
|----|---|----|-----------|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/users/{userid}| |Creates a new user|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/users/{userid}| |Deletes a user|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/users/{userid}| |Gets a single user|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/users/{userid}/colls/{colid}/{permissionMode}| |Sets the permission mode on a collection for a user|
---

####Misc Code Features
|Feature|
|-------|
|DocumentClientException|
|AggregateException|
|UriFactory|
|IndexingPolicy|
|RequestOptions|
|FeedOptions|
|async / await / Task|
|Repository Pattern|
|Extension Methods|
|Model Factory|
|TimeSpan|
|Function 'RetryAfter' |
|Func delegate|

---

####Resources
|Title|Author|Publisher|
|-----|------|---------|
|[DocumentDB documentation](https://azure.microsoft.com/en-us/documentation/services/documentdb/)| | Microsoft Azure |
|[DocumentDB Tutorial](http://www.tutorialspoint.com/documentdb/index.htm)| | Tutorials Point |
|[azure-documentdb-dotnet](https://github.com/Azure/azure-documentdb-dotnet)| | Github |
|[Azure: New DocumentDB NoSQL Service](http://weblogs.asp.net/scottgu/azure-new-documentdb-nosql-service-new-search-service-new-sql-alwayson-vm-template-and-more)| Scott Guthrie | ScottGu's Blog |

