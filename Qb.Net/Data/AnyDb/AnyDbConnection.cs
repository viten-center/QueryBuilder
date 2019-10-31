using System;
using System.Data;
using System.Data.Common;


namespace Viten.QueryBuilder.Data.AnyDb
{
  public sealed class AnyDbConnection: DbConnection
  {
    private IAnyDbAnnouncer _announcer;
    private readonly DbConnection _connection;
    private AnyDbFactory _factory;
    internal AnyDbConnection(AnyDbFactory factory, DbConnection connection, DatabaseProvider databaseProvider, IAnyDbAnnouncer announcer)
    {
      _connection = connection ?? throw new ArgumentNullException(nameof(connection));
      _factory = factory ?? throw new ArgumentNullException(nameof(factory));
      _announcer = announcer;
      DatabaseProvider = databaseProvider;
    }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
      return _connection.BeginTransaction(isolationLevel);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if(_connection != null && _connection.State != ConnectionState.Closed)
        Close();
    }
    public override void Close()
    {
      _connection.Close();
    }

    public override void ChangeDatabase(string databaseName)
    {
      _connection.ChangeDatabase(databaseName);
    }

    public override void Open()
    {
      _connection.Open();
    }

    public override string ConnectionString
    {
      get { return _connection.ConnectionString; }
      set { _connection.ConnectionString = value; }
    }

    public override string Database
    {
      get { return _connection.Database; }
    }

    public override ConnectionState State
    {
      get { return _connection.State; }
    }

    public override string DataSource
    {
      get { return _connection.DataSource; }
    }

    public override string ServerVersion
    {
      get { return _connection.ServerVersion; }
    }

    public DatabaseProvider DatabaseProvider { get; }

    protected override DbCommand CreateDbCommand()
    {
      return this.CreateCommand();
    }

    public new AnyDbCommand CreateCommand()
    {
      AnyDbCommand retVal =
        new AnyDbCommand(this, _connection.CreateCommand(), _announcer)
        {
          CommandTimeout = DefaultCommandTimeout
        };
      return retVal;
    }

    public int DefaultCommandTimeout
    {
      get { return _factory.AnyDbSetting.CommandTimeout; }
    }
  }
}
