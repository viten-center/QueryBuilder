namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Represents one term in an ORDER BY clause
  /// </summary>
  /// <remarks>
  /// Use OrderByTerm to specify how a result-set should be ordered.
  /// </remarks>
  public class OrderByTerm
  {
    /// <summary>
    /// Gets the name of a field to order by
    /// </summary>
    public string Field { get; set; }
    /// <summary>
    /// Returns the FromTerm associated with this OrderByTerm
    /// </summary>
    public FromTerm Table { get; set; }
    /// <summary>
    /// Gets the direction for this OrderByTerm
    /// </summary>
    public OrderByDir Direction { get; set; }
    internal OrderByTerm()
    {
    }

    /// <summary>
    /// Creates an ORDER BY term with field name and table alias
    /// </summary>
    /// <param name="field">Name of a field to order by</param>
    /// <param name="table">The table this field belongs to</param>
    /// <param name="dir">Order by direction</param>
    public OrderByTerm(string field, FromTerm table, OrderByDir dir)
    {
      this.Field = field;
      this.Table = table;
      this.Direction = dir;
    }

    /// <summary>
    /// Creates an ORDER BY term with field name and no table alias
    /// </summary>
    /// <param name="field">Name of a field to order by</param>
    /// <param name="dir">Order by direction</param>
    public OrderByTerm(string field, OrderByDir dir) : this(field, null, dir)
    {
    }


    /// <summary>
    /// Gets the table alias for this OrderByTerm
    /// </summary>
    /// <remarks>
    /// Gets the name of a FromTerm the field specified by <see cref="OrderByTerm.Field">Field</see> property.
    /// </remarks>
    public string TableAlias
    {
      get { return (Table == null) ? null : Table.RefName; }
    }
  }
}
