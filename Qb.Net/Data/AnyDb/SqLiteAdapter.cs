using System;
using System.Data.Common;
using System.IO;

namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class SqLiteAdapter: IDbAdapter
  {
    public static class Const
    {
      public const int PageSize = 8192;
      public const int MaxPageCount = 1073741823;
      public const int DefaultTimeout = 30;
      public const string JournalMode = "Wal";
    }

    public bool ExistsDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      AnyDbConnectionStringBuilder sb = CheckConnectionString(setting.ConnectionString);
      string fileName = (string)sb["Data Source"];
      return File.Exists(fileName);
    }

    public void CreateDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      AnyDbConnectionStringBuilder sb = CheckConnectionString(setting.ConnectionString);
      CheckFolder(Path.GetDirectoryName((string)sb["Data Source"]));
      AnyDbFactory factory = new AnyDbFactory(setting);
      using (AnyDbConnection con = factory.OpenConnection())
      using (AnyDbCommand cmd = con.CreateCommand())
      {
        cmd.CommandText = "PRAGMA encoding = 'UTF-8'";
        cmd.ExecuteNonQuery();
      }
    }

    public void DropDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      AnyDbConnectionStringBuilder sb = CheckConnectionString(setting.ConnectionString);
      string fileName = (string)sb["Data Source"];
      if (File.Exists(fileName))
        File.Delete(fileName);
    }

    AnyDbConnectionStringBuilder CheckConnectionString(string connectionString)
    {
      AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(connectionString);
      if (!sb.ContainsKey("PageSize"))
        sb.Add("PageSize", Const.PageSize);
      if (!sb.ContainsKey("MaxPageCount"))
        sb.Add("MaxPageCount", Const.MaxPageCount);
      if (!sb.ContainsKey("DefaultTimeout"))
        sb.Add("DefaultTimeout", Const.DefaultTimeout);
      if (!sb.ContainsKey("JournalMode"))
        sb.Add("JournalMode", Const.JournalMode);
      return sb;
    }

    void CheckFolder(string folder)
    {
      if (string.IsNullOrEmpty(folder)) return;
      if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);
    }

    void CheckDatabseProvider(IAnyDbSetting setting)
    {
      if (setting.DatabaseProvider != DatabaseProvider.SqLite)
        throw new Exception("DatabaseProvider must be DatabaseProvider.SqLite");
    }
  }
}