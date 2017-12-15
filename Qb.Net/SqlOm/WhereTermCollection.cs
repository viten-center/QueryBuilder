using System;
using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
  /// <summary>
  ///   A collection that stores <see cref='WhereTerm'/> objects.
  /// </summary>
  public class WhereTermCollection : List<WhereTerm>
  {

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereTermCollection'/>.
    /// </summary>
    public WhereTermCollection()
    {
    }

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereTermCollection'/> based on another <see cref='WhereTermCollection'/>.
    /// </summary>
    /// <param name='val'>
    ///   A <see cref='WhereTermCollection'/> from which the contents are copied
    /// </param>
    public WhereTermCollection(WhereTermCollection val)
      : base(val)
    {
    }

    /// <summary>
    ///   Initializes a new instance of <see cref='WhereTermCollection'/> containing any array of <see cref='WhereTerm'/> objects.
    /// </summary>
    /// <param name='val'>
    ///       A array of <see cref='WhereTerm'/> objects with which to intialize the collection
    /// </param>
    public WhereTermCollection(WhereTerm[] val)
      : base(val)
    {
    }

  }
}
