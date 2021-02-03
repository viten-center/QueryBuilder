using Viten.QueryBuilder.Culture;
using Viten.QueryBuilder.SqlOm;
using System;
using System.Text;

namespace Viten.QueryBuilder.Renderer
{
    /// <summary>
    /// Provides common implementation for ISqlOmRenderer
    /// </summary>
    public abstract class SqlOmRenderer : ISqlOmRenderer //, IClauseRendererContext
	{
		string dateFormat = "yyyy-MM-dd";
		string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
		char identifierOpeningQuote, identifierClosingQuote;
    /// <summary>������� ����� ���������</summary>
    public virtual char ParameterPrefix
    {
      get
      {
        return '@';
      }
    }

	  public string CreateParameterName(string name)
	  {
      if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
	    if (name[0] != ParameterPrefix)
	      return string.Format("{0}{1}", ParameterPrefix, name);
	    return name;
	  }

    public virtual string BatchTerminator
    {
      get { return "GO"; }
    }

    public string GetIdentifier(string objName)
    {
      return string.Format("{0}{1}{2}", identifierOpeningQuote, objName, identifierClosingQuote);
    }
		/// <summary>
		/// Creates a new SqlOmRenderer
		/// </summary>
		public SqlOmRenderer(char identifierOpeningQuote , char identifierClosingQuote)
		{
			this.identifierOpeningQuote = identifierOpeningQuote;
			this.identifierClosingQuote = identifierClosingQuote;
		}

    public char OpeningQuote
    {
      get { return identifierOpeningQuote; }
    }

    public char ClosingQuote
    {
      get { return identifierClosingQuote; }
    }


    /// <summary>������� ����� ������ ISqlOmRenderer</summary>
    /// <returns></returns>
    public abstract ISqlOmRenderer CreateNew();

		/// <summary>
		/// Gets or sets a date format string
		/// </summary>
		/// <remarks>
		/// Use <see cref="DateFormat"/> to specify how date values should be formatted
		/// in order to be properly parsed by your database.
		/// Specific renderers set this property to the appliciable default value, so you
		/// only need to change this if your database is configured to use other then default date format.
		/// <para>
		/// DateFormat will be used to format <see cref="DateTime"/> values which have the Hour, Minute, Second and Milisecond properties set to 0.
		/// Otherwise, <see cref="DateTimeFormat"/> will be used.
		/// </para>
		/// </remarks>
		public string DateFormat
		{
			get { return dateFormat; }
			set { dateFormat = value; }
		}

		/// <summary>
		/// Gets or sets a date-time format string
		/// </summary>
		/// <remarks>
		/// Use <see cref="DateTimeFormat"/> to specify how timestamp values should be formatted
		/// in order to be properly parsed by your database.
		/// Specific renderers set this property to the appliciable default value, so you
		/// only need to change this if your database is configured to use other then default date format.
		/// </remarks>
		public string DateTimeFormat
		{
			get { return dateTimeFormat; }
			set { dateTimeFormat = value; }
		}

		/// <summary>
		/// Renders a SELECT statement
		/// </summary>
		/// <param name="query">Query definition</param>
		/// <returns>Generated SQL statement</returns>
		public abstract string RenderSelect(SelectQuery query);

		/// <summary>
		/// Renders a row count SELECT statement. 
		/// </summary>
		/// <param name="query">Query definition to count rows for</param>
		/// <returns>Generated SQL statement</returns>
		public abstract string RenderRowCount(SelectQuery query);

    protected abstract string GetIdentitySuffix(string identityField);

		/// <summary>
		/// Specifies weather all identifiers should be converted to upper case while rendering
		/// </summary>
		protected virtual bool UpperCaseIdentifiers { get {  return false; } }
		//bool IClauseRendererContext.UpperCaseIdentifiers { get {  return this.UpperCaseIdentifiers; } }

		/// <summary>
		/// Renders an UPDATE statement
		/// </summary>
		/// <param name="query">UPDATE query definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderUpdate(UpdateQuery query)
		{
			return UpdateStatement(query);
		}

