using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  /// Encapsulates SQL DISTINCT or ALL modifiers
  /// </summary>
  public enum DistinctModifier
  {
    /// <summary>Only distinct rows will be returned</summary>
    Distinct,
    /// <summary>All rows will be returned</summary>
    All
  }

  /// <summary></summary>
  public class OmUnionItem 
  {
    SelectQuery query;
    /// <summary></summary>
    public SelectQuery Query
    {
      get
      {
        return query;
      }
      internal set
      {
        query = value;
      }
    }

    DistinctModifier repeatingAction;
    /// <summary></summary>
    public DistinctModifier RepeatingAction
    {
      get
      {
        return repeatingAction;
      }
      internal set
      {
        repeatingAction = value;
      }
    }
    /// <summary>
    /// 
    /// </summary>
    public OmUnionItem()
    {
    }

    /// <summary></summary>
    public OmUnionItem(SelectQuery query, DistinctModifier repeatingAction)
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
    /// <summary></summary>
    public const string Namespace = "http://est.by/Sys/DB/SqlOm";
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
      Add(query, DistinctModifier.Distinct);
    }

    /// <summary>
    /// Adds a query to the UNION clause with the specified DistinctModifier
    /// </summary>
    /// <param name="query">SelectQuery to be added</param>
    /// <param name="repeatingAction">Distinct modifier</param>
    public void Add(SelectQuery query, DistinctModifier repeatingAction)
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
