using System.Text;
using Viten.QueryBuilder.Culture;
using Viten.QueryBuilder.SqlOm;


namespace Viten.QueryBuilder.Renderer
{
  public class PostgreSqlRenderer : SqlOmRenderer
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

    /// <summary>
    /// Renders a comaprison operator
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="op"></param>
    protected override void Operator(StringBuilder builder, CompCond op)
    {
      if (op == CompCond.Equal)
        builder.Append("=");
      else if (op == CompCond.NotEqual)
        builder.Append("<>");
      else if (op == CompCond.Greater)
        builder.Append(">");
      else if (op == CompCond.Less)
        builder.Append("<");
      else if (op == CompCond.LessOrEqual)
        builder.Append("<=");
      else if (op == CompCond.GreaterOrEqual)
        builder.Append(">=");
      else if (op == CompCond.Like)
        builder.Append("ilike");
      else if (op == CompCond.NotLike)
        builder.Append("not ilike");
      else
        throw new InvalidQueryException("Unkown operator: " + op.ToString());
    }


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

      this.Having(selectBuilder, query.HavingPhrase);
      this.WhereClause(selectBuilder, query.HavingPhrase);

      this.OrderBy(selectBuilder, query.OrderByTerms);
      this.OrderByTerms(selectBuilder, query.OrderByTerms);

      if ((query.PageIndex > -1 || query.PageSize > -1) && query.OrderByTerms.Count == 0)
      {
        throw new InvalidQueryException(SR.Err_OrderByNeedForPage);
      }

      if (query.PageSize > -1)
      {
        selectBuilder.AppendFormat(" limit {0}", query.PageSize);
        if (query.PageIndex > 0)
        {
          int offsetRows = query.PageSize * query.PageIndex;
          selectBuilder.AppendFormat(" offset {0}", offsetRows);
        }
      }
      return selectBuilder.ToString();
    }

    //public override string RenderRowCount(SelectQuery query)
    //{
    //  string baseSql = RenderSelect(query);

    //  SelectQuery countQuery = new SelectQuery();
    //  SelectColumn col = new SelectColumn("*", null, "cnt", AggFunc.Count);
    //  countQuery.Columns.Add(col);
    //  countQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "t");
    //  return RenderSelect(countQuery);
    //}

    //public override string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
    //{
    //  //return RenderSelect(query, false, pageIndex * pageSize, pageSize);
    //  return RenderSelect(query);
    //}

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