		/// <summary>
		/// Renders an INSERT statement
		/// </summary>
		/// <param name="query">INSERT query definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderInsert(InsertQuery query)
		{
      if(!string.IsNullOrEmpty(query.IdentityField))
        return InsertStatement(query) + GetIdentitySuffix(query.IdentityField);
      else
        return InsertStatement(query);
		}

    /// <summary>
    /// Renders an INSERT INTO ... SELECT statement
    /// </summary>
    /// <param name="query">INSERT INTO ... SELECT query definition</param>
    /// <returns>Generated SQL statement</returns>
    public virtual string RenderInsertSelect(InsertSelectQuery query)
    {
      return InsertSelectStatement(query);
    }

		/// <summary>
		/// Renders an DELETE statement
		/// </summary>
		/// <param name="query">DELETE query definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderDelete(DeleteQuery query)
		{
			return DeleteStatement(query);
		}

		
		/// <summary>
		/// Renders a UNION clause
		/// </summary>
		/// <param name="union">Union definition</param>
		/// <returns>Generated SQL statement</returns>
		public virtual string RenderUnion(OmUnion union)
		{
			StringBuilder builder = new StringBuilder();
			foreach(OmUnionItem item in union.Items)
			{
				if (item != union.Items[0])
					builder.AppendFormat(" union {0} ", (item.RepeatingAction == UnionMod.All) ? "all" : "");
				builder.Append(RenderSelect(item.Query));
			}
			return builder.ToString();
		}

		/// <summary>
		/// Renders a SELECT statement which a result-set page
		/// </summary>
		/// <param name="pageIndex">The zero based index of the page to be returned</param>
		/// <param name="pageSize">The size of a page</param>
		/// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
		/// <param name="query">Query definition to apply paging on</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// To generate pagination SQL you must supply <paramref name="totalRowCount"/>.
		/// To aquire the total number of rows use the <see cref="RenderRowCount"/> method.
		/// </remarks>
		public virtual string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query)
		{

			if (query.OrderByTerms.Count == 0)
				throw new InvalidQueryException(SR.Err_OrderByNeedForPage);

			int currentPageSize = pageSize;
			if (pageSize * (pageIndex + 1) > totalRowCount)
				currentPageSize = totalRowCount - pageSize * pageIndex;
			if (currentPageSize < 0)
				currentPageSize = 0;
				
			SelectQuery baseQuery = query.Clone();
			
			baseQuery.Top = (pageIndex + 1) * pageSize;
			//baseQuery.Columns.Add(new SelectColumn("*"));
			foreach(OrderByTerm term in baseQuery.OrderByTerms)
				baseQuery.Columns.Add(new SelectColumn(term.Field, term.Table, FormatSortFieldName(term.Field), AggFunc.None));

			string baseSql = RenderSelect(baseQuery);
			
			SelectQuery reverseQuery = new SelectQuery();
			reverseQuery.Columns.Add(new SelectColumn("*"));
			reverseQuery.Top = currentPageSize;
			reverseQuery.FromClause.BaseTable = FromTerm.SubQuery(baseSql, "r");
			ApplyOrderBy(baseQuery.OrderByTerms, reverseQuery, false, reverseQuery.FromClause.BaseTable);
			string reverseSql = RenderSelect(reverseQuery);

			SelectQuery forwardQuery = new SelectQuery();
			foreach(SelectColumn originalCol in query.Columns)
			{
				FromTerm forwardTable = FromTerm.TermRef("f");
				OmExpression expr = null;
				if (originalCol.ColumnAlias != null)
					expr = OmExpression.Field(originalCol.ColumnAlias, forwardTable);
				else if (	originalCol.Expression.Type == OmExpressionType.Field || 
					originalCol.Expression.Type == OmExpressionType.Constant)
				{
					expr = OmExpression.Field((string)originalCol.Expression.Value, forwardTable);
				}

				if (expr != null)
					forwardQuery.Columns.Add(new SelectColumn(expr, originalCol.ColumnAlias));
			}

			forwardQuery.FromClause.BaseTable = FromTerm.SubQuery(reverseSql, "f");
			ApplyOrderBy(baseQuery.OrderByTerms, forwardQuery, true, forwardQuery.FromClause.BaseTable);

			return RenderSelect(forwardQuery);
		}

