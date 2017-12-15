import { expect } from 'chai';
import 'mocha';
import { Cond, Logic } from "../src/index";
import { Constant } from "../src/Constant";
import { OmExpressionType, FromTermType, CompOper, WhereRel, DataType } from "../src/Enums";
import { OmConstant } from '../src/OmConstant';

describe("Logic", () => {
  let c0: Cond = Cond.Equal("a", 1);
  let c1: Cond = Cond.Greater("b", 1);
  let l0: Logic = Logic.And([c0, c1]);
  let l1 = Logic.And(c0, c1);

  let expLogic1 = function (logic: Logic, isOr: boolean) {
    if (isOr)
      expect(logic.Clause.Relationship).equal(WhereRel.Or);
    else
      expect(logic.Clause.Relationship).equal(WhereRel.And);
    expect(logic.Clause.Terms.length).equal(2);

    expect(logic.Clause.Terms[0].Op).equal(CompOper.Equal);
    expect(logic.Clause.Terms[0].Expr1.Type).equal(OmExpressionType.Field);
    expect(logic.Clause.Terms[0].Expr1.Value).equal("a");
    expect(logic.Clause.Terms[0].Expr2.Type).equal(OmExpressionType.Constant);
    expect(logic.Clause.Terms[0].Expr2.Value).instanceof(OmConstant);
    expect((logic.Clause.Terms[0].Expr2.Value as OmConstant).Type).equal(DataType.Numeric);
    expect((logic.Clause.Terms[0].Expr2.Value as OmConstant).Value).equal(1);

    expect(logic.Clause.Terms[1].Op).equal(CompOper.Greater);
    expect(logic.Clause.Terms[1].Expr1.Type).equal(OmExpressionType.Field);
    expect(logic.Clause.Terms[1].Expr1.Value).equal("b");
    expect(logic.Clause.Terms[1].Expr2.Type).equal(OmExpressionType.Constant);
    expect(logic.Clause.Terms[1].Expr2.Value).instanceof(OmConstant);
    expect((logic.Clause.Terms[1].Expr2.Value as OmConstant).Type).equal(DataType.Numeric);
    expect((logic.Clause.Terms[1].Expr2.Value as OmConstant).Value).equal(1);
  }
 
  it("And(conds: Cond[])", () => {
    expLogic1(l0, false);
  })

  it("And(...conds: Cond[])", () => {
    expLogic1(l1, false);
  })

  let expLogic2 = function (logic: Logic, isOr: boolean) {
    expect(logic.Clause.SubClauses.length).eq(2);
    if (isOr)
      expect(logic.Clause.Relationship).eq(WhereRel.Or);
    else
      expect(logic.Clause.Relationship).eq(WhereRel.And);

    expect(logic.Clause.SubClauses[0].Relationship).eq(WhereRel.And);
    expect(logic.Clause.SubClauses[0].Terms.length).eq(2);
    expect(logic.Clause.SubClauses[0].Terms[0].Op).eq(CompOper.Equal);
    expect(logic.Clause.SubClauses[0].Terms[0].Expr1.Type).eq(OmExpressionType.Field);
    expect(logic.Clause.SubClauses[0].Terms[0].Expr1.Value).eq("a");
    expect(logic.Clause.SubClauses[0].Terms[0].Expr2.Type).eq(OmExpressionType.Constant);
    expect((logic.Clause.SubClauses[0].Terms[0].Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((logic.Clause.SubClauses[0].Terms[0].Expr2.Value as OmConstant).Value).eq(1);

    expect(logic.Clause.SubClauses[0].Relationship).eq(WhereRel.And);
    expect(logic.Clause.SubClauses[0].Terms.length).eq(2);
    expect(logic.Clause.SubClauses[0].Terms[1].Op).eq(CompOper.Greater);
    expect(logic.Clause.SubClauses[0].Terms[1].Expr1.Type).eq(OmExpressionType.Field);
    expect(logic.Clause.SubClauses[0].Terms[1].Expr1.Value).eq("b");
    expect(logic.Clause.SubClauses[0].Terms[1].Expr2.Type).eq(OmExpressionType.Constant);
    expect((logic.Clause.SubClauses[0].Terms[1].Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((logic.Clause.SubClauses[0].Terms[1].Expr2.Value as OmConstant).Value).eq(1);

    expect(logic.Clause.SubClauses.length).eq(2);

    expect(logic.Clause.SubClauses[1].Relationship).eq(WhereRel.And);
    expect(logic.Clause.SubClauses[1].Terms.length).eq(2);
    expect(logic.Clause.SubClauses[1].Terms[0].Op).eq(CompOper.Equal);
    expect(logic.Clause.SubClauses[1].Terms[0].Expr1.Type).eq(OmExpressionType.Field);
    expect(logic.Clause.SubClauses[1].Terms[0].Expr1.Value).eq("a");
    expect(logic.Clause.SubClauses[1].Terms[0].Expr2.Type).eq(OmExpressionType.Constant);
    expect((logic.Clause.SubClauses[1].Terms[0].Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((logic.Clause.SubClauses[1].Terms[0].Expr2.Value as OmConstant).Value).eq(1);

    expect(logic.Clause.SubClauses[1].Relationship).eq(WhereRel.And);
    expect(logic.Clause.SubClauses[1].Terms.length).eq(2);
    expect(logic.Clause.SubClauses[1].Terms[1].Op).eq(CompOper.Greater);
    expect(logic.Clause.SubClauses[1].Terms[1].Expr1.Type).eq(OmExpressionType.Field);
    expect(logic.Clause.SubClauses[1].Terms[1].Expr1.Value).eq("b");
    expect(logic.Clause.SubClauses[1].Terms[1].Expr2.Type).eq(OmExpressionType.Constant);
    expect((logic.Clause.SubClauses[1].Terms[1].Expr2.Value as OmConstant).Type).eq(DataType.Numeric);
    expect((logic.Clause.SubClauses[1].Terms[1].Expr2.Value as OmConstant).Value).eq(1);
  }

  it("And(logics: Logic[])", () => {
    let l2 = Logic.And([l0, l1]);
    expLogic2(l2, false);
  })

  it("And(...logics: Logic[])", () => {
    let l3 = Logic.And(l0, l1);
    expLogic2(l3, false);
  })

  it("Or(...logics: Logic[]): Logic", () => {
    let l4 = Logic.Or(l0, l1);
    expLogic2(l4, true);
  })

  it("Or(...opers: Cond[]): Logic", () => {
    let l4 = Logic.Or(c0, c1);
    expLogic1(l4, true);
  })
})
