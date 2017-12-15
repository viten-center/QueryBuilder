using System;
using System.Collections.Generic;
using System.Data;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>Класс вызова хранимой процедуры</summary>
  public class StoredProcQuery 
  {
    ParamCollection commandParams = new ParamCollection();

    /// <summary>Список параметров команды</summary>
    public ParamCollection CommandParams
    {
      get { return commandParams; }
    }

    /// <summary>Конструктор</summary>
    public StoredProcQuery():this(null)
    {
    }

    /// <summary>Конструктор</summary>
    public StoredProcQuery(string storedProcName)
    {
      this.StoredProcName = storedProcName;
    }

    /// <summary>Имя хранимой процедуры</summary>
    public string StoredProcName { get; set; }

    /// <summary>Получить коллекцию выходных параметров команды</summary>
    /// <param name="paramCollection">Коллекция параметров команды</param>
    /// <returns></returns>
    public static ParamCollection GetOutputParams(ParamCollection paramCollection)
    {
      if (paramCollection == null)
        throw new ArgumentNullException("paramCollection");
      ParamCollection retVal = new ParamCollection();
      for (int i = 0; i < paramCollection.Count; i++)
      {
        if (paramCollection[i].Direction == ParameterDirection.Input) continue;
        retVal.Add(paramCollection[i]);
      }
      return retVal;
    }
  }
}
