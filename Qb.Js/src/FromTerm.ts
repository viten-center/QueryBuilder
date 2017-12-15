import { FromTermType } from "./Enums"
import { SelectQuery } from "./SelectQuery"
import { OmUnion } from "./OmUnion"

// interface IFromTerm {
//   Expression: any;
//   Alias: string;
//   Type: FromTermType;
//   RefName: string;
//   Ns1: string;
//   Ns2: string;
// }

export class FromTerm /*implements IFromTerm*/ {
  Alias: string;
  Type: FromTermType;
  Ns1: string;
  Ns2: string;
  StringValue: string;
  QueryValue: SelectQuery;
  UnionValue: OmUnion;

  get Expression(): any {
    switch (this.Type) {
      case FromTermType.SubQueryObj:
        return this.QueryValue;
      case FromTermType.Union:
        return this.UnionValue;
      default:
        return this.StringValue;
    }
  }

  constructor() {
  }

  static Table(tableName: string, alias: string | undefined): FromTerm;
  static Table(tableName: string, alias: string | undefined, ns1: string | undefined): FromTerm;
  static Table(tableName: string, alias: string | undefined, ns1: string | undefined, ns2: string | undefined): FromTerm;
  static Table(tableName: string): FromTerm;
  static Table(): FromTerm {
    var term = new FromTerm();
    var tableName: string;
    var alias: string | undefined;
    var ns1: string | undefined;
    var ns2: string | undefined;
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
    term.StringValue = tableName!;
    if (alias !== undefined)
      term.Alias = alias;
    if (ns1 !== undefined)
      term.Ns1 = ns1;
    if (ns2 !== undefined)
      term.Ns2 = ns2;
    term.Type = FromTermType.Table;
    return term;
  }

  static SubQuery(query: SelectQuery | string, alias: string): FromTerm {
    var term = new FromTerm();
    if (typeof query == "string") {
      term.StringValue = <string>query;
      term.Type = FromTermType.SubQuery;
    } else {
      term.QueryValue = <SelectQuery>query;
      term.Type = FromTermType.SubQueryObj;
    }
    term.Alias = alias;
    return term;
  }

  static Union(union: OmUnion, alias: string): FromTerm {
    var term = new FromTerm();
    term.UnionValue = union;
    term.Alias = alias;
    term.Type = FromTermType.Union;
    return term;
  }

  get RefName(): string {
    return (this.Alias == null && this.Type == FromTermType.Table) ? this.Expression : this.Alias;
  }
} 
