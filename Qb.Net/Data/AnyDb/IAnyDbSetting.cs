namespace Viten.QueryBuilder.Data.AnyDb
{
  public interface IAnyDbSetting
  {
    DatabaseProvider DatabaseProvider { get; set; }
    string ConnectionString { get; set; }
    int CommandTimeout { get; set; }
  }

  public class AnyDbSetting : IAnyDbSetting
  {
    public DatabaseProvider DatabaseProvider { get; set; }
    public string ConnectionString { get; set; }
    public int CommandTimeout { get; set; }
  }
}