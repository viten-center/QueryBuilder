using System;
using System.Collections.Generic;
using System.Linq;
using Viten.QueryBuilder.SqlOm;
using Viten.QueryBuilder.Culture;

namespace Viten.QueryBuilder
{
  /// <summary>Класс команды чтения информации</summary>
  public class Select
  {
    internal SelectQuery Query;

    internal Select(SelectQuery selectQuery)
    {
      if (selectQuery == null)
        throw new ArgumentNullException(nameof(selectQuery));
      Query = selectQuery;
    }
    internal Select(params string[] columnsName)
    {
      if (columnsName == null)
        throw new ArgumentNullException(nameof(columnsName));
      Query = new SelectQuery();
      for (int i = 0; i < columnsName.Length; i++)
        Query.Columns.Add(new SelectColumn(columnsName[i]));
    }

    internal Select(Column[] columns)
    {
      if (columns == null)
        throw new ArgumentNullException(nameof(columns));
      Query = new SelectQuery();
      for (int i = 0; i < columns.Length; i++)
        Query.Columns.Add(columns[i].Col);
    }

    bool topCalled = false;
    /// <summary>Установка параметра Top</summary>
    public Select Top(int top)
    {
      if (topCalled)
        throw new InvalidQueryException(SR.Err_RepeatTop);
      if (pageCalled)
        throw new InvalidQueryException(SR.Err_TopWithPage);
      this.Query.Top = top;
      topCalled = true;
      return this;
    }

    bool pageCalled = false;
    /// <summary>Установка постраничного просмотра</summary>
    public Select Page(int pageIndex, int pageSize)
    {
      if (pageCalled)
        throw new InvalidQueryException(SR.Err_RepeatPage);
      if (pageIndex < 0 || pageSize < 1)
        throw new InvalidQueryException(SR.Err_InvalidPage);
      if (topCalled)
        throw new InvalidQueryException(SR.Err_TopWithPage);
      this.Query.PageIndex = pageIndex;
      this.Query.PageSize = pageSize;
      pageCalled = true;
      return this;
    }

    bool distinctCalled = false;
    /// <summary>Установка параметра Distinct</summary>
    public Select Distinct(bool distinct)
    {
      if (distinctCalled)
        throw new InvalidQueryException(SR.Err_RepeatDistinct);
      this.Query.Distinct = distinct;
      distinctCalled = true;
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... FROM</summary>
    public Select From(string tableName)
    {
      if (this.Query.FromClause.BaseTable != null)
        throw new InvalidQueryException(SR.Err_RepeatFrom);
      this.Query.FromClause.BaseTable = FromTerm.Table(tableName);
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... FROM</summary>
    public Select From(string tableName, string alias)
    {
      if (this.Query.FromClause.BaseTable != null)
        throw new InvalidQueryException(SR.Err_RepeatFrom);
      this.Query.FromClause.BaseTable = FromTerm.Table(tableName, alias);
      return this;
    }

    public Select From(Union union, string alias)
    {
      if (union == null)
        throw new ArgumentNullException(nameof(union));
      if (string.IsNullOrEmpty(alias))
        throw new ArgumentException("Value should not be defined", nameof(alias));
      From from = QueryBuilder.From.Union(union, alias);
      return From(from);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public Select From(From from)
    {
      if (from == null)
        throw new ArgumentNullException(nameof(from));
      if (this.Query.FromClause.BaseTable != null)
        throw new InvalidQueryException(SR.Err_RepeatFrom);
      this.Query.FromClause.BaseTable = from.Term;
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... JOIN</summary>
    public Select Join(string rightTableName, JoinCond joinCondition)
    {
      this.Query.FromClause.Join(JoinType.Inner, this.Query.FromClause.BaseTable, FromTerm.Table(rightTableName), joinCondition.Condition);
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... JOIN</summary>
    public Select Join(JoinType joinType, string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      From left = new From(leftTableName);
      From right = new From(rightTableName);
      return Join(joinType, left, right, joinCondition);
    }
    public Select JoinCross(string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Cross, leftTableName, rightTableName, joinCondition);
    }
    public Select JoinFull(string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Full, leftTableName, rightTableName, joinCondition);
    }
    public Select JoinInner(string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Inner, leftTableName, rightTableName, joinCondition);
    }
    public Select JoinLeft(string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Left, leftTableName, rightTableName, joinCondition);
    }
    public Select JoinRight(string leftTableName, string rightTableName, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Right, leftTableName, rightTableName, joinCondition);
    }





