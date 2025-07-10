using PG.Core.DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBFilters
{
    public class DBFilterSettings
    {
        //Global/Default Filter Settings
        /// <summary>
        /// IncludeWhere: for including 'WHERE' in SQL, Default: False
        /// </summary>
        public bool IncludeWhere = false;    // include where;
        /// <summary>
        /// IncludeOneFilter: Include Minimum One Filter (Where 1=1), Default: True
        /// </summary>
        public bool IncludeOneFilter = false; // include 1=1
        /// <summary>
        /// ParamNameType: How to generate name of the parameter :: StringNumber = {0}{1} :: ParamNumber =  {@0}{@1} :: ParamName = {@id}{@date}
        /// Default: ParamNumber
        /// </summary>
        public DBFilterParamNameTypeEnum ParamNameType = DBFilterParamNameTypeEnum.ParamNumber;     //param name by number or name    
        /// <summary>
        /// DbFilterDBType: Type of the DataBase, Default: SQLServer
        /// </summary>
        //public DBBase.DatabaseTypeEnum DBFilterDBType = DBBase.DatabaseTypeEnum.SQLServer;
        /// <summary>
        /// FilterStyle: Linq= sql genarates by LINQ, Sql = custom sql string, Defult: Linq
        /// </summary>
        public DBFilterStyleEnum FilterStyle = DBFilterStyleEnum.Linq;
        //public static bool SQLStyle = false;        //format sql server style or Linq style

        /// <summary>
        /// SQLStatementCombineBy: For concating existing SQL Statement with Filter SQL String
        /// </summary>
        public string SQLStatementCombineBy = " AND ";

        /// <summary>
        /// PagingRowCount : For Paging default row count if RowCount is 0
        /// </summary>
        public int PagingRowCount = 20;

        public DBContextSettings DBContextSettings = new DBContextSettings();
    }

}
