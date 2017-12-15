import { OmExpression } from "./OmExpression"
import { AggFunc } from "./Enums"
import { FromTerm } from "./FromTerm"
import { Expr } from "./Expr";

export class SelectColumn {
  ColumnAlias: string;
  Expression: OmExpression;

  constructor(columnName: string|Expr, columnAlias: string | undefined, table: FromTerm | undefined, func: AggFunc | undefined) {
    if (func === undefined)
      func = AggFunc.None;

    if(columnName instanceof Expr){
      this.Expression = columnName["Expression"];
    }
    else{
      if (func === AggFunc.None)
        this.Expression = OmExpression.Field(columnName, table);
      else
        this.Expression = OmExpression.Func(func, OmExpression.Field(columnName, table));
    }
    if (columnAlias)
      this.ColumnAlias = columnAlias;
  }

}