    /// <summary>
    /// 
    /// </summary>
    /// <param name="joinType"></param>
    /// <param name="leftTable"></param>
    /// <param name="rightTable"></param>
    /// <param name="joinCondition"></param>
    /// <returns></returns>
    public Select Join(JoinType joinType, From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      List<JoinCondition> conds = new List<JoinCondition>();
      if (joinCondition != null)
        for (int i = 0; i < joinCondition.Length; i++)
          conds.Add(joinCondition[i].Condition);
      this.Query.FromClause.Join(joinType, leftTable.Term, rightTable.Term, conds.ToArray());
      return this;
    }
    public Select JoinCross(From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Cross, leftTable, rightTable, joinCondition);
    }
    public Select JoinFull(From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Full, leftTable, rightTable, joinCondition);
    }
    public Select JoinInner(From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Inner, leftTable, rightTable, joinCondition);
    }
    public Select JoinLeft(From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Left, leftTable, rightTable, joinCondition);
    }
    public Select JoinRight(From leftTable, From rightTable, params JoinCond[] joinCondition)
    {
      return Join(JoinType.Right, leftTable, rightTable, joinCondition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="joinType"></param>
    /// <param name="leftTable"></param>
    /// <param name="rightTable"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public Select Join(JoinType joinType, From leftTable, From rightTable, Logic where)
    {
      Join join = new Join(leftTable.Term, rightTable.Term, where.Clause, joinType);
      Query.FromClause.Joins.Add(join);
      return this;
    }

    public Select JoinInner(From leftTable, From rightTable, Logic where)
    {
      return Join(JoinType.Inner, leftTable, rightTable, where);
    }
    public Select JoinLeft(From leftTable, From rightTable, Logic where)
    {
      return Join(JoinType.Left, leftTable, rightTable, where);
    }
    public Select JoinRight(From leftTable, From rightTable, Logic where)
    {
      return Join(JoinType.Right, leftTable, rightTable, where);
    }
    public Select JoinCross(From leftTable, From rightTable, Logic where)
    {
      return Join(JoinType.Cross, leftTable, rightTable, where);
    }
    public Select JoinFull(From leftTable, From rightTable, Logic where)
    {
      return Join(JoinType.Full, leftTable, rightTable, where);
    }




    /// <summary>
    /// Все условия будут объеденены через AND
    /// </summary>
    public Select Join(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(joinType, leftTable, rightTable, Logic.And(opers));
    }
    public Select JoinCross(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(JoinType.Cross, leftTable, rightTable, Logic.And(opers));
    }
    public Select JoinFull(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(JoinType.Full, leftTable, rightTable, Logic.And(opers));
    }
    public Select JoinInner(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(JoinType.Inner, leftTable, rightTable, Logic.And(opers));
    }
    public Select JoinLeft(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(JoinType.Left, leftTable, rightTable, Logic.And(opers));
    }
    public Select JoinRight(JoinType joinType, From leftTable, From rightTable, params Cond[] opers)
    {
      return Join(JoinType.Right, leftTable, rightTable, Logic.And(opers));
    }



    /// <summary>
    /// Все условия будут объеденены через AND
    /// </summary>
    public Select Where(params Cond[] opers)
    {
      return Where(Logic.And(opers));
    }

    /// <summary>Аналог конструкции SELECT ... WHERE</summary>
    public Select Where(Logic where)
    {
      if (this.Query.WherePhrase.SubClauses.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatWhere);
      this.Query.WherePhrase.SubClauses.Add(where.Clause);
      return this;
    }

    public Select Having(params Cond[] opers)
    {
      return Having(Logic.And(opers));
    }
    /// <summary>Аналог конструкции SELECT ... HAVING</summary>
    public Select Having(Logic where)
    {
      if (this.Query.HavingPhrase.SubClauses.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatHaving);
      this.Query.HavingPhrase.SubClauses.Add(where.Clause);
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... ORDER BY</summary>
    public Select OrderBy(string field, From table, OrderByDir dir)
    {
      this.Query.OrderByTerms.Add(new OrderByTerm(field, table?.Term, dir));
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... ORDER BY</summary>
    public Select OrderBy(string field, OrderByDir dir)
    {
      this.Query.OrderByTerms.Add(new OrderByTerm(field, dir));
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... ORDER BY</summary>
    public Select OrderBy(string field)
    {
      this.Query.OrderByTerms.Add(new OrderByTerm(field, OrderByDir.Asc));
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... GROUP BY</summary>
    public Select GroupBy(string field, From table)
    {
      this.Query.GroupByTerms.Add(new GroupByTerm(field, table?.Term));
      return this;
    }

    /// <summary>Аналог конструкции SELECT ... GROUP BY</summary>
    public Select GroupBy(string field)
    {
      return GroupBy(field, null);
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public Select Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

    public static explicit operator Select(SelectQuery selectQuery)
    {
      return new Select(selectQuery);
    }
  }
}
