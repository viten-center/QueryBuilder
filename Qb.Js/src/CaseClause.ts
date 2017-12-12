import {CaseTerm} from "./CaseTerm"
import {OmExpression} from "./OmExpression"

  interface ICaseClause {
    Terms: Array<CaseTerm>;
    ElseValue: OmExpression;
  }

  export class CaseClause implements ICaseClause{
    Terms = new Array<CaseTerm>();
    ElseValue: OmExpression = null;
  }
