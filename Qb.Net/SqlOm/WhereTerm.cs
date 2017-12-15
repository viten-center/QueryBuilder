using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{

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
		OmConstantCollection values;
		
    public WhereTerm()
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
		public static WhereTerm CreateCompare(OmExpression expr1, OmExpression expr2, CompCond op)
		{
			WhereTerm term = new WhereTerm();
			term.Expr1 = expr1;
			term.Expr2 = expr2;
			term.Op = op;
			term.Type = WhereTermType.Compare;
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
      term.Expr1 = expr1;
      term.Expr2 = expr2;
      term.Expr3 = OmExpression.Constant(DataType.String, new string(escapeChar, 1));
      term.Op = CompCond.Like;
      term.Type = WhereTermType.Compare;
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
      term.Expr1 = expr1;
      term.Expr2 = expr2;
      term.Expr3 = OmExpression.Constant(DataType.String, new string(escapeChar, 1));
      term.Op = CompCond.NotLike;
      term.Type = WhereTermType.Compare;
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
			term.Expr1 = expr;
			term.SubQuery = sql;
			term.Type = WhereTermType.InSubQuery;
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
      term.Expr1 = expr;
      term.SubQuery = subQuery;
      term.Type = WhereTermType.InSubQuery;
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
			term.Expr1 = expr;
			term.values = values;
			term.Type = WhereTermType.In;
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
			term.Expr1 = expr;
			term.SubQuery = sql;
			term.Type = WhereTermType.NotInSubQuery;
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
      term.Expr1 = expr;
      term.SubQuery = subQuery;
      term.Type = WhereTermType.NotInSubQuery;
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
			term.Expr1 = expr;
			term.values = values;
			term.Type = WhereTermType.NotIn;
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
			term.Expr1 = expr;
			term.Type = WhereTermType.IsNull;
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
			term.Expr1 = expr;
			term.Type = WhereTermType.IsNotNull;
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
			term.SubQuery = sql;
			term.Type = WhereTermType.Exists;
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
      term.SubQuery = subQuery;
      term.Type = WhereTermType.Exists;
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
			term.SubQuery = sql;
			term.Type = WhereTermType.NotExists;
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
      term.SubQuery = subQuery;
      term.Type = WhereTermType.NotExists;
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
			term.Expr1 = expr;
			term.Expr2 = lowBound;
			term.Expr3 = highBound;
			term.Type = WhereTermType.Between;			
			return term;
		}

    /// <summary></summary>
    public OmExpression Expr1 { get; set; }

    /// <summary></summary>
    public OmExpression Expr2 { get; set; }

    /// <summary></summary>
    public OmExpression Expr3 { get; set; }

    /// <summary></summary>
    public CompCond Op { get; set; }

    /// <summary></summary>
    public WhereTermType Type { get; set; }

    /// <summary></summary>
    public OmConstantCollection Values
		{
			get { return values; }
    }

    /// <summary></summary>
    public object SubQuery { get; set; }
		
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

			a.Expr1 = Expr1;
			a.Expr2 = Expr2;
			a.Expr3 = Expr3;
			a.Op = Op;
			a.Type = Type;
			//a.subQuery = a.subQuery;
      a.SubQuery = SubQuery;
			a.values = new OmConstantCollection(values);
			
			return a;
		}

  }
}
