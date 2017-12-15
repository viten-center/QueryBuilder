using System;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Encapsulates a single WHEN ... THEN ... statement
  /// </summary>
  public class CaseTerm 
  {
    public CaseTerm()
    {
    }

    /// <summary>
    /// Creates a new CaseTerm
    /// </summary>
    /// <param name="condition">Condition for the WHEN clause</param>
    /// <param name="val">Value for the THEN clause</param>
    public CaseTerm(WhereClause condition, OmExpression val)
    {
      this.Condition = condition;
      this.Value = val;
    }

    /// <summary></summary>
    public WhereClause Condition { get; set; }
    /// <summary></summary>
    public OmExpression Value { get; set; }

  }
}
