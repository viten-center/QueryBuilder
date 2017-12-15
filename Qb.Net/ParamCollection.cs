﻿using System;
using System.Collections.Specialized;

namespace Viten.QueryBuilder
{
  /// <summary>Класс коллекции параметров команды</summary>
  public class ParamCollection : NameObjectCollectionBase
  {
    public void AddRange(Param[] @params)
    {
      if (@params == null)
      {
        throw new ArgumentNullException(nameof(@params));
      }
      foreach (Param item in @params)
        Add(item);
    }
    public void Add(Param item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      if (Contains(item.Name))
        throw new ArgumentException("An element with this name already exists");
      BaseAdd(item.Name, item);
    }

    public void Clear()
    {
      BaseClear();
    }

    public bool Contains(Param item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      return Contains(item.Name);
    }

    public bool Contains(string paramName)
    {
      if (string.IsNullOrEmpty(paramName))
      {
        throw new ArgumentException(nameof(paramName));
      }
      return BaseGet(paramName) != null;
    }

    public bool Remove(Param item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      return Remove(item.Name);
    }

    public bool Remove(string paramName)
    {
      if (string.IsNullOrEmpty(paramName))
      {
        throw new ArgumentException(nameof(paramName));
      }
      bool retVal = Contains(paramName);
      if (retVal)
        BaseRemove(paramName);
      return retVal;
    }

    public Param this[string paramName]
    {
      get
      {
        return (Param)BaseGet(paramName);
      }
    }
    public Param this[int index]
    {
      get
      {
        string key = BaseGetKey(index);
        return this[key];
      }
    }
  }
}
