using System.Collections.Generic;
namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// A collection of elements of type OrderByTerm
	/// </summary>
	public class OrderByTermCollection : List<OrderByTerm>
	{
		/// <summary>
		/// Initializes a new empty instance of the OrderByTermCollection class.
		/// </summary>
		public OrderByTermCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the OrderByTermCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new OrderByTermCollection.
		/// </param>
		public OrderByTermCollection(OrderByTerm[] items)
      :base(items)
		{
		}

		/// <summary>
		/// Initializes a new instance of the OrderByTermCollection class, containing elements
		/// copied from another instance of OrderByTermCollection
		/// </summary>
		/// <param name="items">
		/// The OrderByTermCollection whose elements are to be added to the new OrderByTermCollection.
		/// </param>
		public OrderByTermCollection(OrderByTermCollection items)
      :base(items)
		{
		}

	}
}