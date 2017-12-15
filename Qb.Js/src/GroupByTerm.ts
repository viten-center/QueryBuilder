import {FromTerm} from "./FromTerm"

// interface IGroupByTerm {
//     Field: string;
//     Table: FromTerm;
//     TableAlias: string;
//   }

  export class GroupByTerm /*implements IGroupByTerm*/ {
    Field: string;
    Table: FromTerm;

    constructor(field: string, table: FromTerm|undefined ) {
      this.Field = field;
      if(table)
        this.Table = table;
    }

    get TableAlias(): string|undefined {
      return (this.Table instanceof FromTerm) ? this.Table.RefName : undefined;
    }

  }
