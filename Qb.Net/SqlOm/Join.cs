namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Represnts a Join between two tables.
  /// </summary>
  public class Join
	{
    /// <summary></summary>
    public FromTerm LeftTable { get; set; }
    /// <summary></summary>
    public FromTerm RightTable { get; set; }

    /// <summary></summary>
    public WhereClause Conditions { get; set; }

    /// <summary></summary>
    public JoinType Type { get; set; }

    public Join()
    {
    }

    /// <summary></summary>
    public Join(FromTerm leftTable, FromTerm rightTable, WhereClause conditions, JoinType type)
		{
			LeftTable = leftTable;
			RightTable = rightTable;
			Conditions = conditions;
			Type = type;
		}


  }
}
