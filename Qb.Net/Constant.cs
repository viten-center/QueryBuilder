using Viten.QueryBuilder.SqlOm;
using System;

namespace Viten.QueryBuilder
{

  /// <summary>Класс описания константы</summary>
  public class Constant
  {
    internal OmConstant Const;
    private Constant()
    {
    }

    /// <summary>Константа</summary>
    public static Constant Date(DateTime val)
    {
      Constant retVal = new Constant();
      retVal.Const = OmConstant.Date(val);
      return retVal;
    }

    /// <summary>Константа</summary>
    public static Constant String(string val)
    {
      Constant retVal = new Constant();
      retVal.Const = OmConstant.String(val);
      return retVal;
    }

    /// <summary>Константа</summary>
    public static Constant Num(int val)
    {
      Constant retVal = new Constant();
      retVal.Const = OmConstant.Number(val);
      return retVal;
    }

    /// <summary>Константа</summary>
    public static Constant Num(long val)
    {
      Constant retVal = new Constant();
      retVal.Const = OmConstant.Number(val);
      return retVal;
    }

    /// <summary>Константа</summary>
    public static Constant Num(double val)
    {
      Constant retVal = new Constant();
      retVal.Const = OmConstant.Number(val);
      return retVal;
    }
  }
}
