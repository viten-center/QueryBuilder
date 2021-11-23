using System;
using System.Collections.Generic;
using System.Text;
using Viten.QueryBuilder.Data.AnyDb;
using Viten.QueryBuilder.SqlOm;
using Xunit;

namespace Viten.QueryBuilder.Test
{
  public class QbTest
  {
    internal static void TestAll()
    {
      QbTest t = new QbTest();
      t.TestCase();
      t.TestNotIn();
      t.TestPaging();
      t.TestLong();
      t.TestDelete();
      t.TestInsert();
      t.TestUpdate();

      t.TestJoin();
      t.TestJoinCond();
      t.TestCond();
      t.TestFrom();
      t.TestExpr();
      t.TestLogic();
      t.TestWhere();
    }

    void TestCase()
    {
      From c = From.Table("country", "c", "nsi");
      Select sel = Qb.Select(
        Column.New("id"),
        Column.New(Expr.Case(
          Expr.String("UNKNOWN"),
          WhenThen.New(Cond.Equal("code", c, "AU"), Expr.String("кингуру")),
          WhenThen.New(Cond.Equal("code", c, "PN"), Expr.String("папуас"))
          ), "caseCol")
        )
        .From(c);
      Renderer.PostgreSqlRenderer pg = new Renderer.PostgreSqlRenderer();
      string sql = pg.RenderSelect(sel);
      Renderer.MySqlRenderer my = new Renderer.MySqlRenderer();
      sql = my.RenderSelect(sel);
      Assert.Equal("select `id`,  case  when (`c`.`code` = 'AU') then 'кингуру' when (`c`.`code` = 'PN') then 'папуас' else 'UNKNOWN' end  `caseCol` from nsi.`country` `c`", sql);
    }
    void TestNotIn()
    {
      Select sel = Qb.Select("*")
        .From("AE", "ae")
        .Where(Cond.NotIn(Expr.Field("f"), 1, 2, 3, 4));
      Renderer.PostgreSqlRenderer pg = new Renderer.PostgreSqlRenderer();
      string sql = pg.RenderSelect(sel);
    }
    void TestPaging()
    {
      Select sel = Qb.Select("*")
        .From("AE", "ae")
        .OrderBy("AE_ID")
        .Page(2, 10);

      Renderer.MySqlRenderer my = new Renderer.MySqlRenderer();
      string sql = my.RenderSelect(sel);


      From u = From.Table("unit", "u", "nsi");
      sel = Qb.Select("*")
        .From(u)
        .OrderBy("id")
        .Page(2, 10);
      Renderer.PostgreSqlRenderer pg = new Renderer.PostgreSqlRenderer();
      sql = pg.RenderSelect(sel);

      Renderer.SqLiteRenderer lite = new Renderer.SqLiteRenderer();
      sql = lite.RenderSelect(sel);

      Renderer.SqlServerRenderer ms = new Renderer.SqlServerRenderer();
      sql = ms.RenderSelect(sel);

      Renderer.OracleRenderer ora = new Renderer.OracleRenderer();
      sql = ora.RenderSelect(sel);
    }

    void TestLong()
    {
      Select sel = Qb.Select("*")
        .From("tab")
        .Where(Cond.Equal("col", DateTime.Now.Ticks));
      AnyDbFactory factory = new AnyDbFactory(new AnyDbSetting());
      string sql = factory.GetSql(sel);
      Update upd = Qb.Update("tab")
        .Values(
          Value.New("col", DateTime.Now.Ticks)
        );
      sql = factory.GetSql(upd);
    }
    [Fact]
    public void TestJoin()
    {
      Select sel = Qb.Select("a").From("lt").Join("rt", JoinCond.Fields("id_0"));

      From lt = From.Table("lt");
      From rt = From.Table("rt");
      Logic l = Logic.And(Cond.Equal(Expr.Field("id_0", lt), Expr.Field("id_0", rt)));

      sel = Qb.Select("a").From("lt").Join(JoinType.Left, lt, rt, l);
    }

    [Fact]
    public void TestJoinCond()
    {
      JoinCond j = JoinCond.Fields("a");
    }

    [Fact]
    public void TestCond()
    {
      Cond c = Cond.Equal(Expr.Field("a"), Expr.Param("p"));
      c = Cond.Like(Expr.Field("a"), Expr.Param("p"), '!');
      c = Cond.Like("a", "%");
      c = Cond.In(Expr.Field("a"), Qb.Select("a"));
      c = Cond.IsNull(Expr.Field("a"));
      c = Cond.Between(Expr.Field("a"), Expr.Param("p1"), Expr.Param("p2"));
      c = Cond.Exists(Qb.Select("a"));
    }
    [Fact]
    public void TestFrom()
    {
      From f = From.Table("a", "b", "dbo");
      f = From.SubQuery(Qb.Select("a"), "t");
      Union u = Qb.Union();
      f = From.Union(u, "a");
    }
    [Fact]
    public void TestExpr()
    {
      Expr e = Expr.Field("a");
      e = Expr.Field("a", From.Table("tab"));
      e = Expr.Function(AggFunc.Max, Expr.Field("a"));
      e = Expr.IfNull(Expr.Field("a"), Expr.Param("p"));
      e = Expr.SubQuery(Qb.Select("a"));
    }
    [Fact]
    public void TestLogic()
    {
      Logic l0 = Logic.And(Cond.Equal("a", 1), Cond.Greater("b", 1));
      Logic l1 = Logic.And(Cond.Equal("a", 1), Cond.Greater("b", 1));
      Logic l2 = Logic.And(l0, l1);
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

      ins = Qb.Insert("Customers", "rem")
      .Values(
        Value.New("FirstName", "Pavel"),
        Value.New("LastName", "Pavel")
      );
      sql = renderer.RenderInsert(ins);

    }

