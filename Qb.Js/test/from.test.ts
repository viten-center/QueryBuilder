import { expect } from 'chai';
import 'mocha';
import { Qb, Union,From, SelectQuery, OmUnion } from "../src/index";

import { FromTerm } from '../src/FromTerm';
import { FromTermType } from '../src/Enums';

describe("From", () => {
  it("Table(tableName: string): From", () => {
    let _: FromTerm = From.Table("a").Term;
    expect(_.Type).eq(FromTermType.Table);
    expect(_.RefName).eq("a");
    expect(_.Expression).eq("a");
    expect(_.StringValue).eq("a");
  })

  it("Table(tableName: string, alias: string): From", () => {
    let _: FromTerm = From.Table("a", "b").Term;
    expect(_.Type).eq(FromTermType.Table);
    expect(_.RefName).eq("b");
    expect(_.Alias).eq("b");
    expect(_.Expression).eq("a");
    expect(_.StringValue).eq("a");
  })

  it("Table(tableName: string, alias: string, ns: string): From", () => {
    let _: FromTerm = From.Table("a", "b", "ns1").Term;
    expect(_.Type).eq(FromTermType.Table);
    expect(_.Ns1).eq("ns1");
    expect(_.RefName).eq("b");
    expect(_.Alias).eq("b");
    expect(_.Expression).eq("a");
    expect(_.StringValue).eq("a");
  })

  it("Table(tableName: string, alias: string, ns1: string, ns2: string): From", () => {
    let _: FromTerm = From.Table("a", "b", "ns1", "ns2").Term;
    expect(_.Type).eq(FromTermType.Table);
    expect(_.Ns1).eq("ns1");
    expect(_.Ns2).eq("ns2");
    expect(_.RefName).eq("b");
    expect(_.Alias).eq("b");
    expect(_.Expression).eq("a");
    expect(_.StringValue).eq("a");
  })

  it("SubQuery(subQuery: Select, alias: string): From", () => {
    let _: FromTerm = From.SubQuery(Qb.Select("a"), "t").Term;
    expect(_.Type).eq(FromTermType.SubQueryObj);
    expect(_.RefName).eq("t");
    expect(_.Alias).eq("t");
    expect(_.Expression).instanceof(SelectQuery);
    expect(_.QueryValue).instanceof(SelectQuery);
  })

  it("Union(union: Union, alias: string): From", ()=>{
    let u: Union = Qb.Union();
    let _: FromTerm = From.Union(u, "t").Term;
    expect(_.Type).eq(FromTermType.Union);
    expect(_.Alias).eq("t");
    expect(_.RefName).eq("t");
    expect(_.Expression).instanceof(OmUnion);
    expect(_.UnionValue).instanceof(OmUnion);
  })
})  