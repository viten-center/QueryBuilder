using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class SqlServerAdapter : IDbAdapter
  {
    public void CreateDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      string original = setting.ConnectionString;
      try
      {
        AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
        string dbName = (string)sb["InitialCatalog"];
        sb["InitialCatalog"] = "master";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"CREATE DATABASE {dbName}";
          cmd.ExecuteNonQuery();
          cmd.CommandText = $"ALTER DATABASE {dbName} SET RECOVERY SIMPLE";
          cmd.ExecuteNonQuery();
          cmd.CommandText = $"alter database {dbName} set allow_snapshot_isolation on;";
          cmd.ExecuteNonQuery();
          cmd.CommandText = $"alter database {dbName} set read_committed_snapshot on;";
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
        string dbName = (string)sb["InitialCatalog"];
        sb["InitialCatalog"] = "master";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"IF DB_ID (N'{dbName}') IS NOT NULL DROP DATABASE {dbName}";
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
        string dbName = (string)sb["InitialCatalog"];
        sb["InitialCatalog"] = "master";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"select isnull(DB_ID(N'{dbName}'), -1)";
          return Convert.ToInt32(cmd.ExecuteScalar()) > -1;
        }
      }
      finally
      {
        setting.ConnectionString = original;
      }
    }

    void CheckDatabseProvider(IAnyDbSetting setting)
    {
      if (setting.DatabaseProvider != DatabaseProvider.SqlServer)
        throw new Exception("DatabaseProvider must be DatabaseProvider.SqlServer");
    }

  }
}
