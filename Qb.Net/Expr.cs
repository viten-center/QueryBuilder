using Viten.QueryBuilder.SqlOm;
using System;

namespace Viten.QueryBuilder
{
    /// <summary>Класс описания выражения</summary>
    public class Expr
  {
    internal OmExpression Expression;

    private Expr()
    {
    }

    /// <summary>Произвольный SQL кон</summary>
    public static Expr Raw(string sqlText)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Raw(sqlText);
      return oper;
    }

    /// <summary>Определение поля</summary>
    public static Expr Field(string fieldName)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Field(fieldName);
      return oper;
    }

    /// <summary>Определение поля</summary>
    public static Expr Field(string fieldName, From from)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Field(fieldName, from != null ? from.Term : null);
      return oper;
    }

    /// <summary>Определение константы</summary>
    public static Expr Constant(Constant val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Constant(val.Const);
      return oper;
    }

    /// <summary>Определение константы</summary>
    internal static Expr Constant(DataType type, object val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Constant(ExprUtil.ConvertDataType(type), val);
      return oper;
    }

    /// <summary>Определение константы (DateTime)</summary>
    public static Expr Date(DateTime val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Date(val);
      return oper;
    }

    /// <summary>Определение функции</summary>
    public static Expr Function(AggFunction func, Expr param)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Function(ExprUtil.ConvertAggregationFunction(func), param.Expression);
      return oper;
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, Expr val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.IfNull(test.Expression, val.Expression);
      return oper;
    }

    /// <summary>Определение NULL</summary>
    public static Expr Null()
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Null();
      return oper;
    }

    /// <summary>Определение константы (int)</summary>
    public static Expr Num(int val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Number(val);
      return oper;
    }

    /// <summary>Определение константы (long)</summary>
    public static Expr Num(long val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Number(val);
      return oper;
    }

    /// <summary>Определение константы (double)</summary>
    public static Expr Num(double val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Number(val);
      return oper;
    }

    /// <summary>Определение параметра</summary>
    public static Expr Param(string paramName)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Parameter(paramName);
      return oper;
    }

    /// <summary>Определение константы (string)</summary>
    public static Expr String(string val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.String(val);
      return oper;
    }

    /// <summary>Определение подзапроса</summary>
    public static Expr SubQuery(Select subQuery)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.SubQuery(subQuery.Query);
      return oper;
    }

  }
}
