import {FromTerm} from "./FromTerm";
import {WhereClause} from "./WhereClause";
import {JoinType} from "./Enums";



interface IJoin {
  LeftTable: FromTerm;
  Conditions: WhereClause;
  RightTable: FromTerm;
  Type: JoinType;
}

export class Join implements IJoin {
  LeftTable: FromTerm;
  Conditions: WhereClause;
  RightTable: FromTerm;
  Type: JoinType;

  constructor(leftTable: FromTerm, rightTable: FromTerm, conditions: WhereClause, type: JoinType) {
    this.LeftTable = leftTable;
    this.RightTable = rightTable;
    this.Conditions = conditions;
    this.Type = type;
  }
}