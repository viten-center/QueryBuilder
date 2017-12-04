using Viten.QueryBuilder.SqlOm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>Перечисление агрегатов</summary>
  public enum AggFunc
  {
    /// <summary>No function</summary>
    None,
    /// <summary>Sum</summary>
    Sum,
    /// <summary>Count rows</summary>
    Count,
    /// <summary>Avarage</summary>
    Avg,
    /// <summary>Minimum</summary>
    Min,
    /// <summary>Maximum</summary>
    Max,
    /// <summary>Returns true is the current row is a super-aggregate row when used with ROLLUP or CUBE</summary>
    /// <remarks>Grouping functions is not supported in all databases</remarks>
    Grouping,
    /// <summary>SQL text</summary>
    Raw
  }

  /// <summary>
  /// Класс колонки запроса
  /// </summary>
  public class Column
  {
    internal SelectColumn Col;

    /// <summary>Конструктор</summary>
    internal Column(string columnName)
    {
      Col = new SelectColumn(columnName);
    }

    /// <summary>Конструктор</summary>
    internal Column(string columnName, From table)
    {
      Col = new SelectColumn(columnName, table != null ? table.Term : null);
    }

    /// <summary>Конструктор</summary>
    internal Column(string columnName, From table, string columnAlias)
    {
      Col = new SelectColumn(columnName, table != null ? table.Term : null, columnAlias);
    }

    /// <summary>Конструктор</summary>
    internal Column(string columnName, string columnAlias, AggFunc function)
    {
      Col = new SelectColumn(columnName, null, columnAlias, ExprUtil.ConvertAggregationFunction(function));
    }

    /// <summary>Конструктор</summary>
    internal Column(string columnName, From table, string columnAlias, AggFunc function)
    {
      Col = new SelectColumn(columnName, table != null ? table.Term : null, columnAlias, ExprUtil.ConvertAggregationFunction(function));
    }

    internal Column(Expr expr, string columnAlias)
    {
      Col = new SelectColumn(expr.Expression, columnAlias);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, string columnAlias, AggFunc function)
    {
      return new Column(columnName, columnAlias, function);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName)
    {
      return new Column(columnName);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, From table)
    {
      return new Column(columnName, table);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, From table, string columnAlias)
    {
      return new Column(columnName, table, columnAlias);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, From table, string columnAlias, AggFunc function)
    {
      return new Column(columnName, table, columnAlias, function);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, AggFunc function)
    {
      return new Column(columnName, null, null, function);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(string columnName, From table, AggFunc function)
    {
      return new Column(columnName, table, null, function);
    }

    /// <summary>Определить колонку</summary>
    public static Column New(Expr expr, string columnAlias)
    {
      return new Column(expr, columnAlias);
    }

  }
}
