using Viten.QueryBuilder.SqlOm;
using System.Text;
using Viten.QueryBuilder.Culture;

namespace Viten.QueryBuilder.Renderer
{
  /// <summary>
  /// Renderer for Oracle
  /// </summary>
  /// <remarks>
  /// Use OracleRenderer to render SQL statements for Oracle database.
  /// This version of Sql.Net has been tested with Oracle 9i.
  /// </remarks>
  public class OracleRenderer : SqlOmRenderer
  {
    /// <summary>Префикс имени параметра</summary>
    public override char ParameterPrefix
    {
      get
      {
        return ':';
      }
    }

    /// <summary>
    /// Creates a new instance of OracleRenderer
    /// </summary>
    public OracleRenderer() : base('"', '"')
    {
      DateFormat = "dd-MMM-yy";
      DateTimeFormat = "dd-MMM-yy HH:mm:ss";
    }

    protected override string GetIdentitySuffix(string identityField)
    {
      return string.Format(" returning {0} into {1}{0}", identityField, ParameterPrefix);
    }

    /// <summary>Создает новый объект OracleRenderer</summary>
    /// <returns></returns>
    public override ISqlOmRenderer CreateNew()
    {
      return new OracleRenderer();
    }

    /// <summary>
    /// Renders IfNull OmExpression
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="expr"></param>
    protected override void IfNull(StringBuilder builder, OmExpression expr)
    {
      builder.Append("nvl(");
      Expression(builder, expr.SubExpr1);
      builder.Append(", ");
      Expression(builder, expr.SubExpr2);
      builder.Append(")");
    }

    /// <summary>
    /// Returns true. 
    /// </summary>
    protected override bool UpperCaseIdentifiers
    {
      get { return true; }
    }

    /// <summary>
    /// Renders bitwise and
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="term"></param>
    protected override void BitwiseAnd(StringBuilder builder, WhereTerm term)
    {
      builder.Append("BITAND(");
      Expression(builder, term.Expr1);
      builder.Append(", ");
      Expression(builder, term.Expr2);
      builder.Append(") > 0");
    }

    /// <summary>
    /// Renders a SELECT statement
    /// </summary>
    /// <param name="query">Query definition</param>
    /// <returns>Generated SQL statement</returns>
    //public override string RenderSelect(SelectQuery query)
    //{
    //	if (query.Top > -1 && query.OrderByTerms.Count > 0)
    //	{
    //		string baseSql = RenderSelect(query, -1);

    //		SelectQuery countQuery = new SelectQuery();
    //		SelectColumn col = new SelectColumn("*");
    //		countQuery.Columns.Add(col);
    //		countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
    //		return RenderSelect(countQuery, query.Top);
    //	}
    //	else
    //		return RenderSelect(query, query.Top);
    //}

    public override string RenderSelect(SelectQuery query)
    {
      query.Validate();

      StringBuilder selectBuilder = new StringBuilder();

      //Start the select statement
      this.Select(selectBuilder, query.Distinct);

      //Render select columns
      this.SelectColumns(selectBuilder, query.Columns);

      this.FromClause(selectBuilder, query.FromClause, query.TableSpace);

      WhereClause fullWhereClause = new WhereClause(WhereRel.And);
      fullWhereClause.SubClauses.Add(query.WherePhrase);

      this.Where(selectBuilder, fullWhereClause);
      this.WhereClause(selectBuilder, fullWhereClause);

      this.GroupBy(selectBuilder, query.GroupByTerms);
      if (query.GroupByWithCube)
        selectBuilder.Append(" cube (");
      else if (query.GroupByWithRollup)
        selectBuilder.Append(" rollup (");
      this.GroupByTerms(selectBuilder, query.GroupByTerms);

      if (query.GroupByWithCube || query.GroupByWithRollup)
        selectBuilder.Append(" )");

      this.Having(selectBuilder, query.HavingPhrase);
      this.WhereClause(selectBuilder, query.HavingPhrase);

      this.OrderBy(selectBuilder, query.OrderByTerms);
      this.OrderByTerms(selectBuilder, query.OrderByTerms);

      if ((query.Offset > -1 || query.Limit > -1) && query.OrderByTerms.Count == 0)
      {
        throw new InvalidQueryException(SR.Err_OrderByNeedForPage);
      }


      if (query.Offset > -1)
      {
        selectBuilder.AppendFormat(" offset {0} rows", query.Offset);
        if (query.Limit > -1)
        {
          selectBuilder.AppendFormat(" fetch next {0} rows only", query.Limit);
        }
      }

      return selectBuilder.ToString();
    }

    /// <summary>
    /// Renders a row count SELECT statement. 
    /// </summary>
    /// <param name="query">Query definition to count rows for</param>
    /// <returns>Generated SQL statement</returns>
    /// <remarks>
    /// Renders a SQL statement which returns a result set with one row and one cell which contains the number of rows <paramref name="query"/> can generate. 
    /// The generated statement will work nicely with <see cref="System.Data.IDbCommand.ExecuteScalar"/> method.
    /// </remarks>
    //public override string RenderRowCount(SelectQuery query)
    //  {
    //    string baseSql = RenderSelect(query);

    //    SelectQuery countQuery = new SelectQuery();
    //    SelectColumn col = new SelectColumn("*", null, "cnt", AggFunc.Count);
    //    countQuery.Columns.Add(col);
    //    countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
    //    return RenderSelect(countQuery);
    //  }
  }
}
