using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Encapsulates SQL CASE clause
  /// </summary>
  public class CaseClause
  {
    /// <summary>
    /// Creates a new CaseClause
    /// </summary>
    public CaseClause()
    {
    }

    CaseTermCollection terms = new CaseTermCollection();

    /// <summary>
    /// Gets the <see cref="CaseTerm"/> collection for this CaseClause
    /// </summary>
    public CaseTermCollection Terms
    {
      get { return this.terms; }
    }

    /// <summary>
    /// Gets or sets the value CASE default value
    /// </summary>
    public OmExpression ElseValue { get; set; }
  }
}
