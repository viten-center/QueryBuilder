using Viten.QueryBuilder.SqlOm;

namespace Viten.QueryBuilder
{
  internal class GroupBy
  {
    internal GroupByTerm Term;

    public GroupBy(string field, From table)
    {
      Term = new GroupByTerm(field, table.Term);
    }

    public GroupBy(string field)
    {
      Term = new GroupByTerm(field);
    }

  }
}
