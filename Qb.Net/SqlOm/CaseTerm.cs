using System;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Encapsulates a single WHEN ... THEN ... statement
  /// </summary>
  public class CaseTerm 
  {
    WhereClause cond;
    OmExpression val;
    internal CaseTerm()
    {
    }

    /// <summary>
    /// Creates a new CaseTerm
    /// </summary>
    /// <param name="condition">Condition for the WHEN clause</param>
    /// <param name="val">Value for the THEN clause</param>
    public CaseTerm(WhereClause condition, OmExpression val)
    {
      this.cond = condition;
      this.val = val;
    }

    /// <summary></summary>
    public WhereClause Condition
    {
      get { return this.cond; }
    }

    /// <summary></summary>
    public OmExpression Value
    {
      get { return this.val; }
    }

  }
}
