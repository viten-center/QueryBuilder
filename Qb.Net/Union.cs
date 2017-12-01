using Viten.QueryBuilder.SqlOm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>
  /// Перечисление реализации SQL DISTINCT or ALL для UNION
  /// </summary>
  public enum UnionModifier
  {
    /// <summary>Only distinct rows will be returned</summary>
    Distinct,
    /// <summary>All rows will be returned</summary>
    All
  }
  /// <summary>Класс реализации SQL UNION оператора</summary>
  public class Union
  {
    static DistinctModifier ToDistinctModifier(UnionModifier modifier)
    {
      if (modifier == UnionModifier.All)
        return DistinctModifier.All;
      return DistinctModifier.Distinct;
    }

    internal OmUnion Uni;

    internal Union()
    {
      Uni = new OmUnion();
    }

    /// <summary>Добавить объект запроса. По умолчанию UnionModifier.Distinct</summary>
    public Union Add(Select query)
    {
      return Add(query, UnionModifier.Distinct);
    }

    /// <summary>Добавить объект запроса</summary>
    public Union Add(Select query, UnionModifier modifier)
    {
      Uni.Add(Qb.GetQueryObject(query), ToDistinctModifier(modifier));
      return this;
    }
  }
}
