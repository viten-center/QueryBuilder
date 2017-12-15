using Viten.QueryBuilder.SqlOm;
using System.Linq;

namespace Viten.QueryBuilder
{
  /// <summary>Класс описания конструкции SELECT ... FROM</summary>
  public class From
  {
    internal FromTerm Term;

    internal From()
    {
    }

    internal From(string tableName)
    {
      Term = FromTerm.Table(tableName);
    }

    internal From(string tableName, string alias)
    {
      Term = FromTerm.Table(tableName, alias);
    }

    internal From(string tableName, string alias, string ns)
    {
      Term = FromTerm.Table(tableName, alias, ns);
    }

    internal From(string tableName, string alias, string ns1, string ns2)
    {
      Term = FromTerm.Table(tableName, alias, ns1, ns2);
    }

    internal From(SelectQuery subQuery, string alias)
    {
      Term = FromTerm.SubQuery(subQuery, alias);
    }

    internal From(OmUnion union, string alias)
    {
      Term = FromTerm.Union(union, alias);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From Table(string tableName)
    {
      return new From(tableName);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From Table(string tableName, string alias)
    {
      return new From(tableName, alias);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From Table(string tableName, string alias, string ns)
    {
      return new From(tableName, alias, ns);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From Table(string tableName, string alias, string ns1, string ns2)
    {
      return new From(tableName, alias, ns1, ns2);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From SubQuery(Select subQuery, string alias)
    {
      SelectQuery selectQuery = Qb.GetQueryObject(subQuery);
      return new From(selectQuery, alias);
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From SubQuery(string subQuery, string alias)
    {
      From from = new From();
      from.Term = FromTerm.SubQuery(subQuery, alias);
      return from;
    }

    /// <summary>Описание конструкции SELECT ... FROM</summary>
    public static From Union(Union union, string alias)
    {
      From from = new From();
      from.Term = FromTerm.Union(Qb.GetQueryObject(union), alias);
      return from;
    }

  }
}
