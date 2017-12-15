import { CaseTerm } from "./CaseTerm"
import { OmExpression } from "./OmExpression"

// export interface ICaseClause {
//   Terms: Array<CaseTerm>;
//   ElseValue: OmExpression;
// }

export class CaseClause /*implements ICaseClause*/ {
  Terms = new Array<CaseTerm>();
  ElseValue: OmExpression;
}
