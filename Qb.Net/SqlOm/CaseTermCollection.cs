using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// A collection of elements of type CaseTerm
	/// </summary>
	public class CaseTermCollection: List<CaseTerm>
	{
		/// <summary>
		/// Initializes a new empty instance of the CaseTermCollection class.
		/// </summary>
		public CaseTermCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the CaseTermCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new CaseTermCollection.
		/// </param>
		public CaseTermCollection(CaseTerm[] items)
      :base(items)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CaseTermCollection class, containing elements
		/// copied from another instance of CaseTermCollection
		/// </summary>
		/// <param name="items">
		/// The CaseTermCollection whose elements are to be added to the new CaseTermCollection.
		/// </param>
		public CaseTermCollection(CaseTermCollection items)
      :base(items)
		{
		}

	}

}
