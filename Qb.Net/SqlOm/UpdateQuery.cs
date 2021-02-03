using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Encapsulates a SQL UPDATE statement
  /// </summary>
  /// <remarks>
  /// Use UpdateQuery to update data in a database table.
  /// Set <see cref="TableName"/> to the table you would like to update, populate 
  /// the <see cref="Terms"/> collection with column-value pairs and define which rows 
  /// should be affected using the <see cref="WhereClause"/>.
  /// </remarks>
  /// <example>
  /// <code>
  ///	UpdateQuery query = new UpdateQuery("products");
  ///	query.Terms.Add(new UpdateTerm("price", SqlExpression.Number(12.1)));
  ///	query.Terms.Add(new UpdateTerm("quantaty", SqlExpression.Field("quantaty")));
  ///	query.WhereClause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productId"), SqlExpression.Number(1), CompareOperator.Equal) );
  ///	</code>
  /// </example>
  public class UpdateQuery
	{
    /// <summary>
    /// Gets the terms collection for this UpdateQuery
    /// </summary>
    /// <remarks>
    /// Terms specify which columns should be updated and to what values.
    /// </remarks>
    public UpdateTermCollection Terms { get; } = new UpdateTermCollection();
       
    /// <summary>
    /// Spicifies which rows are to be updated
    /// </summary>
    public WhereClause WhereClause { get; } = new WhereClause(WhereRel.And);

    /// <summary>
    /// Gets or set the name of a table to be updated
    /// </summary>
    public string TableName { get; set; }

    public string Schema { get; set; }

    /// <summary>
    /// Creates a new UpdateQuery
    /// </summary>
    public UpdateQuery() : this(null)
		{
		}

		/// <summary>
		/// Creates a new UpdateQuery
		/// </summary>
		/// <param name="tableName"></param>
		public UpdateQuery(string tableName)
		{
			this.TableName = tableName;
		}

    public UpdateQuery(string tableName, string schema)
      :this(tableName)
    {
      Schema = schema;
    }


    /// <summary>
    /// Validates UpdateQuery
    /// </summary>
    public void Validate()
		{
			if (TableName == null)
				throw new InvalidQueryException("TableName is empty.");
			if (Terms.Count == 0)
				throw new InvalidQueryException("Terms collection is empty.");
		}

    /// <summary>�������� ���������� ���� Update � UpdateQuery</summary>
    /// <param name="query">�������� ���� Update</param>
    /// <returns>�������� ���� UpdateQuery</returns>
    public static implicit operator UpdateQuery(Update query)
    {
      return query.Query;
    }

    ParamCollection commandParams = new ParamCollection();
    /// <summary>������ ���������� �������</summary>
    public ParamCollection CommandParams
    {
      get { return commandParams; }
    }

  }
}
