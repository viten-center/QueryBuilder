using System.Text;
using Viten.QueryBuilder.SqlOm;


namespace Viten.QueryBuilder.Renderer
{
  public class PostgreSqlRenderer: SqlOmRenderer
  {
    public PostgreSqlRenderer() 
      : base('"', '"')
    {
    }

    public override string BatchTerminator
    {
      get { return "--GO"; }
    }

    public override ISqlOmRenderer CreateNew()
    {
      return new PostgreSqlRenderer();
    }

    public override string RenderSelect(SelectQuery query)
    {
      return RenderSelect(query, false, 0, query.Top);
    }


    string RenderSelect(SelectQuery query, bool forRowCount, int offset, int limitRows)
    {
      query.Validate();

      StringBuilder selectBuilder = new StringBuilder();

      //Start the select statement
      this.Select(selectBuilder, query.Distinct);

      //Render select columns
      if (forRowCount)
        this.SelectColumn(selectBuilder, new SelectColumn("*", null, "cnt", OmAggregationFunction.Count));
      else
        this.SelectColumns(selectBuilder, query.Columns);

      this.FromClause(selectBuilder, query.FromClause, query.TableSpace);

      this.Where(selectBuilder, query.WherePhrase);
      this.WhereClause(selectBuilder, query.WherePhrase);

      this.GroupBy(selectBuilder, query.GroupByTerms);
      this.GroupByTerms(selectBuilder, query.GroupByTerms);

      /*
      if (query.GroupByWithCube)
        throw new InvalidQueryException("MySql does not support WITH CUBE modifier.");

      if (query.GroupByWithRollup)
        selectBuilder.Append(" with rollup");
      */

      this.Having(selectBuilder, query.HavingPhrase);
      this.WhereClause(selectBuilder, query.HavingPhrase);

      this.OrderBy(selectBuilder, query.OrderByTerms);
      this.OrderByTerms(selectBuilder, query.OrderByTerms);

      if (limitRows > -1)
      {
        selectBuilder.AppendFormat(" limit {0}", limitRows);
        if(offset > 0)
          selectBuilder.AppendFormat(" offset {0}", offset);
      }
      return selectBuilder.ToString();
    }

    public override string RenderRowCount(SelectQuery query)
    {
      string baseSql = RenderSelect(query);

      SelectQuery countQuery = new SelectQuery();
      SelectColumn col = new SelectColumn("*", null, "cnt", OmAggregationFunction.Count);
      countQuery.Columns.Add(col);
      countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
      return RenderSelect(countQuery);
    }

    protected override string GetIdentitySuffix(string identityField)
    {
      return string.Format(" returning {0}", GetIdentifier(identityField));
    }

    protected override void IfNull(StringBuilder builder, OmExpression expr)
    {
      builder.Append("coalesce(");
      Expression(builder, expr.SubExpr1);
      builder.Append(", ");
      Expression(builder, expr.SubExpr2);
      builder.Append(")");
    }
  }
}
