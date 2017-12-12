import {FromTerm} from "./FromTerm"

interface IGroupByTerm {
    Field: string;
    Table: FromTerm;
    TableAlias: string;
  }

  export class GroupByTerm implements IGroupByTerm {
    Field: string;
    Table: FromTerm;

    constructor(field: string, table: FromTerm ) {
      this.Field = field;
      this.Table = table;
    }

    get TableAlias(): string {
      return (this.Table == null) ? null : this.Table.RefName;
    }

  }
