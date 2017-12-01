using Viten.QueryBuilder.SqlOm;
using System.Data;
using System;
using System.Linq;

namespace Viten.QueryBuilder
{
  class ExprUtil
  {
    public static OmAggregationFunction ConvertAggregationFunction(AggFunction agr)
    {
      switch (agr)
      {
        case AggFunction.Avg:
          return OmAggregationFunction.Avg;
        case AggFunction.Count:
          return OmAggregationFunction.Count;
        case AggFunction.Grouping:
          return OmAggregationFunction.Grouping;
        case AggFunction.Max:
          return OmAggregationFunction.Max;
        case AggFunction.Min:
          return OmAggregationFunction.Min;
        case AggFunction.Sum:
          return OmAggregationFunction.Sum;
        default:
          return OmAggregationFunction.None;
      }
    }

    public static OmDataType ConvertDataType(DataType dataType)
    {
      switch (dataType)
      {
        case DataType.Date:
          return OmDataType.Date;
        case DataType.Number:
          return OmDataType.Number;
        default:
          return OmDataType.String;
      }
    }

    public static CompareOperator ConvertCompOper(CompCond oper)
    {
      switch (oper)
      {
        case CompCond.BitwiseAnd:
          return CompareOperator.BitwiseAnd;
        case CompCond.Equal:
          return CompareOperator.Equal;
        case CompCond.Greater:
          return CompareOperator.Greater;
        case CompCond.GreaterOrEqual:
          return CompareOperator.GreaterOrEqual;
        case CompCond.Less:
          return CompareOperator.Less;
        case CompCond.LessOrEqual:
          return CompareOperator.LessOrEqual;
        case CompCond.Like:
          return CompareOperator.Like;
        case CompCond.NotLike:
          return CompareOperator.NotLike;
        default:
          return CompareOperator.NotEqual;
      }
    }

    public static WhereClauseRelationship ConvertWhereRel(WhereRel whereRel)
    {
      switch (whereRel)
      {
        case WhereRel.Or:
          return WhereClauseRelationship.Or;
        default:
          return WhereClauseRelationship.And;
      }
    }

    public static OrderByDirection ConvertOrderByDir(OrderByDir orderByDir)
    {
      switch (orderByDir)
      {
        case OrderByDir.Desc:
          return OrderByDirection.Descending;
        default:
          return OrderByDirection.Ascending;
      }
    }


  }
}