		string FormatSortFieldName(string fieldName)
		{
			return "sort_" + fieldName;
		}

		void ApplyOrderBy(OrderByTermCollection terms, SelectQuery orderQuery, bool forward, FromTerm table)
		{
			foreach(OrderByTerm expr in terms)
			{
        OrderByDir dir = expr.Direction;
				
				//Reverse order direction if required
				if (!forward && dir == OrderByDir.Asc) 
					dir = OrderByDir.Desc;
				else if (!forward && dir == OrderByDir.Desc) 
					dir = OrderByDir.Asc;
					
				orderQuery.OrderByTerms.Add(new OrderByTerm(FormatSortFieldName(expr.Field.ToString()), table , dir));
			}
		}

		//protected abstract void SelectStatement(StringBuilder builder);

		/// <summary>
		/// Renders a the beginning of a SELECT clause with an optional DISTINCT setting
		/// </summary>
		/// <param name="builder">Select statement string builder</param>
		/// <param name="distinct">Turns on or off SQL distinct option</param>
		protected virtual void Select(StringBuilder builder, bool distinct)
		{
			builder.Append("select ");
			if (distinct)
				builder.Append("distinct ");
		}
		
		/// <summary>
		/// Renders columns of SELECT clause
		/// </summary>
		protected virtual void SelectColumns(StringBuilder builder, SelectColumnCollection columns)
		{
			foreach(SelectColumn col in columns)
			{
				if (col != columns[0])
					Coma(builder);

				SelectColumn(builder, col);
			}
		}
		
		/// <summary>
		/// Renders a sinle select column
		/// </summary>
		protected virtual void SelectColumn(StringBuilder builder, SelectColumn col)
		{
			Expression(builder, col.Expression);
			if (col.ColumnAlias != null)
			{
				builder.Append(" ");
				Identifier(builder, col.ColumnAlias);
			}
		}
		
		/// <summary>
		/// Renders a separator between select columns
		/// </summary>
		protected virtual void Coma(StringBuilder builder)
		{
			builder.Append(", ");
		}

		/// <summary>
		/// Renders the begining of a FROM clause
		/// </summary>
		/// <param name="builder"></param>
		protected virtual void From(StringBuilder builder)
		{
			builder.Append(" from ");
		}

		/// <summary>
		/// Renders the terms of a from clause
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="fromClause"></param>
		/// <param name="tableSpace">Common prefix for all tables in the clause</param>
		protected virtual void FromClause(StringBuilder builder, FromClause fromClause, string tableSpace)
		{
			From(builder);
			RenderFromTerm(builder, fromClause.BaseTable, tableSpace);

			foreach(Join join in fromClause.Joins)
			{
				builder.AppendFormat(" {0} join ", join.Type.ToString().ToLower());
				RenderFromTerm(builder, join.RightTable, tableSpace);
			
				if (join.Type != JoinType.Cross)
				{
					builder.AppendFormat(" on ");
					WhereClause(builder, join.Conditions);
				}
			}
		}
		
		/// <summary>
		/// Renders a single FROM term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="table"></param>
		/// <param name="tableSpace">Common prefix for all tables in the term</param>
		protected virtual void RenderFromTerm(StringBuilder builder, FromTerm table, string tableSpace)
		{
			if (table.Type == FromTermType.Table)
			{
				if (table.Ns1 != null)
					TableNamespace(builder, table.Ns1);
				if (table.Ns2 != null)
					TableNamespace(builder, table.Ns2);
				if (table.Ns1 == null && table.Ns2 == null && tableSpace != null)
					TableNamespace(builder, tableSpace);
				Identifier(builder, (string)table.Expression);
			}
			else if (table.Type == FromTermType.SubQuery)
				builder.AppendFormat("( {0} )", table.Expression);
			else if (table.Type == FromTermType.SubQueryObj)
				builder.AppendFormat("( {0} )", RenderSelect((SelectQuery)table.Expression));
      else if(table.Type == FromTermType.Union)
        builder.AppendFormat("( {0} )", RenderUnion((OmUnion)table.Expression));
			else 
				throw new InvalidQueryException("Unknown FromExpressionType: " + table.Type.ToString());
			
			if (table.Alias != null)
			{
				builder.AppendFormat(" ");
				Identifier(builder, table.Alias);
			}
		}

