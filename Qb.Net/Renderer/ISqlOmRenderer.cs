using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder.Renderer
{
	/// <summary>
	/// Defines methods common to all SqlOM renderers.
	/// </summary>
	/// <remarks>
	/// Derive from <see cref="ISqlOmRenderer"/> when you wish to develop a brand new renderer. 
	/// You can write 100% proprietery code for while implementing the interface methods but it is not advised.
	/// Instead you can inherit the <see cref="SqlOmRenderer"/> class which implements 80-95% of your rendering functionality.
	/// All renderers must return a string as their rendering result.
	/// </remarks>
	public interface ISqlOmRenderer
	{
		/// <summary>
		/// Sets or returns default date format for the database
		/// </summary>
		/// <remarks>
		/// Set DateFormat property when your database is configured to use a different date format
		/// then "yyyy-MM-dd HH:mm:ss". SqlServer and MySql are configured to this format by default.
		/// Oracle's default date format is "dd-MMM-yy HH:mm:ss".
		/// </remarks>
		string DateFormat { get; set; }
    string BatchTerminator { get; }
    char OpeningQuote { get; }
    char ClosingQuote { get; }
    /// <summary>������� ����� ���������</summary>
    char ParameterPrefix { get; }

	  string CreateParameterName(string name);
		/// <summary>
		/// Renders a SELECT statement
		/// </summary>
		/// <param name="query">Query definition</param>
		/// <returns>Generated SQL statement</returns>
		string RenderSelect(SelectQuery query);

    /// <summary>
    /// Renders a SELECT statement
    /// </summary>
    /// <param name="query">Query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderSelect(Select query);

		/// <summary>
		/// Renders a row count SELECT statement. 
		/// </summary>
		/// <param name="query">Query definition to count rows for</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// Renders a SQL statement which returns a result set with one row and one cell which contains the number of rows <paramref name="query"/> can generate. 
		/// The generated statement will work nicely with <see cref="System.Data.IDbCommand.ExecuteScalar"/> method.
		/// </remarks>
		string RenderRowCount(SelectQuery query);

    /// <summary>
    /// Renders a row count SELECT statement. 
    /// </summary>
    /// <param name="query">Query definition to count rows for</param>
    /// <returns>Generated SQL statement</returns>
    /// <remarks>
    /// Renders a SQL statement which returns a result set with one row and one cell which contains the number of rows <paramref name="query"/> can generate. 
    /// The generated statement will work nicely with <see cref="System.Data.IDbCommand.ExecuteScalar"/> method.
    /// </remarks>
    string RenderRowCount(Select query);

		/// <summary>
		/// Renders a paged SELECT statement
		/// </summary>
		/// <param name="pageIndex">The zero based index of the page to be returned</param>
		/// <param name="pageSize">The size of a page</param>
		/// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
		/// <param name="query">Query definition to apply paging on</param>
		/// <returns>Generated SQL statement</returns>
		/// <remarks>
		/// Generating pagination SQL is different on different databases because every database offers different levels of support for such functioanality.
		/// Some databases (SqlServer 2000) require the programmer to supply the total number of rows to produce a page. If your renderer does not use the totalRowCount parameter, please state so in your implementation documentation.
		/// </remarks>
		string RenderPage(int pageIndex, int pageSize, int totalRowCount, SelectQuery query);

    /// <summary>
    /// Renders a paged SELECT statement
    /// </summary>
    /// <param name="pageIndex">The zero based index of the page to be returned</param>
    /// <param name="pageSize">The size of a page</param>
    /// <param name="totalRowCount">Total number of rows the query would yeild if not paged</param>
    /// <param name="query">Query definition to apply paging on</param>
    /// <returns>Generated SQL statement</returns>
    /// <remarks>
    /// Generating pagination SQL is different on different databases because every database offers different levels of support for such functioanality.
    /// Some databases (SqlServer 2000) require the programmer to supply the total number of rows to produce a page. If your renderer does not use the totalRowCount parameter, please state so in your implementation documentation.
    /// </remarks>
    string RenderPage(int pageIndex, int pageSize, int totalRowCount, Select query);

		/// <summary>
		/// Renders an UPDATE statement
		/// </summary>
		/// <param name="query">UPDATE query definition</param>
		/// <returns>Generated SQL statement</returns>
		string RenderUpdate(UpdateQuery query);

    /// <summary>
    /// Renders an UPDATE statement
    /// </summary>
    /// <param name="query">UPDATE query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderUpdate(Update query);

		/// <summary>
		/// Renders an INSERT statement
		/// </summary>
		/// <param name="query">INSERT query definition</param>
		/// <returns>Generated SQL statement</returns>
		string RenderInsert(InsertQuery query);

    /// <summary>
    /// Renders an INSERT statement
    /// </summary>
    /// <param name="query">INSERT query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderInsert(Insert query);

    /// <summary>
    /// Renders an INSERT INTO ... SELECT statement
    /// </summary>
    /// <param name="query">INSERT INTO ... SELECT query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderInsertSelect(InsertSelectQuery query);

    /// <summary>
    /// Renders an INSERT INTO ... SELECT statement
    /// </summary>
    /// <param name="query">INSERT INTO ... SELECT query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderInsertSelect(InsertSelect query);

		/// <summary>
		/// Renders an DELETE statement
		/// </summary>
		/// <param name="query">DELETE query definition</param>
		/// <returns>Generated SQL statement</returns>
		string RenderDelete(DeleteQuery query);

    /// <summary>
    /// Renders an DELETE statement
    /// </summary>
    /// <param name="query">DELETE query definition</param>
    /// <returns>Generated SQL statement</returns>
    string RenderDelete(Delete query);

		/// <summary>
		/// Renders a UNION clause
		/// </summary>
		/// <param name="union">Union definition</param>
		/// <returns>Generated SQL statement</returns>
		string RenderUnion(OmUnion union);

    /// <summary>
    /// ������� ����� ��������� ISqlOmRenderer
    /// </summary>
    /// <returns></returns>
    ISqlOmRenderer CreateNew();

    /// <summary>
    /// ������� ���������� ��� ������� �� (�������, �������...)
    /// </summary>
    /// <param name="objName">��� �������</param>
    /// <returns></returns>
    string GetIdentifier(string objName);

	}
}
