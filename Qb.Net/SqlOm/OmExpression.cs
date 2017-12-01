using System;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Describes the type of a <see cref="OmExpression"/>
	/// </summary>
	public enum OmExpressionType 
	{
    /// <summary></summary>
    Function,
    /// <summary></summary>
    Field,
    /// <summary></summary>
    Constant,
    /// <summary></summary>
    SubQueryText,
    /// <summary></summary>
    SubQueryObject,
    /// <summary></summary>
    PseudoField,
    /// <summary></summary>
    Parameter,
    /// <summary></summary>
    Raw,
    /// <summary></summary>
    Case,
    /// <summary></summary>
    IfNull,
    /// <summary></summary>
    Null
	}

	/// <summary>
	/// Describes one expression of a <see cref="WhereTerm"/>
	/// </summary>
	/// <remarks>
	/// SqlExpression has no public constructor. Use one of the supplied static methods to create the type
	/// of the expression you need. 
	/// <para>
	/// <see cref="OmExpression"/> can represent a database field, or a static value. 
	/// To create a <see cref="OmExpression"/> represnting a field use the SqlExpression.Field method.
	/// To create a static value, use one of the methods <see cref="OmExpression.String"/>, <see cref="OmExpression.Date"/> or SqlExpression.Number accordingly to the type of the value.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// ...
	/// query.WherePhrase.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("name", tCustomers), SqlExpression.String("John"), CompareOperator.Equal));
	/// ...
	/// </code>
	/// </example>
  public class OmExpression 
	{
		OmExpressionType type;
		FromTerm table = null;
		OmAggregationFunction func = OmAggregationFunction.None;

    OmExpression subExpr1, subExpr2;
    CaseClause caseClause = new CaseClause();
    object val;

    class ValCode
    {
      public const byte String = 0;
      public const byte SqlConst = 1;
      public const byte SelQuery = 2;
    }

    internal OmExpression()
		{
		}

		/// <summary>
		/// Creates a SqlExpression which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a numeric value</returns>
		public static OmExpression Number(double val)
		{
			return Constant(OmConstant.Number(val));
		}

		/// <summary>
		/// Creates a SqlExpression which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a numeric value</returns>
		public static OmExpression Number(long val)
		{
			return Constant(OmConstant.Number(val));		
		}

    /// <summary>
    /// Creates a SqlExpression which represents a numeric value.
    /// </summary>
    /// <param name="val">Value of the expression</param>
    /// <returns>A SqlExpression which represents a numeric value</returns>
    public static OmExpression Number(int val)
    {
      return Constant(OmConstant.Number(val));
    }

		/// <summary>
		/// Creates a SqlExpression which represents a textual value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a textual value</returns>
		public static OmExpression String(string val)
		{
			return Constant(OmConstant.String(val));
		}


		/// <summary>
		/// Creates a SqlExpression which represents a date value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlExpression which represents a date value</returns>
		public static OmExpression Date(DateTime val)
		{
			return Constant(OmConstant.Date(val));
		}
		
		/// <summary>
		/// Creates a SqlExpression which represents a constant typed value.
		/// </summary>
		/// <param name="val">SqlConstant instance</param>
		/// <returns>A SqlExpression which represents a date value</returns>
		public static OmExpression Constant(OmConstant val)
		{
			OmExpression expr = new OmExpression();
			expr.val = val;
			expr.type = OmExpressionType.Constant;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a constant typed value
		/// </summary>
		/// <param name="dataType">Value's data type</param>
		/// <param name="val">The value</param>
		/// <returns></returns>
		public static OmExpression Constant(OmDataType dataType, object val)
		{
			OmExpression expr = new OmExpression();
			expr.val = new OmConstant(dataType, val);
			expr.type = OmExpressionType.Constant;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a field in a database table.
		/// </summary>
		/// <param name="fieldName">Name of a field</param>
		/// <param name="table">The table this field belongs to</param>
		/// <returns></returns>
		public static OmExpression Field(string fieldName, FromTerm table)
		{
			OmExpression expr = new OmExpression();
			expr.val = fieldName;
			expr.table = table;
			expr.type = OmExpressionType.Field;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with a CASE statement.
		/// </summary>
		/// <param name="caseClause"></param>
		/// <returns></returns>
		public static OmExpression Case(CaseClause caseClause)
		{
			OmExpression expr = new OmExpression();
			expr.type = OmExpressionType.Case;
			expr.caseClause = caseClause;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with IfNull function.
		/// </summary>
		/// <param name="test">Expression to be checked for being NULL</param>
		/// <param name="val">Substitution</param>
		/// <returns></returns>
		/// <remarks>
		/// Works as SQL Server's ISNULL() function.
		/// </remarks>
		public static OmExpression IfNull(OmExpression test, OmExpression val)
		{
			OmExpression expr = new OmExpression();
			expr.type = OmExpressionType.IfNull;
			expr.subExpr1 = test;
			expr.subExpr2 = val;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with an aggergation function
		/// </summary>
		/// <param name="func">Aggregation function to be applied on the supplied expression</param>
		/// <param name="param">Parameter of the aggregation function</param>
		/// <returns></returns>
		public static OmExpression Function(OmAggregationFunction func, OmExpression param)
		{
			OmExpression expr = new OmExpression();
			expr.type = OmExpressionType.Function;
			expr.subExpr1 = param;
			expr.func = func;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression representing a NULL value
		/// </summary>
		/// <returns></returns>
		public static OmExpression Null()
		{
			OmExpression expr = new OmExpression();
			expr.type = OmExpressionType.Null;
			return expr;
		}

    /// <summary></summary>
    public static OmExpression PseudoField(string fieldName)
		{
			OmExpression expr = new OmExpression();
			expr.val = fieldName;
			expr.type = OmExpressionType.PseudoField;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a field in a database table.
		/// </summary>
		/// <param name="fieldName">Name of a field</param>
		/// <returns></returns>
		public static OmExpression Field(string fieldName)
		{
			return Field(fieldName, null);
		}

		/// <summary>
		/// Creates a SqlExpression which represents a subquery.
		/// </summary>
		/// <param name="queryText">Text of the subquery.</param>
		/// <returns>A new SqlExpression</returns>
		/// <remarks>
		/// In many cases you can use an inner or outer JOIN instead of a subquery. 
		/// If you prefer using subqueries it is recomended that you construct the subquery
		/// using another instance of <see cref="SelectQuery"/>, render it using the correct 
		/// renderer and pass the resulting SQL statement to the <paramref name="queryText"/> parameter.
		/// </remarks>
		public static OmExpression SubQuery(string queryText)
		{
			OmExpression expr = new OmExpression();
			expr.val = queryText;
			expr.type = OmExpressionType.SubQueryText;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression which represents a subquery.
		/// </summary>
		/// <param name="query">A SelectQuery object</param>
		/// <returns>A new SqlExpression</returns>
		public static OmExpression SubQuery(SelectQuery query)
		{
			OmExpression expr = new OmExpression();
			expr.val = query;
			expr.type = OmExpressionType.SubQueryObject;
			return expr;
		}
		/// <summary>
		/// Create a parameter placeholder.
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns></returns>
		/// <remarks>
		/// Correct parameter name depends on your specifc data provider. OLEDB expects
		/// all parameters to be '?' and matches parameters to values based on their index.
		/// SQL Server Native driver matches parameters by names and expects to find "@paramName"
		/// parameter placeholder in the query.
		/// </remarks>
		public static OmExpression Parameter(string paramName)
		{
			OmExpression expr = new OmExpression();
			expr.val = paramName;
			expr.type = OmExpressionType.Parameter;
			return expr;
		}

		/// <summary>
		/// Creates a SqlExpression with raw SQL
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static OmExpression Raw(string sql)
		{
			OmExpression expr = new OmExpression();
			expr.val = sql;
			expr.type = OmExpressionType.Raw;
			return expr;
		}

    /// <summary></summary>
    public string TableAlias
		{
			get { return (table == null) ? null : table.RefName; }
		}

    /// <summary></summary>
    public OmExpressionType Type
		{
			get { return this.type; }
		}

    /// <summary></summary>
    public object Value
		{
			get { return this.val; }
		}

    /// <summary></summary>
    public OmAggregationFunction AggFunction
		{
			get { return func; }
		}

    /// <summary></summary>
    public CaseClause CaseClause
		{
			get { return caseClause; }
		}

    /// <summary></summary>
    public OmExpression SubExpr1
		{
			get { return subExpr1; }
		}

    /// <summary></summary>
		public OmExpression SubExpr2
		{
			get { return subExpr2; }
		}

  }

}
