import { WhereClause } from "./WhereClause";
import { Cond } from "./Cond";
import { WhereRel } from "./Enums";
import { Utils } from "./Utils";



export class Logic {
  Clause: WhereClause;

  static And(conds: Cond[]): Logic;
  static And(logics: Logic[]): Logic;
  static And(...logics: Logic[]): Logic;
  static And(...opers: Cond[]): Logic;
  static And(): Logic {
    if (arguments.length == 0)
      return Logic.AndOrLogic(WhereRel.And, new Array<Logic>());

    let arr;
    if (arguments[0] instanceof Array)
      arr = arguments[0];
    else
      arr = arguments;

      if (arr.length > 0) {
        let isCond = arr[0] instanceof Cond;
        if (isCond) {
          let c = new Array<Cond>();
          for (let i = 0; i < arr.length; i++) {
            c.push(arr[i]);
          }
          return Logic.AndOrOper(WhereRel.And, c);
        } else {
          let l = new Array<Logic>();
          for (let i = 0; i < arr.length; i++) {
            l.push(arr[i]);
          }
          return Logic.AndOrLogic(WhereRel.And, l);
        }
      }
  }

  static Or(...logics: Logic[]): Logic;
  static Or(...opers: Cond[]): Logic;
  static Or(): Logic {
    if (arguments.length >= 1) {
      if (arguments[0] instanceof Logic) {
        var logics = new Array<Logic>();
        for (var i = 0; i < arguments.length; i++) {
          logics.push(arguments[i]);
        }
        return Logic.AndOrLogic(WhereRel.Or, logics);
      }
    }
    var opers = new Array<Cond>();
    for (var i = 0; i < arguments.length; i++) {
      opers.push(arguments[i]);
    }
    return Logic.AndOrOper(WhereRel.Or, opers);
  }

  private static AndOrOper(whereRel: WhereRel, opers: Cond[]): Logic {
    var res = new Logic();
    res.Clause = new WhereClause(Utils.ConvertWhereRel(whereRel));
    for (var i = 0; i < opers.length; i++) {
      res.Clause.Terms.push(opers[i]["Term"]);
    }
    return res;
  }

  private static AndOrLogic(whereRel: WhereRel, logics: Logic[]): Logic {
    var res = new Logic();
    res.Clause = new WhereClause(Utils.ConvertWhereRel(whereRel));
    for (var i = 0; i < logics.length; i++) {
      res.Clause.SubClauses.push(logics[i].Clause);
    }
    return res;
  }

}