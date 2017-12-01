using System;
using System.Collections.Generic;
using System.Text;

namespace Viten.QueryBuilder.Test
{
  class Program
  {
    public static void Main()
    {
      try
      {
        QbTest t = new QbTest();
        t.TestWhere();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}
