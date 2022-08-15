# Dockerized asp.Net 6 REST API with mongoDB

### To Build the REST API using Docker
```bash
docker build --rm -t masum/dotnet_web_api:latest .
```

### To Run the REST API using Docker
```bash
docker image ls | findstr dotnet_web_api
docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 masum/dotnet_web_api
```

### To Stop the REST API using Docker
```bash
docker ps
docker container stop <first 3 digit of container id>
```

#### Data Modeling
data modeling is a way to organize fields in a document to support your application performance and querying capabilities

#### mongo database `import`, `export`, `restore`, `dump`

```sql
mongodump --uri "mongodb+srv://<your username>:<your password>@<your cluster>.mongodb.net/<your_database>"

mongoexport --uri="mongodb+srv://<your username>:<your password>@<your cluster>.mongodb.net/<your_database>" --collection=<collection> --out=<file_name>.json

mongorestore --uri "mongodb+srv://<your username>:<your password>@<your cluster>.mongodb.net/<your_database>"  --drop dump

mongoimport --uri="mongodb+srv://<your username>:<your password>@<your cluster>.mongodb.net/<your_database>" --drop <file_name>.json
```

#### Mongo Shell Commands (Basic)

```sql
show dbs

use <database_name>

show collections
```

#### Cursor methods `sort()` , `linit()` , `pretty()` , `count()` 

- find 10 cities where population is in ascending order & city name is in descending order

```sql
db.collection_name.find().sort({ "population": 1, "city": -1 }).limit(10).count()
```

#### Find Operations

```sql
db.<collection>.find(<query_in_key_value_pair>)

db.<collection>.find(<query_in_key_value_pair>).count()

db.<collection>.find(<query_in_key_value_pair>).pretty()

db.<collection>.find().pretty()

db.<collection>.findOne()

db.<collection>.find({ "test": 1 }).pretty()
```

#### Insert Operations

```sql
db.<collection>.insert([ <comma_separated_second_bracket_document_key_value_pair> ])

db.<collection>.insert([ <comma_separated_second_bracket_document_key_value_pair> ],{ "ordered": false })
```

#### Update Operations

```sql
db.collection.updateOne(<filter> , <update>)

db.collection.replaceOne(<filter> , <update>)

db.collection.updateMany(<filter> , <update>)
```

- with `UpdateOne()`, if there are multiple documents that match a given criteria, only one of them will be updated, whichever one this operation finds first<br>
- Whereas using `updateMany()` will update all documents that match a given query

```sql
db.inventory.updateMany(
   { "qty": { $lt: 50 } },
   {
     $set: { "size.uom": "in", status: "P" },
     $currentDate: { lastModified: true }
   }
)
```

- uses the `$set` operator to update the value of the size.uom field to "in" and the value of the status field to "P",
- uses the `$currentDate` operator to update the value of the lastModified field to the current date. If lastModified field does not exist, $currentDate will create the field. See $currentDate for details.
- here `$lt` operator is a comparison operator in MongoDB that is used to match values that are less than a specified value

```sql
db.grades.updateOne(
    { "student_id": 250, "class_id": 339 },
    { "$push": { "scores": { "type": "extra credit","score": 100 }}})
```

- Just like with the set operator, if the field that you specify doesn't exist in the document, then `$push` will add an array field to the document with a specified value.

#### Upsert - Update or Insert ?
- by default, upsert is false when `OpdateOne()` command 

```sql
db.iot.updateOne({ "sensor": r.sensor, "date": r.date,
                   "valcount": { "$lt": 48 } },
                         { "$push": { "readings": { "v": r.value, "t": r.time } },
                        "$inc": { "valcount": 1, "total": r.value } },
                 { "upsert": true })

```

#### Delete Operations

```sql
db.<collection>.deleteOne({ "_id": 3 })

db.<collection>.deleteMany({ "_id": 1 })

db.<collection>.drop()
```

- Find all documents where the trip started and ended at the same station:

```sql
db.trips.find({ "$expr": { "$eq": [ "$end station id", "$start station id"] }
              }).count()
```

#### Array Operators
- `$all` return a cursor with all documents in which the specified array field contains all the given elements, regardless of their order in the array.
- `$size` return all documents where the specified array field is exactly the given length
```sql
{<array_field> : {<$size> : <number>} }

{<array_field> : {<$all> : <array>} }

{<array_field> : { "$eleMatch" : {<element_of_array_field> : <value>} }}
```

#### Update Operators 
- `$currentDate` Sets the value of a field to current date, either as a Date or a Timestamp.
- `$inc` Increments the value of the field by the specified amount.
- `$min` Only updates the field if the specified value is less than the existing field value.
- `$max` Only updates the field if the specified value is greater than the existing field value.
- `$mul` Multiplies the value of the field by the specified amount.
- `$rename` Renames a field.
- `$set` Sets the value of a field in a document.
- `$setOnInsert` Sets the value of a field if an update results in an insert of a document. Has no effect on update operations that modify existing documents.
- `$unset` Removes the specified field from a document.


#### Projection Operators

```sql
db.<collection>.find({<query>},{<projection>})

1 - include the field
0 - exclude the field

db.<collection>.find({<query>},{<field_1>:0 , <field_2>:1})
```
- see details of <a href="[http://](https://www.mongodb.com/docs/manual/reference/operator/query/)" target="_blank">Query and Projection Operators in mongoDB</a>


#### To query an element in sub-documents
- use dot-notation to go as deep into the nested document as you wish.
```sql
db.collection_name.find({ "address.city": "NEW YORK" }).count()

db.companies.find({ "relationships":
                      { "$elemMatch": { title": { "$regex": "CEO" },
                                       "person.first_name": "Mark" } }
                  },{ "name": 1 }).count()
```

#### Aggregation pipeline 
- Aggregation does not inherently modify or change your original data
- Aggregation framework syntax is in the form of a pipeline, where stages are executed in the order in which they are listed
```sql
db.<collection>.aggregate({$subtract:[db.<collection>.find({"<field_name>":<field_value>}).count(),db.<collection>.find({"<field_name>":{$gt:<field_value>}}).count()]})

db.listingsAndReviews.aggregate([ { "$project": { "address": 1, "_id": 0 }},
                                  { "$group": { "_id": "$address.country" }}])
```

#### Indexing 
```sql
db.collection_name.createIndex({ "<field_name>" : -1 }) // descending order
db.collection_name.createIndex({ "<field_name>" :  1 }) // ascending order
```

#### To use MQL operator in query

```sql
{<field> : {<operator> : <value>} }
```

#### To use Aggregation operator in query

```sql
{<operator>: { <field> : <value>} }
```



### Tools used to build this project:
- *MongoDB Compass*
- *Docker Desktop*
- *Studio 3T*
- *Jetbrains Rider*
- *Visual Studio*
- *Putty*
- *WinSCP*
- *RedisInsight*
- *PostMan*

### Feedback

If you have any feedback, kindly reach out to me at abdullahmasum6035@gmail.com

<a href="https://github.com/MasumBhai"><img alt="Abdullah Al Masum's Github Stats" src="https://github-readme-stats.vercel.app/api?username=masumBhai&show_icons=true&count_private=true&theme=great-gatsby" width=400></a>
<a href="https://github.com/MasumBhai"><img alt="Abdullah Al Masum's Github Streak" src="https://github-readme-streak-stats.herokuapp.com?user=MasumBhai&theme=vision-friendly-dark&fire=DD2727&sideNums=CD5CDD" width=400></a>

