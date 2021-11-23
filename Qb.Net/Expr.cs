using Viten.QueryBuilder.SqlOm;
using System;

namespace Viten.QueryBuilder
{
  public class WhenThen
  {
    internal Cond _when;
    internal Expr _then;
    public static WhenThen New(Cond when, Expr then)
    {
      WhenThen whenThen = new WhenThen();
      whenThen._when = when;
      whenThen._then = then;
      return whenThen;
    }
  }
  /// <summary>Класс описания выражения</summary>
  public class Expr
  {
    internal OmExpression Expression;

    private Expr()
    {
    }

    /// <summary>Определение константы</summary>
    internal static Expr Constant(DataType type, object val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Constant(type, val);
      return oper;
    }

    #region Raw
    /// <summary>Произвольный SQL кон</summary>
    public static Expr Raw(string sqlText)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Raw(sqlText);
      return oper;
    }
    #endregion Raw

    #region Case
    public static Expr Case(Expr @else, params WhenThen[] whenThen)
    {
      Expr oper = new Expr();
      CaseClause clause = new CaseClause();
      clause.ElseValue = @else.Expression;
      
      
      foreach (WhenThen item in whenThen)
      {
        CaseTerm caseTerm = new CaseTerm();
        caseTerm.Condition = new WhereClause();
        caseTerm.Condition.Terms.Add(item._when.Term);
        caseTerm.Value = item._then.Expression;
        clause.Terms.Add(caseTerm);
      }
      oper.Expression= OmExpression.Case(clause);
      return oper;
    }
    #endregion Case

    #region Field
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
    #endregion Field

    #region Date
    /// <summary>Определение константы (DateTime)</summary>
    public static Expr Date(DateTime val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Date(val);
      return oper;
    }
    #endregion Date

    #region Function
    /// <summary>Определение функции</summary>
    public static Expr Function(AggFunc func, Expr val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Function(func, val.Expression);
      return oper;
    }
    #endregion Function

    #region IfNull
    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, Expr val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.IfNull(test.Expression, val.Expression);
      return oper;
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, string val)
    {
      return IfNull(test, Expr.Constant(DataType.String, val));
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, int val)
    {
      return IfNull(test, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, long val)
    {
      return IfNull(test, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, double val)
    {
      return IfNull(test, Expr.Constant(DataType.Number, val));
    }

    /// <summary>Определение проверки на NULL</summary>
    public static Expr IfNull(Expr test, DateTime val)
    {
      return IfNull(test, Expr.Constant(DataType.Date, val));
    }
    #endregion IfNull

    #region Null
    /// <summary>Определение NULL</summary>
    public static Expr Null()
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Null();
      return oper;
    }
    #endregion Null

    #region Num
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
    #endregion Num

    #region Param
    /// <summary>Определение параметра</summary>
    public static Expr Param(string paramName)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.Parameter(paramName);
      return oper;
    }
    #endregion Param

    #region String
    /// <summary>Определение константы (string)</summary>
    public static Expr String(string val)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.String(val);
      return oper;
    }
    #endregion String

    #region SubQuery
    /// <summary>Определение подзапроса</summary>
    public static Expr SubQuery(Select subQuery)
    {
      Expr oper = new Expr();
      oper.Expression = OmExpression.SubQuery(subQuery.Query);
      return oper;
    }
    #endregion SubQuery
  }
}
