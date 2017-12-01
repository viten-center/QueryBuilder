using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{
  /// <summary>
  /// Переичсление направлений сортировки
  /// </summary>
  public enum OrderByDir
  {
    /// <summary>Ascending Order</summary>
    Asc,
    /// <summary>Descending Order</summary>
    Desc
  }

  /// <summary>Класс параметров сортировки информации при чтении</summary>
  internal class OrderBy
  {
    internal OrderByTerm Term;

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, From table, OrderByDir dir)
    {
      Term = new OrderByTerm(field, table != null ? table.Term : null, ExprUtil.ConvertOrderByDir(dir));
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, From table)
    {
      Term = new OrderByTerm(field, table != null ? table.Term : null, OrderByDirection.Ascending);
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field)
    {
      Term = new OrderByTerm(field, null, OrderByDirection.Ascending);
    }

    /// <summary>Конструктор</summary>
    internal OrderBy(string field, OrderByDir dir)
    {
      Term = new OrderByTerm(field, null, ExprUtil.ConvertOrderByDir(dir));
    }

  }
}
