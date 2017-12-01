using System;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Data type of a constant value
	/// </summary>
	public enum OmDataType
	{
		/// <summary>String value</summary>
		String, 
		/// <summary>Numeric value (int, double, float, decimal)</summary>
		Number, 
		/// <summary>DateTime object</summary>
		Date
	}

	/// <summary>
	/// Represents a typed constant value.
	/// </summary>
  public class OmConstant 
	{
		OmDataType type;
		object val;
    internal OmConstant()
    {
    }

		/// <summary>
		/// Creates a new SqlConstant instance
		/// </summary>
		/// <param name="type">Constant's date type</param>
		/// <param name="val">Constant's value</param>
		public OmConstant(OmDataType type, object val)
		{
      if (val == null && type != OmDataType.String)
        throw new ArgumentNullException("val");
			this.type = type;
			this.val = val;
		}

		/// <summary>
		/// Creates a SqlConstant which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a floating point value</returns>
		public static OmConstant Number(double val)
		{
			return new OmConstant(OmDataType.Number, val);
		}

    //public static SqlConstant Number(decimal val)
    //{
    //  return new SqlConstant(SqlDataType.Number, val);
    //}

		/// <summary>
		/// Creates a SqlConstant which represents a numeric value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a numeric value</returns>
		public static OmConstant Number(long val)
		{
			return new OmConstant(OmDataType.Number, val);			
		}

    /// <summary>
    /// Creates a SqlConstant which represents a numeric value.
    /// </summary>
    /// <param name="val">Value of the expression</param>
    /// <returns>A SqlConstant which represents a numeric value</returns>
    public static OmConstant Number(int val)
    {
      return new OmConstant(OmDataType.Number, val);
    }


		/// <summary>
		/// Creates a SqlConstant which represents a textual value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a textual value</returns>
		public static OmConstant String(string val)
		{
			return new OmConstant(OmDataType.String, val);
		}


		/// <summary>
		/// Creates a SqlConstant which represents a date value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a date value</returns>
		public static OmConstant Date(DateTime val)
		{
			return new OmConstant(OmDataType.Date, val);
		}

    /// <summary></summary>
    public OmDataType Type
		{
			get { return this.type; }
		}

    /// <summary></summary>
    public object Value
		{
			get { return this.val; }
		}

  }
}
