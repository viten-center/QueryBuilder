using Viten.QueryBuilder.SqlOm;
using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder
{

  /// <summary>Класс описания операции</summary>
  public class Cond
  {
    internal WhereTerm Term;

    static Cond Compare(Expr expr1, Expr expr2, CompCond op)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateCompare(expr1.Expression, expr2.Expression, op);
      return oper;
    }

    static Cond CreateLikeCompare(OmExpression expr1, OmExpression expr2, char escapeChar)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateLikeCompare(expr1, expr2, escapeChar);
      return oper;
    }

    static Cond CreateNotLikeCompare(OmExpression expr1, OmExpression expr2, char escapeChar)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateNotLikeCompare(expr1, expr2, escapeChar);
      return oper;
    }

    #region Equal
    /// <summary>Операция Equal</summary>
    public static Cond Equal(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.Equal);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, Expr expr2)
    {
      return Equal(field, null, expr2);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.Equal);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, long value)
    {
      return Equal(field, null, value);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Equal);
    }


    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, int value)
    {
      return Equal(field, null, value);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Equal);
    }


    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, double value)
    {
      return Equal(field, null, value);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Equal);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, string value)
    {
      return Equal(field, null, value);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.Equal);
    }


    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, DateTime value)
    {
      return Equal(field, null, value);
    }

    /// <summary>Операция Equal</summary>
    public static Cond Equal(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.Equal);
    }

    #endregion Equal

    #region NotEqual
    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.NotEqual);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, Expr expr2)
    {
      return NotEqual(field, null, expr2);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.NotEqual);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, long value)
    {
      return NotEqual(field, null, value);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.NotEqual);
    }


    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, int value)
    {
      return NotEqual(field, null, value);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.NotEqual);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, double value)
    {
      return NotEqual(field, null, value);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.NotEqual);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, string value)
    {
      return NotEqual(field, null, value);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.NotEqual);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, DateTime value)
    {
      return NotEqual(field, null, value);
    }

    /// <summary>Операция NotEqual</summary>
    public static Cond NotEqual(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.NotEqual);
    }

    #endregion NotEqual

    #region Like
    /// <summary>Операция Like</summary>
    public static Cond Like(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.Like);
    }

    /// <summary>Операция Like</summary>
    public static Cond Like(Expr expr1, Expr expr2, char escapeChar)
    {
      return CreateLikeCompare(expr1.Expression, expr2.Expression, escapeChar);
    }

    /// <summary>Операция Like</summary>
    public static Cond Like(string field, Expr expr2)
    {
      return Like(field, null, expr2);
    }

    /// <summary>Операция Like</summary>
    public static Cond Like(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.Like);
    }

    /// <summary>Операция Like</summary>
    public static Cond Like(string field, string value)
    {
      return Like(field, null, value);
    }

    /// <summary>Операция Like</summary>
    public static Cond Like(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.Constant(DataType.String, value), CompCond.Like);
    }

    #endregion Like

    #region NotLike
    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.NotLike);
    }

    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(Expr expr1, Expr expr2, char escapeChar)
    {
      return CreateNotLikeCompare(expr1.Expression, expr2.Expression, escapeChar);
    }

    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(string field, Expr expr2)
    {
      return NotLike(field, null, expr2);
    }

    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.NotLike);
    }

    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(string field, string value)
    {
      return NotLike(field, null, value);
    }

    /// <summary>Операция NotLike</summary>
    public static Cond NotLike(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.Constant(DataType.String, value), CompCond.NotLike);
    }

    #endregion Like

    #region LessOrEqual
    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.LessOrEqual);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, Expr expr2)
    {
      return LessOrEqual(field, null, expr2);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.LessOrEqual);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, long value)
    {
      return LessOrEqual(field, null, value);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.LessOrEqual);
    }


    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, int value)
    {
      return LessOrEqual(field, null, value);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.LessOrEqual);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, double value)
    {
      return LessOrEqual(field, null, value);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.LessOrEqual);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, string value)
    {
      return LessOrEqual(field, null, value);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.LessOrEqual);
    }


    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, DateTime value)
    {
      return LessOrEqual(field, null, value);
    }

    /// <summary>Операция LessOrEqual</summary>
    public static Cond LessOrEqual(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.LessOrEqual);
    }

    #endregion LessOrEqual

    #region Less

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, Expr expr2)
    {
      return Less(field, null, expr2);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.Less);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.Less);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, long value)
    {
      return Less(field, null, value);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Less);
    }


    /// <summary>Операция Less</summary>
    public static Cond Less(string field, int value)
    {
      return Less(field, null, value);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Less);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, double value)
    {
      return Less(field, null, value);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Less);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, string value)
    {
      return Less(field, null, value);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.Less);
    }


    /// <summary>Операция Less</summary>
    public static Cond Less(string field, DateTime value)
    {
      return Less(field, null, value);
    }

    /// <summary>Операция Less</summary>
    public static Cond Less(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.Less);
    }

    #endregion Less

    #region GreaterOrEqual
    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.GreaterOrEqual);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, Expr expr2)
    {
      return GreaterOrEqual(field, null, expr2);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.GreaterOrEqual);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, long value)
    {
      return GreaterOrEqual(field, null, value);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.GreaterOrEqual);
    }


    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, int value)
    {
      return GreaterOrEqual(field, null, value);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.GreaterOrEqual);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, double value)
    {
      return GreaterOrEqual(field, null, value);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.GreaterOrEqual);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, string value)
    {
      return GreaterOrEqual(field, null, value);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.GreaterOrEqual);
    }


    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, DateTime value)
    {
      return GreaterOrEqual(field, null, value);
    }

    /// <summary>Операция GreaterOrEqual</summary>
    public static Cond GreaterOrEqual(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.GreaterOrEqual);
    }
    #endregion GreaterOrEqual

    #region Greater
    /// <summary>Операция Greater</summary>
    public static Cond Greater(Expr expr1, Expr expr2)
    {
      return Compare(expr1, expr2, CompCond.Greater);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, Expr expr2)
    {
      return Greater(field, null, expr2);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, Expr expr2)
    {
      return Compare(Expr.Field(field, alias), expr2, CompCond.Greater);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, long value)
    {
      return Greater(field, null, value);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, long value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Greater);
    }


    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, int value)
    {
      return Greater(field, null, value);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, int value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Greater);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, double value)
    {
      return Greater(field, null, value);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, double value)
    {
      return Compare(Expr.Field(field, alias), Expr.Num(value), CompCond.Greater);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, string value)
    {
      return Greater(field, null, value);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, string value)
    {
      return Compare(Expr.Field(field, alias), Expr.String(value), CompCond.Greater);
    }


    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, DateTime value)
    {
      return Greater(field, null, value);
    }

    /// <summary>Операция Greater</summary>
    public static Cond Greater(string field, From alias, DateTime value)
    {
      return Compare(Expr.Field(field, alias), Expr.Date(value), CompCond.Greater);
    }

    #endregion Greater

    #region In
    /// <summary>
    /// Creates a WhereTerm which represents SQL IN clause
    /// </summary>
    /// <param name="expr">Expression to be looked up</param>
    /// <param name="values">List of values</param>
    /// <returns></returns>
    static Cond In(Expr expr, List<Constant> values)
    {
      OmConstantCollection col = new OmConstantCollection();
      for (int i = 0; i < values.Count; i++)
        col.Add(values[i].Const.Value);
      Cond term = new Cond();
      term.Term = WhereTerm.CreateIn(expr.Expression, col);
      return term;
    }

    public static Cond In(Expr expr, params long[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Num(values[i]));
      return Cond.In(expr, col);
    }

    public static Cond In(Expr expr, params int[] values)
    {
      List<Constant> col = new List<Constant>();
      for(int i = 0; i < values.Length; i++)
        col.Add(Constant.Num(values[i]));
      return Cond.In(expr, col);
    }

    public static Cond In(Expr expr, params string[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.String(values[i]));
      return Cond.In(expr, col);
    }

    public static Cond In(Expr expr, params DateTime[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Date(values[i]));
      return Cond.In(expr, col);
    }

    /// <summary>Операция In</summary>
    public static Cond In(Expr expr, Select subQuery)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateIn(expr.Expression, subQuery.Query);
      return oper;
    }

    /// <summary>Операция In</summary>
    public static Cond In(string field, Select subQuery)
    {
      return Cond.In(field, null, subQuery);
    }

    /// <summary>Операция In</summary>
    public static Cond In(string field, From alias, Select subQuery)
    {
      return Cond.In(Expr.Field(field, alias), subQuery);
    }

    #endregion In

    #region NotIn
    /// <summary>
    /// Creates a WhereTerm which represents SQL NOT IN clause
    /// </summary>
    /// <param name="expr">Expression to be looked up</param>
    /// <param name="values">Sub query</param>
    /// <returns></returns>

    public static Cond NotIn(string field, From alias,params int[] values)
    {
      return NotIn(Expr.Field(field, alias), values);
    }

    public static Cond NotIn(string field, params int[] values)
    {
      return NotIn(Expr.Field(field), values);
    }

    public static Cond NotIn(Expr expr, params int[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Num(values[i]));
      return Cond.NotIn(expr, col);
    }

    public static Cond NotIn(string field, From alias, params long[] values)
    {
      return NotIn(Expr.Field(field, alias), values);
    }

    public static Cond NotIn(string field, params long[] values)
    {
      return NotIn(Expr.Field(field), values);
    }

    public static Cond NotIn(Expr expr, params long[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Num(values[i]));
      return Cond.NotIn(expr, col);
    }

    public static Cond NotIn(string field, From alias, params double[] values)
    {
      return NotIn(Expr.Field(field, alias), values);
    }

    public static Cond NotIn(string field, params double[] values)
    {
      return NotIn(Expr.Field(field), values);
    }

    public static Cond NotIn(Expr expr, params double[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Num(values[i]));
      return Cond.In(expr, col);
    }

    public static Cond NotIn(string field, From alias, params DateTime[] values)
    {
      return NotIn(Expr.Field(field, alias), values);
    }

    public static Cond NotIn(string field, params DateTime[] values)
    {
      return NotIn(Expr.Field(field), values);
    }

    public static Cond NotIn(Expr expr, params DateTime[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.Date(values[i]));
      return Cond.NotIn(expr, col);
    }

    public static Cond NotIn(string field, From alias, params string[] values)
    {
      return NotIn(Expr.Field(field, alias), values);
    }

    public static Cond NotIn(string field, params string[] values)
    {
      return NotIn(Expr.Field(field), values);
    }

    public static Cond NotIn(Expr expr, params string[] values)
    {
      List<Constant> col = new List<Constant>();
      for (int i = 0; i < values.Length; i++)
        col.Add(Constant.String(values[i]));
      return Cond.NotIn(expr, col);
    }

    public static Cond NotIn(Expr expr, List<Constant> values)
    {
      Cond oper = new Cond();
      OmConstantCollection col = new OmConstantCollection();
      for (int i = 0; i < values.Count; i++)
        col.Add(values[i].Const);
      oper.Term = WhereTerm.CreateNotIn(expr.Expression, col);
      return oper;
    }

    /// <summary>Операция NotIn</summary>
    //public static Cond NotIn(Expr expr, params Constant[] values)
    //{
    //  List<Constant> list = new List<Constant>();
    //  if (values != null)
    //    list.AddRange(values);
    //  return Cond.NotIn(expr, list);
    //}

    /// <summary>Операция NotIn</summary>
    //public static Cond NotIn(string field, From alias, List<Constant> values)
    //{
    //  return Cond.NotIn(Expr.Field(field, alias), values);
    //}

    /// <summary>Операция NotIn</summary>
    //public static Cond NotIn(string field, List<Constant> values)
    //{
    //  return NotIn(field, null, values);
    //}

    /// <summary>Операция NotIn</summary>
    //public static Cond NotIn(string field, params Constant[] values)
    //{
    //  return Cond.NotIn(field, null, values);
    //}

    /// <summary>Операция NotIn</summary>
    //public static Cond NotIn(string field, From alias, params Constant[] values)
    //{
    //  return Cond.NotIn(Expr.Field(field, alias), values);
    //}

    /// <summary>Операция NotIn</summary>
    public static Cond NotIn(Expr expr, Select subQuery)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateNotIn(expr.Expression, subQuery.Query);
      return oper;
    }

    /// <summary>Операция NotIn</summary>
    public static Cond NotIn(string field, Select subQuery)
    {
      return Cond.NotIn(field, null, subQuery);
    }

    /// <summary>Операция NotIn</summary>
    public static Cond NotIn(string field, From alias, Select subQuery)
    {
      return Cond.NotIn(Expr.Field(field, alias), subQuery);
    }

    #endregion NotIn

    #region IsNull
    /// <summary>
    /// Creates a WhereTerm which returns TRUE if an expression is NULL
    /// </summary>
    /// <param name="expr">Expression to be evaluated</param>
    /// <returns></returns>
    public static Cond IsNull(Expr expr)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateIsNull(expr.Expression);
      return oper;
    }

    /// <summary>Операция IsNull</summary>
    public static Cond IsNull(string field)
    {
      return IsNull(field, null);
    }

    /// <summary>Операция IsNull</summary>
    public static Cond IsNull(string field, From alias)
    {
      return IsNull(Expr.Field(field, alias));
    }
    #endregion IsNull

    #region IsNotNull
    /// <summary>
    /// Creates a WhereTerm which returns TRUE if an expression is NOT NULL
    /// </summary>
    /// <param name="expr"></param>
    /// <returns></returns>
    public static Cond IsNotNull(Expr expr)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateIsNotNull(expr.Expression);
      return oper;
    }

    /// <summary>Операция IsNotNull</summary>
    public static Cond IsNotNull(string field, From alias)
    {
      return IsNotNull(Expr.Field(field, alias));
    }

    /// <summary>Операция IsNotNull</summary>
    public static Cond IsNotNull(string field)
    {
      return IsNotNull(field, null);
    }

    #endregion IsNotNull

    #region Between
    /// <summary>
    /// Creates a WhereTerm which checks weather a value is in a specifed range.
    /// </summary>
    /// <param name="expr">Expression which yeilds the value to be checked</param>
    /// <param name="lowBound">Expression which yeilds the low bound of the range</param>
    /// <param name="highBound">Expression which yeilds the high bound of the range</param>
    /// <returns>A new WhereTerm</returns>
    /// <remarks>
    /// CreateBetween only accepts expressions which yeild a 'Date' or 'Number' values.
    /// All expressions must be of compatible types.
    /// </remarks>
    public static Cond Between(Expr expr, Expr lowBound, Expr highBound)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateBetween(expr.Expression, lowBound.Expression, highBound.Expression);
      return oper;
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, From alias, Expr lowBound, Expr highBound)
    {
      return Between(Expr.Field(field, alias), lowBound, highBound);
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, Expr lowBound, Expr highBound)
    {
      return Between(Expr.Field(field, null), lowBound, highBound);
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, From alias, long lowBound, long highBound)
    {
      return Between(Expr.Field(field, alias), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, long lowBound, long highBound)
    {
      return Between(Expr.Field(field, null), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }


    /// <summary>Операция Between</summary>
    public static Cond Between(string field, From alias, int lowBound, int highBound)
    {
      return Between(Expr.Field(field, alias), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, int lowBound, int highBound)
    {
      return Between(Expr.Field(field, null), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, From alias, double lowBound, double highBound)
    {
      return Between(Expr.Field(field, alias), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, double lowBound, double highBound)
    {
      return Between(Expr.Field(field, null), Expr.Constant(DataType.Number, lowBound), Expr.Constant(DataType.Number, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, From alias, DateTime lowBound, DateTime highBound)
    {
      return Between(Expr.Field(field, alias), Expr.Constant(DataType.Date, lowBound), Expr.Constant(DataType.Date, highBound));
    }

    /// <summary>Операция Between</summary>
    public static Cond Between(string field, DateTime lowBound, DateTime highBound)
    {
      return Between(Expr.Field(field, null), Expr.Constant(DataType.Date, lowBound), Expr.Constant(DataType.Date, highBound));
    }

    #endregion Between

    #region Exists
    /// <summary>
    /// Creates a WhereTerm which encapsulates SQL EXISTS clause
    /// </summary>
    /// <param name="subQuery">Sub query for the EXISTS clause</param>
    /// <returns></returns>
    public static Cond Exists(Select subQuery)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateExists(subQuery.Query);
      return oper;
    }
    #endregion Exists

    #region NotExists
    /// <summary>
    /// Creates a WhereTerm which encapsulates SQL NOT EXISTS clause
    /// </summary>
    /// <param name="subQuery">Sub query for the NOT EXISTS clause</param>
    /// <returns></returns>
    public static Cond NotExists(Select subQuery)
    {
      Cond oper = new Cond();
      oper.Term = WhereTerm.CreateNotExists(subQuery.Query);
      return oper;
    }
    #endregion NotExists

  }
}
