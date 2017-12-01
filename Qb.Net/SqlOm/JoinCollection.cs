

using System;
using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	///   A collection that stores <see cref='Join'/> objects.
	/// </summary>
	public class JoinCollection : List<Join> 
  {
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/>.
		/// </summary>
		internal JoinCollection()
		{
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/> based on another <see cref='JoinCollection'/>.
		/// </summary>
		/// <param name='val'>
		///   A <see cref='JoinCollection'/> from which the contents are copied
		/// </param>
		internal JoinCollection(JoinCollection val)
		{
			this.AddRange(val);
		}
		
		/// <summary>
		///   Initializes a new instance of <see cref='JoinCollection'/> containing any array of <see cref='Join'/> objects.
		/// </summary>
		/// <param name='val'>
		///       A array of <see cref='Join'/> objects with which to intialize the collection
		/// </param>
		internal JoinCollection(Join[] val)
		{
			this.AddRange(val);
		}
		
	}
}
