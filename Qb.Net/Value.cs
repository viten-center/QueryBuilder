using Viten.QueryBuilder.SqlOm;
using System;

namespace Viten.QueryBuilder
{
  /// <summary>Класс описания значения поля для обновления</summary>
  public class Value
  {
    internal UpdateTerm Term;

    internal Value(string fieldName, Expr val)
    {
      Term = new UpdateTerm(fieldName, val?.Expression);
    }

    /// <summary>Значение поля</summary>
    public static Value New(string fieldName, Expr val)
    {
      return new Value(fieldName, val);
    }

    /// <summary>Значение поля</summary>
    public static Value New(string fieldName, string val)
    {
      return new Value(fieldName, Expr.Constant(DataType.String, val));
    }

    /// <summary>Значение поля</summary>
    public static Value New(string fieldName, int val)
    {
      return new Value(fieldName, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Значение поля</summary>
    public static Value New(string fieldName, double val)
    {
      return new Value(fieldName, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Значение поля</summary>
    public static Value New(string fieldName, DateTime val)
    {
      return new Value(fieldName, Expr.Constant(DataType.Date, val));
    }

  }
}
