using System;
namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Specifies the type of a FromTerm
	/// </summary>
  public enum FromTermType
	{ 
		/// <summary>The FromTerm designates a database table or view</summary>
		Table, 
		/// <summary>The FromTerm designates a sub-query. Not all databases support sub-queries.</summary>
		SubQuery,
		/// <summary></summary>
		SubQueryObj,
    /// <summary></summary>
    Union
	}

  /// <summary>
  /// Represents one term in the the FROM clause of a select statement.
  /// </summary>
  /// <remarks>
  /// A from term can be either a table, table reference or a subquery.
  /// subqueries. FromTerm.Table is a name of a table or view with an optional alias. 
  /// Use FromTerm.Table to specify a reference to another term. 
  /// You will usually use TableRef to specify one of the previously defined tables in a join.
  /// FromTerm.SubQuery creates a term with a subquery and mandatory alias. Please note that not all databases support sub-queires.
  /// </remarks>
  /// <example>
  /// The follwoing example selects some columns from two tables joined by a left outer join.
  /// <code>
  /// FromTerm tCustomers = FromTerm.Table("customers");
  /// FromTerm tProducts = FromTerm.Table("products", "p");
  /// FromTerm tOrders = FromTerm.Table("orders", "o");
  /// 
  /// SelectQuery query = new SelectQuery();
  /// query.Columns.Add(new SelectColumn("name", tCustomers));
  /// query.Columns.Add(new SelectColumn("name", tProducts));
  /// query.FromClause.BaseTable = tCustomers;
  /// query.FromClause.Join(JoinType.Inner, query.FromClause.BaseTable, tOrders, "customerId", "customerId");
  /// query.FromClause.Join(JoinType.Inner, tOrders, tProducts, "productId", "productId");
  /// </code>
  /// </example>
  public class FromTerm
  {
    public FromTerm()
    {
    }

    /// <summary>
    /// Creates a FromTerm which represents a database table or view.
    /// </summary>
    /// <param name="name">Name of the table or view</param>
    /// <returns>A FromTerm which represents a database table or view</returns>
    /// <remarks>Creates a <see cref="FromTerm">FromTerm</see> without an alias. 
    /// The created term will be referenced by the table's name.</remarks>
    public static FromTerm Table(string name)
    {
      return Table(name, null);
    }

    /// <summary>
    /// Creates a FromTerm which represents a database table or view.
    /// </summary>
    /// <param name="tableName">Name of the table or view</param>
    /// <param name="alias">Alias of the FromTerm</param>
    /// <returns>A FromTerm which represents a database table or view</returns>
    public static FromTerm Table(string tableName, string alias)
    {
      return Table(tableName, alias, null);
    }

    /// <summary>
    /// Creates a FromTerm which represents a database table or view.
    /// </summary>
    /// <param name="tableName">Name of the table or view</param>
    /// <param name="alias">Alias of the FromTerm</param>
    /// <param name="ns">Namespace of the table.</param>
    /// <returns>A FromTerm which represents a database table or view</returns>
    /// <remarks>Use the <paramref name="ns"/> parameter to resolve table ownership</remarks>
    public static FromTerm Table(string tableName, string alias, string ns)
    {
      return Table(tableName, alias, ns, null);
    }

    /// <summary>
    /// Creates a FromTerm which represents a database table or view.
    /// </summary>
    /// <param name="tableName">Name of the table or view</param>
    /// <param name="alias">Alias of the FromTerm</param>
    /// <param name="ns1">First table namespace</param>
    /// <param name="ns2">Second table namespace</param>
    /// <returns>A FromTerm which represents a database table or view</returns>
    /// <remarks>Use the <paramref name="ns1"/> parameter to set table database and <paramref name="ns2"/> to set table owner.</remarks>
    public static FromTerm Table(string tableName, string alias, string ns1, string ns2)
    {
      FromTerm term = new FromTerm();
      term.StringValue = tableName;
      term.Alias = alias;
      term.Type = FromTermType.Table;
      term.Ns1 = ns1;
      term.Ns2 = ns2;
      return term;
    }

    /// <summary>
    /// Creates a FromTerm which refernces abother FromTerm.
    /// </summary>
    /// <param name="name">The name of the referenced term.</param>
    /// <returns>A FromTerm which refernces another FromTerm.</returns>
    /// <remarks>
    /// Use TermRef to reference other terms of a from clause by <see cref="FromTerm.RefName">RefName</see>
    /// </remarks>
    public static FromTerm TermRef(string name)
    {
      return Table(null, name);
    }

    /// <summary>
    /// Creates a FromTerm which represents a sub-query.
    /// </summary>
    /// <param name="query">sub-query sql</param>
    /// <param name="alias">term alias</param>
    /// <returns>A FromTerm which represents a sub-query.</returns>
    public static FromTerm SubQuery(string query, string alias)
    {
      FromTerm term = new FromTerm();
      term.StringValue = query;
      term.Alias = alias;
      term.Type = FromTermType.SubQuery;
      return term;
    }

    /// <summary>
    /// Creates a FromTerm which represents a sub-query.
    /// </summary>
    /// <param name="query">A SelectQuery instance representing the sub query</param>
    /// <param name="alias">term alias</param>
    /// <returns>A FromTerm which represents a sub-query.</returns>
    public static FromTerm SubQuery(SelectQuery query, string alias)
    {
      FromTerm term = new FromTerm();
      term.QueryValue = query;
      term.Alias = alias;
      term.Type = FromTermType.SubQueryObj;
      return term;
    }

    /// <summary>
    /// Creates a FromTerm which represents a UNION.
    /// </summary>
    public static FromTerm Union(OmUnion union, string alias)
    {
      FromTerm term = new FromTerm();
      term.UnionValue = union;
      term.Alias = alias;
      term.Type = FromTermType.Union;
      return term;
    }

    public string StringValue { get; set; }
    public SelectQuery QueryValue { get; set; }
    public OmUnion UnionValue { get; set; }

    /// <summary>
    /// Gets the expression defined for this term.
    /// </summary>
    /// <remarks>
    /// The value of this property depends on the type of term. It will be table name
    /// for Table terms, SQL for SubQueries or null for TermRefs.
    /// </remarks>
    public object Expression
    {
      get
      {
        switch (Type)
        {
          case FromTermType.SubQueryObj:
            return QueryValue;
          case FromTermType.Union:
            return UnionValue;
          default:
            return StringValue;
        }
      }
    }

    /// <summary>
    /// Gets the alias of the term.
    /// </summary>
    /// <remarks>This property can be null for some types of FromTerm</remarks>
    public string Alias { get; set; }

    /// <summary>
    /// Gets the type of the FromTerm
    /// </summary>
    public FromTermType Type { get; set; }

    /// <summary>
    /// Gets the reference name of this term.
    /// </summary>
    public string RefName
    {
      get { return (Alias == null && Type == FromTermType.Table) ? (string)Expression : Alias; }
    }

    /// <summary></summary>
    public string Ns1 { get; set; }

    /// <summary></summary>
    public string Ns2 { get; set; }
  }
}
