import { expect } from 'chai';
import 'mocha';

import { Qb, Expr, From, Constant } from "../src/qb";
import { OmExpression } from '../src/OmExpression';
import { OmExpressionType, ExprValCode, OmDataType, AggFunc, OmAggregationFunction } from '../src/Enums';
import { OmConstant } from '../src/OmConstant';
import { SelectQuery } from '../src/SelectQuery';

describe("Expr", ()=>{
  let d: Date = new Date(2018, 0, 1, 0, 0, 0, 0);

  it("Raw(sql: string)", ()=>{
    let _: OmExpression = Expr.Raw("SQL")["Expression"];
    expect(_.StringValue).eq("SQL");
    expect(_.Type).eq(OmExpressionType.Raw);
    expect(_.ValueCode).eq(ExprValCode.String);
  })

  it("Field(name: string)", ()=>{
    let _: OmExpression = Expr.Field("a")["Expression"];
    expect(_.Type).eq(OmExpressionType.Field);
    expect(_.Value).eq("a");
  })

  it("Field(name: string)", ()=>{
    let _: OmExpression = Expr.Field("a", From.Table("tab"))["Expression"];
    expect(_.Type).eq(OmExpressionType.Field);
    expect(_.Value).eq("a");
    expect(_.TableAlias).eq("tab");
  })

  it("Field(name: string, from: From)", ()=>{
    let _: OmExpression = Expr.Field("a", From.Table("tab"))["Expression"];
    expect(_.Type).eq(OmExpressionType.Field);
    expect(_.Value).eq("a");
    expect(_.TableAlias).eq("tab");

    _ = Expr.Field("a", From.Table("tab", "b"))["Expression"];
    expect(_.Type).eq(OmExpressionType.Field);
    expect(_.Value).eq("a");
    expect(_.TableAlias).eq("b");
  })
  
  // it("Constant(val: string|number|Date|Constant)", ()=>{
  //   let _: OmExpression = Expr.Constant("a")["Expression"];
  //   expect(_.Type).eq(OmExpressionType.Constant);
  //   expect(_.Value).instanceOf(OmConstant);
  //   expect((_.Value as OmConstant).Type).eq(OmDataType.String);
  //   expect((_.Value as OmConstant).Value).eq("a");

  //   _ = Expr.Constant(1)["Expression"];
  //   expect(_.Type).eq(OmExpressionType.Constant);
  //   expect(_.Value).instanceOf(OmConstant);
  //   expect((_.Value as OmConstant).Type).eq(OmDataType.Numeric);
  //   expect((_.Value as OmConstant).Value).eq(1);

  //   _ = Expr.Constant(d)["Expression"];
  //   expect(_.Type).eq(OmExpressionType.Constant);
  //   expect(_.Value).instanceOf(OmConstant);
  //   expect((_.Value as OmConstant).Type).eq(OmDataType.Date);
  //   expect(((_.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);

  //   _ = Expr.Constant(Constant.Number(1))["Expression"];
  //   expect(_.Type).eq(OmExpressionType.Constant);
  //   expect(_.Value).instanceOf(OmConstant);
  //   expect((_.Value as OmConstant).Type).eq(OmDataType.Numeric);
  //   expect((_.Value as OmConstant).Value).eq(1);

  // })
  
  it("Date(val: Date)", ()=>{
    let _: OmExpression = Expr.Date(d)["Expression"];
    expect(_.Type).eq(OmExpressionType.Constant);
    expect(_.Value).instanceOf(OmConstant);
    expect((_.Value as OmConstant).Type).eq(OmDataType.Date);
    expect(((_.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("Number(val: number)", ()=>{
    let _: OmExpression = Expr.Number(1)["Expression"];
    expect(_.Type).eq(OmExpressionType.Constant);
    expect(_.Value).instanceOf(OmConstant);
    expect((_.Value as OmConstant).Type).eq(OmDataType.Numeric);
    expect((_.Value as OmConstant).Value).eq(1);
  })

  it("String(val: string)", ()=>{
    let _: OmExpression = Expr.String("abc")["Expression"];
    expect(_.Type).eq(OmExpressionType.Constant);
    expect(_.Value).instanceOf(OmConstant);
    expect((_.Value as OmConstant).Type).eq(OmDataType.String);
    expect((_.Value as OmConstant).Value).eq("abc");
  })

  it("Null()", ()=>{
    let _: OmExpression = Expr.Null()["Expression"];
    expect(_.Type).eq(OmExpressionType.Null);
    expect(_.Value).undefined;
  })
  
  it("Func(func: ArgFunction, param: Expr)", ()=>{
    let _: OmExpression = Expr.Func(AggFunc.Max, Expr.Field("a"))["Expression"];
    expect(_.AggFunction).eq(OmAggregationFunction.Max);
    expect(_.Type).eq(OmExpressionType.Function);
    expect(_.SubExpr1).not.undefined;
    expect(_.SubExpr1.Type).eq(OmExpressionType.Field);
    expect(_.SubExpr1.Value).eq("a");
    expect(_.SubExpr1.StringValue).eq("a");
  })

  it("IfNull(test: Expr, val: string)", ()=>{
    let _: OmExpression = Expr.IfNull(Expr.Field("a"), "abc")["Expression"];
    expect(_.Type).eq(OmExpressionType.IfNull);
    expect(_.SubExpr1).not.undefined;
    expect(_.SubExpr1.Type).eq(OmExpressionType.Field);
    expect(_.SubExpr1.Value).eq("a");
    expect(_.SubExpr1.StringValue).eq("a");

    expect(_.SubExpr2).not.undefined;
    expect(_.SubExpr2.Type).eq(OmExpressionType.Constant);
    expect((_.SubExpr2.Value as OmConstant).Type).eq(OmDataType.String);
    expect((_.SubExpr2.Value as OmConstant).Value).eq("abc");
    expect((_.SubExpr2.Value as OmConstant).StringValue).eq("abc");
  })

  it("IfNull(test: Expr, val: number)", ()=>{
    let _: OmExpression = Expr.IfNull(Expr.Field("a"), 10)["Expression"];
    expect(_.Type).eq(OmExpressionType.IfNull);
    expect(_.SubExpr1).not.undefined;
    expect(_.SubExpr1.Type).eq(OmExpressionType.Field);
    expect(_.SubExpr1.Value).eq("a");
    expect(_.SubExpr1.StringValue).eq("a");

    expect(_.SubExpr2).not.undefined;
    expect(_.SubExpr2.Type).eq(OmExpressionType.Constant);
    expect((_.SubExpr2.Value as OmConstant).Type).eq(OmDataType.Numeric);
    expect((_.SubExpr2.Value as OmConstant).Value).eq(10);
    expect((_.SubExpr2.Value as OmConstant).NumericValue).eq(10);
  })

  it("IfNull(test: Expr, val: Date)", ()=>{
    let _: OmExpression = Expr.IfNull(Expr.Field("a"), d)["Expression"];
    expect(_.Type).eq(OmExpressionType.IfNull);
    expect(_.SubExpr1).not.undefined;
    expect(_.SubExpr1.Type).eq(OmExpressionType.Field);
    expect(_.SubExpr1.Value).eq("a");
    expect(_.SubExpr1.StringValue).eq("a");

    expect(_.SubExpr2).not.undefined;
    expect(_.SubExpr2.Type).eq(OmExpressionType.Constant);
    expect((_.SubExpr2.Value as OmConstant).Type).eq(OmDataType.Date);
    expect(((_.SubExpr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
    expect((_.SubExpr2.Value as OmConstant).DateValue.valueOf).eq(d.valueOf);
  })

  it("IfNull(test: Expr, val: Expr)", ()=>{
    let _: OmExpression = Expr.IfNull(Expr.Field("a"), Expr.Param("p"))["Expression"];
    expect(_.Type).eq(OmExpressionType.IfNull);
    expect(_.SubExpr1).not.undefined;
    expect(_.SubExpr1.Type).eq(OmExpressionType.Field);
    expect(_.SubExpr1.Value).eq("a");
    expect(_.SubExpr1.StringValue).eq("a");

    expect(_.SubExpr2).not.undefined;
    expect(_.SubExpr2.Type).eq(OmExpressionType.Parameter);
    expect(_.SubExpr2.Type).eq(OmExpressionType.Parameter);
    expect(_.SubExpr2.Value).eq("p");
    expect(_.SubExpr2.StringValue).eq("p");
  })

  it("SubQuery(subQuery: Select): Expr", ()=>{
    let _: OmExpression = Expr.SubQuery(Qb.Select("a"))["Expression"];
    expect(_.Type).eq(OmExpressionType.SubQueryObject);
    expect(_.Value).instanceof(SelectQuery)
    expect(_.QueryValue).instanceof(SelectQuery)
  })
})