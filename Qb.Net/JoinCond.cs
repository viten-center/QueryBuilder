using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{
  /// <summary>Класс определения условий объединения</summary>
  public class JoinCond
  {
    internal JoinCondition Condition;

    internal JoinCond(string leftField, string rightField)
    {
      Condition = new JoinCondition(leftField, rightField);
    }

    /// <summary>Определение соединения по полям с одинаковыми именами</summary>
    public static JoinCond Fields(string field)
    {
      return JoinCond.Fields(field, field);
    }

    /// <summary>Определение соединения по полям с разными именами</summary>
    public static JoinCond Fields(string leftField, string rightField)
    {
      return new JoinCond(leftField, rightField);
    }

  }
}
