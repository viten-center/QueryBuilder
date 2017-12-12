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
      this.Type = type;
      switch (type)
      {
        case OmDataType.Date:
          this.DateValue = (DateTime)val;
          break;
        case OmDataType.Number:
          this.NumericValue = val;
          break;
        default:
          this.StringValue = Convert.ToString(val);
          break;
      }
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

    public string StringValue;
    public object NumericValue;
    public DateTime DateValue;

    /// <summary></summary>
    OmDataType _type;
    /// <summary></summary>
    public OmDataType Type
    {
      get { return _type; }
      set { _type = value; }
    }

    /// <summary></summary>
    public object Value
    {
      get
      {
        if (_type == OmDataType.Date)
          return DateValue;
        if (_type == OmDataType.Number)
          return NumericValue;
        return StringValue;
      }
    }

  }
}
