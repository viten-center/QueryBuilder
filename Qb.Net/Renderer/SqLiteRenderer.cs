using Viten.QueryBuilder.SqlOm;
using System.Text;
using Viten.QueryBuilder.Culture;

namespace Viten.QueryBuilder.Renderer
{
  public class SqLiteRenderer : SqlOmRenderer
  {
		/// <summary>
		/// Creates a new MySqlRenderer
		/// </summary>
    public SqLiteRenderer()
      : base('[', ']')
		{
		}

    /// <summary>Создает новый объект MySqlRenderer</summary>
    /// <returns></returns>
    public override ISqlOmRenderer CreateNew()
    {
      return new SqLiteRenderer();
    }

    public override string BatchTerminator
    {
      get { return "--GO"; }
    }

		/// <summary>
		/// Renders IfNull OmExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected override void IfNull(StringBuilder builder, OmExpression expr)
		{
			builder.Append("ifnull(");
			Expression(builder, expr.SubExpr1);
			builder.Append(", ");
			Expression(builder, expr.SubExpr2);
			builder.Append(")");
		}

    protected override string GetIdentitySuffix(string identityField)
    {
      return ";select last_insert_rowid()";
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
			
			//Render select columns
			this.SelectColumns(selectBuilder, query.Columns);

			this.FromClause(selectBuilder, query.FromClause, query.TableSpace);
			
			this.Where(selectBuilder, query.WherePhrase);
			this.WhereClause(selectBuilder, query.WherePhrase);

			this.GroupBy(selectBuilder, query.GroupByTerms);
			this.GroupByTerms(selectBuilder, query.GroupByTerms);

			if (query.GroupByWithCube)
				throw new InvalidQueryException("SQLite does not support WITH CUBE modifier.");
			
			if (query.GroupByWithRollup)
				selectBuilder.Append(" with rollup");
			
			this.Having(selectBuilder, query.HavingPhrase) ;
			this.WhereClause(selectBuilder, query.HavingPhrase);

			this.OrderBy(selectBuilder, query.OrderByTerms);
			this.OrderByTerms(selectBuilder, query.OrderByTerms);

			if ((query.Offset > -1 || query.Limit > -1) && query.OrderByTerms.Count == 0)
			{
				throw new InvalidQueryException(SR.Err_OrderByNeedForPage);
			}

			if (query.Limit > -1)
			{
				selectBuilder.AppendFormat(" limit {0}", query.Limit);
				if (query.Offset > -1)
				{
					selectBuilder.AppendFormat(" offset {0}", query.Offset);
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
		//{
		//	string baseSql = RenderSelect(query);

		//	SelectQuery countQuery = new SelectQuery();
		//	SelectColumn col = new SelectColumn("*", null, "cnt", AggFunc.Count);
		//	countQuery.Columns.Add(col);
		//	countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
		//	return RenderSelect(countQuery);
		//}

		/// <summary>
		/// Renders a SELECT statement which a result-set page
		/// </summary>
		/// <param name="pageIndex">The zero based index of the page to be returned</param>
		/// <param name="pageSize">The size of a page</param>
		/// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
		/// <param name="query">Query definition to apply paging on</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// Parameter <paramref name="totalRowCount"/> is ignored.
		/// </remarks>
		//public override string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
		//{
		//	return RenderSelect(query);		
		//}
  }
}
