interface IJoinCondition {
  LeftField: string;
  RightField: string;
}

export class JoinCondition implements IJoinCondition {
  LeftField: string;
  RightField: string;

  constructor(leftField: string, rightField: string) {
    this.LeftField = leftField;
    this.RightField = rightField;
  }
}