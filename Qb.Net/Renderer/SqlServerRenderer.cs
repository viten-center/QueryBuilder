using Viten.QueryBuilder.SqlOm;
using System.Text;
using Viten.QueryBuilder.Culture;

namespace Viten.QueryBuilder.Renderer
{
  /// <summary>
  /// Renderer for SqlServer
  /// </summary>
  /// <remarks>
  /// Use SqlServerRenderer to render SQL statements for Microsoft SQL Server database.
  /// This version of Sql.Net has been tested with MSSQL 2000
  /// </remarks>
  public class SqlServerRenderer : SqlOmRenderer
  {
    /// <summary>������ ������� ��� ����������� TOP</summary>
    protected string _topFormat = "top {0} ";
    /// <summary>
    /// Creates a new SqlServerRenderer
    /// </summary>
    public SqlServerRenderer() : base('[', ']')
    {
      //clauseRenderer = new ClauseRenderer(this, );
    }

    /// <summary>������� ����� ������ SqlServerRenderer</summary>
    /// <returns></returns>
    public override ISqlOmRenderer CreateNew()
    {
      return new SqlServerRenderer();
    }

    protected override string GetIdentitySuffix(string identityField)
    {
      return ";select scope_identity()";
    }

    /// <summary>
    /// Renders IfNull OmExpression
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="expr"></param>
    protected override void IfNull(StringBuilder builder, OmExpression expr)
    {
      builder.Append("isnull(");
      Expression(builder, expr.SubExpr1);
      builder.Append(", ");
      Expression(builder, expr.SubExpr2);
      builder.Append(")");
    }

    /// <summary>
    /// Renders a SELECT statement
    /// </summary>
    /// <param name="query">Query definition</param>
    /// <returns>Generated SQL statement</returns>
    public override string RenderSelect(SelectQuery query)
    {
      query.Validate();

      StringBuilder selectBuilder = new StringBuilder();

      //Start the select statement
      this.Select(selectBuilder, query.Distinct);

      //Render Top clause
      //if (query.Top > -1)
      //  selectBuilder.AppendFormat(_topFormat, query.Top);

      //Render select columns
      this.SelectColumns(selectBuilder, query.Columns);

      this.FromClause(selectBuilder, query.FromClause, query.TableSpace);

      this.Where(selectBuilder, query.WherePhrase);
      this.WhereClause(selectBuilder, query.WherePhrase);

      this.GroupBy(selectBuilder, query.GroupByTerms);
      this.GroupByTerms(selectBuilder, query.GroupByTerms);

      if (query.GroupByWithCube)
        selectBuilder.Append(" with cube");
      else if (query.GroupByWithRollup)
        selectBuilder.Append(" with rollup");

      this.Having(selectBuilder, query.HavingPhrase);
      this.WhereClause(selectBuilder, query.HavingPhrase);

      this.OrderBy(selectBuilder, query.OrderByTerms);
      this.OrderByTerms(selectBuilder, query.OrderByTerms);

      if ((query.PageIndex > -1 || query.PageSize > -1) && query.OrderByTerms.Count == 0)
      {
        throw new InvalidQueryException(SR.Err_OrderByNeedForPage);
      }

      if (query.PageSize > -1 || query.PageIndex > 0)
      {
        int offsetRows = query.PageSize * query.PageIndex;
        selectBuilder.AppendFormat(" offset {0} rows fetch next {1} rows only", offsetRows, query.PageSize);
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
    //{
    //  string baseSql = RenderSelect(query);

    //  SelectQuery countQuery = new SelectQuery();
    //  SelectColumn col = new SelectColumn("*", null, "cnt", AggFunc.Count);
    //  countQuery.Columns.Add(col);
    //  countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
    //  return RenderSelect(countQuery);
    //}
    /*
        /// <summary>
        /// Renders a SELECT statement which a result-set page
        /// </summary>
        /// <param name="pageIndex">The zero based index of the page to be returned</param>
        /// <param name="pageSize">The size of a page</param>
        /// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
        /// <param name="query">Query definition to apply paging on</param>
        /// <returns>Generated SQL statement</returns>
        /// <remarks>
        /// To generate pagination SQL on SqlServer 2000 you must supply <paramref name="totalRowCount"/>.
        /// To aquire the total number of rows use the <see cref="RenderRowCount"/> method.
        /// </remarks>
        public override string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
        {
          if (query.OrderByTerms.Count == 0)
            throw new InvalidQueryException("OrderBy must be specified for paging to work on SqlServer.");

          int currentPageSize = pageSize;
          if (pageSize * (pageIndex + 1) > totalRowCount)
            currentPageSize = totalRowCount - pageSize * pageIndex;

          SelectQuery baseQuery = query.Clone();

          baseQuery.Top = (pageIndex + 1) * pageSize;
          baseQuery.Columns.Clear();
          baseQuery.Columns.Add(new SelectColumn("*"));

          string baseSql = RenderSelect(baseQuery);

          SelectQuery reverseQuery = new SelectQuery();
          reverseQuery.Columns.Add(new SelectColumn("*"));
          reverseQuery.Top = currentPageSize;
          reverseQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "r");
          ApplyOrderBy(baseQuery.OrderByTerms, reverseQuery, false, reverseQuery.FromClause.BaseTable.Alias);
          string reverseSql = RenderSelect(reverseQuery);

          SelectQuery forwardQuery = new SelectQuery();
          forwardQuery.Columns.AddRange(query.Columns);
          forwardQuery.Top = currentPageSize;
          forwardQuery.FromClause.BaseTable = FromTerm.SubQuery(reverseSql, "f");
          ApplyOrderBy(baseQuery.OrderByTerms, forwardQuery, true, forwardQuery.FromClause.BaseTable.Alias);

          return RenderSelect(forwardQuery);
        }

        void ApplyOrderBy(OrderByTermCollection terms, SelectQuery orderQuery, bool forward, string tableAlias)
        {
          foreach(OrderByTerm expr in terms)
          {
            OrderByDirection dir = expr.Direction;

            //Reverse order direction if required
            if (!forward && dir == OrderByDirection.Ascending) 
              dir = OrderByDirection.Descending;
            else if (!forward && dir == OrderByDirection.Descending) 
              dir = OrderByDirection.Ascending;

            orderQuery.OrderByTerms.Add(new OrderByTerm(expr.Field.ToString(), FromTerm.TermRef(tableAlias) , dir));
          }
        }
    */
  }
}
