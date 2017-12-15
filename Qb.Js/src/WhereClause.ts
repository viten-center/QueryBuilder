import {WhereRel} from "./Enums"
import {WhereTerm} from "./WhereTerm"

// interface IWhereClause {
//     Relationship: WhereClauseRelationship;
//     Terms: Array<WhereTerm>;
//     SubClauses: Array<WhereClause>;
//     IsEmpty: boolean;
// }

  export class WhereClause /*implements IWhereClause*/ {
    Relationship: WhereRel = WhereRel.And;
    public Terms = new Array<WhereTerm>();
    public SubClauses = new Array<WhereClause>();

    constructor(relationship: WhereRel) {
      this.Relationship = relationship;
    }

    // private static _defaultWhere: WhereClause = new WhereClause(WhereRel.And);
    // private static _defaultHaving: WhereClause = new WhereClause(WhereRel.And);
    
    get IsEmpty(): boolean {
      var len = this.SubClauses.length;
      for (var i = 0; i < len; i++) {
        if (!this.SubClauses[i].IsEmpty)
          return false;
      }
      return this.Terms.length == 0;
    }

  }
