import { OmExpression } from "./OmExpression";
import { From } from "./From";
import { Utils } from "./Utils";
import { Constant } from "./Constant";
import { OmConstant } from "./OmConstant";
import { DataType, AggFunc } from "./Enums";
import { Select } from "./Select";
import { Qb } from "./Qb";



export class Expr {
  private Expression: OmExpression;

  static Raw(sqlText: string): Expr {
    var ex = new Expr();
    ex.Expression = OmExpression.Raw(sqlText);
    return ex;
  }

  static Field(fieldName: string): Expr;
  static Field(fieldName: string, table: From): Expr;
  static Field(): Expr {
    var fieldName: string = "*";
    var table: From | undefined;
    if (arguments.length >= 1) {
      fieldName = arguments[0];
    }
    if (arguments.length >= 2) {
      table = arguments[1];
    }
    var res = new Expr();
    res.Expression = OmExpression.Field(fieldName, table instanceof From ? table["Term"] : undefined);
    //res.Expression = OmExpression.Field(fieldName, Utils.IsEmpty(table) ? null : table["Term"]);
    return res;
  }

  // static Constant(val: string|number|Date|Constant): Expr {
  //   var res = new Expr();
  //   switch (typeof val) {
  //     case "string":
  //       res.Expression = OmExpression.String(<string>val);
  //       break;
  //     case "number":
  //       res.Expression = OmExpression.Number(<number>val);
  //       break;
  //     default:
  //       if (val instanceof Date) {
  //         res.Expression = OmExpression.Date(<Date>val);
  //       } else {
  //         var c: OmConstant = val["Const"];
  //         res.Expression = OmExpression.Constant(c, c.Type);
  //       }
  //       break;
  //   }
  //   return res;
  // }

  static Date(val: Date): Expr {
    var res = new Expr();
    res.Expression = OmExpression.Date(val);
    return res;
  }

  static Null(): Expr {
    var res = new Expr();
    res.Expression = OmExpression.Null();
    return res;
  }

  static Number(val: number): Expr {
    var res = new Expr();
    res.Expression = OmExpression.Number(val);
    return res;
  }

  static String(val: string): Expr {
    var res = new Expr();
    res.Expression = OmExpression.String(val);
    return res;
  }

  static IfNull(test: Expr, val: Expr): Expr;
  static IfNull(test: Expr, val: string): Expr;
  static IfNull(test: Expr, val: number): Expr;
  static IfNull(test: Expr, val: Date): Expr;
  static IfNull(): Expr {
    var test, val: Expr;
    test = arguments[0] as Expr;
    if (typeof arguments[1] === "string") {
      val = Expr.String(arguments[1]);
    } else if (typeof arguments[1] === "number") {
      val = Expr.Number(arguments[1]);
    } else if (arguments[1] instanceof Date) {
      val = Expr.Date(arguments[1]);
    } else {
      val = arguments[1];
    }
    var res = new Expr();
    res.Expression = OmExpression.IfNull(test.Expression, val.Expression);
    return res;
  }

  static Func(func: AggFunc, param: Expr): Expr {
    var res = new Expr();
    res.Expression = OmExpression.Func(func, param.Expression);
    return res;
  }

  /// <summary>Определение NULL</summary>

  static Param(paramName: string): Expr {
    var res = new Expr();
    res.Expression = OmExpression.Parameter(paramName);
    return res;
  }


  static SubQuery(subQuery: Select): Expr {
    var res = new Expr();
    res.Expression = OmExpression.SubQuery(Qb.GetQueryObject(subQuery));
    return res;
  }
}