using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class PostgreSqlAdapter : IDbAdapter
  {
    public void CreateDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      string original = setting.ConnectionString;
      try
      {
        AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
        string dbName = (string)sb["Database"];
        sb["Database"] = "postgres";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"CREATE DATABASE {dbName} ENCODING = 'UTF8';";
          cmd.ExecuteNonQuery();
        }
      }
      finally
      {
        setting.ConnectionString = original;
      }
    }

    public void DropDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      string original = setting.ConnectionString;
      try
      {
        AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
        string dbName = (string)sb["Database"];
        sb["Database"] = "postgres";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = '{dbName}' AND pid <> pg_backend_pid(); ";
          cmd.ExecuteNonQuery();
          cmd.CommandText = $"drop database if exists {dbName}";
          cmd.ExecuteNonQuery();
        }
      }
      finally
      {
        setting.ConnectionString = original;
      }
    }

    public bool ExistsDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      string original = setting.ConnectionString;
      try
      {
        AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
        string dbName = (string)sb["Database"];
        sb["Database"] = "postgres";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"SELECT count(datname) from pg_database WHERE datname='{dbName}'";
          return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
      }
      finally
      {
        setting.ConnectionString = original;
      }
    }

    void CheckDatabseProvider(IAnyDbSetting setting)
    {
      if (setting.DatabaseProvider != DatabaseProvider.PostgreSql)
        throw new Exception("DatabaseProvider must be DatabaseProvider.PostgreSql");
    }

  }
}
