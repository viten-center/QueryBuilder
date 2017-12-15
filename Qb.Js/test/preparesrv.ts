/// <reference path="../node_modules/@types/node/index.d.ts" />

import { SelectQuery, OrderByDir, Cond, Qb, Select, From, AggFunc, Expr, Param, DbType, JoinType, JoinCond } from "../src/index"
import fs = require("fs");
import { Logic } from "../src/Logic";
import { Column } from "../src/Column";
import { UnionMod } from "../src/Enums";

let getStr = function (sel: Select): string {
  return JSON.stringify(Qb.GetQueryObject(sel));
}

let d = new Date(2010, 0, 1, 12, 13, 14, 555);
let fromNode = From.Table("Node", "n");
let fromType = From.Table("ComponentType", "t");
let fromHeat = From.Table("HeatChannelEx", "h");

let q_0 = Qb.Select("a", "b")
  .From("t")
  .Where(Cond.Equal("id", 0));

let q_1 = Qb.Select("c", "d")
  .From("tt")
  .Where(Cond.Equal("id", 0));

let inner = Qb.Select(
  Column.New("FirstName", fromNode),
  Column.New("LastName", fromNode),
  Column.New("Count", "sum", fromType, AggFunc.Sum)
)
  .From(fromNode)
  .Join(JoinType.Left, fromNode, fromType, JoinCond.Fields("Id", "CustomerId"))
  .GroupBy("FirstName", fromNode)
  .GroupBy("LastName", fromType);
let t = From.SubQuery(inner, "t");

let queryes: Select[] =
  [
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
        Cond.Like(Expr.Field("DisplayName", fromNode), Expr.String("dn"), "#"),
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
          Cond.Like(Expr.Field("DisplayName"), Expr.Param("dn"), "#"),
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
      Column.New(Expr.IfNull(Expr.Field("sum", t), 0), "total")
    )
      .From(From.SubQuery(inner, "t"))

  ];





let _path: string = "test_cmd.txt";
let lines: string = "";
for (let i = 0; i < queryes.length; i++) {
  lines += getStr(queryes[i]) + "\n";
}
let f = fs.openSync(_path, "w");
let buff = new Buffer(lines);
fs.writeSync(f, buff);


