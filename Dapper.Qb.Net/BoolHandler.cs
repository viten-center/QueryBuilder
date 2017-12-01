using System;
using System.Collections.Generic;
using System.Data;

// ReSharper disable once CheckNamespace
namespace Dapper
{
  class BoolHandler : SqlMapper.ITypeHandler
  {
    public static readonly BoolHandler Default = new BoolHandler();
    public void SetValue(IDbDataParameter parameter, object value)
    {
      parameter.Value = value;
    }

    public object Parse(Type destinationType, object value)
    {
      if (value == null || value == DBNull.Value)
        return null;
      return Convert.ToBoolean(value);
    }
  }
}