    [Fact]
    public void TestUpdate()
    {
      Update upd = Qb.Update("Customers")
        .Values(
          Value.New("LastName", "Pavlov")
        )
        .Where(Cond.Equal("FirstName", "Pavel"));
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderUpdate(upd);

      upd = Qb.Update("Customers", "rem")
      .Values(
        Value.New("LastName", "Pavlov")
      )
      .Where(Cond.Equal("FirstName", "Pavel"));
      sql = renderer.RenderUpdate(upd);

    }

    [Fact]
    public void TestDelete()
    {
      Delete del = Qb.Delete("Customers")
        .Where(Cond.Equal("Id", 20));
      Renderer.SqlServerRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderDelete(del);
      del = Qb.Delete("Customers", "nsi")
        .Where(Cond.Equal("Id", 20));
      sql = renderer.RenderDelete(del);
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
      Renderer.ISqlOmRenderer renderer = new Renderer.SqlServerRenderer();
      string sql = renderer.RenderSelect(sel);
      Assert.Equal("select * from [tab] where (([a] = 1 and [b] > 2))", sql);

      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Cond.NotIn(Expr.Field("a"), 1, 2),
          Cond.NotIn("a", 1, 2),
          Cond.NotIn("b", "bb", "bbb"),
          Cond.NotIn("a", DateTime.Now, DateTime.UtcNow)
          );
      sql = renderer.RenderSelect(sel);

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
      Assert.Equal("select * from [tab] where (([a] = 1 and [b] > 2))", sql);

      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Logic.Or(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
          )
        );
      sql = renderer.RenderSelect(sel);
      Assert.Equal("select * from [tab] where (([a] = 1 or [b] > 2))", sql);

      From customer = From.Table("Customers", "c");
      From orders = From.Table("Orders", "o");
      Select inner = Qb.Select(
        Column.New("FirstName", customer),
        Column.New("LastName", customer),
        Column.New("Count", "sum", orders, AggFunc.Sum)
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
        .From(From.SubQuery(inner, "t"))
        .Where(Cond.NotLike(Expr.Field("FirstName"), Expr.String("aa$")));

      sql = renderer.RenderSelect(sel);
      Renderer.PostgreSqlRenderer pg = new Renderer.PostgreSqlRenderer();
      sql = pg.RenderSelect(sel);

      List<string> grants = new List<string>() { "1.$", "2.$" };
      List<string> bans = new List<string>() { "3.$", "4.$" };

      string ss = GetSqlDemans(grants, bans);


      sel = Qb.Select("*").From("tab").Where(Cond.Like("FirstName", "%abc%"));
      sql = renderer.RenderSelect(sel);
      renderer = new Renderer.PostgreSqlRenderer();
      sql = renderer.RenderSelect(sel);
    }

    public static string GetSqlDemans(List<string> grants, List<string> bans)
    {
      From d = From.Table("demand", "d", "dem");
      List<Cond> condGrant = new List<Cond>();
      List<Cond> condBan = new List<Cond>();
      foreach (string grant in grants)
      {
        condGrant.Add(Cond.Like(Expr.Field(nameof(Demand.OkpCode), d), Expr.String(grant)));
      }
      foreach (string ban in bans)
      {
        condBan.Add(Cond.NotLike(Expr.Field(nameof(Demand.OkpCode), d), Expr.String(ban)));
      }

      Logic logicGrant = Logic.Or(condGrant.ToArray());
      Logic logicBan = Logic.Or(condBan.ToArray());

      Select sel = Qb.Select("*")
        .From(d);
      if (condGrant.Count == 0 && condBan.Count == 0)
        return GetSql(sel);
      if (condGrant.Count != 0 && condBan.Count != 0)
      {
        sel.Where(Logic.And(logicGrant, logicBan));
        return GetSql(sel);
      }
      if (condBan.Count == 0)
      {
        sel.Where(Logic.And(logicGrant));
        return GetSql(sel);
      }
      sel.Where(Logic.And(logicBan));
      return GetSql(sel);
    }

    static string GetSql(Select sel)
    {
      Viten.QueryBuilder.Renderer.PostgreSqlRenderer render = new Viten.QueryBuilder.Renderer.PostgreSqlRenderer();
      return render.RenderSelect(sel);
    }

  }
  class Demand
  {
    public string OkpCode { get; set; }
  }

}
