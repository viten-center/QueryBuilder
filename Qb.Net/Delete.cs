using Viten.QueryBuilder.Culture;
using Viten.QueryBuilder.SqlOm;
using System.Linq;

namespace Viten.QueryBuilder
{
  /// <summary>Класс команды удаления информации</summary>
  public class Delete
  {
    internal DeleteQuery Query;

    internal Delete(string tableName)
    {
      Query = new DeleteQuery(tableName);
    }

    internal Delete(string tableName, string schema)
    {
      Query = new DeleteQuery(tableName, schema);
    }

    public Delete Where(params Cond[] opers)
    {
      return Where(Logic.And(opers));
    }

    /// <summary>Аналог конструкции DELETE ... WHERE</summary>
    public Delete Where(Logic where)
    {
      if (this.Query.WhereClause.SubClauses.Count > 0)
        throw new InvalidQueryException(SR.Err_RepeatWhere);
      this.Query.WhereClause.SubClauses.Add(where.Clause);
      return this;
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public Delete Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

  }
}
