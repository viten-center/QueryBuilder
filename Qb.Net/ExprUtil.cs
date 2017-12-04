using Viten.QueryBuilder.SqlOm;
using System.Data;
using System;
using System.Linq;

namespace Viten.QueryBuilder
{
  class ExprUtil
  {
    public static OmAggregationFunction ConvertAggregationFunction(AggFunc agr)
    {
      switch (agr)
      {
        case AggFunc.Avg:
          return OmAggregationFunction.Avg;
        case AggFunc.Count:
          return OmAggregationFunction.Count;
        case AggFunc.Grouping:
          return OmAggregationFunction.Grouping;
        case AggFunc.Max:
          return OmAggregationFunction.Max;
        case AggFunc.Min:
          return OmAggregationFunction.Min;
        case AggFunc.Sum:
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
