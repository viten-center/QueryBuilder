using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
    /// <summary>
    /// Реализует SQL INSERT INTO SELECT
    /// </summary>
    public class InsertSelectQuery 
  {
    SelectQuery selectQuery;
    string tableName;

    		/// <summary>
    /// Create an InsertSelectQuery
		/// </summary>
		public InsertSelectQuery() : this(null)
		{
		}

		/// <summary>
    /// Create an InsertSelectQuery
		/// </summary>
		/// <param name="tableName">The name of the table to be inseserted into</param>
    public InsertSelectQuery(string tableName)
		{
			this.tableName = tableName;
		}

    /// <summary>
    /// Gets or set the name of a table to be inserted into
    /// </summary>
    public string TableName
    {
      get { return this.tableName; }
      set { this.tableName = value; }
    }

    /// <summary>Команда запроса</summary>
    public SelectQuery SelectQuery
    {
      get { return this.selectQuery; }
      set { selectQuery = value; }
    }

    /// <summary>
    /// Validates InsertQuery
    /// </summary>
    public void Validate()
    {
      if (tableName == null)
        throw new InvalidQueryException("TableName is empty.");
      if (selectQuery == null)
        throw new InvalidQueryException("SelectQuery is empty.");
    }

    /// <summary>Оператор приведения типа InsertSelect к InsertSelectQuery</summary>
    /// <param name="query">Значение типа Insert</param>
    /// <returns>Значение типа InsertSelectQuery</returns>
    public static implicit operator InsertSelectQuery(InsertSelect query)
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
