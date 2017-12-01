using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
    /// <summary>
    /// Encapsulates SQL CASE clause
    /// </summary>
    public class CaseClause 
	{
		CaseTermCollection terms = new CaseTermCollection();
		OmExpression elseVal = null;
        /// <summary>
		/// Creates a new CaseClause
		/// </summary>
		public CaseClause()
		{
		}

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
		public OmExpression ElseValue
		{
			get { return this.elseVal; }
			set { this.elseVal = value; }
		}

  }
}
