import {JoinCondition} from "./JoinCondition";

export class JoinCond {
  private Condition: JoinCondition;
  constructor(leftField: string, rightField: string) {
    this.Condition = new JoinCondition(leftField, rightField);
  }

  static Field(leftField: string, rightField: string): JoinCond;
  static Field(field: string): JoinCond;
  static Field(): JoinCond {
    var leftField, rightField: string;
    if (arguments.length >= 1) {
      leftField = arguments[0];
      rightField = arguments[0];
    }
    if (arguments.length >= 2) {
      rightField = arguments[1];
    }
    return new JoinCond(leftField, rightField);
  }
}
