import {SelectColumn} from "./SelectColumn";
import {OmAggregationFunction} from "./Enums";
import {From} from "./From";
import {FromTerm} from "./FromTerm";
import {Utils} from "./Utils";

interface IColumn {

}
export class Column implements IColumn {
  private Col: SelectColumn;

  static New(columnName: string): Column;
  static New(columnName: string, columnAlias: string, func: OmAggregationFunction): Column;
  static New(columnName: string, fromTable: From): Column;
  static New(columnName: string, columnAlias: string, fromTable: From): Column;
  static New(columnName: string, columnAlias: string, fromTable: From, func: OmAggregationFunction): Column;
  static New(): Column {
    var columnName, columnAlias: string;
    var fromTable: From;
    var func: OmAggregationFunction;
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
    var fromTerm: FromTerm = Utils.IsEmpty(fromTable) ? null : <FromTerm>fromTable.Term;
    var retVal = new Column();
    retVal.Col = new SelectColumn(columnName, columnAlias, fromTerm, func);
    return retVal;
  }
}