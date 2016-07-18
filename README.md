# Azure_DocumentDB_WebApiApp

---

Built using VS2015 Community

---

#####FEATURES
|Feature|Database|Collection|Document|
|-------|--------|----------|--------|
|Insert| Yes | Yes | |
|Update| | | |
|Delete| Yes | Yes | |
|Get / Read One | Yes | Yes | |
|Get / Read All | Yes | Yes | |

---

####URLS
|Verb|Url|Data|Description|
|----|---|----|-----------|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Creates a new database|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Deletes a database|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}| |Gets a single database|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db| |Gets a list of all databases|
|GET|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls| |Gets a list of collections for a specified database|
|POST|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls| collection id (colid) |Creates a new collection|
|DELETE|http://localhost:[YOUR_PORT_NUMBER]/api/db/{dbid}/colls/{colid}| |Deletes a collection|
