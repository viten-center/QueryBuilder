import {FromTerm} from "./FromTerm";
import {Join} from "./Join";
import {Utils} from "./Utils";
import {JoinType, WhereClauseRelationship, CompareOperator} from "./Enums";
import {JoinCondition} from "./JoinCondition";
import {WhereClause} from "./WhereClause";
import {WhereTerm} from "./WhereTerm";
import {OmExpression} from "./OmExpression";



interface IFromClause {
  BaseTable: FromTerm;
  Joins: Array<Join>;
  IsEmpty: boolean;
}

export class FromClause implements IFromClause {
  Joins = new Array<Join>();
  BaseTable: FromTerm;


  TermExists(alias: string): boolean {

    if (this.Joins.length == 0 && !Utils.IsEmpty(this.BaseTable))
      return this.BaseTable.RefName == alias;

    for (var i = 0; i < this.Joins.length; i++) {
      if (this.Joins[i].RightTable.RefName == alias)
        return true;
    }
    return false;
  }

  Join(type: JoinType, leftTable: FromTerm, rightTable: FromTerm, leftField: string, rightField: string);
  //Creates an uncoditional join. type Must be JoinType.CrossJoin
  Join(type: JoinType, leftTable: FromTerm, rightTable: FromTerm);
  Join(type: JoinType, leftTable: FromTerm, rightTable: FromTerm, ...conditions: JoinCondition[]);
  Join(type: JoinType, leftTable: FromTerm, rightTable: FromTerm, conditions: WhereClause);
  Join(type: JoinType, leftTable: FromTerm, rightTable: FromTerm);
  Join(type: JoinType) {
    var leftTable, rightTable: FromTerm;
    var leftField, rightField: string
    var conditionsWhere: WhereClause;
    if (arguments.length >= 2) {
      leftTable = arguments[1];
    }
    if (arguments.length >= 3) {
      rightTable = arguments[2];
    }
    if (arguments.length >= 4) {
      if (typeof arguments[3] == "string")
        leftField = arguments[3];
      if (arguments[3] instanceof  Array) {
        var conditions: JoinCondition[];
        conditions = arguments[3];
        conditionsWhere = new WhereClause(WhereClauseRelationship.And);
        for (var i = 0; i < conditions.length; i++) {
          conditionsWhere.Terms.push(WhereTerm.CreateCompare(OmExpression.Field(conditions[i].LeftField, leftTable), OmExpression.Field(conditions[i].RightField, rightTable), CompareOperator.Equal));
        }
      } else {
        conditionsWhere = arguments[3];
      }
    }
    if (arguments.length >= 5) {
      rightField = arguments[3];
    }

    if (typeof rightField == "string") {
      var condition = new JoinCondition(leftField, rightField);
      conditionsWhere.Terms.push(WhereTerm.CreateCompare(OmExpression.Field(condition.LeftField, leftTable), OmExpression.Field(condition.RightField, rightTable), CompareOperator.Equal));
    }

    if (conditionsWhere.IsEmpty && type != JoinType.Cross)
      throw new Error("A join must have at least one condition.");

    this.Joins.push(new Join(leftTable, rightTable, conditionsWhere, type));
  }

  get IsEmpty(): boolean {
    return Utils.IsEmpty(this.BaseTable) && this.Joins.length == 0;
  }

}