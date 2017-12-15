﻿export enum FromTermType {
  Table,
  SubQuery,
  SubQueryObj,
  Union
}

export enum AggFunc {
  /// <summary>No function</summary>
  None,
  /// <summary>Sum</summary>
  Sum,
  /// <summary>Count rows</summary>
  Count,
  /// <summary>Avarage</summary>
  Avg,
  /// <summary>Minimum</summary>
  Min,
  /// <summary>Maximum</summary>
  Max,
  /// <summary>Returns true is the current row is a super-aggregate row when used with ROLLUP or CUBE</summary>
  /// <remarks>Grouping functions is not supported in all databases</remarks>
  Grouping
  // /// <summary>SQL text</summary>
  // Raw
}


export enum OmExpressionType {
  Function,
  Field,
  Constant,
  SubQueryText,
  SubQueryObject,
  PseudoField,
  Parameter,
  Raw,
  Case,
  IfNull,
  Null
}

// Specifies how tow operands are to be compared
export enum CompOper {
  // <summary>Equal</summary>
  Equal,
  // <summary>Different</summary>
  NotEqual,
  // <summary>Left operand is greater</summary>
  Greater,
  // <summary>Left operand is less</summary>
  Less,
  // <summary>Left operand is less or equal</summary>
  LessOrEqual,
  // <summary>Left operand is greater or equal</summary>
  GreaterOrEqual,
  // <summary>Make a bitwise AND and check the result for being not null (ex: (a &amp; b) > 0) ) </summary>
  BitwiseAnd,
  // <summary>Substring. Use '%' signs in the value to match anything</summary>
  Like,
  // <summary>Substring. Use '%' signs in the value to match anything</summary>
  NotLike
}

/// Encapsulates SQL DISTINCT or ALL modifiers
export enum UnionMod {
  // <summary>Only distinct rows will be returned</summary>
  Distinct = 0,
  // <summary>All rows will be returned</summary>
  All = 1
}

export enum WhereTermType {
  Compare,
  Between,
  In,
  NotIn,
  InSubQuery,
  NotInSubQuery,
  IsNull,
  IsNotNull,
  Exists,
  NotExists
}

export enum DataType {
  // String value
  String,
  // Numeric value (int, long, double, float, decimal)
  Numeric,
  // DateTime object
  Date
}

export enum ExprValCode {
  String,
  SqlConst,
  SelQuery,
}

// Describes the logical relationship between terms of a WHERE clause
export enum WhereRel {
  // Logical And
  And,
  // Logical Or
  Or
}

// Specifies how a result set should be ordered.
export enum OrderByDir {
  // Ascending Order
  Asc,
  // Descending Order
  Desc
}


  //     Specifies the data type of a field, a property, or a Parameter object of a .NET
  //     Framework data provider.
  export enum DbType
  {
    //     A variable-length stream of non-Unicode characters ranging between 1 and 8,000
    //     characters.
    AnsiString = 0,
    //     A variable-length stream of binary data ranging between 1 and 8,000 bytes.
    Binary = 1,
    //     An 8-bit unsigned integer ranging in value from 0 to 255.
    Byte = 2,
    //     A simple type representing Boolean values of true or false.
    Boolean = 3,
    //     A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1
    //     (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency
    //     unit.
    Currency = 4,
    //     A type representing a date value.
    Date = 5,
    //     A type representing a date and time value.
    DateTime = 6,
    //     A simple type representing values ranging from 1.0 x 10 -28 to approximately
    //     7.9 x 10 28 with 28-29 significant digits.
    Decimal = 7,
    //     A floating point type representing values ranging from approximately 5.0 x 10
    //     -324 to 1.7 x 10 308 with a precision of 15-16 digits.
    Double = 8,
    //     A globally unique identifier (or GUID).
    Guid = 9,
    //     An integral type representing signed 16-bit integers with values between -32768
    //     and 32767.
    Int16 = 10,
    //     An integral type representing signed 32-bit integers with values between -2147483648
    //     and 2147483647.
    Int32 = 11,
    //     An integral type representing signed 64-bit integers with values between -9223372036854775808
    //     and 9223372036854775807.
    Int64 = 12,
    //     A general type representing any reference or value type not explicitly represented
    //     by another DbType value.
    Object = 13,
    //     An integral type representing signed 8-bit integers with values between -128
    //     and 127.
    SByte = 14,
    //     A floating point type representing values ranging from approximately 1.5 x 10
    //     -45 to 3.4 x 10 38 with a precision of 7 digits.
    Single = 15,
    //     A type representing Unicode character strings.
    String = 16,
    //     A type representing a SQL Server DateTime value. If you want to use a SQL Server
    //     time value, use System.Data.SqlDbType.Time.
    Time = 17,
    //     An integral type representing unsigned 16-bit integers with values between 0
    //     and 65535.
    UInt16 = 18,
    //     An integral type representing unsigned 32-bit integers with values between 0
    //     and 4294967295.
    UInt32 = 19,
    //     An integral type representing unsigned 64-bit integers with values between 0
    //     and 18446744073709551615.
    UInt64 = 20,
    //     A variable-length numeric value.
    VarNumeric = 21,
    //     A fixed-length stream of non-Unicode characters.
    AnsiStringFixedLength = 22,
    //     A fixed-length string of Unicode characters.
    StringFixedLength = 23,
    //     A parsed representation of an XML document or fragment.
    Xml = 25,
    //     Date and time data. Date value range is from January 1,1 AD through December
    //     31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy
    //     of 100 nanoseconds.
    DateTime2 = 26,
    //     Date and time data with time zone awareness. Date value range is from January
    //     1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999
    //     with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through
    //     +14:00.
    DateTimeOffset = 27
  }
// Specifies what kind of join should be rendered
export enum JoinType {
  Inner,
  Left,
  Right,
  Full,
  Cross
}



