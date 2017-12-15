using System;

namespace Viten.QueryBuilder.SqlOm
{
	/// <summary>
	/// Represents a typed constant value.
	/// </summary>
  public class OmConstant 
	{
		
    public OmConstant()
    {
    }

		/// <summary>
		/// Creates a new SqlConstant instance
		/// </summary>
		/// <param name="type">Constant's date type</param>
		/// <param name="val">Constant's value</param>
		public OmConstant(DataType type, object val)
		{
      if (val == null && type != DataType.String)
        throw new ArgumentNullException("val");
      this.Type = type;
      switch (type)
      {
        case DataType.Date:
          this.DateValue = Convert.ToDateTime(val);
          break;
        case DataType.Number:
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
			return new OmConstant(DataType.Number, val);
		}
    /// <summary>
    /// Creates a SqlConstant which represents a numeric value.
    /// </summary>
    /// <param name="val">Value of the expression</param>
    /// <returns>A SqlConstant which represents a numeric value</returns>
    public static OmConstant Number(float val)
    {
      return new OmConstant(DataType.Number, val);
    }

    /// <summary>
    /// Creates a SqlConstant which represents a numeric value.
    /// </summary>
    /// <param name="val">Value of the expression</param>
    /// <returns>A SqlConstant which represents a numeric value</returns>
    public static OmConstant Number(long val)
		{
			return new OmConstant(DataType.Number, val);			
		}

    /// <summary>
    /// Creates a SqlConstant which represents a numeric value.
    /// </summary>
    /// <param name="val">Value of the expression</param>
    /// <returns>A SqlConstant which represents a numeric value</returns>
    public static OmConstant Number(int val)
    {
      return new OmConstant(DataType.Number, val);
    }


		/// <summary>
		/// Creates a SqlConstant which represents a textual value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a textual value</returns>
		public static OmConstant String(string val)
		{
			return new OmConstant(DataType.String, val);
		}


		/// <summary>
		/// Creates a SqlConstant which represents a date value.
		/// </summary>
		/// <param name="val">Value of the expression</param>
		/// <returns>A SqlConstant which represents a date value</returns>
		public static OmConstant Date(DateTime val)
		{
			return new OmConstant(DataType.Date, val);
		}

    public string StringValue { get; set; }
    public object NumericValue { get; set; }
    public DateTime DateValue { get; set; }
    public DataType Type { get; set; }

    /// <summary></summary>
    public object Value
    {
      get
      {
        if (Type == DataType.Date)
          return DateValue;
        if (Type == DataType.Number)
          return NumericValue;
        return StringValue;
      }
    }

  }
}
