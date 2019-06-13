import { WhereClause } from "./WhereClause"
import { OmExpression } from "./OmExpression"

export class CaseTerm {
  Condition: WhereClause;
  Value: OmExpression;

  constructor(condition: WhereClause, val: OmExpression) {
    this.Condition = condition;
    this.Value = val;
  }
}
