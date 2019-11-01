using System;
using System.Collections.Generic;
using System.Text;
using Viten.QueryBuilder.Data.AnyDb;
using Dapper;

namespace Viten.QueryBuilder.Test
{
  public class Customer
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  class Program
  {
    public static void Main()
    {
      try
      {
        //SerializationTest.TestAll();
        //QbTest.TestAll();
        AnyDbFactory pg_factory = new AnyDbFactory(new PgDbSetting());
        using(AnyDbConnection con = pg_factory.OpenConnection())
        using(AnyDbCommand cmd = con.CreateCommand())
        {
          Npgsql.NpgsqlParameter p = (Npgsql.NpgsqlParameter)cmd.CreateParameter();
          p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Boolean;
          p.ParameterName = "@p0";
          cmd.Parameters.Add(p);
        }
        


        Dapper.AnyDbConnectionInitialiser.Initialise();
        AnyDbFactory factory = new AnyDbFactory(new AnyDbSetting());
        Select sel = Qb.Select("*")
          .From("Customers");
        IEnumerable<Customer> customers;
        using (AnyDbConnection con = factory.OpenConnection())
        using (AnyDbCommand cmd = con.CreateCommand())
        {
          
          //customers = con.Query<Customer>(sel);
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }

  class PgDbSetting : Data.AnyDb.IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.PostgreSql;
    public string ConnectionString { get; set; } = "host=localhost;database=umk_test;username=developer;password=developer";
    public int CommandTimeout { get; set; } = 30;
  }

  class AnyDbSetting : Data.AnyDb.IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; } = DatabaseProvider.SqLite;
    public string ConnectionString { get; set; } = "Data Source=test.sqlite";
    public int CommandTimeout { get; set; } = 30;
  }


}
