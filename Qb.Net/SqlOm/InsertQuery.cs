using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Encapsulates a SQL INSERT statement
	/// </summary>
	/// <remarks>
	/// Use InsertQuery to insert a new row into a database table.
	/// Set <see cref="TableName"/> to the table you would like to insert into and use
	/// the <see cref="Terms"/> collection to specify values to be inserted.
	/// </remarks>
	/// <example>
	/// <code>
	/// InsertQuery query = new InsertQuery("products");
	/// query.Terms.Add(new UpdateTerm("productId", SqlExpression.Number(999)));
	/// query.Terms.Add(new UpdateTerm("name", SqlExpression.String("Temporary Test Product")));
	/// query.Terms.Add(new UpdateTerm("price", SqlExpression.Number(123.45)));
	/// query.Terms.Add(new UpdateTerm("quantaty", SqlExpression.Number(97)));
	/// RenderInsert(query);
	/// </code>
	/// </example>
  public class InsertQuery
	{
		UpdateTermCollection terms = new UpdateTermCollection();

    /// <summary>
    /// Gets or set the name of a table to be inserted into
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// ��� ������������� ���� ��������������
    /// </summary>
    public string IdentityField { get; set; }

    ParamCollection commandParams = new ParamCollection();

    /// <summary>������ ���������� �������</summary>
    public ParamCollection CommandParams
    {
      get { return commandParams; }
    }

    /// <summary>
    /// Create an InsertQuery
    /// </summary>
    public InsertQuery() : this(null)
		{
		}

		/// <summary>
		/// Create an InsertQuery
		/// </summary>
		/// <param name="tableName">The name of the table to be inseserted into</param>
		public InsertQuery(string tableName)
		{
			this.TableName = tableName;
		}
    /// <summary>
		/// Gets the collection if column-value pairs
		/// </summary>
		/// <remarks>
		/// Terms specify which values should be inserted into the table.
		/// </remarks>
		public UpdateTermCollection Terms
		{
			get { return this.terms; }
		}



		/// <summary>
		/// Validates InsertQuery
		/// </summary>
		public void Validate()
		{
			if (TableName == null)
				throw new InvalidQueryException("TableName is empty.");
      if (terms.Count == 0)
				throw new InvalidQueryException("Terms collection is empty.");
		}

    /// <summary>�������� ���������� ���� Insert � InsertQuery</summary>
    /// <param name="query">�������� ���� Insert</param>
    /// <returns>�������� ���� InsertQuery</returns>
    public static implicit operator InsertQuery(Insert query)
    {
      return query.Query;
    }


  }
}
