using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{

  /// <summary></summary>
  public class OmUnionItem
  {
    /// <summary></summary>
    public SelectQuery Query { get; set; }


    /// <summary></summary>
    public UnionMod RepeatingAction { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public OmUnionItem()
    {
    }

    /// <summary></summary>
    public OmUnionItem(SelectQuery query, UnionMod repeatingAction)
    {
      Query = query;
      RepeatingAction = repeatingAction;
    }

  }

  /// <summary>
  /// Encapsulates SQL UNION statement
  /// </summary>
  public class OmUnion
  {
    List<OmUnionItem> items = new List<OmUnionItem>(5);
    /// <summary>
    /// Creates a new SqlUnion
    /// </summary>
    public OmUnion()
    {
    }

    /// <summary>
    /// Adds a query to the UNION clause
    /// </summary>
    /// <param name="query">SelectQuery to be added</param>
    /// <remarks>Query will be added with DistinctModifier.Distinct </remarks>
    public void Add(SelectQuery query)
    {
      Add(query, UnionMod.Distinct);
    }

    /// <summary>
    /// Adds a query to the UNION clause with the specified DistinctModifier
    /// </summary>
    /// <param name="query">SelectQuery to be added</param>
    /// <param name="repeatingAction">Distinct modifier</param>
    public void Add(SelectQuery query, UnionMod repeatingAction)
    {
      items.Add(new OmUnionItem(query, repeatingAction));
    }

    /// <summary></summary>
    public IList Items
    {
      get { return items; }
    }

  }
}
