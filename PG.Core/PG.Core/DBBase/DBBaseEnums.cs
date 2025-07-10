using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBBase
{
    public enum DatabaseTypeEnum
    {
        SQLServer = 1,
        MSAccess = 2,
        Oracle = 3,
        SQLite = 4,
        MySQL = 5,
    }

    public enum DatabaseConnectModeEnum
    {
        Direct = 1,
        OleDb = 2,
        ODBC = 3,
    }

}
