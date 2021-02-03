using Viten.QueryBuilder.Culture;
using Viten.QueryBuilder.SqlOm;
using System.Collections.Generic;
using System.Linq;

namespace Viten.QueryBuilder
{
  /// <summary>Класс команды обновления информации</summary>
  public class Update
  {
    internal UpdateQuery Query;

    internal Update(string tableName)
    {
      Query = new UpdateQuery(tableName);
    }

    internal Update(string tableName, string schema)
    {
      Query = new UpdateQuery(tableName, schema);
    }

    public Update Where(params Cond[] opers)
    {
      return Where(Logic.And(opers));
    }

    /// <summary>Аналог SQL конструкции WHERE</summary>
    public Update Where(Logic where)
    {
      if (this.Query.WhereClause.SubClauses.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatWhere);
      this.Query.WhereClause.SubClauses.Add(where.Clause);
      return this;
    }

    /// <summary>Аналог SQL конструкции UPDATE ... VALUES</summary>
    public Update Values(params Value[] values)
    {
      if (this.Query.Terms.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatValues);
      List<UpdateTerm> terms = new List<UpdateTerm>();
      if (values != null)
        for (int i = 0; i < values.Length; i++)
          terms.Add(values[i].Term);
      this.Query.Terms.AddRange(terms.ToArray());
      return this;
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public Update Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

  }
}
