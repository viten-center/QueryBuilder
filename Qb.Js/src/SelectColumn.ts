import {OmExpression} from "./OmExpression"
import {OmAggregationFunction} from "./Enums"
import {FromTerm} from "./FromTerm"

interface ISelectColumn {
    ColumnAlias: string;
    Expression: OmExpression;
  }

  export class SelectColumn implements ISelectColumn {
    ColumnAlias: string;
    Expression: OmExpression;

    constructor(columnName: string, columnAlias: string, table: FromTerm, func: OmAggregationFunction) {
      if (func === undefined || func === null)
        func = OmAggregationFunction.None;

      if (func == OmAggregationFunction.None)
        this.Expression = OmExpression.Field(columnName, table);
      else
        this.Expression = OmExpression.Func(func, OmExpression.Field(columnName, table));
      this.ColumnAlias = columnAlias;
    }
  }


