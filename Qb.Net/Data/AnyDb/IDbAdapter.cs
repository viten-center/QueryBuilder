using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder.Data.AnyDb
{
  interface IDbAdapter
  {
    void CreateDatabase(IAnyDbSetting setting);
    void DropDatabase(IAnyDbSetting setting);
    bool ExistsDatabase(IAnyDbSetting setting);
  }
}
