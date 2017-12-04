# QueryBuilder

**QueryBuilder** allows you to create queries against a database, describing them as an object, without using tools such as LinqToSql, ORM, etc. and generate their SQL code in the syntax of the DBMS used

## Supported DBMS
* MS SQL
* MS SQL CE
* SQLite
* Oracle
* MySQL
* PostgreSQL

## Examples of working with query objects

### SELECT

Query
```csharp
      Select sel = Qb.Select("*")
        .From("MyTable")
        .Where(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
        );
```
generation (MS SQL)
```SQL
select * 
from [MyTable] 
where (([a] = 1 and [b] > 2))
```

Query
```csharp
      Select sel = Qb.Select("*")
      .From("tab")
      .Where(Logic.Or(
        Cond.Equal("a", 1),
        Cond.Equal("a", 2)
        ));
```

generation (MS SQL)
```SQL
select * 
from [tab] 
where (([a] = 1 or [a] = 2))
```

Query
```csharp
      From customer = From.Table("Customers", "c");
      From orders = From.Table("Orders", "o");
      Select sel = Qb.Select(
        Column.New("FirstName", customer),
        Column.New("LastName", customer),
        Column.New("Count", orders, "sum", AggFunc.Sum)
        )
        .From(customer)
        .Join(JoinType.Left, customer, orders, JoinCond.Fields("Id", "CustomerId"))
        .GroupBy("FirstName", customer)
        .GroupBy("LastName", customer);
```
generation (MS SQL)
```SQL
select 
    [c].[FirstName], 
    [c].[LastName], 
    Sum([o].[Count]) [sum] 
from [Customers] [c] 
left join [Orders] [o] on (
    [c].[Id] = [o].[CustomerId]
    ) 
group by 
    [c].[FirstName], 
    [c].[LastName]
```

Query
```csharp
      From customer = From.Table("Customers", "c");
      From orders = From.Table("Orders", "o");
      Select inner = Qb.Select(
        Column.New("FirstName", customer),
        Column.New("LastName", customer),
        Column.New("Count", orders, "sum", AggFunc.Sum)
        )
        .From(customer)
        .Join(JoinType.Left, customer, orders, JoinCond.Fields("Id", "CustomerId"))
        .GroupBy("FirstName", customer)
        .GroupBy("LastName", customer);

      From t = From.SubQuery(inner, "t");
      sel = Qb.Select(
          Column.New("FirstName", t),
          Column.New("LastName", t),
          Column.New(Expr.IfNull(Expr.Field("sum", t), 0), "total")
        )
        .From(From.SubQuery(inner, "t"));
```

generation (MS SQL)
```SQL
select 
    [t].[FirstName], 
    [t].[LastName], 
    isnull([t].[sum], 0) [total] 
from 
( 
	select 
        [c].[FirstName], 
        [c].[LastName], 
        Sum([o].[Count]) [sum] 
	from [Customers] [c] 
	left join [Orders] [o] on (
        [c].[Id] = [o].[CustomerId]
        ) 
	group by 
        [c].[FirstName], 
        [c].[LastName] 
) [t]
```

generation (PostgreSQL)
```SQL
select 
    "t"."FirstName", 
    "t"."LastName", 
    coalesce("t"."sum", 0) "total" 
from 
( 
	select 
        "c"."FirstName", 
        "c"."LastName", 
        Sum("o"."Count") "sum" 
	from "Customers" "c" 
	left join "Orders" "o" on (
        "c"."Id" = "o"."CustomerId"
        ) 
	group by 
        "c"."FirstName", 
        "c"."LastName" 
) "t"
```
### INSERT

Query
```csharp
      Insert ins = Qb.Insert("Customers")
        .Values(
          Value.New("FirstName", "Pavel"),
          Value.New("LastName", "Pavel")
        );
```

generation (MS SQL)
```SQL
insert into [Customers] 
    ([FirstName], [LastName]) 
values 
    ('Pavel', 'Pavel')
```

### UPDATE

Query
```csharp
      Update upd = Qb.Update("Customers")
        .Values(
          Value.New("LastName", "Pavlov")
        )
        .Where(Cond.Equal("FirstName", "Pavel"));
```

generation (MS SQL)
```SQL
update [Customers] 
    set [LastName] = 'Pavlov' 
where 
    (([FirstName] = 'Pavel'))
```    

### DELETE

Query
```csharp
      Delete del = Qb.Delete("Customers")
        .Where(Cond.Equal("Id", 20));
```

generation (MS SQL)
```SQL
delete from [Customers]  
where 
    (([Id] = 20))
```

## An example of using QueryBuilder in conjunction with AnyDbFactory and mini-ORM Dapper

```csharp
  public class Customer
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  class AnyDbSetting : Data.AnyDb.IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SqLite;
    public string ConnectionString { get; set; } = "Data Source=test.sqlite";
    public int CommandTimeout { get; set; } = 30;
  }

  class Program
  {
    public static void Main()
    {
        Dapper.AnyDbConnectionInitialiser.Initialise();
        AnyDbFactory factory = new AnyDbFactory(new AnyDbSetting());
        Select sel = Qb.Select("*")
          .From("Customers");
        IEnumerable<Customer> customers;
        using (AnyDbConnection con = factory.OpenConnection())
        {
          customers = con.Query<Customer>(sel);
        }
    }
  }
```
