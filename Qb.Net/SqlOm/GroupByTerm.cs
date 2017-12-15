namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Represents one term in a GROUP BY clause
	/// </summary>
	/// <remarks>
	/// Use OrderByTerm to specify how rows of a result-set should be grouped. 
	/// Please note that when you use GROUP BY, your SELECT statement can only include columns which are specified in the GROUP BY clause and aggregation columns.
	/// </remarks>

  public class GroupByTerm
  {
    /// <summary>
    /// Gets the name of a field to group by
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Gets the table the field belongs to
    /// </summary>
    public FromTerm Table { get; set; }

    /// <summary>
    /// Gets the table alias for this GroupByTerm
    /// </summary>
    /// <remarks>
    /// Gets the name of a FromTerm the field specified by <see cref="GroupByTerm.Field">Field</see> property.
    /// </remarks>
    public string TableAlias
    {
      get { return (Table == null) ? null : Table.RefName; }
    }

    public GroupByTerm()
    {
    }
		/// <summary>
		/// Creates a GROUP BY term with field name and table alias
		/// </summary>
		/// <param name="field">Name of a field to group by</param>
		/// <param name="table">The table this field belongs to</param>
		public GroupByTerm(string field, FromTerm table)
		{
			this.Field = field;
			this.Table = table;
		}

		/// <summary>
		/// Creates a GROUP BY term with field name and no FromTerm alias
		/// </summary>
		/// <param name="field">Name of a field to group by</param>
		public GroupByTerm(string field) : this(field, null)
		{
		}


  }
}
