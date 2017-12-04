using System;
using System.Collections.Generic;
using System.Text;
using Viten.QueryBuilder.SqlOm;
using Xunit;

namespace Viten.QueryBuilder.Test
{
  public class QbTest
  {
    public static void TestAll()
    {
      QbTest t = new QbTest();
      t.TestWhere();
      t.TestInsert();
      t.TestUpdate();
      t.TestDelete();
    }
    [Fact]
    public void TestInsert()
    {
      Insert ins = Qb.Insert("Customers")
        .Values(
          Value.New("FirstName", "Pavel"),
          Value.New("LastName", "Pavel")
        );
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderInsert(ins);
    }

    public void TestUpdate()
    {
      Update upd = Qb.Update("Customers")
        .Values(
          Value.New("LastName", "Pavlov")
        )
        .Where(Cond.Equal("FirstName", "Pavel"));
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderUpdate(upd);
    }

    public void TestDelete()
    {
      Delete del = Qb.Delete("Customers")
        .Where(Cond.Equal("Id", 20));
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderDelete(del);
    }

    [Fact]
    public void TestWhere()
    {
      Select sel = Qb.Select("*")
        .From("tab")
        .Where(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
        );
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 and [b] > 2))");

      sel = Qb.Select("*")
      .From("tab")
      .Where(Logic.Or(
        Cond.Equal("a", 1),
        Cond.Equal("a", 2)
        ));
      sql = renderer.RenderSelect(sel);


      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Logic.And(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
          )
        );

      sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 and [b] > 2))");

      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Logic.Or(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
          )
        );
      sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 or [b] > 2))");

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

      sql = renderer.RenderSelect(sel);
      Renderer.PostgreSqlRenderer pg = new Renderer.PostgreSqlRenderer();
      sql = pg.RenderSelect(sel);
    }
  }
}
