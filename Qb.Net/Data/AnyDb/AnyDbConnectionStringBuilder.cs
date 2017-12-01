using System;
using System.Data.Common;

namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class AnyDbConnectionStringBuilder : DbConnectionStringBuilder
  {
    public AnyDbConnectionStringBuilder(string connectionString)
    {
      if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

      string[] keyValuePairs = connectionString.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
      foreach (string pair in keyValuePairs)
      {
        string[] keyValie = pair.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
        if(keyValie.Length < 2)
          throw new Exception("Not valid connection string");
        Add(keyValie[0], keyValie[1]);
      }
    }
  }
}