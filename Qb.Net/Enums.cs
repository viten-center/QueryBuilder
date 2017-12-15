using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viten.QueryBuilder
{
  /// <summary>
  /// Specifies what kind of join should be rendered
  /// </summary>
  public enum JoinType
  {
    /// <summary>Inner Join</summary>
    Inner,
    /// <summary>Left Outer Join</summary>
    Left,
    /// <summary>Right Outer Join</summary>
    Right,
    /// <summary>Full Join</summary>
    Full,
    /// <summary>Cross Join</summary>
    Cross
  }

  /// <summary>
  /// Specifies the type of a FromTerm
  /// </summary>
  public enum FromTermType
  {
    /// <summary>The FromTerm designates a database table or view</summary>
    Table,
    /// <summary>The FromTerm designates a sub-query. Not all databases support sub-queries.</summary>
    SubQuery,
    /// <summary></summary>
    SubQueryObj,
    /// <summary></summary>
    Union
  }

  /// <summary>
  /// Describes the type of a <see cref="OmExpression"/>
  /// </summary>
  public enum OmExpressionType
  {
    /// <summary></summary>
    Function,
    /// <summary></summary>
    Field,
    /// <summary></summary>
    Constant,
    /// <summary></summary>
    SubQueryText,
    /// <summary></summary>
    SubQueryObject,
    /// <summary></summary>
    PseudoField,
    /// <summary></summary>
    Parameter,
    /// <summary></summary>
    Raw,
    /// <summary></summary>
    Case,
    /// <summary></summary>
    IfNull,
    /// <summary></summary>
    Null
  }

  public enum ExprValCode
  {
    String = 0,
    SqlConst = 1,
    SelQuery = 2,
  }

  /// <summary>
  /// Encapsulates SQL DISTINCT or ALL modifiers
  /// </summary>
  public enum UnionMod
  {
    /// <summary>Only distinct rows will be returned</summary>
    Distinct = 0,
    /// <summary>All rows will be returned</summary>
    All = 1
  }

  /// <summary>Перечисление агрегатов</summary>
  public enum AggFunc
  {
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
    Grouping,
    /// <summary>SQL text</summary>
    Raw
  }

  /// <summary>
  /// Класс расширения для AggFunc
  /// </summary>
  public static class AggFuncEx
  {
    /// <summary>Быстрый перевод в строку</summary>
    /// <param name="aggregationFunction"></param>
    /// <returns></returns>
    public static string ToStringFast(this AggFunc aggregationFunction)
    {
      switch (aggregationFunction)
      {
        case AggFunc.None:
          return "None";
        case AggFunc.Sum:
          return "Sum";
        case AggFunc.Count:
          return "Count";
        case AggFunc.Avg:
          return "Avg";
        case AggFunc.Min:
          return "Min";
        case AggFunc.Max:
          return "Max";
        case AggFunc.Grouping:
          return "Grouping";
        default: throw new ArgumentException("aggregationFunction");
      }
    }
  }

  /// <summary>Перечисление логических операций</summary>
  public enum WhereRel
  {
    /// <summary>Logical And</summary>
    And,
    /// <summary>Logical Or</summary>
    Or
  }

  /// <summary>
  /// Класс расширения WhereClauseRelationship
  /// </summary>
  public static class WhereRelEx
  {
    /// <summary>Перевод в строку нижнего регистра</summary>
    /// <param name="whereClauseRelationship"></param>
    /// <returns></returns>
    public static string ToStringFast(this WhereRel whereClauseRelationship)
    {
      switch (whereClauseRelationship)
      {
        case WhereRel.And:
          return "and";
        case WhereRel.Or:
          return "or";
        default: throw new ArgumentException("whereClauseRelationship");
      }
    }
  }

  /// <summary></summary>
  public enum WhereTermType
  {
    /// <summary></summary>
    Compare,
    /// <summary></summary>
    Between,
    /// <summary></summary>
    In,
    /// <summary></summary>
    NotIn,
    /// <summary></summary>
    InSubQuery,
    /// <summary></summary>
    NotInSubQuery,
    /// <summary></summary>
    IsNull,
    /// <summary></summary>
    IsNotNull,
    /// <summary></summary>
    Exists,
    /// <summary></summary>
    NotExists
  }

  /// <summary>Перечисление операций сравнения</summary>
  public enum CompCond
  {
    /// <summary>Equal</summary>
    Equal,
    /// <summary>Different</summary>
    NotEqual,
    /// <summary>Left operand is greater</summary>
    Greater,
    /// <summary>Left operand is less</summary>
    Less,
    /// <summary>Left operand is less or equal</summary>
    LessOrEqual,
    /// <summary>Left operand is greater or equal</summary>
    GreaterOrEqual,
    /// <summary>Make a bitwise AND and check the result for being not null (ex: (a &amp; b) > 0) ) </summary>
    BitwiseAnd,
    /// <summary>Substring. Use '%' signs in the value to match anything</summary>
    Like,
    /// <summary>Substring. Use '%' signs in the value not to match anything</summary>
    NotLike,
  }

  /// <summary>Перечисление типов данных</summary>
  public enum DataType
  {
    /// <summary>String value</summary>
    String,
    /// <summary>Numeric value (int, double, float, decimal)</summary>
    Number,
    /// <summary>DateTime object</summary>
    Date
  }

  /// <summary>
  /// Переичсление направлений сортировки
  /// </summary>
  public enum OrderByDir
  {
    /// <summary>Ascending Order</summary>
    Asc,
    /// <summary>Descending Order</summary>
    Desc
  }

}
