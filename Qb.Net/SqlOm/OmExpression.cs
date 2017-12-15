using System;

namespace Viten.QueryBuilder.SqlOm
{
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
    public OmExpressionType Type { get; set; }
    public AggFunc AggFunction { get; set; }
    public CaseClause CaseClause
    {
      get { return caseClause; }
    }
    public OmExpression SubExpr1 { get; set; }

		public OmExpression SubExpr2 { get; set; }

    public FromTerm Table { get; set; }

    public ExprValCode ValueCode { get; set; }
    public string StringValue { get; set; }
    public OmConstant ConstantValue { get; set; }
    public SelectQuery QueryValue { get; set; }

    /// <summary></summary>
    public object Value
    {
      get
      {
        switch (ValueCode)
        {
          case ExprValCode.SelQuery:
            return QueryValue;
          case ExprValCode.SqlConst:
            return ConstantValue;
          default:
            return StringValue;
        }
      }
    }

    CaseClause caseClause = new CaseClause();

    public OmExpression()
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
      expr.ValueCode = ExprValCode.SqlConst;
      expr.ConstantValue = val;
      expr.Type = OmExpressionType.Constant;
      return expr;
    }

    /// <summary>
    /// Creates a SqlExpression which represents a constant typed value
    /// </summary>
    /// <param name="dataType">Value's data type</param>
    /// <param name="val">The value</param>
    /// <returns></returns>
    public static OmExpression Constant(DataType dataType, object val)
    {
      OmExpression expr = new OmExpression();
      expr.ValueCode = ExprValCode.SqlConst;
      expr.ConstantValue = new OmConstant(dataType, val);
      expr.Type = OmExpressionType.Constant;
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
      expr.StringValue = fieldName;
      expr.ValueCode = ExprValCode.String;
      expr.Table = table;
      expr.Type = OmExpressionType.Field;
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
      expr.Type = OmExpressionType.Case;
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
      expr.Type = OmExpressionType.IfNull;
      expr.SubExpr1 = test;
      expr.SubExpr2 = val;
      return expr;
    }

    /// <summary>
    /// Creates a SqlExpression with an aggergation function
    /// </summary>
    /// <param name="func">Aggregation function to be applied on the supplied expression</param>
    /// <param name="param">Parameter of the aggregation function</param>
    /// <returns></returns>
    public static OmExpression Function(AggFunc func, OmExpression param)
    {
      OmExpression expr = new OmExpression();
      expr.Type = OmExpressionType.Function;
      expr.SubExpr1 = param;
      expr.AggFunction = func;
      return expr;
    }

    /// <summary>
    /// Creates a SqlExpression representing a NULL value
    /// </summary>
    /// <returns></returns>
    public static OmExpression Null()
    {
      OmExpression expr = new OmExpression();
      expr.Type = OmExpressionType.Null;
      return expr;
    }

    /// <summary></summary>
    public static OmExpression PseudoField(string fieldName)
    {
      OmExpression expr = new OmExpression();
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = fieldName;
      expr.Type = OmExpressionType.PseudoField;
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
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = queryText;
      expr.Type = OmExpressionType.SubQueryText;
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
      expr.ValueCode = ExprValCode.SelQuery;
      expr.QueryValue = query;
      expr.Type = OmExpressionType.SubQueryObject;
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
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = paramName;
      expr.Type = OmExpressionType.Parameter;
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
      expr.ValueCode = ExprValCode.String;
      expr.StringValue = sql;
      expr.Type = OmExpressionType.Raw;
      return expr;
    }

    /// <summary></summary>
    public string TableAlias
    {
      get { return (Table == null) ? null : Table.RefName; }
    }

  }

}
