using System.Linq;
using System.Collections.Generic;
using System;
using Viten.QueryBuilder.Data.AnyDb;
using Dapper;
using System.Threading.Tasks;
using Xunit;

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
      using (AnyDbConnection con = _factory.OpenConnection())
      { 
        con.Execute(@"
CREATE TABLE public.customer (
	id serial NOT NULL,
	first_name varchar(50) NULL,
	last_name varchar(50) NULL,
	CONSTRAINT customer_pk PRIMARY KEY (id)
);");
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
        System.Data.IDataReader dataReader = con.ExecuteReader(sel);
      }
    }
    public void GetPageSqlTest()
    {
      Select sel = Qb.Select("*")
        .From("customer").OrderBy("id");
      string sql = _factory.GetSql(sel, 2, 10, int.MaxValue);
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
        Assert.Single(GetAll(con));

        ins = Qb.Insert("customer")
          .Values(
            Value.New("first_name", "321"),
            Value.New("last_name", "654")
          );
        con.ExecuteAsync(ins).Wait();
        Assert.Equal(2, GetAll(con).Count());


        Select sel_0 = Qb.Select("*").From("customer");
        int total = con.QueryTotalCountAsync(sel_0).Result;
        Assert.Equal(2, total);
        total = con.QueryTotalCount(sel_0);
        Assert.Equal(2, total);
        sel_0 = sel_0.OrderBy("first_name");
        MoreDataFlag f = new MoreDataFlag();
        var page = con.QueryPageAsync(sel_0, 0, 3, total, f).Result;
        Assert.Equal(2, page.Count());
        Assert.False(f.HasMoreData);
        page = con.QueryPage<Customer>(sel_0, 0, 3, total, f);
        Assert.Equal(2, page.Count());
        Assert.False(f.HasMoreData);

        Update upd = Qb.Update("customer")
          .Values(
            Value.New("first_name", "XXX")
          );
        int res = con.ExecuteAsync(upd).Result;
        Assert.Equal(2, res);

        Select sel = Qb.Select("*")
          .From("customer");
        IEnumerable<Customer> en = con.QueryAsync<Customer>(sel).Result;
        Assert.Equal(2, en.Count());

        Delete del = Qb.Delete("customer");
        res = con.ExecuteAsync(del).Result;
        Assert.Equal(2, res);
      }
    }
      
  }
}
