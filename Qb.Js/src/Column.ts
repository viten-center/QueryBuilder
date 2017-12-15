import { SelectColumn } from "./SelectColumn";
import { AggFunc } from "./Enums";
import { From } from "./From";
import { FromTerm } from "./FromTerm";
import { Utils } from "./Utils";
import { Expr } from "./Expr";

export class Column {
  private Col: SelectColumn;

  static New(columnName: string): Column;
  static New(columnName: string | Expr, columnAlias: string): Column;
  static New(columnName: string, columnAlias: string, func: AggFunc): Column;
  static New(columnName: string, fromTable: From): Column;
  static New(columnName: string, columnAlias: string, fromTable: From): Column;
  static New(columnName: string, columnAlias: string, fromTable: From, func: AggFunc): Column;
  static New(): Column {
    var columnName, columnAlias: string | undefined;
    var fromTable: From | undefined;
    var func: AggFunc | undefined;
    var expt: Expr | undefined;
    if (arguments.length >= 1) {
      columnName = arguments[0];
    }
    if (arguments.length >= 2) {
      if (typeof arguments[1] == "string") {
        columnAlias = arguments[1];
      } else {
        fromTable = arguments[1];
      }
    }
    if (arguments.length >= 3) {
      if (typeof arguments[2] == "number") {
        func = arguments[2];
      } else {
        fromTable = arguments[2];
      }
    }
    if (arguments.length >= 4) {
      func = arguments[3];
    }
    var fromTerm: FromTerm | undefined = fromTable instanceof From ? <FromTerm>fromTable.Term : undefined;
    var retVal = new Column();
    retVal.Col = new SelectColumn(columnName, columnAlias, fromTerm, func);
    return retVal;
  }
}