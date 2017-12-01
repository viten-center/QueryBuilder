using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Viten.QueryBuilder;
using Viten.QueryBuilder.Data.AnyDb;
using Viten.QueryBuilder.Renderer;

// ReSharper disable once CheckNamespace
namespace Dapper
{
  public static class AnyDbConnectionExtensions
  {
    static DynamicParameters GetParameters(ParamCollection paramCollection)
    {
      if (paramCollection.Count == 0) return null;

      DynamicParameters retVal = new DynamicParameters();

      for (int i = 0; i < paramCollection.Count; i++)
      {
        Param p = paramCollection[i];
        retVal.Add(p.Name, p.Value, p.DbType, p.Direction, p.Size, p.Precision, p.Scale);
      }
      return retVal;
    }

    static string GetSelectSql(AnyDbConnection cnn, Select query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(cnn.DatabaseProvider);
      string sql = renderer.RenderSelect(query);
      return sql;
    }

    static string GetDeleteSql(AnyDbConnection cnn, Delete query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(cnn.DatabaseProvider);
      string sql = renderer.RenderDelete(query);
      return sql;
    }

    static string GetUpdateSql(AnyDbConnection cnn, Update query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(cnn.DatabaseProvider);
      string sql = renderer.RenderUpdate(query);
      return sql;
    }

    static string GetInsertSql(AnyDbConnection cnn, Insert query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(cnn.DatabaseProvider);
      string sql = renderer.RenderInsert(query);
      return sql;
    }

    static string GetInsertSelectSql(AnyDbConnection cnn, InsertSelect query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(cnn.DatabaseProvider);
      string sql = renderer.RenderInsertSelect(query);
      return sql;
    }

    #region Execute
    public static int Execute(this AnyDbConnection cnn, Delete query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetDeleteSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static int Execute(this AnyDbConnection cnn, Update query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetUpdateSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static long Execute(this AnyDbConnection cnn, Insert query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetInsertSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      bool returnIdentity = !string.IsNullOrEmpty(query.Query.IdentityField);
      if (!returnIdentity)
        return Convert.ToInt64(cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text));
      return Convert.ToInt64(cnn.ExecuteScalar(sql, parameters, transaction, commandTimeout, CommandType.Text));
    }

    public static int Execute(this AnyDbConnection cnn, InsertSelect query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetInsertSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    #endregion Execute

    #region Query

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TReturn>(sql, func, parameters, transaction, buffered,  commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<T> Query<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<T>(sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }

    public static IEnumerable<object> Query(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null, bool buffered = true,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query(type, sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }

    public static IEnumerable<dynamic> Query(this AnyDbConnection cnn, Select query, object param = null,
      IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query(sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }
    #endregion Query

    #region ExecuteReader
    public static IDataReader ExecuteReader(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteReader(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }
    #endregion ExecuteReader

    #region MyRegion

    public static T ExecuteScalar<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteScalar<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object ExecuteScalar(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteScalar(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    #endregion
    #region QueryFirst
    public static dynamic QueryFirst(this AnyDbConnection cnn, Select query,
      IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object QueryFirst(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst(type, sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static T QueryFirst<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }
    #endregion QueryFirst

    #region QueryFirstOrDefault
    public static dynamic QueryFirstOrDefault(this AnyDbConnection cnn, Select query,
      IDbTransaction transaction = null, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }


    public static T QueryFirstOrDefault<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object QueryFirstOrDefault(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault(type, sql, parameters, transaction, commandTimeout, CommandType.Text);
    }
    #endregion QueryFirstOrDefault
  }
}
