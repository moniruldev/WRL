using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.Utility
{
    public class SQLHelper
    {
        public static string MakeSqlOrString(List<string> pList, string pFldName, bool pIncludeBracket)
        {
            return MakeSqlOrString(pList.ToArray(), pFldName, pIncludeBracket);
        }

        public static string MakeSqlOrString(string[] pArray, string pFldName, bool pIncludeBracket)
        {
            string strSql;
            int vTot;
            int x;
            string strOR;

            vTot = pArray.GetUpperBound(0);

            strSql = "";
            strOR = "";
            for (x = 0; x <= vTot; x++)
            {
                strSql = strSql + strOR + pFldName + " = " + pArray[x].ToString();
                strOR = " OR ";
            }
            if (pIncludeBracket)
            {
                strSql = "(" + strSql + ")";
            }

            return strSql;

            
        }


        public static string MakeSQLValueString(Object fData)
        {
            string sData;
            if (fData == null || fData == DBNull.Value)
            {
                return "NULL";
            }

            if (fData.GetType() == Type.GetType("System.DateTime"))
            {
                sData = ((DateTime)fData).ToString("dd-MMM-yyyy");
                sData = "'" + sData + "'";
                return sData;
            }

            if (fData.GetType() == Type.GetType("System.String"))
            {
                sData = "'" + (string)fData + "'";
                return sData;
            }

            //any other

            sData = fData.ToString();
            return sData;

        }
    }
}