		/// <summary>
		/// Renders the table namespace
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="ns"></param>
		protected virtual void TableNamespace(StringBuilder builder, string ns)
		{
			builder.AppendFormat("{0}.", ns);
		}

		/// <summary>
		/// Renders the begining of a WHERE statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void Where(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat(" where ");
		}

		/// <summary>
		/// Renders the begining of a HAVING statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void Having(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat(" having ");
		}

		/// <summary>
		/// Recursivly renders a WhereClause
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="group"></param>
		protected virtual void WhereClause(StringBuilder builder, WhereClause group)
		{
			if (group.IsEmpty)
				return;

			builder.AppendFormat("(");
			
			for(int i = 0; i < group.Terms.Count; i++)
			{
				if (i > 0)
					RelationshipOperator(builder, group.Relationship);

				WhereTerm term = (WhereTerm)group.Terms[i];
				WhereClause(builder, term);
			}

			bool operatorRequired = group.Terms.Count > 0;
			foreach(WhereClause childGroup in group.SubClauses)
			{
				if (childGroup.IsEmpty)
					continue;

				if (operatorRequired)
					RelationshipOperator(builder, group.Relationship);

				WhereClause(builder, childGroup);
				operatorRequired = true;
			}

			builder.AppendFormat(")");
		}

		/// <summary>
		/// Renders bitwise and
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void BitwiseAnd(StringBuilder builder, WhereTerm term)
		{
			builder.Append("(");
			Expression(builder, term.Expr1);
			builder.Append(" & ");
			Expression(builder, term.Expr2);
			builder.Append(") > 0");
		}

		/// <summary>
		/// Renders a single WhereTerm
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void WhereClause(StringBuilder builder, WhereTerm term)
		{
			if (term.Type == WhereTermType.Compare && term.Op == CompCond.BitwiseAnd)
				BitwiseAnd(builder, term);
			else if (term.Type == WhereTermType.Compare)
			{
				Expression(builder, term.Expr1);
				builder.Append(" ");
				Operator(builder, term.Op);
				builder.Append(" ");
				Expression(builder, term.Expr2);
        if (term.Op == CompCond.Like && term.Expr3 != null)
          builder.AppendFormat(" escape '{0}'", Convert.ToString(((OmConstant)term.Expr3.Value).Value));
			}
			else if (term.Type == WhereTermType.In || term.Type == WhereTermType.NotIn || term.Type == WhereTermType.InSubQuery || term.Type == WhereTermType.NotInSubQuery)
			{
				Expression(builder, term.Expr1);
				if (term.Type == WhereTermType.NotIn || term.Type == WhereTermType.NotInSubQuery)
					builder.Append(" not");
				builder.Append(" in (");
        if (term.Type == WhereTermType.InSubQuery || term.Type == WhereTermType.NotInSubQuery)
        {
          if (term.SubQuery is SelectQuery)
          {
            ISqlOmRenderer innerRender = this.CreateNew();
            builder.Append(innerRender.RenderSelect(term.SubQuery as SelectQuery));
          }
          else if (term.SubQuery is string)
            builder.Append((string)term.SubQuery);
          else
            throw new InvalidQueryException(SR.Err_SubQueryUnavelable);
        }
        else
          ConstantList(builder, term.Values);
				builder.Append(")");
			}
			else if (term.Type == WhereTermType.Exists || term.Type == WhereTermType.NotExists)
			{
				if (term.Type == WhereTermType.NotExists)
					builder.Append(" not");
				builder.Append(" exists (");
        if (term.SubQuery is SelectQuery)
        {
          ISqlOmRenderer innerRender = this.CreateNew();
          builder.Append(innerRender.RenderSelect(term.SubQuery as SelectQuery));
        }
        else if (term.SubQuery is string)
          builder.Append((string)term.SubQuery);
        else
          throw new InvalidQueryException(
#if !TEST
            SR.Err_SubQueryUnavelable
#else
            ""
#endif
            );
        builder.Append(")");
			}
			else if (term.Type == WhereTermType.Between)
			{
				Expression(builder, term.Expr1);
				builder.AppendFormat(" between ");
				Expression(builder, term.Expr2);
				builder.AppendFormat(" and ");
				Expression(builder, term.Expr3);
			}
			else if (term.Type == WhereTermType.IsNull || term.Type == WhereTermType.IsNotNull)
			{
				Expression(builder, term.Expr1);
				builder.Append(" is "); 
				if (term.Type == WhereTermType.IsNotNull)
					builder.Append("not ");
				builder.Append(" null ");
			}

		}

		/// <summary>
		/// Renders IfNull OmExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected abstract void IfNull(StringBuilder builder, OmExpression expr);

		/// <summary>
		/// Renders OmExpression
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected virtual void Expression(StringBuilder builder, OmExpression expr)
		{
			OmExpressionType type = expr.Type;
			if (type == OmExpressionType.Field)
				QualifiedIdentifier(builder, expr.TableAlias, expr.Value.ToString());
			else if (type == OmExpressionType.Function)
				Function(builder, expr.AggFunction, expr.SubExpr1);
			else if (type == OmExpressionType.Constant)
				Constant(builder, (OmConstant)expr.Value);
			else if (type == OmExpressionType.SubQueryText)
				builder.AppendFormat("({0})", (string)expr.Value);
			else if (type == OmExpressionType.SubQueryObject)
				builder.AppendFormat("({0})", RenderSelect((SelectQuery)expr.Value));
			else if (type == OmExpressionType.PseudoField)
				builder.AppendFormat("{0}", (string)expr.Value);
			else if (type == OmExpressionType.Parameter)
				builder.AppendFormat("{0}{1}", ParameterPrefix, (string)expr.Value);
			else if (type == OmExpressionType.Raw)
				builder.AppendFormat("{0}", (string)expr.Value);
			else if (type == OmExpressionType.Case)
				CaseClause(builder, expr.CaseClause);
			else if (type == OmExpressionType.IfNull)
				IfNull(builder, expr);
			else if (type == OmExpressionType.Null)
				builder.Append("null");
			else
				throw new InvalidQueryException("Unkown expression type: " + type.ToString());
		}

		/// <summary>
		/// Renders a OmExpression of type Function 
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="func"></param>
		/// <param name="param"></param>
		protected virtual void Function(StringBuilder builder, AggFunc func, OmExpression param)
		{
			builder.AppendFormat("{0}(", func.ToStringFast());// .ToString());
			Expression(builder, param);
			builder.AppendFormat(")");
		}

		/// <summary>
		/// Renders a constant
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expr"></param>
		protected virtual void Constant(StringBuilder builder, OmConstant expr)
		{
			DataType type = expr.Type;

			if (type == DataType.Number)
        builder.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0}", expr.Value);
      else if (type == DataType.String)
      {
        if (expr.Value == null)
          builder.Append("null");
        else
          builder.AppendFormat("'{0}'", expr.Value.ToString());
      }
      else if (type == DataType.Date)
      {
        DateTime val = (DateTime)expr.Value;
        //������ �������������� �.�. �� ��������� ������������� ������ ����
        //��������: 2000-01-31 00:00:00 (�������� �������� ���������)
        //������������� ��� 2000-01-31 - ��� �������� � ������������ ����������� ��� ���������
        //�������� Sqlite ���������� ������ � "2000-01-31 00:00:00" != "2000-01-31"
        //���� ������� �����
        //bool dateOnly = (val.Hour == 0 && val.Minute == 0 && val.Second == 0 && val.Millisecond == 0);
        bool dateOnly = false;
        string format = (dateOnly) ? dateFormat : dateTimeFormat;
        builder.AppendFormat("'{0}'", val.ToString(format));
      }
		}
		
		/// <summary>
		/// Renders a comaprison operator
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="op"></param>
		protected virtual void Operator(StringBuilder builder, CompCond op)
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
				builder.Append("like");
      else if (op == CompCond.NotLike)
        builder.Append("not like");
      else
				throw new InvalidQueryException("Unkown operator: " + op.ToString());
		}
		
		/// <summary>
		/// Renders a list of values
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		protected virtual void ConstantList(StringBuilder builder, OmConstantCollection values)
		{
			for(int i = 0; i < values.Count; i++)
			{
				OmConstant val = values[i];
				Constant(builder, val);
				if (i != values.Count - 1)
					Coma(builder);
			}
		}

		/// <summary>
		/// Encodes a textual string.
		/// </summary>
		/// <param name="val">Text to be encoded</param>
		/// <returns>Encoded text</returns>
		/// <remarks>All text string must be encoded before they are appended to a SQL statement.</remarks>
		public virtual string SqlEncode(string val)
		{
			return val.Replace("'", "''");
		}

		/// <summary>
		/// Renders a relationship operator
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="relationship"></param>
		protected virtual void RelationshipOperator(StringBuilder builder, WhereRel relationship)
		{
			builder.AppendFormat(" {0} ", relationship.ToStringFast());//.ToString().ToLower());
		}

		/// <summary>
		/// Renders the begining of a GROUP BY statement.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="groupByTerms"></param>
		/// <remarks>If <paramref name="groupByTerms"/> has no items, nothing will be appended.</remarks>
		protected virtual void GroupBy(StringBuilder builder, GroupByTermCollection groupByTerms)
		{
			if (groupByTerms.Count > 0)
				builder.Append(" group by ");
		}
		
		/// <summary>
		/// Renders GROUP BY terms 
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="groupByTerms"></param>
		protected virtual void GroupByTerms(StringBuilder builder, GroupByTermCollection groupByTerms)
		{
			foreach(GroupByTerm clause in groupByTerms)
			{
				if (clause != groupByTerms[0])
					builder.Append(", ");
				
				GroupByTerm(builder, clause);
			}
		}
		
		/// <summary>
		/// Renders a single GROUP BY term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void GroupByTerm(StringBuilder builder, GroupByTerm term)
		{
			QualifiedIdentifier(builder, term.TableAlias, term.Field);
		}

		/// <summary>
		/// Renders the begining of a ORDER BY statement.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="orderByTerms"></param>
		/// <remarks>If <paramref name="orderByTerms"/> has no items, nothing will be appended.</remarks>
		protected virtual void OrderBy(StringBuilder builder, OrderByTermCollection orderByTerms)
		{
			if (orderByTerms.Count > 0)
				builder.Append(" order by ");
		}

		/// <summary>
		/// Renders ORDER BY terms
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="orderByTerms"></param>
		protected virtual void OrderByTerms(StringBuilder builder, OrderByTermCollection orderByTerms)
		{
			for(int i = 0; i < orderByTerms.Count; i++)
			{
				OrderByTerm term = (OrderByTerm)orderByTerms[i];
				if (i > 0)
					builder.Append(", ");
				
				OrderByTerm(builder, term);
			}
		}
		
		/// <summary>
		/// Renders a single ORDER BY term
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void OrderByTerm(StringBuilder builder, OrderByTerm term)
		{
			string dir = (term.Direction == OrderByDir.Desc) ? "desc" : "asc";
			QualifiedIdentifier(builder, term.TableAlias, term.Field);
			builder.AppendFormat(" {0}", dir);
		}

		/// <summary>
		/// Renders an identifier name.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="name">Identifier name</param>
		protected virtual void Identifier(StringBuilder builder, string name)
		{
			if (name == "*")
				builder.Append(name);
			else
			{
				if (UpperCaseIdentifiers)
					name = name.ToUpper();
				builder.AppendFormat("{0}{1}{2}", identifierOpeningQuote, name, identifierClosingQuote);
			}
		}

		/// <summary>
		/// Renders a fully qualified identifer.
		/// </summary>
		/// <param name="builder">Select statement string builder</param>
		/// <param name="qnamespace">Identifier namespace</param>
		/// <param name="name">Identifier name</param>
		/// <remarks>
		/// <see cref="QualifiedIdentifier"/> is usually to render database fields with optional table alias prefixes.
		/// <paramref name="name"/> is a mandatory parameter while <paramref name="qnamespace"/> is optional.
		/// If <paramref name="qnamespace"/> is null, identifier will be rendered without a namespace (aka table alias)
		/// </remarks>
		protected virtual void QualifiedIdentifier(StringBuilder builder, string qnamespace, string name)
		{
			if (qnamespace != null)
			{
				Identifier(builder, qnamespace);
				builder.Append(".");
			}
				
			Identifier(builder, name);
		}

		/// <summary>
		/// Renders a the beginning of an UPDATE clause with the table name
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName">Name of the table to be updated</param>
		protected virtual void Update(StringBuilder builder, string tableName)
		{
			builder.Append("update ");
			Identifier(builder, tableName);
			builder.Append(" set ");
		}

		/// <summary>
		/// Renders update phrases
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void UpdateTerms(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm updateTerm in terms)
			{
				if (terms[0] != updateTerm)
					Coma(builder);
				UpdateTerm(builder, updateTerm);
			}
		}

		/// <summary>
		/// Render a single update phrase
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void UpdateTerm(StringBuilder builder, UpdateTerm term)
		{
			Identifier(builder, term.FieldName);
			builder.Append(" = ");
			Expression(builder, term.Value);
		}

		/// <summary>
		/// Renders the whole UPDATE statement using ANSI SQL standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL string</returns>
		public virtual string UpdateStatement(UpdateQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
			Update(builder, query.TableName);
			UpdateTerms(builder, query.Terms);
			Where(builder, query.WhereClause);
			WhereClause(builder, query.WhereClause);

			return builder.ToString();
		}

		/// <summary>
		/// Render the beginning of an INSERT statement with table name
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName"></param>
		protected virtual void Insert(StringBuilder builder, string tableName)
		{
			builder.Append("insert into ");
			Identifier(builder, tableName);
			builder.Append(" ");
		}

		/// <summary>
		/// Render the list of columns which are to be inserted.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void InsertColumns(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm term in terms)
			{
				if (terms[0] != term)
					Coma(builder);
				InsertColumn(builder, term);
			}
		}

		/// <summary>
		/// Render a single column name in an INSERT statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void InsertColumn(StringBuilder builder, UpdateTerm term)
		{
			Identifier(builder, term.FieldName);
		}

		/// <summary>
		/// Render the values of an INSERT statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="terms"></param>
		protected virtual void InsertValues(StringBuilder builder, UpdateTermCollection terms)
		{
			foreach (UpdateTerm term in terms)
			{
				if (terms[0] != term)
					Coma(builder);
				InsertValue(builder, term);
			}
		}

		/// <summary>
		/// Render a single INSERT value
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void InsertValue(StringBuilder builder, UpdateTerm term)
		{
			Expression(builder, term.Value);
		}

		/// <summary>
		/// Render the whole INSERT statement in ANSI standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL INSERT statement</returns>
		public virtual string InsertStatement(InsertQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
			Insert(builder, query.TableName);

			builder.Append("(");
			InsertColumns(builder, query.Terms);
			builder.Append(") values (");
			InsertValues(builder, query.Terms);
			builder.Append(")");
			return builder.ToString();
		}

    public virtual string InsertSelectStatement(InsertSelectQuery query)
    {
      query.Validate();
      StringBuilder builder = new StringBuilder();
      Insert(builder, query.TableName);
      builder.Append(RenderSelect(query.SelectQuery));
      return builder.ToString();
    }

		/// <summary>
		/// Render the beginning of a DELETE statement
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="tableName"></param>
    protected virtual void Delete(StringBuilder builder, object fromObject, FromTermType fromTermType, string schema)
		{
			builder.Append("delete from ");
      switch (fromTermType)
      {
        case FromTermType.Table:
					if (!string.IsNullOrEmpty(schema))
					{
						Identifier(builder, schema);
						builder.Append(".");
					}
					Identifier(builder, (string)fromObject);
          builder.Append(" ");
          break;
        case FromTermType.SubQueryObj:
          builder.Append("(");
          builder.Append(RenderSelect((SelectQuery)fromObject));
          builder.Append(")");
          break;
        default:
          throw new ArgumentException("fromTermType");
      }
		}

		/// <summary>
		/// Render the whole DELETE statement in ANSI SQL standard
		/// </summary>
		/// <param name="query"></param>
		/// <returns>The rendered SQL statement</returns>
		public virtual string DeleteStatement(DeleteQuery query)
		{
			query.Validate();
			StringBuilder builder = new StringBuilder();
      Delete(builder, query.FromObject, query.FromType, query.Schema);
			Where(builder, query.WhereClause);
			WhereClause(builder, query.WhereClause);
			return builder.ToString();
		}

		/// <summary>
		/// Renders a CaseCluase
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="clause"></param>
		protected virtual void CaseClause(StringBuilder builder, CaseClause clause)
		{
			builder.Append(" case ");
			foreach(CaseTerm term in clause.Terms)
				CaseTerm(builder, term);
			if (clause.ElseValue != null)
			{
				builder.Append(" else ");
				Expression(builder, clause.ElseValue);
			}

			builder.Append(" end ");
		}

		/// <summary>
		/// Renders a CaseTerm
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="term"></param>
		protected virtual void CaseTerm(StringBuilder builder, CaseTerm term)
		{
			builder.Append(" when ");
			WhereClause(builder, term.Condition);
			builder.Append(" then ");
			Expression(builder, term.Value);
		}

