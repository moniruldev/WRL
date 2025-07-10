using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.Web
{
    public enum MessageTypeEnum
    {
        None = 0,
        Information = 1,
        Successful = 2,
        Error = 3,
        Permission = 4,
        InvalidData = 5,
        MissingData = 6,
        Wait = 7,
        InProgress = 8,
        Processing = 9
    }

    public enum PageModeEnum
    {
        None = 0,
        InTab = 1,
        InDialog = 2,
        InWindow = 3,
        InTabDialog = 4
    }
}
