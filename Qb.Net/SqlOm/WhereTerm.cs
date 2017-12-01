using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary></summary>
  public enum WhereTermType 
  {
    /// <summary></summary>
    Compare,
    /// <summary></summary>
    Between,
    /// <summary></summary>
    In,
    /// <summary></summary>
    NotIn,
    /// <summary></summary>
    InSubQuery,
    /// <summary></summary>
    NotInSubQuery,
    /// <summary></summary>
    IsNull,
    /// <summary></summary>
    IsNotNull,
    /// <summary></summary>
    Exists,
    /// <summary></summary>
    NotExists
  }

	/// <summary>
	/// Represents one term in a WHERE clause
	/// </summary>
	/// <remarks>
	/// <see cref="WhereTerm"/> usually consists of one or more <see cref="OmExpression"/> objects and an a conditional operator which applies to those expressions.
	/// <see cref="WhereTerm"/> has no public constructor. Use one of the supplied static methods to create a term. 
	/// <para>
	/// Use WhereTerm.CreateCompare to create a comparison term. A comparison term can apply one of <see cref="CompareOperator"/> operators on the supplied expressions.
  /// Use WhereTerm.CreateIn to create a term which checks wheather an expression exists in a list of supplied values.
	/// Use WhereTerm.CreateBetween to create a term which checks wheather an expression value is between a supplied lower and upper bounds.
	/// </para>
	/// </remarks>
  public class WhereTerm : ICloneable
	{
		OmExpression expr1, expr2, expr3;
		CompareOperator op;
		WhereTermType type;
		OmConstantCollection values;
		object subQuery;
    internal WhereTerm()
		{
		}

		/// <summary>
		/// Creates a comparison WhereTerm.
		/// </summary>
		/// <param name="expr1">Expression on the left side of the operator</param>
		/// <param name="expr2">Expression on the right side of the operator</param>
		/// <param name="op">Conditional operator to be applied on the expressions</param>
		/// <returns>A new conditional WhereTerm</returns>
		/// <remarks>
		/// A comparison term compares two expression on the basis of their values. Expressions can be of any type but their results must be of comparible types. 
		/// For instance, you can not compare a database field of type 'date' and a static value of type 'int'.
		/// </remarks>
		/// <example>
		/// <code>
		/// ...
		/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("J%"), CompareOperator.Like));
		/// </code>
		/// </example>
		public static WhereTerm CreateCompare(OmExpression expr1, OmExpression expr2, CompareOperator op)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr1;
			term.expr2 = expr2;
			term.op = op;
			term.type = WhereTermType.Compare;
			return term;
		}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <param name="escapeChar"></param>
    /// <returns></returns>
    public static WhereTerm CreateLikeCompare(OmExpression expr1, OmExpression expr2, char escapeChar)
    {
      WhereTerm term = new WhereTerm();
      term.expr1 = expr1;
      term.expr2 = expr2;
      term.expr3 = OmExpression.Constant(OmDataType.String, new string(escapeChar, 1));
      term.op = CompareOperator.Like;
      term.type = WhereTermType.Compare;
      return term;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <param name="escapeChar"></param>
    /// <returns></returns>
    public static WhereTerm CreateNotLikeCompare(OmExpression expr1, OmExpression expr2, char escapeChar)
    {
      WhereTerm term = new WhereTerm();
      term.expr1 = expr1;
      term.expr2 = expr2;
      term.expr3 = OmExpression.Constant(OmDataType.String, new string(escapeChar, 1));
      term.op = CompareOperator.NotLike;
      term.type = WhereTermType.Compare;
      return term;
    }

		/// <summary>
		/// Creates a WhereTerm which represents SQL IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="sql">Sub query</param>
		/// <returns></returns>
		public static WhereTerm CreateIn(OmExpression expr, string sql)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.subQuery = sql;

			term.type = WhereTermType.InSubQuery;
			
			return term;
		}

    /// <summary>
    /// Creates a WhereTerm which represents SQL IN clause
    /// </summary>
    /// <param name="expr">Expression to be looked up</param>
    /// <param name="subQuery">Sub query</param>
    /// <returns></returns>
    public static WhereTerm CreateIn(OmExpression expr, SelectQuery subQuery)
    {
      WhereTerm term = new WhereTerm();
      term.expr1 = expr;
      term.subQuery = subQuery;

      term.type = WhereTermType.InSubQuery;

      return term;
    }

		/// <summary>
		/// Creates a WhereTerm which represents SQL IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="values">List of values</param>
		/// <returns></returns>
		public static WhereTerm CreateIn(OmExpression expr, OmConstantCollection values)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.values = values;

			term.type = WhereTermType.In;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which represents SQL NOT IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="sql">Sub query</param>
		/// <returns></returns>
		public static WhereTerm CreateNotIn(OmExpression expr, string sql)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.subQuery = sql;

			term.type = WhereTermType.NotInSubQuery;
			
			return term;
		}

    /// <summary>
    /// Creates a WhereTerm which represents SQL NOT IN clause
    /// </summary>
    /// <param name="expr">Expression to be looked up</param>
    /// <param name="subQuery">Sub query</param>
    /// <returns></returns>
    public static WhereTerm CreateNotIn(OmExpression expr, SelectQuery subQuery)
    {
      WhereTerm term = new WhereTerm();
      term.expr1 = expr;
      term.subQuery = subQuery;

      term.type = WhereTermType.NotInSubQuery;

      return term;
    }


		/// <summary>
		/// Creates a WhereTerm which represents SQL NOT IN clause
		/// </summary>
		/// <param name="expr">Expression to be looked up</param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static WhereTerm CreateNotIn(OmExpression expr, OmConstantCollection values)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.values = values;

			term.type = WhereTermType.NotIn;
			
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which returns TRUE if an expression is NULL
		/// </summary>
		/// <param name="expr">Expression to be evaluated</param>
		/// <returns></returns>
		public static WhereTerm CreateIsNull(OmExpression expr)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.type = WhereTermType.IsNull;
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which returns TRUE if an expression is NOT NULL
		/// </summary>
		/// <param name="expr"></param>
		/// <returns></returns>
		public static WhereTerm CreateIsNotNull(OmExpression expr)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.type = WhereTermType.IsNotNull;
			return term;
		}

		/// <summary>
		/// Creates a WhereTerm which encapsulates SQL EXISTS clause
		/// </summary>
		/// <param name="sql">Sub query for the EXISTS clause</param>
		/// <returns></returns>
		public static WhereTerm CreateExists(string sql)
		{
			WhereTerm term = new WhereTerm();
			term.subQuery = sql;
			term.type = WhereTermType.Exists;
			return term;
		}

    /// <summary>
    /// Creates a WhereTerm which encapsulates SQL EXISTS clause
    /// </summary>
    /// <param name="subQuery">Sub query for the EXISTS clause</param>
    /// <returns></returns>
    public static WhereTerm CreateExists(SelectQuery subQuery)
    {
      WhereTerm term = new WhereTerm();
      term.subQuery = subQuery;
      term.type = WhereTermType.Exists;
      return term;
    }

		/// <summary>
		/// Creates a WhereTerm which encapsulates SQL NOT EXISTS clause
		/// </summary>
		/// <param name="sql">Sub query for the NOT EXISTS clause</param>
		/// <returns></returns>
		public static WhereTerm CreateNotExists(string sql)
		{
			WhereTerm term = new WhereTerm();
			term.subQuery = sql;
			term.type = WhereTermType.NotExists;
			return term;
		}

    /// <summary>
    /// Creates a WhereTerm which encapsulates SQL NOT EXISTS clause
    /// </summary>
    /// <param name="subQuery">Sub query for the NOT EXISTS clause</param>
    /// <returns></returns>
    public static WhereTerm CreateNotExists(SelectQuery subQuery)
    {
      WhereTerm term = new WhereTerm();
      term.subQuery = subQuery;
      term.type = WhereTermType.NotExists;
      return term;
    }

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
		public static WhereTerm CreateBetween(OmExpression expr, OmExpression lowBound, OmExpression highBound)
		{
			WhereTerm term = new WhereTerm();
			term.expr1 = expr;
			term.expr2 = lowBound;
			term.expr3 = highBound;
			
			term.type = WhereTermType.Between;			
			
			return term;
		}

    /// <summary></summary>
    public OmExpression Expr1
		{
			get { return expr1; }
		}

    /// <summary></summary>
    public OmExpression Expr2
		{
			get { return expr2; }
		}

    /// <summary></summary>
    public OmExpression Expr3
		{
			get { return expr3;	}
		}

    /// <summary></summary>
    public CompareOperator Op
		{
			get { return op; }
		}

    /// <summary></summary>
    public WhereTermType Type
		{
			get { return type; }
		}

    /// <summary></summary>
    public OmConstantCollection Values
		{
			get { return values; }
		}

    /// <summary></summary>
    public object SubQuery
		{
			get { return this.subQuery; }
		}
		
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Creates a copy of this WhereTerm
		/// </summary>
		/// <returns>A new WhereTerm which exactly the same as the current one.</returns>
		public WhereTerm Clone()
		{
			WhereTerm a = new WhereTerm();

			a.expr1 = expr1;
			a.expr2 = expr2;
			a.expr3 = expr3;
			a.op = op;
			a.type = type;
			//a.subQuery = a.subQuery;
      a.subQuery = subQuery;
			a.values = new OmConstantCollection(values);
			
			return a;
		}

  }
}
