using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Encapsulates a SQL DELETE statement
	/// </summary>
	/// <remarks>
	/// Use DeleteQuery to delete a row in database table.
	/// Set <see cref="From"/> to the table you would like to delete rows from and use
	/// <see cref="WhereClause"/> to specify which rows are to be deleted.
	/// </remarks>
	/// <example>
	/// <code>
	/// DeleteQuery query = new DeleteQuery("products");
	/// query.WhereClause.Terms.Add(WhereTerm.CreateCompare(SqlExpression.Field("productId"), SqlExpression.Number(999), CompareOperator.Equal));
	/// RenderDelete(query);
	/// </code>
	/// </example>
  public class DeleteQuery 
	{
    object fromObject;
    FromTermType fromTermType = FromTermType.Table;
		WhereClause whereClause = new WhereClause(WhereClauseRelationship.And);

		/// <summary>
		/// Creates a DeleteQuery
		/// </summary>
		public DeleteQuery(): this((string)null)
		{
		}
    /// <summary>
		/// Creates a DeleteQuery
		/// </summary>
		/// <param name="tableName">Name of the table records are to be deleted from</param>
		public DeleteQuery(string tableName)
		{
      this.fromObject = tableName;
		}

    /// <summary>
    /// Creates a DeleteQuery
    /// </summary>
    /// <param name="select">Name of the table records are to be deleted from</param>
    public DeleteQuery(SelectQuery select)
    {
      this.fromObject = select;
      this.fromTermType = FromTermType.SubQueryObj;
    }
		/// <summary>
		/// Specifies which rows are to be deleted
		/// </summary>
		public WhereClause WhereClause
		{
			get { return this.whereClause; }
		}

		/// <summary>
		/// Gets or set the name of a table records are to be deleted from
		/// </summary>
		public object FromObject
		{
			get { return this.fromObject; }
      set 
      {
        this.fromObject = value;
        if (value == null)
          fromTermType = FromTermType.Table;
        else
          fromTermType = value is SelectQuery ? FromTermType.SubQueryObj : FromTermType.Table;
      }
		}

    /// <summary>
    /// Type of From
    /// </summary>
    public FromTermType FromType
    {
      get { return fromTermType; }
    }

		/// <summary>
		/// Validates DeleteQuery
		/// </summary>
		public void Validate()
		{
      if (fromObject == null)
				throw new InvalidQueryException("TableName is empty.");
      //if (WhereClause.IsEmpty)
      //  throw new InvalidQueryException("DeleteQuery has no where condition.");
		}

    /// <summary>Оператор приведения типа Delete к DeleteQuery</summary>
    /// <param name="query">Значение типа Delete</param>
    /// <returns>Значение типа DeleteQuery</returns>
    public static implicit operator DeleteQuery(Delete query)
    {
      return query.Query;
    }

    ParamCollection commandParams = new ParamCollection();

    /// <summary>Список параметров команды</summary>
    public ParamCollection CommandParams
    {
      get { return commandParams; }
    }

  }
}
