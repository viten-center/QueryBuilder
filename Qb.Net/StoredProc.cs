using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{
  /// <summary>Класс команды выполнения хранимой процедуры</summary>
  public class StoredProc
  {
    internal StoredProcQuery Query;

    internal StoredProc(string storedProcName)
    {
      Query = new StoredProcQuery(storedProcName);
    }

    /// <summary>Список параметров команды</summary>
    /// <param name="parameters">Параметры</param>
    /// <returns></returns>
    public StoredProc Params(params Param[] parameters)
    {
      if (parameters != null)
      {
        this.Query.CommandParams.AddRange(parameters);
      }
      return this;
    }

  }
}
