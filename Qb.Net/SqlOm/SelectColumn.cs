using System;
namespace Viten.QueryBuilder.SqlOm
{

	/// <summary>
	/// Describes a column of a select clause
	/// </summary>
  public class SelectColumn 
	{
    internal SelectColumn()
    {
    }
		/// <summary>
		/// Creates a SelectColumn with a column name, no table, no column alias and no function
		/// </summary>
		/// <param name="columnName">Name of a column</param>
		public SelectColumn(string columnName) : this(columnName, null)
		{
		}

		/// <summary>
		/// Creates a SelectColumn with a column name, table, no column alias and no function
		/// </summary>
		/// <param name="columnName">Name of a column</param>
		/// <param name="table">The table this field belongs to</param>
		public SelectColumn(string columnName, FromTerm table) : this(columnName, table, null)
		{
		}

		/// <summary>
		/// Creates a SelectColumn with a column name, table and column alias
		/// </summary>
		/// <param name="columnName">Name of a column</param>
		/// <param name="table">The table this field belongs to</param>
		/// <param name="columnAlias">Alias of the column</param>
		public SelectColumn(string columnName, FromTerm table, string columnAlias) : this(columnName, table, columnAlias, AggFunc.None)
		{
		}

		/// <summary>
		/// Creates a SelectColumn with a column name, table, column alias and optional aggregation function
		/// </summary>
		/// <param name="columnName">Name of a column</param>
		/// <param name="table">The table this field belongs to</param>
		/// <param name="columnAlias">Alias of the column</param>
		/// <param name="function">Aggregation function to be applied to the column. Use SqlAggregationFunction.None to specify that no function should be applied.</param>
		public SelectColumn(string columnName, FromTerm table, string columnAlias, AggFunc function)
		{
			if (function == AggFunc.None)
        Expression = OmExpression.Field(columnName, table);
			else
        Expression = OmExpression.Function(function, OmExpression.Field(columnName, table));
			this.ColumnAlias = columnAlias;
		}

		/// <summary>
		/// Creates a SelectColumn
		/// </summary>
		/// <param name="expr">Expression</param>
		/// <param name="columnAlias">Column alias</param>
		public SelectColumn(OmExpression expr, string columnAlias)
		{
			this.Expression = expr;
			this.ColumnAlias = columnAlias;
		}

		/// <summary>
		/// Gets the column alias for this SelectColumn
		/// </summary>
		public string ColumnAlias { get; set; }

    /// <summary></summary>
    public OmExpression Expression { get; set; }

  }
}
