import { expect } from 'chai';
import 'mocha';
import {JoinCond} from '../src/index'
import {JoinCondition} from '../src/JoinCondition'

describe("JoinCond", ()=>{
  it("Field(leftField: string, rightField: string): JoinCond", ()=>{
    let _: JoinCondition = JoinCond.Fields("a", "b")["Condition"];
    expect(_.LeftField).eq("a");
    expect(_.RightField).eq("b");
  })
  it("Field(field: string): JoinCond", ()=>{
    let _: JoinCondition = JoinCond.Fields("a")["Condition"];
    expect(_.LeftField).eq("a");
    expect(_.RightField).eq("a");
  })

})

