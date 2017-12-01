using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder
{
  /// <summary>Класс параметра команды</summary>
  public class Param 
  {
    /// <summary>Конструктор</summary>
    internal Param()
    {
      this.DbType = System.Data.DbType.String;
      this.Size = 0;
      this.Scale = 0;
      this.Precision = 0;
      this.IsNullable = false;
      this.Direction = ParameterDirection.Input;
    }
    /// <summary>Возвращает или задает имя объекта</summary>
    public string Name { get; set; }
    
    /// <summary></summary>
    public System.Data.DbType DbType { get; set; }
    /// <summary>Возвращает или задает значение параметра</summary>
    public object Value { get; set; }
    /// <summary>Возвращает или задает максимальную длину данных в столбце</summary>
    public int Size { get; set; }
    /// <summary>Возвращает или задает значение, указывающее, принимает ли этот параметр значения null.</summary>
    public bool IsNullable { get; set; }
    /// <summary></summary>
    public ParameterDirection Direction { get; set; }
    /// <summary>Возвращает или задает максимальное число цифр, используемых для представления свойства Value</summary>
    public byte Precision { get; set; }
    /// <summary>Возвращает или задает число десятичных позиций, которые распознаются для значения Value.</summary>
    public byte Scale { get; set; }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName,
      System.Data.DbType dataType , object value)
    {
      return New(parameterName, dataType, 0, false, ParameterDirection.Input, (byte)0, (byte)0, value);
    }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="size">Максимальная длина значения парамера (Для типа данных String) </param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName, 
      System.Data.DbType dataType, int size, object value)
    {
      return New(parameterName, dataType, size, false, ParameterDirection.Input, (byte)0, (byte)0, value);
    }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="size">Максимальная длина значения парамера (Для типа данных String) </param>
    /// <param name="isNullable">Принимает ли этот параметр значения null</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName, 
      System.Data.DbType dataType, int size, bool isNullable,
      object value)
    {
      return New(parameterName, dataType, size, isNullable, ParameterDirection.Input, (byte)0, (byte)0, value);
    }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="size">Максимальная длина значения парамера (Для типа данных String) </param>
    /// <param name="isNullable">Принимает ли этот параметр значения null</param>
    /// <param name="direction">Направление передачи параметра</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName, 
      System.Data.DbType dataType, int size, bool isNullable, ParameterDirection direction,
      object value)
    {
      return New(parameterName, dataType, size, isNullable, direction, (byte)0, (byte)0, value);
    }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="size">Максимальная длина значения парамера (Для типа данных String) </param>
    /// <param name="isNullable">Принимает ли этот параметр значения null</param>
    /// <param name="direction">Направление передачи параметра</param>
    /// <param name="precision">Максимальное число цифр, используемых для представления свойства Value (Для типа данных Decimal)</param>
    /// <param name="scale">Число десятичных позиций, которые распознаются для значения Value (Для типа данных Decimal)</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName, 
      System.Data.DbType dataType, int size, bool isNullable, ParameterDirection direction,
      byte precision, byte scale, object value)
    {
      return new Param()
      {
        Name = parameterName,
        DbType = dataType,
        Size = size,
        IsNullable = isNullable,
        Direction = direction,
        Precision = precision,
        Scale = scale,
        Value = value
      };
    }

    /// <summary>Создает новый параметр команды</summary>
    /// <param name="parameterName">Имя параметра (имена параметров регистрозависимы)</param>
    /// <param name="dataType">Тип данных</param>
    /// <param name="direction">Направление передачи параметра</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Новый объект параметра</returns>
    public static Param New(string parameterName, 
      System.Data.DbType dataType, ParameterDirection direction,
      object value)
    {
      return New(parameterName, dataType, 0, false, direction, (byte)0, (byte)0, value);
    }
  }
}