/*		protected void SelectModifiers(StringBuilder builder, SelectQuery query)
		{
		}

		protected void GroupByModifiers(StringBuilder builder, SelectQuery query)
		{
		}

		//protected void SelectStatement(StringBuilder builder, bool distinct, int top, SelectColumnCollection columns, FromClause from, WhereClause where, GroupByTermCollection groupBy, bool withCube, bool withRollup, OrderByTermCollection orderBy, WhereClause having)
		protected void SelectStatement(StringBuilder builder, SelectQuery query)
		{
			query.Validate();
			
			StringBuilder selectBuilder = new StringBuilder();

			//Start the select statement
			Select(selectBuilder, query.Distinct);
			SelectModifiers(selectBuilder, query);
			SelectColumns(selectBuilder, query.Columns);

			this.FromClause(selectBuilder, query.FromClause, query.TableSpace);
			
			this.Where(selectBuilder, query.WherePhrase);
			this.WhereClause(selectBuilder, query.WherePhrase);

			GroupBy(selectBuilder, query.GroupByTerms);
			GroupByTerms(selectBuilder, query.GroupByTerms);
			GroupByModifiers(selectBuilder, query);
			
			Having(selectBuilder, query.HavingPhrase);
			WhereClause(selectBuilder, query.HavingPhrase);

			OrderBy(selectBuilder, query.OrderByTerms);
			OrderByTerms(selectBuilder, query.OrderByTerms);
		}
*/


    #region ISqlOmRenderer Members


    public string RenderSelect(Select query)
    {
      return RenderSelect(Qb.GetQueryObject(query));
    }

    public string RenderRowCount(Select query)
    {
      return RenderRowCount(Qb.GetQueryObject(query));
    }

    public string RenderPage(int pageIndex, int pageSize, int totalRowCount, Select query)
    {
      return RenderPage(pageIndex, pageSize, totalRowCount, Qb.GetQueryObject(query));
    }

    public string RenderUpdate(Update query)
    {
      return RenderUpdate(Qb.GetQueryObject(query));
    }

    public string RenderInsert(Insert query)
    {
      return RenderInsert(Qb.GetQueryObject(query));
    }

    public string RenderInsertSelect(InsertSelect query)
    {
      return RenderInsertSelect(Qb.GetQueryObject(query));
    }

    public string RenderDelete(Delete query)
    {
      return RenderDelete(Qb.GetQueryObject(query));
    }

    #endregion


  }
}
