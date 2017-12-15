import { OrderByDir } from "./Enums"
import { FromTerm } from "./FromTerm"

// interface IOrderByTerm {
//     Direction: OrderByDirection;
//     Field: string;
//     TableAlias: string;
//     Table: FromTerm;
//   }

export class OrderByTerm /*implements IOrderByTerm*/ {
  Field: string;
  Table: FromTerm|undefined;
  Direction: OrderByDir;

  constructor(field: string, dir: OrderByDir, table: FromTerm|undefined) {
    this.Field = field;
    this.Table = table;
    this.Direction = dir;
  }

  get TableAlias(): string | undefined {
    return (this.Table) ? this.Table.RefName : undefined;
  }
}
