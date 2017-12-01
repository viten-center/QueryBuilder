using Viten.QueryBuilder.SqlOm;
using System;

namespace Viten.QueryBuilder
{
    /// <summary>Класс описания значения поля для обновления</summary>
    public class UpdateVal
  {
    internal UpdateTerm Term;

    internal UpdateVal(string fieldName, Expr val)
		{
      Term = new UpdateTerm(fieldName, val?.Expression);
		}

    /// <summary>Значение поля</summary>
    public static UpdateVal Val(string fieldName, Expr val)
    {
      return new UpdateVal(fieldName, val);
    }

    /// <summary>Значение поля</summary>
    public static UpdateVal Val(string fieldName, string val)
    {
      return new UpdateVal(fieldName, Expr.Constant(DataType.String, val));
    }

    /// <summary>Значение поля</summary>
    public static UpdateVal Val(string fieldName, int val)
    {
      return new UpdateVal(fieldName, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Значение поля</summary>
    public static UpdateVal Val(string fieldName, double val)
    {
      return new UpdateVal(fieldName, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Значение поля</summary>
    public static UpdateVal Val(string fieldName, DateTime val)
    {
      return new UpdateVal(fieldName, Expr.Constant(DataType.Date, val));
    }

  }
}
