import { SelectQuery } from "./SelectQuery";
import { Column } from "./Column";
import { From } from "./From";
import { InvalidQueryError } from "./InvalidQueryError";
import { FromTerm } from "./FromTerm";
import { Utils } from "./Utils";
import { JoinCond } from "./JoinCond";
import { JoinType, OrderByDir } from "./Enums";
import { Logic } from "./Logic";
import { Join } from "./Join";
import { OrderByTerm } from "./OrderByTerm";
import { GroupByTerm } from "./GroupByTerm";
import { Param } from "./Param";
import { Cond } from "./Cond";
import { Union } from "./Union";

/*
interface ISelect {
  Top(top: number): Select;
  Page(pageNum: number, pageSize: number): Select;
  Distinct(): Select;
  From(tableName: string): Select;
  From(tableName: string, alias: string): Select;
  From(table: From): Select;
  Join(rightTableName: string, joinCond: JoinCond): Select;
  Join(joinType: JoinType, leftTableName: From, rightTableName: From, where: Logic): Select;
  Where(where: Logic): Select;
  Having(where: Logic): Select;
  OrderBy(field: string, table: From, dir: OrderByDir): Select;
  OrderBy(field: string, dir: OrderByDir): Select;
  OrderBy(field: string): Select;
  GroupBy(field: string, table: From): Select;
  GroupBy(field: string): Select;
  Params(...parameters: Param[]): Select;
}
*/
export class Select /*implements ISelect*/ {
  private Query: SelectQuery;

  constructor(columns: Column[]) {
    this.Query = new SelectQuery();
    for (var i = 0; i < columns.length; i++)
      this.Query.Columns.push(columns[i]["Col"]);
  }

  Page(pageNum: number, pageSize: number): Select {
    this.Query.Offset = pageNum * pageSize;
    this.Query.Limit = pageSize;
    return this;
  }

  Distinct(): Select {
    this.Query.Distinct = true;
    return this;
  }

  From(tableName: string): Select;
  From(tableName: string, alias: string): Select;
  From(union: Union, alias: string): Select
  From(table: From): Select;
  From(): Select {
    if (this.Query.FromClause.BaseTable !== undefined)
      throw new InvalidQueryError("BaseTable already defined");
    var tableName: string | undefined;
    var alias: string | undefined;
    var table: From | undefined;
    var union: Union | undefined;


    if (arguments.length >= 1) {
      if (typeof arguments[0] == "string") {
        tableName = arguments[0];
      }
      else if (arguments[0] instanceof From) {
        table = arguments[0];
      }
      else if (arguments[0] instanceof Union) {
        union = arguments[0];
      }
    }
    if (arguments.length >= 2)
      alias = arguments[1];

    if (union && alias)
      table = From.Union(union, alias);

    if (table instanceof From)
      this.Query.FromClause.BaseTable = table.Term;
    else {
      if (tableName)
        this.Query.FromClause.BaseTable = FromTerm.Table(tableName, alias);
      else
        throw new InvalidQueryError("Table name not specified")
    }
    return this;
  }

  Join(rightTableName: string, joinCond: JoinCond): Select;
  Join(joinType: JoinType, leftTable: From, rightTable: From, joinCond: JoinCond): Select;
  Join(joinType: JoinType, leftTable: From, rightTable: From, where: Logic): Select;
  Join(): Select {
    var rightTableName: string | undefined;
    var joinCond: JoinCond | undefined;
    var joinType: JoinType = JoinType.Inner;
    var leftTable: From | undefined;
    var rightTable: From | undefined;
    var where: Logic | undefined;

    if (arguments.length >= 1) {
      if (typeof arguments[0] == "string") {
        rightTableName = arguments[0];
      } else {
        joinType = arguments[0];
      }
    }
    if (arguments.length >= 2) {
      if (arguments[1] instanceof JoinCond) {
        joinCond = arguments[1];
      } else {
        leftTable = arguments[1];
      }
    }
    if (arguments.length >= 3 && arguments[2] instanceof From) {
      rightTable = arguments[2];
    }
    if (arguments.length >= 4) {
      if (arguments[3] instanceof Logic) {
        where = arguments[3];
      } else if (arguments[3] instanceof JoinCond){
        joinCond = arguments[3];
      }
    }

    if (rightTableName && joinCond) {
      this.Query.FromClause.Join(joinType, this.Query.FromClause.BaseTable, From.Table(rightTableName).Term, [joinCond["Condition"]]);
    }
    else if(leftTable && rightTable && joinCond){
      this.Query.FromClause.Join(joinType, leftTable.Term, rightTable.Term, [joinCond["Condition"]]);
    } else if (leftTable && rightTable && where) {
      var join = new Join(leftTable.Term, rightTable.Term, where.Clause, joinType);
      this.Query.FromClause.Joins.push(join);
    } else {
      throw new InvalidQueryError("Invalid arguments")
    }
    return this;
  }

  Where(...conds: Cond[]): Select;
  Where(where: Logic): Select;
  Where(): Select {
    let where: Logic;
    if (arguments.length == 1 && arguments[0] instanceof Logic) {
      where = arguments[0];
    } else {
      let c: Cond[] = [];
      for (var i = 0; i < arguments.length; i++) {
        if (arguments[i] instanceof Cond) {
          c.push(arguments[i]);
        }
      }
      where = Logic.And(c);
    }
    this.Query.WherePhrase.SubClauses.push(where.Clause);
    return this;
  }

  Having(...conds: Cond[]): Select;
  Having(where: Logic): Select;
  Having(): Select {
    let where: Logic;
    if (arguments.length == 1 && arguments[0] instanceof Logic) {
      where = arguments[0];
    } else {
      let c: Cond[] = [];
      for (var i = 0; i < arguments.length; i++) {
        if (arguments[i] instanceof Cond) {
          c.push(arguments[i]);
        }
      }
      where = Logic.And(c);
    }
    this.Query.HavingPhrase.SubClauses.push(where.Clause);
    return this;
  }

  OrderBy(field: string, table: From, dir: OrderByDir): Select;
  OrderBy(field: string, dir: OrderByDir): Select;
  OrderBy(field: string): Select;
  OrderBy(): Select {
    var field: string;
    var table: From | undefined;
    var dir: OrderByDir = OrderByDir.Asc;
    if (arguments.length >= 1) {
      field = arguments[0];
    }
    if (arguments.length >= 2) {
      if (arguments[1] instanceof From) {
        table = arguments[1];
      } else {
        dir = arguments[1];
      }
    }
    if (arguments.length >= 3) {
      dir = arguments[2];
    }

    this.Query.OrderByTerms.push(new OrderByTerm(field!, dir, (table instanceof From) ? table.Term : undefined));
    return this;
  }

  GroupBy(field: string, table: From): Select;
  GroupBy(field: string): Select;
  GroupBy(): Select {
    var field: string | undefined;
    var table: From | undefined;
    if (arguments.length >= 1) {
      field = arguments[0];
    }
    if (arguments.length >= 2) {
      table = arguments[1];
    }
    this.Query.GroupByTerms.push(new GroupByTerm(field!, table ? table.Term : undefined));
    return this;
  }

  Params(...parameters: Param[]): Select {
    for (var i = 0; i < parameters.length; i++) {
      this.Query.CommandParams.push(parameters[i]);
    }
    return this;
  }
}