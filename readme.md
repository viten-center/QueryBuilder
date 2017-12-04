# QueryBuilder

**QueryBuilder** позволяет создавать запросы к базе данных, описывая их в виде объекта, без использования таких средств как LinqToSql, ORM и т.д. и генерировать их SQL код в синтаксисе используемой СУБД 

## Поддерживаемые СУБД
* MS SQL
* MS SQL CE
* SQLite
* Oracle
* MySQL
* PostgreSQL

## Примеры работы с объектами запросов

### SELECT

Запрос
```csharp
      Select sel = Qb.Select("*")
        .From("MyTable")
        .Where(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
        );
```
генерация MS SQL
```SQL
"select * 
from [MyTable] 
where (([a] = 1 and [b] > 2))"
```

Запрос
```csharp
      Select sel = Qb.Select("*")
      .From("tab")
      .Where(Logic.Or(
        Cond.Equal("a", 1),
        Cond.Equal("a", 2)
        ));
```

генерация MS SQL
```SQL
select * 
from [tab] 
where (([a] = 1 or [a] = 2))
```

Запрос
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
генерация MS SQL
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

Запрос
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
      Selectsel = Qb.Select(
          Column.New("FirstName", t),
          Column.New("LastName", t),
          Column.New(Expr.IfNull(Expr.Field("sum", t), Expr.Num(0)), "total")
        )
        .From(From.SubQuery(inner, "t"));
```

генерация MS SQL
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

генерация PostgreSQL
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

Запрос
```csharp
      Insert ins = Qb.Insert("Customers")
        .Values(
          Value.New("FirstName", "Pavel"),
          Value.New("LastName", "Pavel")
        );
```

генерация MS SQL
```SQL
insert into [Customers] 
    ([FirstName], [LastName]) 
values 
    ('Pavel', 'Pavel')
```

### UPDATE

Запрос
```csharp
      Update upd = Qb.Update("Customers")
        .Values(
          Value.New("LastName", "Pavlov")
        )
        .Where(Cond.Equal("FirstName", "Pavel"));
```

генерация MS SQL
```SQL
update [Customers] 
    set [LastName] = 'Pavlov' 
where 
    (([FirstName] = 'Pavel'))
```    

### DELETE

Запрос
```csharp
      Delete del = Qb.Delete("Customers")
        .Where(Cond.Equal("Id", 20));
```

генерация MS SQL
```SQL
delete from [Customers]  
where 
    (([Id] = 20))
```

## Пример использования совместно с AnyDbFactory и мини-ORM Dapper

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
