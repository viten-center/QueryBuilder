import { expect } from 'chai';
import 'mocha';
import { Expr, Cond, From, Select, Qb, SelectQuery } from '../src/index';

import { WhereTerm } from '../src/WhereTerm'
//import { Cond } from '../src/Cond'
//import { Expr } from '../src/Expr';
import { CompOper, WhereTermType, OmExpressionType, ExprValCode, DataType } from '../src/Enums';
//import { From } from '../src/From';
import { OmConstant } from '../src/OmConstant';
import { FromTerm } from '../src/FromTerm';
//import { Select, Qb } from '../src/Qb';
//import { SelectQuery } from '../src/SelectQuery';

describe("Cond", () => {
  let d: Date = new Date(2018, 0, 1, 0, 0, 0, 0);

  it("Equal(field: string, expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.ValueCode).eq(ExprValCode.String);
    expect(_.Expr2.Value).eq("p");
  })

  it("Equal(field: string, alias: From, expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", From.Table("t"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("Equal(field: string, value: number): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", 1)["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);
  })

  it("Equal(field: string, alias: From, value: number): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", From.Table("t"), 1)["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("Equal(field: string, value: string): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", "abc")["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");
  })

  it("Equal(field: string, alias: From, value: string): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", From.Table("t"), "abc")["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("Equal(field: string, value: Date): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", d)["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("Equal(field: string, alias: From, value: Date): Cond", () => {
    let _: WhereTerm = Cond.Equal("a", From.Table("t"), d)["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("Equal(expr1: Expr, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.Equal(Expr.Field("a"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Equal);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("NotEqual(field: string, expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.ValueCode).eq(ExprValCode.String);
    expect(_.Expr2.Value).eq("p");
  })

  it("NotEqual(field: string, alias: From, expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", From.Table("t"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("NotEqual(field: string, value: number): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", 1)["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);
  })

  it("NotEqual(field: string, alias: From, value: number): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", From.Table("t"), 1)["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("NotEqual(field: string, value: string): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", "abc")["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");
  })

  it("NotEqual(field: string, alias: From, value: string): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", From.Table("t"), "abc")["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("NotEqual(field: string, value: Date): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", d)["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("NotEqual(field: string, alias: From, value: Date): Cond", () => {
    let _: WhereTerm = Cond.NotEqual("a", From.Table("t"), d)["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("NotEqual(expr1: Expr, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotEqual(Expr.Field("a"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  //LIKE
  it("Like(expr1: Expr, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.Like(Expr.Field("a"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("Like(expr1: Expr, expr2: Expr, escapeChar: string): Cond", () => {
    let _: WhereTerm = Cond.Like(Expr.Field("a"), Expr.Param("p"), "!")["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
    expect(_.Expr3.ValueCode).eq(ExprValCode.SqlConst);
    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr3.Value as OmConstant).Value).eq("!");
  })

  it("Like(field: string, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.Like("a", Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("Like(field: string, alias: From, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.Like("a", From.Table("t"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("Like(field: string, value: string): Cond", () => {
    let _: WhereTerm = Cond.Like("a", "%")["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("%");
  })

  it("Like(field: string, alias: From, value: string): Cond", (() => {
    let _: WhereTerm = Cond.Like("a", From.Table("t"), "%")["Term"];
    expect(_.Op).eq(CompOper.Like);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");

    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("%");
  }))
  //NOTLIKE
  it("NotLike(expr1: Expr, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotLike(Expr.Field("a"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("NotLike(expr1: Expr, expr2: Expr, escapeChar: string): Cond", () => {
    let _: WhereTerm = Cond.NotLike(Expr.Field("a"), Expr.Param("p"), "!")["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
    expect(_.Expr3.ValueCode).eq(ExprValCode.SqlConst);
    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr3.Value as OmConstant).Value).eq("!");
  })

  it("NotLike(field: string, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotLike("a", Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("NotLike(field: string, alias: From, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.NotLike("a", From.Table("t"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("NotLike(field: string, value: string): Cond", () => {
    let _: WhereTerm = Cond.NotLike("a", "%")["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("%");
  })

  it("NotLike(field: string, alias: From, value: string): Cond", (() => {
    let _: WhereTerm = Cond.NotLike("a", From.Table("t"), "%")["Term"];
    expect(_.Op).eq(CompOper.NotLike);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");

    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("%");
  }))

  // LessOrEqual
  it("LessOrEqual(expr1: Expr, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual(Expr.Field("a"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("LessOrEqual(field: string, alias: From, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", From.Table("t"), Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("LessOrEqual(field: string, expr2: Expr): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", Expr.Param("p"))["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p");
    expect(_.Expr2.ValueCode).eq(DataType.String);
  })

  it("LessOrEqual(field: string, value: number): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", 1)["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);

  })

  it("LessOrEqual(field: string, alias: From, value: number): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", From.Table("t"), 1)["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })


  it("LessOrEqual(field: string, value: string): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", "abc")["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");

  })

  it("LessOrEqual(field: string, alias: From, value: string): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", From.Table("t"), "abc")["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.String);
    expect((_.Expr2.Value as OmConstant).Value).eq("abc");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  it("LessOrEqual(field: string, value: Date): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", d)["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);

  })

  it("LessOrEqual(field: string, alias: From, value: Date): Cond", () => {
    let _: WhereTerm = Cond.LessOrEqual("a", From.Table("t"), d)["Term"];
    expect(_.Op).eq(CompOper.LessOrEqual);
    expect(_.Type).eq(WhereTermType.Compare);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).not.undefined;
    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table).instanceof(FromTerm);
    expect(_.Expr1.TableAlias).eq("t");
  })

  // IN
  it("In(expr: Expr, values: number[]): Cond", () => {
    let _: WhereTerm = Cond.In(Expr.Field("a"), [1, 2, 3])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Values[0] as OmConstant).Value).eq(1);
  })

  it("In(expr: Expr, values: string[]): Cond", () => {
    let _: WhereTerm = Cond.In(Expr.Field("a"), ["1", "2", "3"])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.String);
    expect((_.Values[0] as OmConstant).Value).eq("1");
  })

  it("In(expr: Expr, values: Date[]): Cond", () => {
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let d2: Date = new Date(2020, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.In(Expr.Field("a"), [d, d1, d2])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Date);
    expect(((_.Values[0] as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("In(field: string, number[]): Cond", () => {
    let _: WhereTerm = Cond.In("a", [1, 2, 3])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Values[0] as OmConstant).Value).eq(1);
  })

  it("In(field: string, values: string[]): Cond", () => {
    let _: WhereTerm = Cond.In("a", ["1", "2", "3"])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.String);
    expect((_.Values[0] as OmConstant).Value).eq("1");
  })

  it("In(field: string, values: Date[]): Cond", () => {
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let d2: Date = new Date(2020, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.In("a", [d, d1, d2])["Term"];
    expect(_.Type).eq(WhereTermType.In);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Date);
    expect(((_.Values[0] as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("In(expr: Expr, subQuery: Select): Cond", () => {
    let s: Select = Qb.Select("a");
    let _: WhereTerm = Cond.In(Expr.Field("a"), s)["Term"];
    expect(_.Type).eq(WhereTermType.InSubQuery);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

  it("In(field: string, subQuery: Select): Cond", () => {
    let s: Select = Qb.Select("a");
    let _: WhereTerm = Cond.In("a", s)["Term"];
    expect(_.Type).eq(WhereTermType.InSubQuery);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

  //NOT IN
  it("NotIn(expr: Expr, values: number[]): Cond", () => {
    let _: WhereTerm = Cond.NotIn(Expr.Field("a"), [1, 2, 3])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Values[0] as OmConstant).Value).eq(1);
  })

  it("NotIn(expr: Expr, values: string[]): Cond", () => {
    let _: WhereTerm = Cond.NotIn(Expr.Field("a"), ["1", "2", "3"])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.String);
    expect((_.Values[0] as OmConstant).Value).eq("1");
  })

  it("NotIn(expr: Expr, values: Date[]): Cond", () => {
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let d2: Date = new Date(2020, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.NotIn(Expr.Field("a"), [d, d1, d2])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Date);
    expect(((_.Values[0] as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("NotIn(field: string, number[]): Cond", () => {
    let _: WhereTerm = Cond.NotIn("a", [1, 2, 3])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Values[0] as OmConstant).Value).eq(1);
  })

  it("NotIn(field: string, values: string[]): Cond", () => {
    let _: WhereTerm = Cond.NotIn("a", ["1", "2", "3"])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.String);
    expect((_.Values[0] as OmConstant).Value).eq("1");
  })

  it("NotIn(field: string, values: Date[]): Cond", () => {
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let d2: Date = new Date(2020, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.NotIn("a", [d, d1, d2])["Term"];
    expect(_.Type).eq(WhereTermType.NotIn);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2).undefined;
    expect(_.Values).not.undefined;
    expect(_.Values).instanceOf(Array);
    expect(_.Values.length).eq(3);
    expect(_.Values[0]).instanceof(OmConstant);
    expect((_.Values[0] as OmConstant).Type).eq(DataType.Date);
    expect(((_.Values[0] as OmConstant).Value as Date).valueOf).eq(d.valueOf);
  })

  it("NotIn(expr: Expr, subQuery: Select): Cond", () => {
    let s: Select = Qb.Select("a");
    let _: WhereTerm = Cond.NotIn(Expr.Field("a"), s)["Term"];
    expect(_.Type).eq(WhereTermType.NotInSubQuery);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

  it("NotIn(field: string, subQuery: Select): Cond", () => {
    let s: Select = Qb.Select("a");
    let _: WhereTerm = Cond.NotIn("a", s)["Term"];
    expect(_.Type).eq(WhereTermType.NotInSubQuery);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

  // IS NULL
  it("IsNull(expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.IsNull(Expr.Field("a"))["Term"];
    expect(_.Type).eq(WhereTermType.IsNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
  })

  it("IsNull(field: string): Cond", () => {
    let _: WhereTerm = Cond.IsNull("a")["Term"];
    expect(_.Type).eq(WhereTermType.IsNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
  })

  it("IsNull(field: string, alias: From): Cond", () => {
    let _: WhereTerm = Cond.IsNull("a", From.Table("t"))["Term"];
    expect(_.Type).eq(WhereTermType.IsNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table.RefName).eq("t");
  })

  // IS NOT NULL
  it("IsNotNull(expr: Expr): Cond", () => {
    let _: WhereTerm = Cond.IsNotNull(Expr.Field("a"))["Term"];
    expect(_.Type).eq(WhereTermType.IsNotNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
  })

  it("IsNotNull(field: string): Cond", () => {
    let _: WhereTerm = Cond.IsNotNull("a")["Term"];
    expect(_.Type).eq(WhereTermType.IsNotNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
  })

  it("IsNotNull(field: string, alias: From): Cond", () => {
    let _: WhereTerm = Cond.IsNotNull("a", From.Table("t"))["Term"];
    expect(_.Type).eq(WhereTermType.IsNotNull);
    expect(_.Expr1).not.undefined;
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table.RefName).eq("t");
  })
  //BETWEEN
  it("Between(expr: Expr, lowBound: Expr, highBound: Expr): Cond", ()=>{
    let _: WhereTerm = Cond.Between(Expr.Field("a"), Expr.Param("p1"), Expr.Param("p2"))["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p1");
    expect(_.Expr2.ValueCode).eq(DataType.String);
    expect(_.Expr3.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr3.Value).eq("p2");
    expect(_.Expr3.ValueCode).eq(DataType.String);
  })

  it("Between(field: string, lowBound: Expr, highBound: Expr): Cond", ()=>{
    let _: WhereTerm = Cond.Between("a", Expr.Param("p1"), Expr.Param("p2"))["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p1");
    expect(_.Expr2.ValueCode).eq(DataType.String);
    expect(_.Expr3.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr3.Value).eq("p2");
    expect(_.Expr3.ValueCode).eq(DataType.String);
  })

  it("Between(field: string, alias: From, lowBound: Expr, highBound: Expr): Cond", ()=>{
    let _: WhereTerm = Cond.Between("a", From.Table("t"), Expr.Param("p1"), Expr.Param("p2"))["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table.RefName).eq("t");
    expect(_.Expr2.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr2.Value).eq("p1");
    expect(_.Expr2.ValueCode).eq(DataType.String);
    expect(_.Expr3.Type).eq(OmExpressionType.Parameter);
    expect(_.Expr3.Value).eq("p2");
    expect(_.Expr3.ValueCode).eq(DataType.String);
  })

  it("Between(field: string, alias: From, lowBound: number, highBound: number): Cond", ()=>{
    let _: WhereTerm = Cond.Between("a", From.Table("t"), 1, 2)["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table.RefName).eq("t");

    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);

    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr3.Value as OmConstant).Value).eq(2);
  })

  it("Between(field: string, alias: From, lowBound: Date, highBound: Date): Cond", ()=>{
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.Between("a", From.Table("t"), d, d1)["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");
    expect(_.Expr1.Table).not.undefined;
    expect(_.Expr1.Table.RefName).eq("t");

    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);

    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr3.Value as OmConstant).Value as Date).valueOf).eq(d1.valueOf);
  })

  it("Between(field: string, lowBound: number, highBound: number): Cond", ()=>{
    let _: WhereTerm = Cond.Between("a", 1, 2)["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");

    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr2.Value as OmConstant).Value).eq(1);

    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((_.Expr3.Value as OmConstant).Value).eq(2);
  })

  it("Between(field: string, lowBound: Date, highBound: Date): Cond", ()=>{
    let d1: Date = new Date(2019, 0, 1, 0, 0, 0, 0);
    let _: WhereTerm = Cond.Between("a", d, d1)["Term"];
    expect(_.Type).eq(WhereTermType.Between);
    expect(_.Expr1.Type).eq(OmExpressionType.Field);
    expect(_.Expr1.ValueCode).eq(ExprValCode.String);
    expect(_.Expr1.Value).eq("a");

    expect(_.Expr2.Type).eq(OmExpressionType.Constant);
    expect(_.Expr2.Value).instanceof(OmConstant);
    expect((_.Expr2.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr2.Value as OmConstant).Value as Date).valueOf).eq(d.valueOf);

    expect(_.Expr3.Type).eq(OmExpressionType.Constant);
    expect(_.Expr3.Value).instanceof(OmConstant);
    expect((_.Expr3.Value as OmConstant).Type).eq(DataType.Date);
    expect(((_.Expr3.Value as OmConstant).Value as Date).valueOf).eq(d1.valueOf);
  })

  it("Exists(subQuery: Select): Cond", ()=>{
    let _: WhereTerm = Cond.Exists(Qb.Select("a"))["Term"];
    expect(_.Type).eq(WhereTermType.Exists);
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

  it("NotExists(subQuery: Select): Cond", ()=>{
    let _: WhereTerm = Cond.NotExists(Qb.Select("a"))["Term"];
    expect(_.Type).eq(WhereTermType.NotExists);
    expect(_.SubQuery).not.undefined;
    expect(_.SubQuery).instanceof(SelectQuery);
  })

})
