import { FromTerm } from "./FromTerm";
import { Select } from "./Select";
import { Union } from "./Union";
import { Qb } from "./Qb";
import { InvalidQueryError } from "./InvalidQueryError";


export class From {
  public Term: FromTerm;

  static Table(tableName: string): From;
  static Table(tableName: string, alias: string): From;
  static Table(tableName: string, alias: string, ns: string): From;
  static Table(tableName: string, alias: string, ns1: string, ns2: string): From;
  static Table(): From {
    var tableName: string|undefined;
    var alias: string|undefined;
    var ns1: string|undefined;
    var ns2: string|undefined;

    if (arguments.length >= 1) {
      tableName = arguments[0];
    }
    if (arguments.length >= 2) {
      alias = arguments[1];
    }
    if (arguments.length >= 3) {
      ns1 = arguments[2];
    }
    if (arguments.length >= 4) {
      ns2 = arguments[3];
    }
    var res = new From();
    if(tableName === undefined)
      throw new InvalidQueryError("Table name not specified");
    res.Term = FromTerm.Table(tableName, alias, ns1, ns2);
    return res;
  }

  static SubQuery(subQuery: Select, alias: string): From {
    var res = new From();
    res.Term = FromTerm.SubQuery((<Select>subQuery)["Query"], alias);
    return res;
  }

  static Union(union: Union, alias: string): From {
    var res = new From();
    res.Term = FromTerm.Union(Qb.GetQueryObject(union), alias);
    return res;
  }
}