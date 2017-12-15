import { expect } from 'chai';
import 'mocha';

import { Constant } from "../src/Constant";
import { OmConstant } from '../src/OmConstant';
import { DataType } from "../src/Enums";

describe("Constant", () => {
  it("Number", ()=>{
    let c = Constant.Number(10)["Const"] as OmConstant;
    expect(c.Type).eq(DataType.Numeric);
    expect(c.Value).eq(10);
    expect(c.NumericValue).eq(10);
    expect(c.DateValue).undefined;
    expect(c.StringValue).undefined;
  })
  it("String", ()=>{
    let c = Constant.String("abc")["Const"] as OmConstant;
    expect(c.Type).eq(DataType.String);
    expect(c.Value).eq("abc");
    expect(c.NumericValue).undefined;
    expect(c.DateValue).undefined;
    expect(c.StringValue).eq("abc");
  })
  it("Date", ()=>{
    let d: Date = new Date(2018, 0, 1, 0, 0, 0, 0);
    let c = Constant.Date(d)["Const"] as OmConstant;
    expect(c.Type).eq(DataType.Date);
    expect(c.Value).instanceof(Date);
    expect((c.Value as Date).valueOf).eq(new Date(2018, 0, 1, 0, 0, 0, 0).valueOf)
    expect(c.NumericValue).undefined;
    expect(c.DateValue.valueOf).eq(new Date(2018, 0, 1, 0, 0, 0, 0).valueOf);
    expect(c.StringValue).undefined;
  })
})
