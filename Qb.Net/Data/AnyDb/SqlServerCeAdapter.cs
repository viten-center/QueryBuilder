using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Viten.QueryBuilder.Data.AnyDb
{
  /*
  internal class SqlServerCeAdapter: IDbAdapter
  {
    const string _engineClass = "System.Data.SqlServerCe.SqlCeEngine, System.Data.SqlServerCe";
    public void CreateDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      Type type = Type.GetType(_engineClass, true);
      object engine = Activator.CreateInstance(type);
      type.InvokeMember("LocalConnectionString", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
        null, engine, new object[] {setting.ConnectionString});
      type.InvokeMember("CreateDatabase", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null,
        engine, new object[0]);
    }

    public void DropDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
      string fileName = (string)sb["Data Source"];
      if (File.Exists(fileName))
        File.Delete(fileName);
    }

    public bool ExistsDatabase(IAnyDbSetting setting)
    {
      if (setting == null) throw new ArgumentNullException(nameof(setting));
      CheckDatabseProvider(setting);
      AnyDbConnectionStringBuilder sb = new AnyDbConnectionStringBuilder(setting.ConnectionString);
      string fileName = (string)sb["Data Source"];
      return File.Exists(fileName);
    }

    void CheckDatabseProvider(IAnyDbSetting setting)
    {
      if (setting.DatabaseProvider != DatabaseProvider.SqlServerCe)
        throw new Exception("DatabaseProvider must be DatabaseProvider.SqlServerCe");
    }
  }
  */
}
