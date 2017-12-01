using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Viten.QueryBuilder.Test
{
  public class QbTest
  {
    [Fact]
    public void TestWhere()
    {
      Select sel = Qb.Select("*")
        .From("tab")
        .Where(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
        );
      Renderer.SqLiteRenderer renderer = new Renderer.SqLiteRenderer();
      string sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 and [b] > 2))");

      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Logic.And(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
          )
        );
      sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 and [b] > 2))");

      sel = Qb.Select("*")
        .From("tab")
        .Where(
          Logic.Or(
          Cond.Equal("a", 1),
          Cond.Greater("b", 2)
          )
        );
      sql = renderer.RenderSelect(sel);
      Assert.Equal(sql, "select * from [tab] where (([a] = 1 or [b] > 2))");
    } 
  }
}
