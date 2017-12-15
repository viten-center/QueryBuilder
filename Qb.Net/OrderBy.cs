using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{

  /// <summary>Класс параметров сортировки информации при чтении</summary>
  internal class OrderBy
  {
    internal OrderByTerm Term;

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, From table, OrderByDir dir)
    {
      Term = new OrderByTerm(field, table != null ? table.Term : null, dir);
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, From table)
    {
      Term = new OrderByTerm(field, table != null ? table.Term : null, OrderByDir.Asc);
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field)
    {
      Term = new OrderByTerm(field, null, OrderByDir.Asc);
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, OrderByDir dir)
    {
      Term = new OrderByTerm(field, null, dir);
    }

  }
}
