export enum FromTermType {
  Table,
  SubQuery,
  SubQueryObj,
  Union
}

export enum OmAggregationFunction {
  // <summary>No function</summary>
  None, 
  // <summary>Sum</summary>
  Sum, 
  // <summary>Count rows</summary>
  Count, 
  // <summary>Avarage</summary>
  Avg, 
  // <summary>Minimum</summary>
  Min, 
  // <summary>Maximum</summary>
  Max,
  // <summary>Returns true is the current row is a super-aggregate row when used with ROLLUP or CUBE</summary>
  // <remarks>Grouping functions is not supported in all databases</remarks>
  Grouping
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
  Grouping,
  /// <summary>SQL text</summary>
  Raw
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
export enum CompareOperator {
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

export enum CompOper {
  Equal,
  NotEqual,
  Greater,
  Less,
  LessOrEqual,
  GreaterOrEqual,
  BitwiseAnd,
  Like,
  NotLike,
}


/// Encapsulates SQL DISTINCT or ALL modifiers
export enum DistinctModifier {
  // <summary>Only distinct rows will be returned</summary>
  Distinct,
  // <summary>All rows will be returned</summary>
  All
}

export enum UnionMode {
  Distinct,
  All
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

export enum OmDataType {
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
export enum WhereClauseRelationship {
  // Logical And
  And,
  // Logical Or
  Or
}

export enum WhereRel {
  And,
  Or
}

// Specifies how a result set should be ordered.
export enum OrderByDirection { 
  // Ascending Order
  Ascending, 
  // Descending Order
  Descending
}

export enum OrderByDir {
  Asc,
  Desc
}


// Указывает тип данных поля, свойства или объекта Parameter поставщика данных
export enum DbType {
  // Поток переменной длины из символов, не принадлежащих кодировке Юникод. В нем может быть от 1 до 8000 символов.
  AnsiString,
  //               Поток переменной длины из двоичных данных, имеющий длину от 1 до 8000 байт.
  Binary,
  //               8-битовое целое число без знака, которое может принимать значения от 0 до 255.
  Byte,
  //               Простой тип для представления логических значений true и false.
  Boolean,
  //               Значение типа currency, лежащее в диапазоне от -2 63 (или -922,337,203,685,477.5808) до 2 63 -1 (или +922,337,203,685,477.5807) и имеющее точность до одной десятитысячной денежной единицы.
  Currency,
  //               Тип для представления значений даты.
  Date,
  //               Тип для представления значений даты и времени.
  DateTime,
  //               Простой тип для представления значений, лежащих в диапазоне от 1,0 x 10 -28 до приблизительно 7,9 x 10 28 с 28-29 значимыми цифрами.
  Decimal,
  //               Простой тип для представления значений с плавающей запятой, лежащих в диапазоне от 5,0 x 10 -324 до приблизительно 1,7 x 10 308 с точностью до 15-16 знаков.
  Double,
  //               Глобальный уникальный идентификатор (GUID).
  Guid,
  //               Целочисленный тип для представления 16-разрядных целых чисел со знаком, лежащих в диапазоне от -32768 до 32767.
  Int16,
  //               Целочисленный тип для представления 32-битовых целых чисел со знаком в диапазоне от -2147483648 до 2147483647.
  Int32,
  //               Целочисленный тип для представления 64-битовых целых чисел со знаком в диапазоне от -9223372036854775808 до 9223372036854775807.
  Int64,
  //               Общий тип для представления всех значений и ссылок, которые не могут быть представлены ни одним другим значением DbType.
  Object,
  //               Целочисленный тип для представления 8-разрядных целых чисел со знаком, лежащих в диапазоне от -128 до 127.
  SByte,
  //               Простой тип для представления значений с плавающей запятой, лежащих в диапазоне от 1,5 x 10 -45 до 3,4 x 10 38 с точностью до 15-16 знаков.
  Single,
  //               Тип для представления символьных строк Юникода.
  String,
  //               Тип для представления значений времени.
  Time,
  //               Целочисленный тип для представления 16-разрядных целых чисел без знака, лежащих в диапазоне от 0 до 65535.
  UInt16,
  //               Целочисленный тип для представления 32-битовых целых чисел без знака в диапазоне от 0 до 4294967295.
  UInt32,
  //               Целочисленный тип для представления 64-разрядных целых чисел без знака, лежащих в диапазоне от 0 до 18446744073709551615.
  UInt64,
  //               Числовое значение переменной длины.
  VarNumeric,
  //               Поток фиксированной длины из символов, не принадлежащих кодировке Юникод.
  AnsiStringFixedLength,
  //               Строка фиксированной длины из символов Юникода.
  StringFixedLength,
  //               Проанализированное представление фрагмента или документа XML.
  Xml = 25,
  //               Данные даты и времени. Значение даты может находиться в диапазоне от 1 января 1 г. н. э. до 31 декабря 9999 года н. э. Значение времени может находиться в диапазоне от 00:00:00 до 23:59:59.9999999 с точностью до 100 наносекунд.
  DateTime2,
  //               Данные даты и времени, поддерживающие часовые пояса. Значение даты может находиться в диапазоне от 1 января 1 г. н. э. до 31 декабря 9999 года н. э. Значение времени может находиться в диапазоне от 00:00:00 до 23:59:59.9999999 с точностью до 100 наносекунд. Часовые пояса могут находиться в диапазоне от -14:00 до +14:00. 
  DateTimeOffset
}

// Перечисление направлений передачи параметров команды
export enum ParamDirection {
  Input,
  InputOutput,
  Output,
  ReturnValue
}

// Specifies what kind of join should be rendered
export enum JoinType {
		Inner,
		Left,
		Right,
		Full,
		Cross
}

// Перечисление реализации SQL DISTINCT or ALL для UNION
export enum UnionModifier {
  // Only distinct rows will be returned
  Distinct,
  // All rows will be returned
  All
}


