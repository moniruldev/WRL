using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.SecurityDC
{
    [Flags]
    public enum PermissionEnum
    {
        None = 0,
        Read = 1,
        Add = 2,
        Edit = 4,
        Delete = 8,
        Execute = 16,
        Enabled = 32,
        Visible = 64,
        List = 128,
        Full = 255,
        //128,256,512,
    }
}
