using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{
  /// <summary>
  /// Класс реализации INSERT INTO...SELECT
  /// </summary>
  public class InsertSelect
  {
    internal InsertSelectQuery Query;

    internal InsertSelect(string tableName)
    {
      Query = new InsertSelectQuery(tableName);
    }
    /// <summary>Определение команды SELECT для вставки</summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public InsertSelect Select(Select select)
    {
      Query.SelectQuery = select;
      return this;
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public InsertSelect Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

  }
}
