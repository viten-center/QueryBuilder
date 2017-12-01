namespace Viten.QueryBuilder.Data.AnyDb
{
  public interface IAnyDbSetting
  {
    DatabaseProvider DatabaseProvider { get; set; }
    string ConnectionString { get; set; }
    int CommandTimeout { get; set; }
  }
}