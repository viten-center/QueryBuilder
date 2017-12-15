using System;
using System.Collections;
using System.Collections.Generic;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// A collection of elements of type SqlConstant
	/// </summary>
  public class OmConstantCollection : List<OmConstant>
	{
		/// <summary>
		/// Initializes a new empty instance of the SqlConstantCollection class.
		/// </summary>
		public OmConstantCollection()
		{
		}

		/// <summary>
		/// Initializes a new empty instance of the SqlConstantCollection class with the specified initial capacity
		/// </summary>
		public OmConstantCollection(int capacity)
      :base(capacity)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlConstantCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new SqlConstantCollection.
		/// </param>
		public OmConstantCollection(OmConstant[] items)
      :base(items)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlConstantCollection class, containing elements
		/// copied from another instance of SqlConstantCollection
		/// </summary>
		/// <param name="items">
		/// The SqlConstantCollection whose elements are to be added to the new SqlConstantCollection.
		/// </param>
		public OmConstantCollection(OmConstantCollection items)
      : base(items)
		{
		}

		/// <summary>
		/// Creates a SqlConstantCollection from a list of values.
		/// </summary>
		/// <remarks>
		/// The type of SqlConstant items in the collection is determined automatically.
		/// See Add method for more info.
		/// </remarks>
		/// <param name="values"></param>
		/// <returns></returns>
		public static OmConstantCollection FromList(IList values)
		{
			OmConstantCollection collection = new OmConstantCollection(values.Count);
			foreach(object val in values)
				collection.Add(val);
			return collection;
		}

		/// <summary>
		/// Adds a value
		/// </summary>
		/// <param name="val">The value which is to be added</param>
		/// <remarks>
		/// This method automatically determins the type of the value and creates the appropriate SqlConstant object.
		/// </remarks>
		internal void Add(object val)
		{
			if (val == null)
				return;

			OmConstant constant;
			if (val is string)
				constant = OmConstant.String((string)val);
			else if (val is DateTime)
				constant = OmConstant.Date((DateTime)val);
      else if (val is int)
        constant = OmConstant.Number((int)val);
      else if (val is double)
				constant = OmConstant.Number((double)val);
			else if (val is float)
				constant = OmConstant.Number((double)val);
			else
				constant = OmConstant.String(val.ToString());
			
			base.Add(constant);
		}
 
	}

}
