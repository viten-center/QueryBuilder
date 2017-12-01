using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Viten.QueryBuilder.Renderer;


namespace Viten.QueryBuilder.Data.AnyDb
{
  public sealed class AnyDbFactory
  {
    private IAnyDbAnnouncer _announcer;
    // ReSharper disable once InconsistentNaming
    internal static readonly Dictionary<DatabaseProvider, string> _mapProvider = new Dictionary<DatabaseProvider, string>();
    // ReSharper disable once InconsistentNaming
    internal static readonly Dictionary<DatabaseProvider, IDbAdapter> _mapAdapter = new Dictionary<DatabaseProvider, IDbAdapter>();

    static AnyDbFactory()
    {
      _mapProvider[DatabaseProvider.SqLite] = "System.Data.SQLite.SQLiteConnection, System.Data.SQLite";
      _mapProvider[DatabaseProvider.MySql] = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";
      _mapProvider[DatabaseProvider.SqlServerCe] = "System.Data.SqlServerCe.SqlCeConnection, System.Data.SqlServerCe";
      _mapProvider[DatabaseProvider.Oracle] = "Oracle.DataAccess.Client.OracleConnection, Oracle.DataAccess";
      _mapProvider[DatabaseProvider.PostgreSql] = "Npgsql.NpgsqlConnection, Npgsql";
      _mapProvider[DatabaseProvider.SqlServer] = null;

      _mapAdapter[DatabaseProvider.SqLite] = new SqLiteAdapter();
      _mapAdapter[DatabaseProvider.SqlServer] = new SqlServerAdapter();
      _mapAdapter[DatabaseProvider.PostgreSql] = new PostgreSqlAdapter();
      _mapAdapter[DatabaseProvider.MySql] = new MySqlAdapter();
      _mapAdapter[DatabaseProvider.SqlServerCe] = new SqlServerCeAdapter();
      _mapAdapter[DatabaseProvider.Oracle] = new OracleAdapter();

    }

    internal readonly IAnyDbSetting AnyDbSetting;


    public AnyDbFactory(IAnyDbSetting anyDbSetting, IAnyDbAnnouncer announcer = null)
    {
      if (anyDbSetting == null) throw new ArgumentNullException(nameof(anyDbSetting));
      _announcer = announcer;
      AnyDbSetting = anyDbSetting;
    }

    public string GetSqlRowCount(Select query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderRowCount(query);
    }

    public string GetSql(Select query, int pageIndex, int pageSize, int totalCount)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderPage(pageIndex, pageSize, totalCount, query);
    }

    public string GetSql(Select query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderSelect(query);
    }

    public string GetSql(Update query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderUpdate(query);
    }

    public string GetSql(Delete query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderDelete(query);
    }

    public string GetSql(Insert query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderInsert(query);
    }

    public string GetSql(InsertSelect query)
    {
      ISqlOmRenderer renderer = Qb.CreateRenderer(AnyDbSetting.DatabaseProvider);
      return renderer.RenderInsertSelect(query);
    }

    public AnyDbConnection OpenConnection()
    {
      DbConnection con = CreateConnection();
      AnyDbConnection any = new AnyDbConnection(this, con, AnyDbSetting.DatabaseProvider, _announcer);
      any.ConnectionString = AnyDbSetting.ConnectionString;
      any.Open();
      return any;
    }

    DbConnection CreateConnection()
    {
      if (!_mapProvider.ContainsKey(AnyDbSetting.DatabaseProvider))
        throw new Exception("Invalid DatabaseProvider");
      string typeName = _mapProvider[AnyDbSetting.DatabaseProvider];
      switch (AnyDbSetting.DatabaseProvider)
      {
        case DatabaseProvider.SqlServer:
          return new SqlConnection();
        default:
          Type type = Type.GetType(typeName, true);
          return (DbConnection)Activator.CreateInstance(type);
      }
    }

    public void CreateDatabase()
    {
      IDbAdapter adapter = GetAdapter();
      adapter.CreateDatabase(AnyDbSetting);
    }

    public void DropDatabase()
    {
      IDbAdapter adapter = GetAdapter();
      adapter.DropDatabase(AnyDbSetting);
    }

    public bool ExistsDatabase()
    {
      IDbAdapter adapter = GetAdapter();
      return adapter.ExistsDatabase(AnyDbSetting);
    }

    IDbAdapter GetAdapter()
    {
      if (_mapAdapter.ContainsKey(AnyDbSetting.DatabaseProvider))
        return _mapAdapter[AnyDbSetting.DatabaseProvider];
      throw new Exception("Invalid DatabaseProvider");
    }
  }
}
