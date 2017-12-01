using System;
namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Specifies which function should be applied on a column
	/// </summary>
	public enum OmAggregationFunction 
	{ 
		/// <summary>No function</summary>
		None, 
		/// <summary>Sum</summary>
		Sum, 
		/// <summary>Count rows</summary>
		Count, 
		/// <summary>Avarage</summary>
		Avg, 
		/// <summary>Minimum</summary>
		Min, 
		/// <summary>Maximum</summary>
		Max,
		/// <summary>Returns true is the current row is a super-aggregate row when used with ROLLUP or CUBE</summary>
		/// <remarks>Grouping functions is not supported in all databases</remarks>
		Grouping,
	}

  /// <summary>
  /// Класс расширения для OmAggregationFunction
  /// </summary>
  public static class OmAggregationFunctionEx
  {
    /// <summary>Быстрый перевод в строку</summary>
    /// <param name="aggregationFunction"></param>
    /// <returns></returns>
    public static string ToStringFast(this OmAggregationFunction aggregationFunction)
    {
      switch (aggregationFunction)
      {
        case OmAggregationFunction.None:
          return "None";
        case OmAggregationFunction.Sum:
          return "Sum";
        case OmAggregationFunction.Count:
          return "Count";
        case OmAggregationFunction.Avg:
          return "Avg";
        case OmAggregationFunction.Min:
          return "Min";
        case OmAggregationFunction.Max:
          return "Max";
        case OmAggregationFunction.Grouping:
          return "Grouping";
        default: throw new ArgumentException("aggregationFunction"); 
      }
    }
  }

	/// <summary>
	/// Describes a column of a select clause
	/// </summary>
  public class SelectColumn 
	{
		OmExpression expr;
		string alias;
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
		public SelectColumn(string columnName, FromTerm table, string columnAlias) : this(columnName, table, columnAlias, OmAggregationFunction.None)
		{
		}

		/// <summary>
		/// Creates a SelectColumn with a column name, table, column alias and optional aggregation function
		/// </summary>
		/// <param name="columnName">Name of a column</param>
		/// <param name="table">The table this field belongs to</param>
		/// <param name="columnAlias">Alias of the column</param>
		/// <param name="function">Aggregation function to be applied to the column. Use SqlAggregationFunction.None to specify that no function should be applied.</param>
		public SelectColumn(string columnName, FromTerm table, string columnAlias, OmAggregationFunction function)
		{
			if (function == OmAggregationFunction.None)
				expr = OmExpression.Field(columnName, table);
			else
				expr = OmExpression.Function(function, OmExpression.Field(columnName, table));
			this.alias = columnAlias;
		}

		/// <summary>
		/// Creates a SelectColumn
		/// </summary>
		/// <param name="expr">Expression</param>
		/// <param name="columnAlias">Column alias</param>
		public SelectColumn(OmExpression expr, string columnAlias)
		{
			this.expr = expr;
			this.alias = columnAlias;
		}

		/// <summary>
		/// Gets the column alias for this SelectColumn
		/// </summary>
		public string ColumnAlias
		{
			get { return alias; } 
		}

    /// <summary></summary>
    public OmExpression Expression
		{
			get { return expr; }
		}

  }
}
