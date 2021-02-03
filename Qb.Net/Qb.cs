using Viten.QueryBuilder.Renderer;
using Viten.QueryBuilder.SqlOm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>
  /// Класс построения команд работы с данными
  /// </summary>
  public static class Qb
  {
    public static ISqlOmRenderer CreateRenderer(DatabaseProvider db)
    {
      switch(db)
      {
        case DatabaseProvider.MySql:
          return new MySqlRenderer();
        case DatabaseProvider.Oracle:
          return new OracleRenderer();
        case DatabaseProvider.SqLite:
          return new SqLiteRenderer();
        case DatabaseProvider.SqlServer:
          return new SqlServerRenderer();
        case DatabaseProvider.SqlServerCe:
          return new SqlServerCeRenderer();
        case DatabaseProvider.PostgreSql:
          return new PostgreSqlRenderer();
        default:
          throw new ArgumentException($"Not valid argument '{db}'");
      }
    }
    #region Select
    /// <summary>Построение команды SELECT</summary>
    /// <param name="columnsName">Список колонок</param>
    /// <returns>Описатель команды</returns>
    public static Select Select(params string[] columnsName)
    {
      return new Select(columnsName);
    }

    /// <summary>Построение команды SELECT</summary>
    /// <param name="columns">Список колонок</param>
    /// <returns>Описатель команды</returns>
    public static Select Select(params Column[] columns)
    {
      return new Select(columns);
    }
    #endregion Select

    #region Delete
    /// <summary>Построение команды DELETE</summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Описатель команды</returns>
    public static Delete Delete(string tableName)
    {
      return new Delete(tableName);
    }

    public static Delete Delete(string tableName, string schema)
    {
      return new Delete(tableName, schema);
    }

    ///// <summary>Построение команды DELETE</summary>
    ///// <param name="select">SELECT для удаления</param>
    ///// <returns>Описатель команды</returns>
    //public static Delete Delete(Select select)
    //{
    //  return new Delete(select);
    //}

    #endregion Delete

    #region Insert
    /// <summary>Построение команды INSERT</summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Описатель команды</returns>
    public static Insert Insert(string tableName)
    {
      return new Insert(tableName);
    }
    #endregion Insert

    #region InsertSelect
    /// <summary>Построение команды INSERT INTO ... SELECT</summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Описатель команды</returns>
    public static InsertSelect InsertSelect(string tableName)
    {
      return new InsertSelect(tableName);
    }
    #endregion InsertSelect

    #region Update
    /// <summary>Построение команды UPDATE</summary>
    /// <param name="tableName">Имя таблицы</param>
    /// <returns>Описатель команды</returns>
    public static Update Update(string tableName)
    {
      return new Update(tableName);
    }
    #endregion Update


    #region StoredProc
    /// <summary>Построение команды вызова хранимой процедуры на удаление, добавление, обновление</summary>
    /// <param name="storedProcName">Имя хранимой процедуры</param>
    /// <returns>Описатель команды</returns>
    public static StoredProc StoredProc(string storedProcName)
    {
      return new StoredProc(storedProcName);
    }
    #endregion

    #region Union
    /// <summary>Построение объединения команд SELECT</summary>
    /// <returns>Описатель команды</returns>
    public static Union Union()
    {
      return new Union();
    }
    #endregion Union

    #region GetQueryObject
    /// <summary>Получить объект запроса SELECT</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static SelectQuery GetQueryObject(Select query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }

    /// <summary>Получить объект запроса UNION SELECT</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static OmUnion GetQueryObject(Union query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Uni;
    }

    /// <summary>Получить объект запроса DELETE</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static DeleteQuery GetQueryObject(Delete query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }

    /// <summary>Получить объект запроса INSERT</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static InsertQuery GetQueryObject(Insert query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }

    /// <summary>Получить объект запроса INSERT</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static InsertSelectQuery GetQueryObject(InsertSelect query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }

    /// <summary>Получить объект запроса UPDATE</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static UpdateQuery GetQueryObject(Update query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }
    /// <summary>Получить объект команды выполнения хранимой процедуры</summary>
    /// <param name="query">Описатель запроса</param>
    /// <returns>Объект запроса</returns>
    public static StoredProcQuery GetQueryObject(StoredProc query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof(query));
      return query.Query;
    }
    #endregion GetQueryObject
  }
}
