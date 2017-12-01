namespace Viten.QueryBuilder.Renderer
{
    /// <summary>
    /// Renderer for SqlServer
    /// </summary>
    /// <remarks>
    /// Use SqlServerRenderer to render SQL statements for Microsoft SQL Server Compact Edition database.
    /// This version of Sql.Net has been tested with MSSQLCE 3.5
    /// </remarks>
    public class SqlServerCeRenderer : SqlServerRenderer
  {
    /// <summary>Конструктор</summary>
    public SqlServerCeRenderer()
      :base()
    {
      base._topFormat = "top ({0}) ";
    }

    /// <summary>Создает новый объект SqlServerCeRenderer</summary>
    /// <returns></returns>
    public override ISqlOmRenderer CreateNew()
    {
      return new SqlServerCeRenderer();
    }

  }
}
