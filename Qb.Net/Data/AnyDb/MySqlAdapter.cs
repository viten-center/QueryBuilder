using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class MySqlAdapter : IDbAdapter
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
        sb["Database"] = "mysql";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"CREATE DATABASE {dbName};";
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
        sb["Database"] = "mysql";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"DROP DATABASE {dbName};";
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
        sb["Database"] = "mysql";
        setting.ConnectionString = sb.ToString();
        AnyDbFactory factory = new AnyDbFactory(setting);
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          cmd.CommandText = $"SHOW DATABASES LIKE '{dbName}';";
          using (IDataReader reader = cmd.ExecuteReader())
          {
            return reader.Read();
          }
        }
      }
      finally
      {
        setting.ConnectionString = original;
      }
    }

    void CheckDatabseProvider(IAnyDbSetting setting)
    {
      if (setting.DatabaseProvider != DatabaseProvider.MySql)
        throw new Exception("DatabaseProvider must be DatabaseProvider.MySql");
    }

  }
}
