import { OmExpression } from "./OmExpression"
import { OmConstant } from "./OmConstant"
import { CompareOperator, WhereTermType } from "./Enums"
import { SelectQuery } from "./SelectQuery"


interface IWhereTerm {
  Expr1: OmExpression;
  Expr2: OmExpression;
  Expr3: OmExpression;
  Op: CompareOperator;
  Type: WhereTermType;
  Values: Array<OmConstant>;
  SubQuery: any;
}

export class WhereTerm implements IWhereTerm {
  Expr1: OmExpression;
  Expr2: OmExpression;
  Expr3: OmExpression;
  Op: CompareOperator;
  Type: WhereTermType;
  Values = new Array<OmConstant>();
  SubQuery: any;

  static CreateCompare(expr1: OmExpression, expr2: OmExpression, op: CompareOperator): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr1;
    term.Expr2 = expr2;
    term.Op = op;
    term.Type = WhereTermType.Compare;
    return term;
  }

  static CreateLikeCompare(expr1: OmExpression, expr2: OmExpression, escapeChar: string): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr1;
    term.Expr2 = expr2;
    term.Expr3 = OmExpression.String(escapeChar);
    term.Op = CompareOperator.Like;
    term.Type = WhereTermType.Compare;
    return term;
  }

  static CreateNotLikeCompare(expr1: OmExpression, expr2: OmExpression, escapeChar: string): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr1;
    term.Expr2 = expr2;
    term.Expr3 = OmExpression.String(escapeChar);
    term.Op = CompareOperator.NotLike;
    term.Type = WhereTermType.Compare;
    return term;
  }

  static CreateIn(expr: OmExpression, scope: string | SelectQuery | Array<OmConstant>): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr;
    if (typeof scope == "string") {
      term.SubQuery = scope;
      term.Type = WhereTermType.InSubQuery;
    } else if (<any>scope instanceof Array) {
      term.Values = <Array<OmConstant>>scope;
      term.Type = WhereTermType.In;
    } else {
      term.SubQuery = scope;
      term.Type = WhereTermType.InSubQuery;
    }
    return term;
  }

  static CreateNotIn(expr: OmExpression, scope: string | SelectQuery | Array<OmConstant>): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr;
    if (typeof scope == "string") {
      term.SubQuery = scope;
      term.Type = WhereTermType.NotInSubQuery;
    } else if (<any>scope instanceof Array) {
      term.Values = <Array<OmConstant>>scope;
      term.Type = WhereTermType.NotIn;
    } else {
      term.SubQuery = scope;
      term.Type = WhereTermType.NotInSubQuery;
    }

    return term;
  }

  static CreateIsNull(expr: OmExpression): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr;
    term.Type = WhereTermType.IsNull;
    return term;
  }

  static CreateIsNotNull(expr: OmExpression): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr;
    term.Type = WhereTermType.IsNotNull;
    return term;
  }

  static CreateExists(sql: string | SelectQuery): WhereTerm {
    var term = new WhereTerm();
    term.SubQuery = sql;
    term.Type = WhereTermType.Exists;
    return term;
  }

  static CreateNotExists(sql: string | SelectQuery): WhereTerm {
    var term = new WhereTerm();
    term.SubQuery = sql;
    term.Type = WhereTermType.NotExists;
    return term;
  }

  static CreateBetween(expr: OmExpression, lowBound: OmExpression, highBound: OmExpression): WhereTerm {
    var term = new WhereTerm();
    term.Expr1 = expr;
    term.Expr2 = lowBound;
    term.Expr3 = highBound;
    term.Type = WhereTermType.Between;
    return term;
  }

}
