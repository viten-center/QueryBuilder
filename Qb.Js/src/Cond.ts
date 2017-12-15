import { CompOper, WhereTermType } from "./Enums";
import { WhereTerm } from "./WhereTerm";
import { Expr } from "./Expr";
import { Utils } from "./Utils";
import { From } from "./From";
import { Constant } from "./Constant";
import { Select } from "./Select";
import { OmConstant } from "./OmConstant";

class EqualArgs {
  expr1: Expr;
  expr2: Expr;
  field: string;
  alias: From;
  v1: number;
  v2: string;
  v3: Date;
}

class LikeArgs {
  expr1: Expr;
  expr2: Expr;
  field: string;
  v2: string;
  alias: From;
  escapeChar: string;
}

class LessOrEqualArg {
  expr1: Expr;
  expr2: Expr;
  field: string;
  alias: From;
  v1: number;
  v2: string;
  v3: Date;
}

class InArg {
  expr: Expr;
  field: string;
  values: Array<Constant>;
  alias: From;
  subQuery: Select;
}

class IsNullArg {
  expr: Expr;
  field: string;
  alias: From;
}

class BetweenArg {
  expr: Expr;
  lowBound: Expr;
  highBound: Expr;
  alias: From;
  field: string;
}


export class Cond {
  private Term: WhereTerm;

  private static Compare(expr1: Expr, expr2: Expr, op: CompOper): Cond {
    var res = new Cond();
    res.Term = WhereTerm.CreateCompare(expr1["Expression"], expr2["Expression"], op);
    return res;
  }

  private static CreateLikeCompare(expr1: Expr, expr2: Expr, escapeChar: string): Cond {
    var res = new Cond();
    res.Term = WhereTerm.CreateLikeCompare(expr1["Expression"], expr2["Expression"], escapeChar);
    return res;
  }

  private static CreateNotLikeCompare(expr1: Expr, expr2: Expr, escapeChar: string): Cond {
    var res = new Cond();
    res.Term = WhereTerm.CreateNotLikeCompare(expr1["Expression"], expr2["Expression"], escapeChar);
    return res;
  }

  // EQUAL NOTEQUAL
  private static ParseEqualArg(arg: IArguments): EqualArgs {
    var expr1: Expr | undefined;
    var expr2: Expr | undefined;
    var field: string | undefined;
    var v2: string | undefined;
    var alias: From | undefined;
    var v1: number | undefined;
    var v3: Date | undefined;

    if (arg.length >= 1) {
      if (typeof arg[0] == "string") {
        field = arg[0]
      } else {
        expr1 = arg[0];
      }
    }

    if (arg.length >= 2) {
      if (typeof arg[1] == "number") {
        v1 = arg[1];
      } else if (typeof arg[1] == "string") {
        v2 = arg[1];
      } else if (arg[1] instanceof Date) {
        v3 = arg[1];
      } else if (arg[1] instanceof From) {
        alias = arg[1];
      } else {
        expr2 = arg[1];
      }
    }

    if (arg.length >= 3) {
      if (typeof arg[2] == "number") {
        v1 = arg[2];
      } else if (typeof arg[2] == "string") {
        v2 = arg[2];
      } else if (arg[2] instanceof Date) {
        v3 = arg[2];
      } else {
        expr2 = arg[2];
      }
    }

    var res = new EqualArgs();
    if (alias !== undefined)
      res.alias = alias;
    if (expr1 !== undefined)
      res.expr1 = expr1;
    if (expr2 !== undefined)
      res.expr2 = expr2;
    if (field !== undefined)
      res.field = field;
    if (v1 !== undefined)
      res.v1 = v1;
    if (v2 !== undefined)
      res.v2 = v2;
    if (v3 != undefined)
      res.v3 = v3;
    return res;
  }

