using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core
{
    public enum ApplicationTypeEnum
    {
        Desktop = 1,
        Web = 2,
    }



    public enum FormDataMode
    {
        None = 0,
        Read = 1,
        Add = 2,
        Edit = 3,
        Delete = 4,
        List = 5,
    }

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
        Full = 127,
        //128,256,512,
    }


    public enum TriState
    {
        None = 0,
        True = 1,
        False = 2,
    }

}
