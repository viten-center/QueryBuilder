import {AggFunc, CompOper, WhereRel, OrderByDir, UnionMod} from "./Enums";

export class Utils {
  static IsEmpty(obj: any): boolean {
    if (obj === undefined || obj === null) return true;
    return false;
  }

  // static ConvertAggregationFunction(agr: AggFunc): OmAggregationFunction {
  //   switch (agr) {
  //     case AggFunc.Avg:
  //       return OmAggregationFunction.Avg;
  //     case AggFunc.Count:
  //       return OmAggregationFunction.Count;
  //     case AggFunc.Grouping:
  //       return OmAggregationFunction.Grouping;
  //     case AggFunc.Max:
  //       return OmAggregationFunction.Max;
  //     case AggFunc.Min:
  //       return OmAggregationFunction.Min;
  //     case AggFunc.Sum:
  //       return OmAggregationFunction.Sum;
  //     default:
  //       return OmAggregationFunction.None;
  //   }
  // }

  // static ConvertCompOper(oper: CompOper): CompareOperator {
  //   switch (oper) {
  //     case CompOper.BitwiseAnd:
  //       return CompareOperator.BitwiseAnd;
  //     case CompOper.Equal:
  //       return CompareOperator.Equal;
  //     case CompOper.Greater:
  //       return CompareOperator.Greater;
  //     case CompOper.GreaterOrEqual:
  //       return CompareOperator.GreaterOrEqual;
  //     case CompOper.Less:
  //       return CompareOperator.Less;
  //     case CompOper.LessOrEqual:
  //       return CompareOperator.LessOrEqual;
  //     case CompOper.Like:
  //       return CompareOperator.Like;
  //     case CompOper.NotLike:
  //       return CompareOperator.NotLike;
  //     default:
  //       return CompareOperator.NotEqual;
  //   }
  // }

  // static ConvertWhereRel(whereRel: WhereRel): WhereClauseRelationship {
  //   switch (whereRel) {
  //     case WhereRel.Or:
  //       return WhereClauseRelationship.Or;
  //     default:
  //       return WhereClauseRelationship.And;
  //   }
  // }

  // static ToDistinctModifier(modifier: UnionMode): DistinctModifier {
  //   if (modifier == UnionMode.All)
  //     return DistinctModifier.All;
  //   return DistinctModifier.Distinct;
  // }

}