  static Equal(expr1: Expr, expr2: Expr): Cond;
  static Equal(field: string, expr: Expr): Cond;
  static Equal(field: string, alias: From, expr: Expr): Cond;
  static Equal(field: string, value: number): Cond;
  static Equal(field: string, alias: From, value: number): Cond;
  static Equal(field: string, value: string): Cond;
  static Equal(field: string, alias: From, value: string): Cond;
  static Equal(field: string, value: Date): Cond;
  static Equal(field: string, alias: From, value: Date): Cond;
  static Equal(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.Equal);
  }

  static NotEqual(expr1: Expr, expr2: Expr): Cond;
  static NotEqual(field: string, expr: Expr): Cond;
  static NotEqual(field: string, alias: From, expr: Expr): Cond;
  static NotEqual(field: string, value: number): Cond;
  static NotEqual(field: string, alias: From, value: number): Cond;
  static NotEqual(field: string, value: string): Cond;
  static NotEqual(field: string, alias: From, value: string): Cond;
  static NotEqual(field: string, value: Date): Cond;
  static NotEqual(field: string, alias: From, value: Date): Cond;
  static NotEqual(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.NotEqual);
  }

  private static EqualOrNotEqual(arg: EqualArgs, op: CompOper): Cond {
    if (!Utils.IsEmpty(arg.expr1) && !Utils.IsEmpty(arg.expr2))
      return Cond.Compare(arg.expr1, arg.expr2, op);

    if (!Utils.IsEmpty(arg.v1))
      return Cond.Compare(Expr.Field(arg.field, arg.alias), Expr.Number(arg.v1), op);

    if (!Utils.IsEmpty(arg.v2))
      return Cond.Compare(Expr.Field(arg.field, arg.alias), Expr.String(arg.v2), op);

    if (!Utils.IsEmpty(arg.v3))
      return Cond.Compare(Expr.Field(arg.field, arg.alias), Expr.Date(arg.v3), op);

    return Cond.Compare(Expr.Field(arg.field, arg.alias), arg.expr2, op);
  }

  // LIKE NOTLIKE

  private static ParseLikeArg(arg: IArguments): LikeArgs {
    var res = new LikeArgs();
    if (arg.length >= 1) {
      if (typeof arg[0] == "string")
        res.field = arg[0];
      else
        res.expr1 = arg[0];
    }
    if (arg.length >= 2) {
      if (typeof arg[1] == "string") {
        res.v2 = arg[1];
      } else if (arg[1] instanceof Expr) {
        res.expr2 = arg[1];
      } else {
        res.alias = arg[1];
      }
    }
    if (arg.length >= 3) {
      if (typeof arg[2] == "string") {
        if (!Utils.IsEmpty(res.expr2))
          res.escapeChar = arg[2];
        else
          res.v2 = arg[2];
      } else {
        res.expr2 = arg[2];
      }
    }
    return res;
  }

  static Like(expr1: Expr, expr2: Expr): Cond;
  static Like(expr1: Expr, expr2: Expr, escapeChar: string): Cond;
  static Like(field: string, expr2: Expr): Cond;
  static Like(field: string, alias: From, expr2: Expr): Cond;
  static Like(field: string, value: string): Cond;
  static Like(field: string, alias: From, value: string): Cond;
  static Like(): Cond {
    var arg = Cond.ParseLikeArg(arguments);
    return Cond.LikeOrNotLike(arg, CompOper.Like);
  }

  static NotLike(expr1: Expr, expr2: Expr): Cond;
  static NotLike(expr1: Expr, expr2: Expr, escapeChar: string): Cond;
  static NotLike(field: string, expr2: Expr): Cond;
  static NotLike(field: string, alias: From, expr2: Expr): Cond;
  static NotLike(field: string, value: string): Cond;
  static NotLike(field: string, alias: From, value: string): Cond;
  static NotLike(): Cond {
    var arg = Cond.ParseLikeArg(arguments);
    return Cond.LikeOrNotLike(arg, CompOper.NotLike);
  }

  private static LikeOrNotLike(arg: LikeArgs, op: CompOper): Cond {
    if (!Utils.IsEmpty(arg.expr1)) {
      if (op == CompOper.Like)
        return Cond.CreateLikeCompare(arg.expr1, arg.expr2, arg.escapeChar);
      else
        return Cond.CreateNotLikeCompare(arg.expr1, arg.expr2, arg.escapeChar);
    }

    if (!Utils.IsEmpty(arg.expr2))
      return Cond.Compare(Expr.Field(arg.field, arg.alias), arg.expr2, op);

    return Cond.Compare(Expr.Field(arg.field, arg.alias), Expr.String(arg.v2), op);
  }

  static LessOrEqual(expr1: Expr, expr2: Expr): Cond;
  static LessOrEqual(field: string, alias: From, expr2: Expr): Cond;
  static LessOrEqual(field: string, expr2: Expr): Cond;
  static LessOrEqual(field: string, value: number): Cond;
  static LessOrEqual(field: string, alias: From, value: number): Cond;
  static LessOrEqual(field: string, value: string): Cond;
  static LessOrEqual(field: string, alias: From, value: string): Cond;
  static LessOrEqual(field: string, value: Date): Cond;
  static LessOrEqual(field: string, alias: From, value: Date): Cond;
  static LessOrEqual(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.LessOrEqual);
  }

  static Less(expr1: Expr, expr2: Expr): Cond;
  static Less(expr1: Expr, alias: From, expr2: Expr): Cond;
  static Less(field: string, expr2: Expr): Cond;
  static Less(field: string, value: number): Cond;
  static Less(field: string, alias: From, value: number): Cond;
  static Less(field: string, value: string): Cond;
  static Less(field: string, alias: From, value: string): Cond;
  static Less(field: string, value: Date): Cond;
  static Less(field: string, alias: From, value: Date): Cond;
  static Less(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.Less);
  }

  static GreaterOrEqual(expr1: Expr, expr2: Expr): Cond;
  static GreaterOrEqual(expr1: Expr, alias: From, expr2: Expr): Cond;
  static GreaterOrEqual(field: string, expr2: Expr): Cond;
  static GreaterOrEqual(field: string, value: number): Cond;
  static GreaterOrEqual(field: string, alias: From, value: number): Cond;
  static GreaterOrEqual(field: string, value: string): Cond;
  static GreaterOrEqual(field: string, alias: From, value: string): Cond;
  static GreaterOrEqual(field: string, value: Date): Cond;
  static GreaterOrEqual(field: string, alias: From, value: Date): Cond;
  static GreaterOrEqual(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.GreaterOrEqual);

  }

  static Greater(expr1: Expr, expr2: Expr): Cond;
  static Greater(expr1: Expr, alias: From, expr2: Expr): Cond;
  static Greater(field: string, expr2: Expr): Cond;
  static Greater(field: string, value: number): Cond;
  static Greater(field: string, alias: From, value: number): Cond;
  static Greater(field: string, value: string): Cond;
  static Greater(field: string, alias: From, value: string): Cond;
  static Greater(field: string, value: Date): Cond;
  static Greater(field: string, alias: From, value: Date): Cond;
  static Greater(): Cond {
    var arg = Cond.ParseEqualArg(arguments);
    return Cond.EqualOrNotEqual(arg, CompOper.Greater);
  }

  // IN
  private static ParseInArg(arg: IArguments): InArg {
    var res = new InArg();
    if (arg.length >= 1) {
      if (typeof arg[0] == "string") {
        res.field = arg[0];
      } else {
        res.expr = arg[0];
      }
    }
    if (arg.length >= 2) {
      if (arg[1] instanceof Array && arg[1].length > 0) {
        let c: Constant[] = new Array<Constant>();
        if (typeof arg[1][0] == "string") {
          for (let i = 0; i < arg[1].length; i++)
            c.push(Constant.String(arg[1][i]))
        } else if (typeof arg[1][0] == "number") {
          for (let i = 0; i < arg[1].length; i++)
            c.push(Constant.Number(arg[1][i]))
        } else if (arg[1][0] instanceof Date) {
          for (let i = 0; i < arg[1].length; i++)
            c.push(Constant.Date(arg[1][i]))
        }
        res.values = c;
      } else if (arg[1] instanceof From) {
        res.alias = arg[1];
      } else {
        res.subQuery = arg[1];
      }
    }
    if (arg.length >= 3) {
      if (arg[2] instanceof Select) {
        res.subQuery = arg[2];
      } else {
        res.values = arg[2];
      }
    }
    return res;
  }

  static In(expr: Expr, values: number[]): Cond;
  static In(expr: Expr, values: string[]): Cond;
  static In(expr: Expr, values: Date[]): Cond;
  static In(field: string, values: number[]): Cond;
  static In(field: string, values: string[]): Cond;
  static In(field: string, values: Date[]): Cond;
  static In(expr: Expr, subQuery: Select): Cond;
  static In(field: string, subQuery: Select): Cond;
  static In(): Cond {
    var arg = Cond.ParseInArg(arguments);
    return Cond.InOrNotIn(arg, WhereTermType.In);
  }

  static NotIn(expr: Expr, values: number[]): Cond;
  static NotIn(expr: Expr, values: string[]): Cond;
  static NotIn(expr: Expr, values: Date[]): Cond;
  static NotIn(field: string, values: number[]): Cond;
  static NotIn(field: string, values: string[]): Cond;
  static NotIn(field: string, values: Date[]): Cond;
  static NotIn(expr: Expr, subQuery: Select): Cond;
  static NotIn(field: string, subQuery: Select): Cond;
  static NotIn(): Cond {
    var arg = Cond.ParseInArg(arguments);
    return Cond.InOrNotIn(arg, WhereTermType.NotIn);
  }

  private static InOrNotIn(arg: InArg, op: WhereTermType): Cond {
    var e = arg.expr;
    if (Utils.IsEmpty(e)) {
      e = Expr.Field(arg.field, arg.alias);
    }
    var res = new Cond();
    if (Utils.IsEmpty(arg.subQuery)) {
      var constants = new Array<OmConstant>();
      for (var i = 0; i < arg.values.length; i++) {
        constants.push(arg.values[i]["Const"]);
      }
      if (op == WhereTermType.In) {
        res.Term = WhereTerm.CreateIn(e["Expression"], constants);
      } else {
        res.Term = WhereTerm.CreateNotIn(e["Expression"], constants);
      }
    } else {
      if (op == WhereTermType.In) {
        res.Term = WhereTerm.CreateIn(e["Expression"], arg.subQuery["Query"]);
      } else {
        res.Term = WhereTerm.CreateNotIn(e["Expression"], arg.subQuery["Query"]);
      }
    }
    return res;
  }

  // ISNULL

  private static ParseIsNullArg(arg: IArguments): IsNullArg {
    var res = new IsNullArg();
    if (arg.length >= 1) {
      if (typeof arg[0] == "string") {
        res.field = arg[0];
      } else {
        res.expr = arg[0];
      }
    }
    if (arg.length >= 2) {
      res.alias = arg[1];
    }
    return res;
  }
  static IsNull(expr: Expr): Cond;
  static IsNull(field: string): Cond;
  static IsNull(field: string, alias: From): Cond;
  static IsNull(): Cond {
    var arg = Cond.ParseIsNullArg(arguments);
    return Cond.IsNullOrIsNotNull(arg, WhereTermType.IsNull);
  }

  static IsNotNull(expr: Expr): Cond;
  static IsNotNull(field: string): Cond;
  static IsNotNull(field: string, alias: From): Cond;
  static IsNotNull(): Cond {
    var arg = Cond.ParseIsNullArg(arguments);
    return Cond.IsNullOrIsNotNull(arg, WhereTermType.IsNotNull);
  }

  private static IsNullOrIsNotNull(arg: IsNullArg, op: WhereTermType): Cond {
    var res = new Cond();
    var e = arg.expr;
    if (Utils.IsEmpty(e)) {
      e = Expr.Field(arg.field, arg.alias);
    }
    if (op == WhereTermType.IsNull) {
      res.Term = WhereTerm.CreateIsNull(e["Expression"]);
    } else {
      res.Term = WhereTerm.CreateIsNotNull(e["Expression"]);
    }
    return res;
  }

  // BETWEEN

  private static ParseBetweenArg(arg: IArguments): BetweenArg {
    var res = new BetweenArg();

    if (arg.length >= 1) {
      if (typeof arg[0] == "string") {
        res.field = arg[0];
      } else {
        res.expr = arg[0];
      }
    }
    if (arg.length >= 2) {
      if (typeof arg[1] == "number") {
        res.lowBound = Expr.Number(arg[1]);
      } else if (arg[1] instanceof Date) {
        res.lowBound = Expr.Date(arg[1]);
      } else if (arg[1] instanceof Expr) {
        res.lowBound = arg[1];
      } else {
        res.alias = arg[1];
      }
    }
    if (arg.length >= 3) {
      if (arg[2] instanceof Expr) {
        if (Utils.IsEmpty(res.alias)) {
          res.highBound = arg[2];
        } else {
          res.lowBound = arg[2];
        }
      } else if (typeof arg[2] == "number") {
        if (Utils.IsEmpty(res.alias)) {
          res.highBound = Expr.Number(arg[2]);
        } else {
          res.lowBound = Expr.Number(arg[2]);
        }
      } else {
        if (Utils.IsEmpty(res.alias)) {
          res.highBound = Expr.Date(arg[2]);
        } else {
          res.lowBound = Expr.Date(arg[2]);
        }
      }
    }
    if (arg.length >= 4) {
      if (typeof arg[3] == "number") {
        res.highBound = Expr.Number(arg[3]);
      } else if (arg[3] instanceof Date) {
        res.highBound = Expr.Date(arg[3]);
      } else {
        res.highBound = arg[3];
      }
    }
    return res;
  }

  static Between(expr: Expr, lowBound: Expr, highBound: Expr): Cond;
  static Between(field: string, lowBound: Expr, highBound: Expr): Cond;
  static Between(field: string, alias: From, lowBound: Expr, highBound: Expr): Cond;
  static Between(field: string, alias: From, lowBound: number, highBound: number): Cond;
  static Between(field: string, lowBound: number, highBound: number): Cond;
  static Between(field: string, alias: From, lowBound: Date, highBound: Date): Cond;
  static Between(field: string, lowBound: Date, highBound: Date): Cond;
  static Between(): Cond {
    var arg = Cond.ParseBetweenArg(arguments);
    var res = new Cond();
    if (!Utils.IsEmpty(arg.expr)) {
      res.Term = WhereTerm.CreateBetween(arg.expr["Expression"], arg.lowBound["Expression"], arg.highBound["Expression"]);
    } else {
      res.Term = WhereTerm.CreateBetween(Expr.Field(arg.field, arg.alias)["Expression"], arg.lowBound["Expression"], arg.highBound["Expression"]);
    }
    return res;
  }

  static Exists(subQuery: Select): Cond {
    var res = new Cond();
    res.Term = WhereTerm.CreateExists(subQuery["Query"]);
    return res;
  }

  static NotExists(subQuery: Select): Cond {
    var res = new Cond();
    res.Term = WhereTerm.CreateNotExists(subQuery["Query"]);
    return res;
  }
}

