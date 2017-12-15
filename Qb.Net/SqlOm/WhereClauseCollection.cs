

using System;
using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  ///   A collection that stores <see cref='WhereClause'/> objects.
  /// </summary>
  public class WhereClauseCollection : List<WhereClause>
  {

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereClauseCollection'/>.
    /// </summary>
    public WhereClauseCollection()
    {
    }

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereClauseCollection'/> based on another <see cref='WhereClauseCollection'/>.
    /// </summary>
    /// <param name='val'>
    ///   A <see cref='WhereClauseCollection'/> from which the contents are copied
    /// </param>
    public WhereClauseCollection(WhereClauseCollection val)
      : base(val)
    {
    }

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereClauseCollection'/> containing any array of <see cref='WhereClause'/> objects.
    /// </summary>
    /// <param name='val'>
    ///       A array of <see cref='WhereClause'/> objects with which to intialize the collection
    /// </param>
    public WhereClauseCollection(WhereClause[] val)
      : base(val)
    {
    }

  }
}
