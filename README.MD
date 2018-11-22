# Language Integrated Query (LINQ)
Language-Integrated Query (LINQ) is the name for a set of technologies based on the integration of query capabilities directly into the C# language. Traditionally, queries against data are expressed as simple strings without type checking at compile time or IntelliSense support. Furthermore, you have to learn a different query language for each type of data source: SQL databases, XML documents, various Web services, and so on. With LINQ, a query is a first-class language construct, just like classes, methods, events.

For a developer who writes queries, the most visible "language-integrated" part of LINQ is the query expression. Query expressions are written in a declarative query syntax. By using query syntax, you can perform filtering, ordering, and grouping operations on data sources with a minimum of code. You use the same basic query expression patterns to query and transform data in SQL databases, ADO .NET Datasets, XML documents and streams, and .NET collections.

* Query expressions can be used to query and to transform data from any LINQ-enabled data source. For example, a single query can retrieve data from a SQL database, and produce an XML stream as output.
* Query expressions are easy to master because they use many familiar C# language constructs.
* The variables in a query expression are all strongly typed, although in many cases you do not have to provide the type explicitly because the compiler can infer it.
* A query is not executed until you iterate over the query variable, for example, in a foreach statement.
* At compile time, query expressions are converted to Standard Query Operator method calls according to the rules set forth in the C# specification. Any query that can be expressed by using query syntax can also be expressed by using method syntax. However, in most cases query syntax is more readable and concise. 
*As a rule when you write LINQ queries, we recommend that you use query syntax whenever possible and method syntax whenever necessary. There is no semantic or performance difference between the two different forms. Query expressions are often more readable than equivalent expressions written in method syntax.
* Some query operations, such as Count or Max, have no equivalent query expression clause and must therefore be expressed as a method call. Method syntax can be combined with query syntax in various ways.
* Query expressions can be compiled to expression trees or to delegates, depending on the type that the query is applied to. IEnumerable<T> queries are compiled to delegates. IQueryable and IQueryable<T> queries are compiled to expression trees. 

## Three Parts of a Query Operation
All LINQ query operations consist of three distinct actions:
1. Obtain the data source.
2. Create the query.
3. Execute the query.

### The Query
The query in the previous example returns all the even numbers from the integer array. The query expression contains three clauses: from, where and select. (If you are familiar with SQL, you will have noticed that the ordering of the clauses is reversed from the order in SQL.) The `<from>` clause specifies the data source, the `<where>` clause applies the filter, and the `<select>` clause specifies the type of the returned elements.
The important point is that in LINQ, the query variable itself takes no action and returns no data. It just stores the information that is required to produce the results when the query is executed at some later point.

#### Forcing Immediate Execution
Queries that perform aggregation functions over a range of source elements must first iterate over those elements. Examples of such queries are `<Count>`, `<Max>`, `<Average>`, and `<First>`. These execute without an explicit `<foreach>` statement because the query itself must use foreach in order to return a result. Note also that these types of queries return a single value, not an `<IEnumerable>` collection.
To force immediate execution of any query and cache its results, you can call the ToList or ToArray methods.

#### Filtering
The filter causes the query to return only those elements for which the expression is true. The result is produced by using the `<where>` clause. The filter in effect specifies which elements to exclude from the source sequence.
You can use the familiar C# logical `<AND>` and `<OR>` operators to apply as many filter expressions as necessary in the `<where>` clause.

#### Ordering
The `<orderby>` clause will cause the elements in the returned sequence to be sorted according to the default comparer for the type being sorted. 

#### Grouping
The `<group>` clause enables you to group your results based on a key that you specify.
When you end a query with a `<group>` clause, your results take the form of a list of lists. Each element in the list is an object that has a `<Key>` member and a list of elements that are grouped under that key. When you iterate over a query that produces a sequence of groups, you must use a nested `<foreach>` loop. The outer loop iterates over each group, and the inner loop iterates over each group's members.

If you must refer to the results of a group operation, you can use the into keyword to create an identifier that can be queried further. 

#### Joining
Join operations create associations between sequences that are not explicitly modeled in the data sources. 
In LINQ you do not have to use `<join>` as often as you do in SQL because foreign keys in LINQ are represented in the object model as properties that hold a collection of items.

#### Selecting (Projections)
The `<select>` clause produces the results of the query and specifies the "shape" or type of each returned element.
When the `<select>` clause produces something other than a copy of the source element, the operation is called a projection. The use of projections to transform data is a powerful capability of LINQ query expressions.