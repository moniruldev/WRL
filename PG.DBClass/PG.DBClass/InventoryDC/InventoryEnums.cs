using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.InventoryDC
{

    public enum InventoryOrderByEnum
    {
        SLNo = 1,
        Code = 2,
        Name = 3,

    }


    public enum InculdeOpBalanceEnum
    {
        None = 0,
        IncludeALL = 1,
        IncludeYear = 2,
        IncludeDateRange = 3,
        IncludeALLIndvidual = 4,
    }

    public enum IncludePostEnum
    {
        All = 0,
        Posted = 1,
        Unposted = 2,
    }


}
