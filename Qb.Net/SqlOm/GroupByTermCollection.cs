using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
    /// <summary>
    /// A collection of elements of type GroupByTerm
    /// </summary>
    public class GroupByTermCollection: List<GroupByTerm>
    {
        /// <summary>
        /// Initializes a new empty instance of the GroupByTermCollection class.
        /// </summary>
        public GroupByTermCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the GroupByTermCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new GroupByTermCollection.
        /// </param>
        public GroupByTermCollection(GroupByTerm[] items)
          :base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GroupByTermCollection class, containing elements
        /// copied from another instance of GroupByTermCollection
        /// </summary>
        /// <param name="items">
        /// The GroupByTermCollection whose elements are to be added to the new GroupByTermCollection.
        /// </param>
        public GroupByTermCollection(GroupByTermCollection items)
          :base(items)
        {
        }

    }
}
