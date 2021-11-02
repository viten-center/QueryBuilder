import { expect } from 'chai';
import 'mocha';
import { SelectQuery, Select, Qb, JoinCond, JoinType, Cond, From } from "../src/index";
import { FromTermType, CompOper, OmExpressionType } from '../src/Enums';
import { Logic } from '../src/Logic';
import { Expr } from '../src/Expr';


describe("Join", () => {
  it("Join(rightTableName: string, joinCond: JoinCond): Select", () => {
    let _: SelectQuery = Qb.Select("a").From("lt").Join("rt", JoinCond.Fields("id_0"))["Query"];
    expect(_.FromClause.BaseTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.BaseTable.Expression).eq("lt");
    expect(_.FromClause.BaseTable.RefName).eq("lt");
    expect(_.FromClause.Joins).not.undefined;
    expect(_.FromClause.Joins.length).eq(1);
    expect(_.FromClause.Joins[0].Type).eq(JoinType.Inner);
    expect(_.FromClause.Joins[0].LeftTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.Joins[0].LeftTable.RefName).eq("lt");
    expect(_.FromClause.Joins[0].RightTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.Joins[0].RightTable.RefName).eq("rt");
    expect(_.FromClause.Joins[0].Conditions.Terms.length).eq(1);

    expect(_.FromClause.Joins[0].Conditions.Terms[0].Op).eq(CompOper.Equal);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.TableAlias).eq("lt");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.Type).eq(OmExpressionType.Field);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.Value).eq("id_0");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.TableAlias).eq("rt");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.Type).eq(OmExpressionType.Field);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.Value).eq("id_0");
  })

  it("Join(joinType: JoinType, leftTable: From, rightTable: From, where: Logic): Select", () => {
    let lt = From.Table("lt");
    let rt = From.Table("rt");

    let l: Logic = Logic.And(Cond.Equal(Expr.Field("id_0", lt), Expr.Field("id_0", rt)));
    let _: SelectQuery = Qb.Select("a").From("lt").Join(JoinType.Left, lt, rt, l)["Query"];

    expect(_.FromClause.BaseTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.BaseTable.Expression).eq("lt");
    expect(_.FromClause.BaseTable.RefName).eq("lt");
    expect(_.FromClause.Joins).not.undefined;
    expect(_.FromClause.Joins.length).eq(1);
    expect(_.FromClause.Joins[0].Type).eq(JoinType.Left);
    expect(_.FromClause.Joins[0].LeftTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.Joins[0].LeftTable.RefName).eq("lt");
    expect(_.FromClause.Joins[0].RightTable.Type).eq(FromTermType.Table);
    expect(_.FromClause.Joins[0].RightTable.RefName).eq("rt");
    expect(_.FromClause.Joins[0].Conditions.Terms.length).eq(1);

    expect(_.FromClause.Joins[0].Conditions.Terms[0].Op).eq(CompOper.Equal);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.TableAlias).eq("lt");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.Type).eq(OmExpressionType.Field);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr1.Value).eq("id_0");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.TableAlias).eq("rt");
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.Type).eq(OmExpressionType.Field);
    expect(_.FromClause.Joins[0].Conditions.Terms[0].Expr2.Value).eq("id_0");

  })
})
