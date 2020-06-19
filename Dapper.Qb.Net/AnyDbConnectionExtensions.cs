using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Viten.QueryBuilder;
using Viten.QueryBuilder.Data.AnyDb;
using Viten.QueryBuilder.Renderer;

// ReSharper disable once CheckNamespace
namespace Dapper
{
  public class MoreDataFlag
  {
    public bool HasMoreData
    {
      get;
      set;
    }
  }

  public static class AnyDbConnectionExtensions
  {
    private static int GetRowCount(int pageNum, int pageSize)
    {
      return pageNum * pageSize + pageSize;
    }

    private static string GetPageSql(AnyDbConnection cnn, Select query, int pageIndex, int pageSize, int totalRowCount)
    {
      return Qb.CreateRenderer(cnn.DatabaseProvider).RenderPage(pageIndex, pageSize, totalRowCount, query);
    }

    private static string GetRowCountSql(AnyDbConnection cnn, Select query)
    {
      return Qb.CreateRenderer(cnn.DatabaseProvider).RenderRowCount(query);
    }

    static DynamicParameters GetParameters(ParamCollection paramCollection)
    {

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
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static int Execute(this AnyDbConnection cnn, Update query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetUpdateSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static long Execute(this AnyDbConnection cnn, Insert query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetInsertSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
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
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Execute(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    //===
    public static async Task<int> ExecuteAsync(this AnyDbConnection cnn, Delete query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string deleteSql = GetDeleteSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteAsync((IDbConnection)cnn, deleteSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<int> ExecuteAsync(this AnyDbConnection cnn, Update query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string updateSql = GetUpdateSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteAsync((IDbConnection)cnn, updateSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<long> ExecuteAsync(this AnyDbConnection cnn, Insert query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string sql = GetInsertSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      if (string.IsNullOrEmpty(query.Query.IdentityField))
      {
        return Convert.ToInt64(await SqlMapper.ExecuteAsync((IDbConnection)cnn, sql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text));
      }
      return Convert.ToInt64(await SqlMapper.ExecuteScalarAsync((IDbConnection)cnn, sql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text));
    }

    public static async Task<int> ExecuteAsync(this AnyDbConnection cnn, InsertSelect query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string insertSelectSql = GetInsertSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteAsync((IDbConnection)cnn, insertSelectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion Execute

    #region Query

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TReturn>(sql, func, parameters, transaction, buffered,  commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, IDbTransaction transaction = null,
      bool buffered = true, int? commandTimeout = null, string splitOn = "Id")
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, func, parameters, transaction, buffered, commandTimeout: commandTimeout, splitOn: splitOn);
    }

    public static IEnumerable<T> Query<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query<T>(sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }

    public static IEnumerable<object> Query(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null, bool buffered = true,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query(type, sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }

    public static IEnumerable<dynamic> Query(this AnyDbConnection cnn, Select query, object param = null,
      IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.Query(sql, parameters, transaction, buffered, commandTimeout, CommandType.Text);
    }

    //===

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this AnyDbConnection cnn, Select query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), string splitOn = "Id")
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>((IDbConnection)cnn, selectSql, func, (object)parameters, transaction, buffered, splitOn, commandTimeout, (CommandType?)null);
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync<T>((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<IEnumerable<object>> QueryAsync(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync((IDbConnection)cnn, type, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<IEnumerable<dynamic>> QueryAsync(this AnyDbConnection cnn, Select query, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryAsync((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static int QueryTotalCount(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string sql = GetRowCountSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteScalar<int>(sql, param: parameters, transaction: transaction, commandTimeout: commandTimeout, commandType: (CommandType?)CommandType.Text);
    }

    public static async Task<int> QueryTotalCountAsync(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string sql = GetRowCountSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await cnn.ExecuteScalarAsync<int>(sql, param: parameters, transaction: transaction, commandTimeout: commandTimeout, commandType: (CommandType?)CommandType.Text);
    }

    public static IEnumerable<T> QueryPage<T>(this AnyDbConnection cnn, Select query, int pageIndex, int pageSize, int totalRowCount, MoreDataFlag flag, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?))
    {
      if (flag == null)
      {
        throw new ArgumentNullException("flag");
      }
      string pageSql = GetPageSql(cnn, query, pageIndex, pageSize, totalRowCount);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      flag.HasMoreData = (totalRowCount > GetRowCount(pageIndex, pageSize));
      return SqlMapper.Query<T>((IDbConnection)cnn, pageSql, (object)parameters, transaction, buffered, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<IEnumerable<T>> QueryPageAsync<T>(this AnyDbConnection cnn, Select query, int pageIndex, int pageSize, int totalRowCount, MoreDataFlag flag, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?))
    {
      string pageSql = GetPageSql(cnn, query, pageIndex, pageSize, totalRowCount);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      flag.HasMoreData = (totalRowCount > GetRowCount(pageIndex, pageSize));
      return await SqlMapper.QueryAsync<T>((IDbConnection)cnn, pageSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<IEnumerable<dynamic>> QueryPageAsync(this AnyDbConnection cnn, Select query, int pageIndex, int pageSize, int totalRowCount, MoreDataFlag flag, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?))
    {
      if (flag == null)
      {
        throw new ArgumentNullException("flag");
      }
      string pageSql = GetPageSql(cnn, query, pageIndex, pageSize, totalRowCount);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      flag.HasMoreData = (totalRowCount > GetRowCount(pageIndex, pageSize));
      return await SqlMapper.QueryAsync((IDbConnection)cnn, pageSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion Query

    #region ExecuteReader
    public static IDataReader ExecuteReader(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteReader(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    //===

    public static async Task<IDataReader> ExecuteReaderAsync(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteReaderAsync((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion ExecuteReader

    #region MyRegion

    public static T ExecuteScalar<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteScalar<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object ExecuteScalar(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.ExecuteScalar(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    //===

    public static async Task<T> ExecuteScalarAsync<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteScalarAsync<T>((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<object> ExecuteScalarAsync(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.ExecuteScalarAsync((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion
    #region QueryFirst
    public static dynamic QueryFirst(this AnyDbConnection cnn, Select query,
      IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object QueryFirst(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst(type, sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static T QueryFirst<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirst<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    //===

    public static async Task<dynamic> QueryFirstAsync(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstAsync((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<object> QueryFirstAsync(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstAsync((IDbConnection)cnn, type, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<T> QueryFirstAsync<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstAsync<T>((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion QueryFirst

    #region QueryFirstOrDefault
    public static dynamic QueryFirstOrDefault(this AnyDbConnection cnn, Select query,
      IDbTransaction transaction = null, int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }


    public static T QueryFirstOrDefault<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault<T>(sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    public static object QueryFirstOrDefault(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null,
      int? commandTimeout = null)
    {
      string sql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
        commandTimeout = cnn.DefaultCommandTimeout;
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return cnn.QueryFirstOrDefault(type, sql, parameters, transaction, commandTimeout, CommandType.Text);
    }

    //===

    public static async Task<dynamic> QueryFirstOrDefaultAsync(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstOrDefaultAsync((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<T> QueryFirstOrDefaultAsync<T>(this AnyDbConnection cnn, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstOrDefaultAsync<T>((IDbConnection)cnn, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    public static async Task<object> QueryFirstOrDefaultAsync(this AnyDbConnection cnn, Type type, Select query, IDbTransaction transaction = null, int? commandTimeout = default(int?))
    {
      string selectSql = GetSelectSql(cnn, query);
      if (!commandTimeout.HasValue)
      {
        commandTimeout = cnn.DefaultCommandTimeout;
      }
      DynamicParameters parameters = GetParameters(query.Query.CommandParams);
      return await SqlMapper.QueryFirstOrDefaultAsync((IDbConnection)cnn, type, selectSql, (object)parameters, transaction, commandTimeout, (CommandType?)CommandType.Text);
    }

    #endregion QueryFirstOrDefault
  }
}
