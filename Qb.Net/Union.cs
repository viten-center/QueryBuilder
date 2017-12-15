using Viten.QueryBuilder.SqlOm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>Класс реализации SQL UNION оператора</summary>
  public class Union
  {
    internal OmUnion Uni;

    internal Union()
    {
      Uni = new OmUnion();
    }

    /// <summary>Добавить объект запроса. По умолчанию UnionModifier.All</summary>
    public Union Add(Select query)
    {
      return Add(query, UnionMod.All);
    }

    /// <summary>Добавить объект запроса</summary>
    public Union Add(Select query, UnionMod modifier)
    {
      Uni.Add(Qb.GetQueryObject(query), modifier);
      return this;
    }
  }
}
