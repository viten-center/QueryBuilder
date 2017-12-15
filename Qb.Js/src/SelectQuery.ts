import {WhereClause} from "./WhereClause";
import {SelectColumn} from "./SelectColumn";
import {OrderByTerm} from "./OrderByTerm";
import {GroupByTerm} from "./GroupByTerm";
import {FromClause} from "./FromClause";
import {Param} from "./Param";
import {Utils} from "./Utils";
import { WhereRel } from "./Enums";


// interface ISelectQuery {
//   FromClause: FromClause;
//   CommandParams: Array<Param>;
//   GroupByTerms: Array<GroupByTerm>;
//   OrderByTerms: Array<OrderByTerm>;
//   WherePhrase: WhereClause;
//   HavingPhrase: WhereClause;
//   Columns: Array<SelectColumn>;
//   Top: number;
//   GroupByWithRollup: boolean;
//   GroupByWithCube: boolean;
//   Distinct: boolean;
//   TableSpace: string;
// }

export class SelectQuery /*implements ISelectQuery*/ {
  Columns = new Array<SelectColumn>();
  WherePhrase: WhereClause = new WhereClause(WhereRel.And);
  HavingPhrase: WhereClause = new WhereClause(WhereRel.And);
  
  FromClause = new FromClause();
  OrderByTerms = new Array<OrderByTerm>();
  GroupByTerms = new Array<GroupByTerm>();
  CommandParams = new Array<Param>();
  Top: number = -1;
  PageNum: number = -1;
  PageSize: number = -1;
  GroupByWithRollup: boolean;
  GroupByWithCube: boolean;
  Distinct: boolean;
  TableSpace: string;

  Validate() {
    if (this.Columns.length == 0) {
      throw new Error("A select query must have at least one column");
    }
    if (Utils.IsEmpty(this.FromClause.BaseTable == null)) {
      throw new Error("A select query must have FromPhrase.BaseTable set");
    }
  }
}

  