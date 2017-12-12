import { expect } from 'chai';
import 'mocha';

import { Qb, Select, Column, Param, Cond, Logic, Constant } from "../src/qb";

import { SelectQuery } from "../src/SelectQuery";
import { OmExpressionType, FromTermType, CompareOperator, WhereClauseRelationship, OmDataType } from "../src/Enums";
import { WhereTerm } from '../src/WhereTerm';
import { OmConstant } from '../src/OmConstant';



describe("SELECT", () => {
  it("check Logic.And", () => {
    let conds = [Cond.Equal("a", 1), Cond.Greater("b", 1)]

    let l0: Logic = Logic.And(conds);
    expect(l0.Clause.Relationship).equal(WhereClauseRelationship.And);
    expect(l0.Clause.Terms.length).equal(2);

    expect(l0.Clause.Terms[0].Op).equal(CompareOperator.Equal);
    expect(l0.Clause.Terms[0].Expr1.Type).equal(OmExpressionType.Field);
    expect(l0.Clause.Terms[0].Expr1.Value).equal("a");
    expect(l0.Clause.Terms[0].Expr2.Type).equal(OmExpressionType.Constant);
    expect(l0.Clause.Terms[0].Expr2.Value).instanceof(OmConstant);
    expect((l0.Clause.Terms[0].Expr2.Value as OmConstant).Type).equal(OmDataType.Numeric);
    expect((l0.Clause.Terms[0].Expr2.Value as OmConstant).Value).equal(1);

    expect(l0.Clause.Terms[1].Op).equal(CompareOperator.Greater);
    expect(l0.Clause.Terms[1].Expr1.Type).equal(OmExpressionType.Field);
    expect(l0.Clause.Terms[1].Expr1.Value).equal("b");
    expect(l0.Clause.Terms[1].Expr2.Type).equal(OmExpressionType.Constant);
    expect(l0.Clause.Terms[1].Expr2.Value).instanceof(OmConstant);
    expect((l0.Clause.Terms[1].Expr2.Value as OmConstant).Type).equal(OmDataType.Numeric);
    expect((l0.Clause.Terms[1].Expr2.Value as OmConstant).Value).equal(1);

    let l1 = Logic.And(Cond.Equal("a", 1), Cond.Greater("b", 1));
    expect(l1.Clause.Relationship).equal(WhereClauseRelationship.And);
    expect(l1.Clause.Terms.length).equal(2);

    expect(l1.Clause.Terms[0].Op).equal(CompareOperator.Equal);
    expect(l1.Clause.Terms[0].Expr1.Type).equal(OmExpressionType.Field);
    expect(l1.Clause.Terms[0].Expr1.Value).equal("a");
    expect(l1.Clause.Terms[0].Expr2.Type).equal(OmExpressionType.Constant);
    expect(l1.Clause.Terms[0].Expr2.Value).instanceof(OmConstant);
    expect((l1.Clause.Terms[0].Expr2.Value as OmConstant).Type).equal(OmDataType.Numeric);
    expect((l1.Clause.Terms[0].Expr2.Value as OmConstant).Value).equal(1);

    expect(l1.Clause.Terms[1].Op).equal(CompareOperator.Greater);
    expect(l1.Clause.Terms[1].Expr1.Type).equal(OmExpressionType.Field);
    expect(l1.Clause.Terms[1].Expr1.Value).equal("b");
    expect(l1.Clause.Terms[1].Expr2.Type).equal(OmExpressionType.Constant);
    expect(l1.Clause.Terms[1].Expr2.Value).instanceof(OmConstant);
    expect((l1.Clause.Terms[1].Expr2.Value as OmConstant).Type).equal(OmDataType.Numeric);
    expect((l1.Clause.Terms[1].Expr2.Value as OmConstant).Value).equal(1);


    // let l2 = Logic.And([l0, l1]);
    // let l3 = Logic.And(l0, l1);
  })

  let q: Select = Qb.Select(
    Column.New("a"),
    Column.New("b"))
    .From("tab")
    .Where(
    Cond.Equal("a", 10),
    Cond.Between("b", 1, 5)
    )
    .Distinct()
    .OrderBy("a");
  let sq: SelectQuery = Qb.GetQueryObject(q);

  it("check columns", () => {
    expect(sq.Columns.length).equal(2);
    expect(sq.Columns[0].ColumnAlias).undefined;
    expect(sq.Columns[0].Expression.Type).equal(OmExpressionType.Field);
    expect(sq.Columns[0].Expression.Value).equal("a");
    expect(sq.Columns[1].ColumnAlias).undefined;
    expect(sq.Columns[1].Expression.Type).equal(OmExpressionType.Field);
    expect(sq.Columns[1].Expression.Value).equal("b");
  })

  it("check from", () => {
    expect(sq.FromClause.BaseTable.Expression).equal("tab");
    expect(sq.FromClause.BaseTable.Type).equal(FromTermType.Table);
  })


  it("check where", () => {
    expect(sq.WherePhrase.SubClauses.length).equal(1);
    expect(sq.WherePhrase.SubClauses[0].Terms.length).equal(2);
    expect(sq.WherePhrase.SubClauses[0].Terms[0].Op).equal(CompareOperator.Equal);
  })
})

