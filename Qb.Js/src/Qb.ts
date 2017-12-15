// import { SelectQuery } from "./SelectQuery";
// import { Select } from "./Select";
// import { Column } from "./Column";
// import { Union } from "./Union";
// import { OmUnion } from "./OmUnion";

import {Select, Union, OmUnion, SelectQuery, Column} from "./index"
import { Cond } from "./Cond";

export class Qb {
  static GetQueryObject(union: Union): OmUnion;
  static GetQueryObject(query: Select): SelectQuery;
  static GetQueryObject(): OmUnion | SelectQuery {
    if (arguments[0] instanceof Select) {
      return arguments[0]["Query"];
    } else if (arguments[0] instanceof Union) {
      return arguments[0]["Uni"];
    } else {
      throw new Error("Invalid argument")
    }
  }

  static Select(...columns: Column[]): Select;
  static Select(...columns: string[]): Select;
  static Select(): Select {
    if (arguments.length == 0)
      throw Error("Не указаны колонки");
    var arr = new Array<Column>();
    for (var i = 0; i < arguments.length; i++) {
      if (typeof arguments[i] == "string") {
        arr.push(Column.New(<string>arguments[i]))
      }
      else
        arr.push(<Column>arguments[i]);
    }
    return new Select(arr);
  }

  static Union(): Union {
    return new Union();
  }
}

// export * from "./Column";
// export * from "./Cond";
// export * from "./Constant";
// //Enums?
// export * from "./Expr";
// export * from "./From";
// export * from "./Join";
// export * from "./JoinCond";
// export * from "./Logic";
// export * from "./Param";
// export * from "./Select";
// export * from "./Union";




