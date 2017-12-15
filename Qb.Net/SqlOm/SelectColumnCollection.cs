using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// A collection of elements of type SelectColumn
  /// </summary>
  public class SelectColumnCollection : List<SelectColumn>
  {
    /// <summary>
    /// Initializes a new empty instance of the SelectColumnCollection class.
    /// </summary>
    public SelectColumnCollection()
    {
      // empty
    }

    /// <summary>
    /// Initializes a new instance of the SelectColumnCollection class, containing elements
    /// copied from an array.
    /// </summary>
    /// <param name="items">
    /// The array whose elements are to be added to the new SelectColumnCollection.
    /// </param>
    public SelectColumnCollection(SelectColumn[] items)
      : base(items)
    {
    }

    /// <summary>
    /// Initializes a new instance of the SelectColumnCollection class, containing elements
    /// copied from another instance of SelectColumnCollection
    /// </summary>
    /// <param name="items">
    /// The SelectColumnCollection whose elements are to be added to the new SelectColumnCollection.
    /// </param>
    public SelectColumnCollection(SelectColumnCollection items)
      : base(items)
    {
    }

  }
}
