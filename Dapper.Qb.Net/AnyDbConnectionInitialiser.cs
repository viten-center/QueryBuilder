using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viten.QueryBuilder;
using Viten.QueryBuilder.Data.AnyDb;

namespace Dapper
{
  public static class AnyDbConnectionInitialiser
  {
    public static void Initialise()
    {
      Dapper.Contrib.Extensions.SqlMapperExtensions.GetDatabaseType = (con) =>
      {
        AnyDbConnection any = con as AnyDbConnection;
        if (any != null)
        {
          switch (any.DatabaseProvider)
          {
            case DatabaseProvider.MySql:
              return "mysqlconnection";
            case DatabaseProvider.PostgreSql:
              return "npgsqlconnection";
            case DatabaseProvider.SqLite:
              return "sqliteconnection";
            case DatabaseProvider.SqlServer:
              return "sqlconnection";
            case DatabaseProvider.SqlServerCe:
              return "sqlceconnection";
          }
        }
        return con.GetType().Name.ToLower();
      };

    }
  }
}
