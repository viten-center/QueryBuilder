﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Viten.QueryBuilder.Renderer;

namespace Viten.QueryBuilder.Data.AnyDb
{
  public sealed class AnyDbCommand : DbCommand
  {
    private IAnyDbAnnouncer _announcer;
    private AnyDbConnection _anyConnection;
    private readonly DbCommand _dbCommand;

    internal AnyDbCommand(AnyDbConnection anyConnection, DbCommand command, IAnyDbAnnouncer announcer)
    {
      if (anyConnection == null) throw new ArgumentNullException(nameof(anyConnection));
      _anyConnection = anyConnection;
      _announcer = announcer;
      _dbCommand = command;
    }

    public override void Prepare()
    {
      _dbCommand.Prepare();
    }

    public override string CommandText
    {
      get { return _dbCommand.CommandText; }
      set { _dbCommand.CommandText = value; }
    }

    public override int CommandTimeout
    {
      get { return _dbCommand.CommandTimeout; }
      set { _dbCommand.CommandTimeout = value; }
    }

    public override CommandType CommandType
    {
      get { return _dbCommand.CommandType; }
      set { _dbCommand.CommandType = value; }
    }

    public override UpdateRowSource UpdatedRowSource
    {
      get { return _dbCommand.UpdatedRowSource; }
      set { _dbCommand.UpdatedRowSource = value; }
    }

    protected override DbConnection DbConnection
    {
      get { return _anyConnection; }
      set
      {
        _anyConnection = (AnyDbConnection)value;
      }
    }

    protected override DbParameterCollection DbParameterCollection
    {
      get { return _dbCommand.Parameters; }
    }

    protected override DbTransaction DbTransaction
    {
      get { return _dbCommand.Transaction; }
      set { _dbCommand.Transaction = value; }
    }

    public override bool DesignTimeVisible
    {
      get { return _dbCommand.DesignTimeVisible; }
      set { _dbCommand.DesignTimeVisible = value; }
    }

    public override void Cancel()
    {
      _dbCommand.Cancel();
    }

    protected override DbParameter CreateDbParameter()
    {
      return _dbCommand.CreateParameter();
    }

    #region ExecuteReader
    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
      return ExecuteReader(behavior);
    }

    public new DbDataReader ExecuteReader(CommandBehavior behavior)
    {
      if (_announcer != null && _announcer.Enabled)
        _announcer?.Announce(GetAnnounce(_dbCommand));
      return _dbCommand.ExecuteReader(behavior);
    }

    public new DbDataReader ExecuteReader()
    {
      return ExecuteReader(CommandBehavior.Default);
    }

    public DbDataReader ExecuteReader(Select query)
    {
      return ExecuteReader(query, CommandBehavior.Default);
    }

    public DbDataReader ExecuteReader(Select query, CommandBehavior behavior)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderSelect(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      return ExecuteReader(behavior);
    }
    #endregion ExecuteReader

    #region ExecuteNonQuery
    public override int ExecuteNonQuery()
    {
      if (_announcer != null && _announcer.Enabled)
        _announcer?.Announce(GetAnnounce(_dbCommand));
      return _dbCommand.ExecuteNonQuery();
    }

    public int ExecuteNonQuery(Delete query)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderDelete(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      if (_announcer != null && _announcer.Enabled)
        _announcer?.Announce(GetAnnounce(_dbCommand));
      return ExecuteNonQuery();
    }

    public int ExecuteNonQuery(Update query)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderUpdate(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      return ExecuteNonQuery();
    }

    public long ExecuteNonQuery(Insert query)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderInsert(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      if (!string.IsNullOrEmpty(query.Query.IdentityField))
        return Convert.ToInt64(ExecuteScalar());
      return ExecuteNonQuery();
    }

    public int ExecuteNonQuery(InsertSelect query)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderInsertSelect(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      return ExecuteNonQuery();
    }

    #endregion ExecuteNonQuery

    #region ExecuteScalar
    public override object ExecuteScalar()
    {
      if (_announcer != null && _announcer.Enabled)
        _announcer?.Announce(GetAnnounce(_dbCommand));
      return _dbCommand.ExecuteScalar();
    }

    public object ExecuteScalar(Select query)
    {
      ISqlOmRenderer render = Qb.CreateRenderer(_anyConnection.DatabaseProvider);
      _dbCommand.CommandText = render.RenderSelect(query);
      _dbCommand.CommandType = CommandType.Text;
      FillParameters(_dbCommand, query.Query.CommandParams, render);
      return ExecuteScalar();
    }

    #endregion ExecuteScalar

    static void FillParameters(DbCommand cmd, ParamCollection parameters, ISqlOmRenderer render)
    {
      if (cmd.Parameters.Count > 0)
        cmd.Parameters.Clear();
      for (int i = 0; i < parameters.Count; i++)
      {
        DbParameter p = cmd.CreateParameter();
        p.DbType = parameters[i].DbType;
        p.Direction = parameters[i].Direction;
        p.ParameterName = render.CreateParameterName(parameters[i].Name);
        ((IDbDataParameter)p).Precision = parameters[i].Precision;
        ((IDbDataParameter)p).Scale = parameters[i].Scale;
        p.Size = parameters[i].Size;
        p.IsNullable = parameters[i].IsNullable;
        p.Value = parameters[i].Value;
        cmd.Parameters.Add(p);
      }
    }

    static string GetAnnounce(DbCommand cmd)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(DateTime.Now.ToString("G"));
      sb.Append(" | SQL: ");

      sb.Append(cmd.CommandText);
      if (cmd.Parameters.Count > 0)
      {
        sb.Append(" Parameters:");
        bool first = true;
        foreach (DbParameter p in cmd.Parameters)
        {
          if (!first)
            sb.Append(", ");
          first = false;
          sb.Append($" {p.ParameterName}={(p.Value == null ? "null" : p.Value)}");
        }
      }
      return sb.ToString();
    }
  }
}
