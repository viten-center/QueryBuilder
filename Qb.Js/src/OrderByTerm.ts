import {OrderByDirection} from "./Enums"
import {FromTerm} from "./FromTerm"

interface IOrderByTerm {
    Direction: OrderByDirection;
    Field: string;
    TableAlias: string;
    Table: FromTerm;
  }

  export class OrderByTerm implements IOrderByTerm {
		Field: string;
    Table: FromTerm ;
    Direction: OrderByDirection;

    constructor(field: string, dir: OrderByDirection, table: FromTerm) {
      this.Field = field;
      this.Table = table;
      this.Direction = dir;
    }

    get TableAlias(): string {
      return (this.Table == null) ? null : this.Table.RefName;
    }
  }
