using System;
using System.Collections.Generic;
using System.Text;
using Viten.QueryBuilder.Data.AnyDb;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Viten.QueryBuilder.Test
{
  [Table("customer")]
  public class Customer
  {
    [Key]
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
  }

  class Program
  {
    public static void Main()
    {
      try
      {
        Dapper.AnyDbConnectionInitialiser.Initialise();

        //SerializationTest.TestAll();
        QbTest.TestAll();
        DapperTest.TestAll();

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }

  class SqliteDbSetting : IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SqLite;
    public string ConnectionString { get; set; } = "Data Source=test.db";
    public int CommandTimeout { get; set; } = 30;
  }
  class PgDbSetting : Data.AnyDb.IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.PostgreSql;
    public string ConnectionString { get; set; } = "host=localhost;database=qb_test;username=developer;password=developer";
    public int CommandTimeout { get; set; } = 30;
  }

  class AnyDbSetting : Data.AnyDb.IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SqLite;
    public string ConnectionString { get; set; } = "Data Source=test.sqlite";
    public int CommandTimeout { get; set; } = 30;
  }


}
