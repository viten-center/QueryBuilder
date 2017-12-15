import { expect } from 'chai';
import 'mocha';
import { Param, DbType } from "../src/index";


describe("Param", () => {
  it("New(parameterName: string, dataType: DbType, value: any)", () => {
    let p: Param = Param.New("p", DbType.Double, 2.2);
    expect(p.Name).eq("p");
    expect(p.DataType).eq(DbType.Double);
    expect(p.Value).eq(2.2);
  })
})
