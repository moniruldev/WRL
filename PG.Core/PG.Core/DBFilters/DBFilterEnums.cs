using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBFilters
{
    public enum DBFilterStyleEnum
    {
        Linq = 1,
        DirectString = 2
    }

    public enum DBFilterParamNameTypeEnum
    {
        StringNumber = 1,
        ParamNumber = 2,
        ParamName = 3
    }

    public enum DBFilterCompareTypeEnum
    {
        EqualTo = 1,
        NotEqualTo = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanEqualTo = 5,
        LessThanEqualTo = 6,
        Range = 7,
        IN = 8,
        Contains = 9,
        StartsWith = 10,
        EndsWith = 11,

    }
    public enum DBFilterCombineTypeEnum
    {
        AND = 1,
        OR = 2
    }

    public enum DBFilterDataTypeEnum
    {
        String = 0,
        Integer = 1,
        Decimal = 2,
        Boolean = 3,
        DateTime = 4,
        Date = 5,
        Time = 6,
    }
}
