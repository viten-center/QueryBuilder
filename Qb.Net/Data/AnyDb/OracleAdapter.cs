using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viten.QueryBuilder.Data.AnyDb
{
  internal class OracleAdapter: IDbAdapter
  {
    public void CreateDatabase(IAnyDbSetting setting)
    {
      throw new NotImplementedException();
    }

    public void DropDatabase(IAnyDbSetting setting)
    {
      throw new NotImplementedException();
    }

    public bool ExistsDatabase(IAnyDbSetting setting)
    {
      throw new NotImplementedException();
    }
  }
}
