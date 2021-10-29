using System.Linq;
using System.Collections.Generic;
using System;
using Viten.QueryBuilder.Data.AnyDb;
using Dapper;
using System.Threading.Tasks;
using Xunit;
using System.IO;

namespace Viten.QueryBuilder.Test
{
  class DapperTest
  {
    class Annonce : IAnyDbAnnouncer
    {
      public bool Enabled => true;

      public void Announce(string message)
      {
        Console.WriteLine(message);
      }
    }
    AnyDbFactory _factory;
    private DapperTest()
    {
      _factory = new AnyDbFactory(new PgDbSetting(), new Annonce());
      //_factory = new AnyDbFactory(new SqliteDbSetting(), new Annonce());
    }
    internal static void TestAll()
    {
      DapperTest t = new DapperTest();
      t.InitDb();
      t.SelectTest();
      t.GetPageSqlTest();
      t.ExecuteAsyncTest();
    }

    void InitDb()
    {
      if (_factory.Provider == DatabaseProvider.PostgreSql)
      {
        PgDbSetting ps = new PgDbSetting();

        Npgsql.NpgsqlConnectionStringBuilder sb = new Npgsql.NpgsqlConnectionStringBuilder(ps.ConnectionString);
        sb.Database = "postgres";
        ps.ConnectionString = sb.ToString();

        AnyDbFactory f = new AnyDbFactory(ps);
        using (AnyDbConnection con = f.OpenConnection())
        {
          con.Execute("drop database if exists qb_test");
          con.Execute("create database qb_test");
        }
      }
      if (_factory.Provider == DatabaseProvider.SqLite)
      {
        Microsoft.Data.Sqlite.SqliteConnectionStringBuilder sb = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder(_factory.ConnectionString);
        if(File.Exists(sb.DataSource))
        {
          File.Delete(sb.DataSource);
        }
      }
        using (AnyDbConnection con = _factory.OpenConnection())
      { 
        con.Execute(@"
CREATE TABLE customer (
	id serial NOT NULL,
	first_name varchar(50) NULL,
	last_name varchar(50) NULL,
	CONSTRAINT customer_pk PRIMARY KEY (id)
);");
        for(int i = 0; i < 100; i++)
        {
          con.Execute($"insert into customer (first_name, last_name) values ('F_{i}', 'L_{i}')");
        }

      }
    }


    IEnumerable<Customer> GetAll(AnyDbConnection con)
    {
      Select sel = Qb.Select("*").From("customer");
      return con.Query<Customer>(sel);
    }

    public void SelectTest()
    {
      Select sel = Qb.Select("*")
        .From("customer").OrderBy("id");
      using(AnyDbConnection con = _factory.OpenConnection())
      {
        var res = con.Query(sel);
        Assert.Equal(100, res.Count());
      }
    }
    public void GetPageSqlTest()
    {
      Select sel = Qb.Select("*")
        .From("customer").OrderBy("id").Page(1, 10);
      string sql = _factory.GetSql(sel);
      using (AnyDbConnection con = _factory.OpenConnection())
      {
        var res = con.Query(sel);
        Assert.Equal(10, res.Count());
      }
    }
    public void ExecuteAsyncTest()
    {
      using (AnyDbConnection con = _factory.OpenConnection())
      {
        Insert ins = Qb.Insert("customer")
          .Values(
            Value.New("first_name", "123"),
            Value.New("last_name", "456")
          );
        con.ExecuteAsync(ins).Wait();
        Assert.Equal(101, GetAll(con).Count());

        ins = Qb.Insert("customer")
          .Values(
            Value.New("first_name", "321"),
            Value.New("last_name", "654")
          );
        con.ExecuteAsync(ins).Wait();
        Assert.Equal(102, GetAll(con).Count());


        Select sel_0 = Qb.Select("*").From("customer").Page(0, 200);
        sel_0 = sel_0.OrderBy("first_name");
        var page = con.QueryAsync(sel_0).Result;
        Assert.Equal(102, page.Count());

        Update upd = Qb.Update("customer")
          .Values(
            Value.New("first_name", "XXX")
          );
        int res = con.ExecuteAsync(upd).Result;
        Assert.Equal(102, res);

        Select sel = Qb.Select("*")
          .From("customer");
        IEnumerable<Customer> en = con.QueryAsync<Customer>(sel).Result;
        Assert.Equal(102, en.Count());

        Delete del = Qb.Delete("customer");
        res = con.ExecuteAsync(del).Result;
        Assert.Equal(102, res);
      }
    }
      
  }
}
