import {JoinCondition} from "./JoinCondition";

export class JoinCond {
  private Condition: JoinCondition;
  constructor(leftField: string, rightField: string) {
    this.Condition = new JoinCondition(leftField, rightField);
  }

  static Fields(leftField: string, rightField: string): JoinCond;
  static Fields(field: string): JoinCond;
  static Fields(): JoinCond {
    var leftField: string;
    var rightField: string;
    if (arguments.length >= 1) {
      leftField = arguments[0];
      rightField = arguments[0];
    }
    if (arguments.length >= 2) {
      rightField = arguments[1];
    }
    return new JoinCond(leftField!, rightField!);
  }
}
