using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Viten.QueryBuilder.SqlOm;
using Xunit;

namespace Viten.QueryBuilder.Test
{
  public class SerializationTest
  {
    internal static void TestAll()
    {
      SerializationTest t = new SerializationTest();
      t.Test();
    }
    //время при сериализации на клиенте в json переводится в UTC
    //указываем сразу в UTC
    static DateTime d = new DateTime(2010, 1, 1, 09, 13, 14, 555, DateTimeKind.Utc);

    static From fromNode = From.Table("Node", "n");
    static From fromType = From.Table("ComponentType", "t");
    static From fromHeat = From.Table("HeatChannelEx", "h");

    static Select q_0 = Qb.Select("a", "b")
      .From("t")
      .Where(Cond.Equal("id", 0));

    static Select q_1 = Qb.Select("c", "d")
      .From("tt")
      .Where(Cond.Equal("id", 0));

    static Select inner = Qb.Select(
      Column.New("FirstName", fromNode),
      Column.New("LastName", fromNode),
      Column.New("Count", "sum", fromType, AggFunc.Sum)
      )
      .From(fromNode)
      .Join(JoinType.Left, fromNode, fromType, JoinCond.Fields("Id", "CustomerId"))
      .GroupBy("FirstName", fromNode)
      .GroupBy("LastName", fromType);
    static From t = From.SubQuery(inner, "t");


    Select[] queryes = new Select[]
    {
    Qb.Select("a", "b")
      .From("t")
      .Where(Cond.Equal("id", 0))
      .OrderBy("a", OrderByDir.Desc),
    /////////////////////////
    Qb.Select("a")
      .From("t")
      .Where(Cond.Equal("id", 0), Cond.Equal("id", 0)),
    /////////////////////////
    Qb.Select("a")
      .From("t")
      .Where(
      Logic.And(Cond.Equal("id", 0), Cond.Equal("id", 0))
      ),
    /////////////////////////
    Qb.Select("a")
      .From("t")
      .Where(
      Logic.Or(Cond.Equal("id", 0), Cond.Equal("id", 0))
      ),
    /////////////////////////
    Qb.Select(Column.New("a"))
      .From("t")
      .Where(
      Logic.Or(Cond.Equal("id", 0), Cond.Equal("id", 0))
      ),
    /////////////////////////
    Qb.Select(Column.New("Id", "cnt", fromNode, AggFunc.Count))
      .From(fromNode),
    /////////////////////////
    Qb.Select("Id")
      .From("Node")
      .Where(
      Cond.Equal("IdOwn", 1),
      Cond.Equal("Del", Expr.Param("p0")
      ))
      .OrderBy("Ord")
      .Params(Param.New("p0", DbType.Boolean, false)),
    /////////////////////////
    Qb.Select(
      Column.New("Id", fromNode),
      Column.New("IdComponentType", fromNode),
      Column.New("DisplayName", fromNode)
    )
      .From(fromNode)
      .Join(JoinType.Inner, fromNode, fromType, Logic.And(
        Cond.Equal(Expr.Field("CanLog", fromType), Expr.Param("p0")),
        Cond.Equal(Expr.Field("Id", fromType), Expr.Field("IdComponentType", fromNode)),
        Cond.Like(Expr.Field("DisplayName", fromNode), Expr.String("dn"), '#'),
        Cond.Equal(Expr.Field("Del", fromNode), Expr.Param("p1"))))
      .OrderBy("IdComponentType")
      .OrderBy("DisplayName")
      .Params(
      Param.New("p0", DbType.Int32, 33),
      Param.New("p1", DbType.Double, 22.22)),
    /////////////////////////      
    Qb.Select(Column.New("Ord", "min", AggFunc.Min))
      .From("Node")
      .Where(Logic.And(
        Cond.Equal("IdOwn", 1),
        Cond.Equal("Del", Expr.Param("p0"))))
      .Params(Param.New("p0", DbType.DateTime, d)),
    /////////////////////////      
    Qb.Select(
      Column.New("Body", fromNode),
      Column.New("EncodingType", fromNode))
      .From(fromNode)
      .Join(JoinType.Inner, fromNode, fromType,
      Logic.And(
        Cond.Equal(Expr.Field("Id", fromNode), Expr.Field("SsLink", fromType)),
        Cond.Equal("Id", fromType, Expr.Param("Id")))),
    /////////////////////////      
    Qb.Select(Column.New("Id", fromNode))
      .From(fromNode)
      .Join(JoinType.Inner, fromNode, fromType, Logic.And(Cond.Equal(Expr.Field("IdLink", fromNode), Expr.Field("Id", fromType))))
      .Join(JoinType.Inner, fromType, fromHeat,
      Logic.And(
        Logic.And(
          Cond.Equal(Expr.Field("Id", fromType), Expr.Field("Id", fromHeat)),
          Cond.Equal(Expr.Field("Id", fromType), Expr.Field("Id", fromHeat))
        ),
        Logic.Or(
          Cond.Equal("IdOwn", fromNode, 1),
          Cond.Equal("IdOwn", fromNode, 1))
      ))
      .Where(
      Logic.And(
        Cond.Equal("IdOwn", fromNode, 1),
        Cond.Equal("Del", fromNode, Expr.Param("Del"))
      )),
    /////////////////////////      
    Qb.Select("Id")
      .From("Node")
      .Where(Logic.And(
        Logic.And(
          Cond.Like(Expr.Field("DisplayName"), Expr.Param("dn"), '#'),
          Cond.Equal("Del", Expr.Param("Del"))),
        Logic.Or(
          Cond.Equal("IdOwn", Expr.Param("IdOwnE")),
          Cond.Equal("IdOwn", Expr.Param("IdOwnH"))
        )
      )),
    /////////////////////////      
    Qb.Select("*")
      .From(
      Qb.Union()
        .Add(q_0)
        .Add(q_1), "u"
      ),
    /////////////////////////      
    Qb.Select(
      Column.New("FirstName", t),
      Column.New("LastName", t),
      Column.New(Expr.IfNull(Expr.Field("sum", t), 0), "total"))
      .From(From.SubQuery(inner, "t"))

    };

    List<string> lines = new List<string>();
    public SerializationTest()
    {
      System.IO.StringReader sr = new System.IO.StringReader(Resource.test_cmd);
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        lines.Add(line);
      }
    }
    [Fact]
    public void Test()
    {
      string str = System.IO.File.ReadAllText("select.json");
      SelectQuery selectQuery = JsonConvert.DeserializeObject<SelectQuery>(str);

      for (int i = 0; i < queryes.Length; i++)
      {
        string line = lines[i];

        SelectQuery net_sel = Qb.GetQueryObject(queryes[i]);
        string net_json = JsonConvert.SerializeObject(net_sel);

        SelectQuery from_js_sel = JsonConvert.DeserializeObject<SelectQuery>(line);
        string from_js_json = JsonConvert.SerializeObject(from_js_sel);

        Assert.Equal(net_json, from_js_json);
      }
    }
  }
}
