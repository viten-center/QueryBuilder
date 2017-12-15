using System;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// A collection of elements of type UpdateTerm
  /// </summary>
  public class UpdateTermCollection : List<UpdateTerm>
  {
    /// <summary>
    /// Initializes a new empty instance of the UpdateTermCollection class.
    /// </summary>
    public UpdateTermCollection()
    {
      // empty
    }

    /// <summary>
    /// Initializes a new instance of the UpdateTermCollection class, containing elements
    /// copied from an array.
    /// </summary>
    /// <param name="items">
    /// The array whose elements are to be added to the new UpdateTermCollection.
    /// </param>
    public UpdateTermCollection(UpdateTerm[] items)
      : base(items)
    {
    }

    /// <summary>
    /// Initializes a new instance of the UpdateTermCollection class, containing elements
    /// copied from another instance of UpdateTermCollection
    /// </summary>
    /// <param name="items">
    /// The UpdateTermCollection whose elements are to be added to the new UpdateTermCollection.
    /// </param>
    public UpdateTermCollection(UpdateTermCollection items)
      : base(items)
    {
    }

  }
}
