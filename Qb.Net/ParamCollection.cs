using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>Класс коллекции параметров команды</summary>
  public class ParamCollection: List<Param>
  {
    /// <summary>Конструктор</summary>
    public ParamCollection()
      :base()
    {
    }

    /// <summary>Конструктор</summary>
    public ParamCollection(Param[] param)
      :base(param)
    {
    }

    /// <summary>Конструктор</summary>
    public ParamCollection(ParamCollection param)
      :base(param)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="paramName"></param>
    /// <returns></returns>
    public Param this[string paramName]
    {
      get
      {
        Param retVal = null;
        for (int i = 0; i < this.Count; i++)
        {
          if (this[i].Name == paramName)
          {
            retVal = this[i];
            break;
          }
        }
        return retVal;
      }
    }
  }
}
