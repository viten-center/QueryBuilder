namespace Viten.QueryBuilder.Data.AnyDb
{
  public interface IAnyDbAnnouncer
  {
    void Announce(string message);
    bool Enabled { get; }
  }
}