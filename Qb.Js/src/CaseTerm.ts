import { WhereClause } from "./WhereClause"
import { OmExpression } from "./OmExpression"

// export interface ICaseTerm {
//     Condition: WhereClause;
//     Value: OmExpression;
//   }

export class CaseTerm /*implements ICaseTerm*/ {
  Condition: WhereClause;
  Value: OmExpression;

  constructor(condition: WhereClause, val: OmExpression) {
    this.Condition = condition;
    this.Value = val;
  }
}
