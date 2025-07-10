using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace PG.Core.Utility
{
    /// <summary>
    /// Summary description for Helper
    /// </summary>
    public class Conversion
    {
        public Conversion()
        {
            //
            // TODO: Add constructor logic here
            //
           
        }

     
        public static string ConvertDateStringTo234Format(string strDate)
        {
            string[] pDatePart;
            string rDate = "";
            pDatePart = strDate.Split(new char[] {'/','-'});

            string sDay = pDatePart[0];
            string sMonth = pDatePart[1];
            string sYear = pDatePart[2];

            int iDay;
            if (Int32.TryParse(sDay, out iDay) == false)
            {
                return rDate;
            }

            

            int iMnt;
            if (Int32.TryParse(sMonth, out iMnt) == false)
            {
                iMnt = Helper.GetMonthNo(sMonth);
                if (iMnt == 0)
                {
                    return rDate;
                }
            }
            else
            {
                if (iMnt < 0 || iMnt > 12)
                {
                    return rDate;
                }
            }
            int iYear;
            if (Int32.TryParse(sYear, out iYear) == false)
            {
                return rDate;
            }
            //iYear 

            try
            {
                DateTime dt = new DateTime(iYear, iMnt, iDay);
                rDate = dt.ToString("dd-MMM-yyyy");
            }
            catch
            {
                return rDate;
            }
                  
            return rDate;
        }

        public static object DBNullToNull(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return null;
            }
            else
            {
                return fld;
            }
        }
        public static int DBNullTinyIntToZero(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return (int)0;
            }
            else
            {
                byte d = (byte)fld;
                return Convert.ToInt32(d);
            }
        }

        public static int DBNullSmallIntToZero(Object fld)
        {
            return DBNullSmallIntToZero(fld, 0);
        }
        public static int DBNullSmallIntToZero(Object fld, int replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                Int16 d = (Int16)fld;
                return Convert.ToInt32(d);
            }
        }

        public static int DBNullIntToZero(Object fld)
        {
            return DBNullIntToZero(fld, 0);
        }
        public static int DBNullIntToZero(Object fld, int replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                return Convert.ToInt32(fld);
            }
        }


        public static long DBNullLongToZero(Object fld)
        {
            return DBNullLongToZero(fld, 0);
        }
        public static long DBNullLongToZero(Object fld, long replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                return Convert.ToInt64(fld);
            }
        }

        public static string DBNullIntToEmpty(Object fld)
        {
            return DBNullIntToEmpty(fld, string.Empty);
        }
        public static string DBNullIntToEmpty(Object fld,string format)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                if (format == string.Empty)
                {
                    return  Convert.ToInt32(fld).ToString();
                }
                else
                {
                    return  Convert.ToInt32(fld).ToString(format);
                }
            }
        }
        public static Nullable<int> DBNullIntToNull(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return null;
            }
            else
            {
                return  Convert.ToInt32(fld);
            }
        }


        public static decimal DBNullDecimalToZero(Object fld)
        {
            return DBNullDecimalToZero(fld, 0);
        }
        public static decimal DBNullDecimalToZero(Object fld,decimal replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                return Convert.ToDecimal( fld);
            }
        }
        public static Nullable<decimal> DBNullDecimalToNull(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(fld);
            }
        }

        public static string DBNullDecimalToEmpty(Object fld)
        {
            return DBNullDecimalToEmpty(fld, string.Empty);
        }
        public static string DBNullDecimalToEmpty(Object fld, string format)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                if (format == string.Empty)
                {
                    return Convert.ToDecimal(fld).ToString();
                }
                else
                {
                    return Convert.ToDecimal(fld).ToString(format);
                }
            }
        }


        public static Double DBNullDoubleToZero(Object fld)
        {
            return DBNullDoubleToZero(fld, 0);
        }
        public static Double DBNullDoubleToZero(Object fld, double replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                return Convert.ToDouble(fld);
            }
        }

        public static Single DBNullRealToZero(Object fld)
        {
            return DBNullRealToZero(fld, 0);
        }
        public static Single DBNullRealToZero(Object fld, Single replace)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return replace;
            }
            else
            {
                return  Convert.ToSingle(fld);
            }
        }
        
        public static string DBNullStringToEmpty(Object fld)
        {
            return DBNullStringToString(fld, string.Empty);
        }
        public static string DBNullStringToString(Object fld, string pDefaultValue)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return pDefaultValue;
            }
            else
            {
                return (string)fld;
            }
        }

        public static string DBNullStringToNull(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return null;
            }
            else
            {
                return (string)fld;
            }
        }

        public static bool DBNullBoolToFalse(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return false;
            }
            else
            {
                return  Convert.ToBoolean(fld);
            }
        }
        public static bool DBNullZeroToBool(int val)
        {
            bool bVal;
                       
            if (val == 0)
            {
                bVal=false;
            }
            else
            {
               bVal = true;
            }
            return bVal;
        }

        public static Nullable<DateTime> DBNullDateToNull(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(fld);
            }
        }
        public static string DBNullDateToEmpty(Object fld)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return "";
            }
            else
            {
                return ((DateTime)fld).ToString();
            }
        }
        public static string DBNullDateToEmpty(Object fld,string strFormat)
        {
            if (Convert.IsDBNull(fld) | fld == DBNull.Value)
            {
                return "";
            }
            else
            {
                return ((DateTime)fld).ToString(strFormat);
            }
        }

        public static string NullToEmpty(Object fld)
        {
            return NullToEmpty(fld, string.Empty);
        }
        public static string NullToEmpty(Object fld, string replace)
        {
            if (null == fld)
            {
                return replace;
            }
            else
            {
                return fld.ToString();
            }
        }

        public static int NullIntToZero(Object fld)
        {
            return NullIntToZero(fld, 0);
        }
        public static int NullIntToZero(Object fld, int replace)
        {
            if (null == fld)
            {
                return replace;
            }
            else
            {
                return Convert.ToInt32(fld);
            }
        }

        public static decimal NullDecimalToZero(Object fld)
        {
            return NullDecimalToZero(fld, 0);
        }
        public static decimal NullDecimalToZero(Object fld, decimal replace)
        {
            if (null == fld)
            {
                return replace;
            }
            else
            {
                return Convert.ToDecimal(fld);
            }
        }

        public static double NullDoubleToZero(Object fld)
        {
            return NullDoubleToZero(fld, 0);
        }
        public static double NullDoubleToZero(Object fld, double replace)
        {
            if (null == fld)
            {
                return replace;
            }
            else
            {
                return Convert.ToDouble(fld);
            }
        }

        public static Single NullSingleToZero(Object fld)
        {
            return NullSingleToZero(fld, 0);
        }
        public static Single NullSingleToZero(Object fld, Single replace)
        {
            if (null == fld)
            {
                return replace;
            }
            else
            {
                return Convert.ToSingle(fld);
            }
        }

        public static DateTime? NullDateToNull(Object fld)
        {
            if (fld == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(fld);
            }
        }

        public static string NullDateToEmpty(Object fld)
        {
            if (null == fld)
            {
                return "";
            }
            else
            {
                return ((DateTime)fld).ToString();
            }
        }
        public static string NullDateToEmpty(Object fld, string strFormat)
        {
            if (null == fld )
            {
                return "";
            }
            else
            {
                return ((DateTime)fld).ToString(strFormat);
            }
        }
        
        public static bool StringToBool(string strBool)
        {
            return StringToBool(strBool, "1");
        }
        public static bool StringToBool(string strBool, string trueVal)
        {
            bool rBool;
            if (strBool != null)
            {
                rBool = (strBool == trueVal) ? true : false;
            }
            else
            {
                rBool = false;
            }
            return rBool;
        }

        public static DateTime StringToDate(string strDate)
        {
            return StringToDate(strDate, string.Empty, true);
        }

        public static DateTime StringToDate(string strDate, string strParseFormat)
        {
            return StringToDate(strDate, strParseFormat, true);
        }

        public static DateTime StringToDate(string strDate, string strParseFormat, bool isErrorMinValue)
        {
            DateTime d;

            bool isDate = false;

            if (strParseFormat == string.Empty)
            {
                isDate = DateTime.TryParse(strDate, out d);
            }
            else
            {
                isDate = DateTime.TryParseExact(strDate, strParseFormat,System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out d);
            }

            if (isDate)
            {
                return d;
            }
            else
            {
                if (isErrorMinValue)
                {
                     return DateTime.MinValue;
                }
                throw new ArgumentException("String to date conversion failed");
            }
        }

        public static DateTime? StringToDateORNull(string strDate)
        {
            return StringToDateORNull(strDate, string.Empty);
        }
        
        public static DateTime? StringToDateORNull(string strDate, string strParseFormat)
        {
            DateTime d;

            bool isDate = false;

            if (strParseFormat == string.Empty)
            {
                isDate = DateTime.TryParse(strDate, out d);
            }
            else
            {
                isDate = DateTime.TryParseExact(strDate, strParseFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out d);
            }

            if (isDate)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        public static string StringToNull(string strData)
        {
            if (strData != string.Empty)
            {
                return strData;
            }
            else
            {
                return null;
            }
        }

        public static int StringToInt(string strData)
        {
            return StringToInt(strData, 0);
        }
        public static int StringToInt(string strData, int pDefaultValue)
        {
            if (strData != string.Empty)
            {
                decimal d = StringToDecimal(strData);
                return  Convert.ToInt32(decimal.Truncate(d));
            }
            else
            {
                return pDefaultValue;
            }
        }

        public static decimal StringToDecimal(string strData)
        {
            return StringToDecimal(strData, 0);
        }
        public static decimal StringToDecimal(string strData, decimal pDefaultValue)
        {
            decimal d = pDefaultValue;
            if (strData != string.Empty)
            {
                if (!decimal.TryParse(strData,out d))
                {
                    d = pDefaultValue;
                }
            }
            else
            {
                d = pDefaultValue;
            }
            return d;
        }

        public static double StringToDouble(string strData)
        {
            return StringToDouble(strData, 0);
        }
        public static double StringToDouble(string strData, double pDefaultValue)
        {
            double d = pDefaultValue;
            if (strData != string.Empty)
            {
                if (!double.TryParse(strData, out d))
                {
                    d = pDefaultValue;
                }
            }
            else
            {
                d = pDefaultValue;
            }
            return d;
        }

        public static float StringToFloat(string strData)
        {
            return StringToFloat(strData);
        }
        public static float StringToFloat(string strData, float pDefaultValue)
        {
            float d = pDefaultValue;
            if (strData != string.Empty)
            {
                if (!float.TryParse(strData, out d))
                {
                    d = pDefaultValue;
                }
            }
            else
            {
                d = pDefaultValue;
            }
            return d;
        }

        public static List<string> StringToStringList(string pStrData)
        {
            return StringToStringList(pStrData, ',');
        }
        public static List<string> StringToStringList(string pStrData, char pSeperator)
        {
            List<string> strList = new List<string>();

            if (pStrData.Trim() != string.Empty)
            {
                string[] strArray = pStrData.Split(new char[] { pSeperator });
                strList = strArray.ToList();
            }
            return strList;
        }

        public static List<int> StringToIntList(string pStrData)
        {
            return StringToIntList(pStrData, ',' , 0);
        }
        public static List<int> StringToIntList(string pStrData, char pSeperator)
        {
            return StringToIntList(pStrData, pSeperator, 0);
        }
        public static List<int> StringToIntList(string pStrData, char pSeperator, int pDefaultValue)
        {
            List<int> intList = new List<int>();

            List<string> strList = StringToStringList(pStrData, pSeperator);
            foreach (string strData in strList)
            {
                intList.Add(Utility.Conversion.StringToInt(strData, pDefaultValue));
            }
            return intList;
        }

        public static List<decimal> StringToDecimalList(string pStrData)
        {
            return StringToDecimalList(pStrData, ',', 0);
        }
        public static List<decimal> StringToDecimalList(string pStrData, char pSeperator)
        {
            return StringToDecimalList(pStrData, pSeperator, 0);
        }
        public static List<decimal> StringToDecimalList(string pStrData, char pSeperator, decimal pDefaultValue)
        {
            List<decimal> decimalList = new List<decimal>();

            List<string> strList = StringToStringList(pStrData, pSeperator);
            foreach (string strData in strList)
            {
                decimalList.Add(Utility.Conversion.StringToDecimal(strData, pDefaultValue));
            }
            return decimalList;
        }

        public static List<DateTime> StringToDatelList(string pStrData)
        {
            return StringToDatelList(pStrData, ',');
        }
        public static List<DateTime> StringToDatelList(string pStrData, char pSeperator)
        {
            List<DateTime> datelList = new List<DateTime>();

            List<string> strList = StringToStringList(pStrData, pSeperator);
            foreach (string strData in strList)
            {
                DateTime? cDate = Utility.Conversion.StringToDateORNull(strData);
                if (cDate != null)
                {
                    datelList.Add(cDate.Value);
                }
            }
            return datelList;
        }


        public static void SetParamDateORNull(SqlParameter pParam, DateTime? pDate)
        {
            if (pDate.HasValue)
            {
                pParam.Value = pDate;
            }
            else
            {
                pParam.Value = DBNull.Value;
            }
        }
        public static void SetParamStringORNull(SqlParameter pParam, string strData)
        {
            if (strData != string.Empty)
            {
                pParam.Value = strData;
            }
            else
            {
                pParam.Value = DBNull.Value;
            }
        }

        public static System.Data.SqlTypes.SqlDateTime DateTimeToSqlNull(DateTime? pDateTime)
        {
            if (pDateTime.HasValue)
            {
                return (System.Data.SqlTypes.SqlDateTime)pDateTime.Value;
            }
            else
            {
                return System.Data.SqlTypes.SqlDateTime.Null;
            }

        }
        public static DateTime DateTimeNullToToday(DateTime? pDateTime)
        {
            if (pDateTime.HasValue)
            {
                return (DateTime)pDateTime.Value;
            }
            else
            {
                return DateTime.Today;
            }
        }

        public static string DateTimeNullToEmpty(DateTime? pDateTime)
        {
            return DateTimeNullToEmpty(pDateTime, "dd-MMM-yyyy");
        }

        public static string DateTimeNullToEmpty(DateTime? pDateTime, string dateFormat)
        {
            if (pDateTime.HasValue)
            {
                return pDateTime.Value.ToString(dateFormat);
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<string> GetDateFormat1_ddMMMyyyy()
        {
            List<string> strFormats = new List<string>();

            strFormats.Add("d");
            strFormats.Add("dd");

            strFormats.Add("d/M");
            strFormats.Add("d/MM");
            strFormats.Add("d/MMM");

            strFormats.Add("dd/M");
            strFormats.Add("dd/MM");
            strFormats.Add("dd/MMM");

            strFormats.Add("d/M/y");
            strFormats.Add("d/M/yy");
            strFormats.Add("d/M/yyyy");

            strFormats.Add("dd/MM/y");
            strFormats.Add("dd/MM/yy");
            strFormats.Add("dd/MM/yyyy");

            strFormats.Add("d/MM/y");
            strFormats.Add("d/MM/yy");
            strFormats.Add("d/MM/yyy");

            strFormats.Add("dd/M/y");
            strFormats.Add("dd/M/yy");
            strFormats.Add("dd/M/yyyy");

            strFormats.Add("dd/MMM/y");
            strFormats.Add("dd/MMM/yy");
            strFormats.Add("dd/MMM/yyyy");

            strFormats.Add("d/MMM/y");
            strFormats.Add("d/MMM/yy");
            strFormats.Add("d/MMM/yyyy");


            //==========
            strFormats.Add("d-M");
            strFormats.Add("d-MM");
            strFormats.Add("d-MMM");

            strFormats.Add("dd-M");
            strFormats.Add("dd-MM");
            strFormats.Add("dd-MMM");

            strFormats.Add("d-M-y");
            strFormats.Add("d-M-yy");
            strFormats.Add("d-M-yyyy");

            strFormats.Add("dd-MM-y");
            strFormats.Add("dd-MM-yy");
            strFormats.Add("dd-MM-yyyy");

            strFormats.Add("d-MM-y");
            strFormats.Add("d-MM-yy");
            strFormats.Add("d-MM-yyy");

            strFormats.Add("dd-M-y");
            strFormats.Add("dd-M-yy");
            strFormats.Add("dd-M-yyyy");

            strFormats.Add("dd-MMM-y");
            strFormats.Add("dd-MMM-yy");
            strFormats.Add("dd-MMM-yyyy");

            strFormats.Add("d-MMM-y");
            strFormats.Add("d-MMM-yy");
            strFormats.Add("d-MMM-yyyy");





            //================

            strFormats.Add("d M");
            strFormats.Add("d MM");
            strFormats.Add("d MMM");

            strFormats.Add("dd M");
            strFormats.Add("dd MM");
            strFormats.Add("dd MMM");

            strFormats.Add("d M y");
            strFormats.Add("d M yy");
            strFormats.Add("d M yyyy");

            strFormats.Add("dd MM y");
            strFormats.Add("dd MM yy");
            strFormats.Add("dd MM yyyy");

            strFormats.Add("d MM y");
            strFormats.Add("d MM yy");
            strFormats.Add("d MM yyy");

            strFormats.Add("dd M y");
            strFormats.Add("dd M yy");
            strFormats.Add("dd M yyyy");

            strFormats.Add("dd MMM y");
            strFormats.Add("dd MMM yy");
            strFormats.Add("dd MMM yyyy");

            strFormats.Add("d MMM y");
            strFormats.Add("d MMM yy");
            strFormats.Add("d MMM yyyy");

            //=======================
            strFormats.Add("d.M");
            strFormats.Add("d.MM");
            strFormats.Add("d.MMM");

            strFormats.Add("dd.M");
            strFormats.Add("dd.MM");
            strFormats.Add("dd.MMM");

            strFormats.Add("d.M.y");
            strFormats.Add("d.M.yy");
            strFormats.Add("d.M.yyyy");

            strFormats.Add("dd.MM.y");
            strFormats.Add("dd.MM.yy");
            strFormats.Add("dd.MM.yyyy");

            strFormats.Add("d.MM.y");
            strFormats.Add("d.MM.yy");
            strFormats.Add("d.MM.yyy");

            strFormats.Add("dd.M.y");
            strFormats.Add("dd.M.yy");
            strFormats.Add("dd.M.yyyy");

            strFormats.Add("dd.MMM.y");
            strFormats.Add("dd.MMM.yy");
            strFormats.Add("dd.MMM.yyyy");

            strFormats.Add("d.MMM.y");
            strFormats.Add("d.MMM.yy");
            strFormats.Add("d.MMM.yyyy");



            //===========
            strFormats.Add("dM");
            strFormats.Add("dMM");

            strFormats.Add("ddMMy");
            strFormats.Add("ddMMyy");
            strFormats.Add("ddMMyyyy");

            strFormats.Add("ddMMMy");
            strFormats.Add("ddMMMyy");
            strFormats.Add("ddMMMyyyy");

            return strFormats;
        }

        public static List<string> GetDateFormat2_MMMddyyyy()
        {
            List<string> strFormats = new List<string>();

            strFormats.Add("M");
            strFormats.Add("MM");

            strFormats.Add("M/d");
            strFormats.Add("MM/d");
            strFormats.Add("MMM/d");

            strFormats.Add("M/dd");
            strFormats.Add("MM/dd");
            strFormats.Add("MMM/dd");

            strFormats.Add("M/d/y");
            strFormats.Add("M/d/yy");
            strFormats.Add("M/d/yyyy");

            strFormats.Add("MM/dd/y");
            strFormats.Add("MM/dd/yy");
            strFormats.Add("MM/dd/yyyy");

            strFormats.Add("MM/d/y");
            strFormats.Add("MM/d/yy");
            strFormats.Add("MM/d/yyy");

            strFormats.Add("M/dd/y");
            strFormats.Add("M/dd/yy");
            strFormats.Add("M/dd/yyyy");

            strFormats.Add("MMM/dd/y");
            strFormats.Add("MMM/dd/yy");
            strFormats.Add("MMM/dd/yyyy");

            strFormats.Add("MMM/d/y");
            strFormats.Add("MMM/d/yy");
            strFormats.Add("MMM/d/yyyy");


            //==========
            strFormats.Add("M-d");
            strFormats.Add("MM-d");
            strFormats.Add("MMM-d");

            strFormats.Add("M-dd");
            strFormats.Add("MM-dd");
            strFormats.Add("MMM-dd");

            strFormats.Add("M-d-y");
            strFormats.Add("M-d-yy");
            strFormats.Add("M-d-yyyy");

            strFormats.Add("MM-dd-y");
            strFormats.Add("MM-dd-yy");
            strFormats.Add("MM-dd-yyyy");

            strFormats.Add("MM-d-y");
            strFormats.Add("MM-d-yy");
            strFormats.Add("MM-d-yyy");

            strFormats.Add("M-dd-y");
            strFormats.Add("M-dd-yy");
            strFormats.Add("M-dd-yyyy");

            strFormats.Add("MMM-dd-y");
            strFormats.Add("MMM-dd-yy");
            strFormats.Add("MMM-dd-yyyy");

            strFormats.Add("MMM-d-y");
            strFormats.Add("MMM-d-yy");
            strFormats.Add("MMM-d-yyyy");





            //================

            strFormats.Add("M d");
            strFormats.Add("MM d");
            strFormats.Add("MMM d");

            strFormats.Add("M dd");
            strFormats.Add("MM dd");
            strFormats.Add("MMM dd");

            strFormats.Add("M d y");
            strFormats.Add("M d yy");
            strFormats.Add("M d yyyy");

            strFormats.Add("MM dd y");
            strFormats.Add("MM dd yy");
            strFormats.Add("MM dd yyyy");

            strFormats.Add("MM d y");
            strFormats.Add("MM d yy");
            strFormats.Add("MM d yyy");

            strFormats.Add("M dd y");
            strFormats.Add("M dd yy");
            strFormats.Add("M dd yyyy");

            strFormats.Add("MMM dd y");
            strFormats.Add("MMM dd yy");
            strFormats.Add("MMM dd yyyy");

            strFormats.Add("MMM d y");
            strFormats.Add("MMM d yy");
            strFormats.Add("MMM d yyyy");

            //=======================
            strFormats.Add("M.d");
            strFormats.Add("MM.d");
            strFormats.Add("MMM.d");

            strFormats.Add("M.dd");
            strFormats.Add("MM.dd");
            strFormats.Add("MMM.dd");

            strFormats.Add("M.d.y");
            strFormats.Add("M.d.yy");
            strFormats.Add("M.d.yyyy");

            strFormats.Add("MM.dd.y");
            strFormats.Add("MM.dd.yy");
            strFormats.Add("MM.dd.yyyy");

            strFormats.Add("MM.d.y");
            strFormats.Add("MM.d.yy");
            strFormats.Add("MM.d.yyy");

            strFormats.Add("M.dd.y");
            strFormats.Add("M.dd.yy");
            strFormats.Add("M.dd.yyyy");

            strFormats.Add("MMM.dd.y");
            strFormats.Add("MMM.dd.yy");
            strFormats.Add("MMM.dd.yyyy");

            strFormats.Add("MMM.d.y");
            strFormats.Add("MMM.d.yy");
            strFormats.Add("MMM.d.yyyy");



            //===========
            strFormats.Add("Md");
            strFormats.Add("MMd");

            strFormats.Add("MMddy");
            strFormats.Add("MMddyy");
            strFormats.Add("MMddyyyy");

            strFormats.Add("MMMddy");
            strFormats.Add("MMMddyy");
            strFormats.Add("MMMddyyyy");

            return strFormats;
        }


        public static List<string> GetDateFormats(int dateFromatNo)
        {

            List<string> strFormats = new List<string>();
            switch (dateFromatNo)
            {
                case 1:
                    strFormats = GetDateFormat1_ddMMMyyyy();
                    break;
                case 2:
                    strFormats = GetDateFormat2_MMMddyyyy();
                    break;
            }
            return strFormats;

        }


        public static bool DateParseExact(string strData, out DateTime? oDate)
        {
            return DateParseExact(strData, 1, out oDate);
        }

        public static bool DateParseExact(string strData, int dateFormatNo , out DateTime? oDate)
        {
            if (dateFormatNo == 1)
            {
                strData = strData.Trim();
                if (strData.Length == 1 | strData.Length == 2)
                {
                    strData = strData + ' ' + DateTime.Today.Month.ToString();
                }
            }

            List<string> strFormats = GetDateFormats(1);
            return DateParseExact(strData, strFormats.ToArray(), out oDate);
        }

        public static bool DateParseExact(string strData, string dateFormat, out DateTime? oDate)
        {
            //string[] dateFormats = new[] { "M.d.yyyy", "dd.MM.yyyy" };
            //string[] dateFormats = new string[1] { dateFormat };

            string[] dateFormats = new string[1] { dateFormat };
             

            return DateParseExact(strData, dateFormats, out oDate);

        }

        public static bool DateParseExact(string strData, string[] dateFormats,  out DateTime? oDate)
        {
            DateTime dt;

            bool result = DateTime.TryParseExact(strData, dateFormats, null, System.Globalization.DateTimeStyles.None, out dt);

            if (result)
            {
                oDate = dt;
                
            }
            else
            {
                oDate = null;
            }

            return result;
        }



        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                    System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret = null;
            try
            {
                if (imageToConvert != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imageToConvert.Save(ms, formatOfImage);
                        Ret = ms.ToArray();
                    }
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }
        public static System.Drawing.Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage = null;

            if (byteArrayIn != null)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                returnImage = System.Drawing.Image.FromStream(ms);
            }
            return returnImage;
        }

        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            try
            {
                // Open file for reading 
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader 
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file 
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer 
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader 
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }


            catch (Exception _Exception)
            {
                // Error 
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            return _Buffer;
        }


        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder();
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        public static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D,
                                 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

        public static byte[] HexToByteArray(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }

       

        private static object ConvertDataType(string pTypeName, object fData)
        {
            object cData = null;
            switch (pTypeName.ToLower())
            {
                case "boolean":
                    //bit
                    //dType = "bool";
                    cData = Convert.ToBoolean(fData);
                    break;
                case "byte":
                    //tinyint
                    //dType = "byte";
                    cData = Convert.ToByte(fData);
                    break;

                case "image":
                case "byte[]":
                    //binary, varbinary,image,timestamp   
                    //dType = "byte[]";
                    //fieldType = typeof(byte[]);
                    cData =  (byte[])fData;
                    break;


                case "guid":
                    //uniqeidentifier
                    //dType = "Guid";
                    //fieldType = typeof(Guid);
                    cData = (Guid)fData;
                    break;

                case "int16":
                    //dType = "int16";
                    //fieldType = typeof(Int16);
                    cData = Convert.ToInt16(fData);
                    break;
                case "int":
                case "int32":
                    //int
                    //dType = "int";
                    //fieldType = typeof(Int32);
                    cData = Convert.ToInt32(fData);
                    break;
                case "int64":
                    //bigint
                    //dType = "int64";
                    //fieldType = typeof(Int64);
                    cData = Convert.ToInt64(fData);
                    break;
                case "single":
                    //real
                    //dType = "single";
                    //fieldType = typeof(Single);
                    cData = Convert.ToSingle(fData);
                    break;
                case "double":
                    //dType = "double";
                    //fieldType = typeof(Double);
                    cData = Convert.ToDouble(fData);
                    break;
                case "decimal":
                    //money, smallmoney
                    //dType = "decimal";
                    //fieldType = typeof(Decimal);
                    cData = Convert.ToDecimal(fData);
                    break;
                case "datetime":
                    //datetime, smalldatetime
                    //dType = "DateTime";
                    //fieldType = typeof(DateTime);
                    cData = Convert.ToDateTime(fData);
                    break;

                case "char":
                case "string":
                    //also char,varchar,nvarchar,nchar,text,ntext
                    //dType = "string";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;
                case "object":
                    //sqlvariant
                    //dType = "object";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;

                default:
                    //dType = "string";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;

            }

            //SqlDataReader r;


            return cData;
        }

        public static T ConvertDataRowToObject<T>(DataRow pDataRow) where T : class
        {
            return ConvertDataRowToObject<T>(pDataRow, typeof(T).GetProperties());
        }

        public static T ConvertDataRowToObject<T>(DataRow pDataRow, PropertyInfo[] pTargetProperties) where T : class
        {

            return DBBase.DBMap.ConvertDataRowToObject<T>(pDataRow, pTargetProperties);

            ////	If either the source, or destination is null, return
            //if (pDataRow == null)
            //    return null;

            ////Type targetType = destObject.GetType();
            //Type targetType = typeof(T);
            //T destObject = Activator.CreateInstance<T>();


            //DataTable dtSchema = pDataRow.Table;

            ////	Loop through the target properties
            ////foreach (PropertyInfo targetProp in targetType.GetProperties())
            //foreach (PropertyInfo targetProp in pTargetProperties)
            //{
            //    //	Get the matching property in the destination object
            //    //PropertyInfo targetObj = targetType.GetProperty(p.Name);
            //    string fieldName = targetProp.Name;

            //    if (dtSchema.Columns.Contains(fieldName))
            //    {
            //        object val = null;
            //        if (IsNullableType(targetProp.PropertyType))
            //        {
            //            if (Convert.IsDBNull(pDataRow[fieldName]))
            //            {
            //                val = null;
            //            }
            //            else
            //            {
            //                val = ConvertDataToType(Nullable.GetUnderlyingType(targetProp.PropertyType), pDataRow[fieldName]);
            //            }
            //        }
            //        else
            //        {
            //            if (Convert.IsDBNull(pDataRow[fieldName]))
            //            {
            //                throw new NoNullAllowedException("Cannot assign null value to Non-null field!");
            //            }
            //            val = ConvertDataToType(targetProp.PropertyType, pDataRow[fieldName]);

            //        }

            //        //	Set the value in the destination
            //        if (targetProp.CanWrite)
            //        {
            //            targetProp.SetValue(destObject, val, null);
            //        }
            //    }
            //}  //property loop
            //return destObject;
        }

        public static T ConvertDbReaderRowToObject<T>(DbDataReader pDbReader) where T : class
        {
            return ConvertDbReaderRowToObject<T>(pDbReader, typeof(T).GetProperties());
        }

        public static T ConvertDbReaderRowToObject<T>(DbDataReader pDbReader, PropertyInfo[] pTargetProperties) where T : class
        {

            return DBBase.DBMap.ConvertDbReaderRowToObject<T>(pDbReader, pTargetProperties);

            ////	If either the source, or destination is null, return
            //if (pDbReader == null)
            //    return null;



            ////Type targetType = destObject.GetType();
            //Type targetType = typeof(T);
            //T destObject = Activator.CreateInstance<T>();


            //DataTable dtSchema = pDbReader.GetSchemaTable();

            ////	Loop through the target properties
            ////foreach (PropertyInfo targetProp in targetType.GetProperties())
            //foreach (PropertyInfo targetProp in pTagetProperties)
            //{
            //    //	Get the matching property in the destination object
            //    //PropertyInfo targetObj = targetType.GetProperty(p.Name);
            //    string fieldName = targetProp.Name;

            //    if (dtSchema.Columns.Contains(fieldName))
            //    {
            //        object val = null;

            //        if (IsNullableType(targetProp.PropertyType))
            //        {
            //            if (Convert.IsDBNull(pDbReader[fieldName]))
            //            {
            //                val = null;
            //            }
            //            else
            //            {
            //                val = ConvertDataToType(Nullable.GetUnderlyingType(targetProp.PropertyType), pDbReader[fieldName]);
            //            }
            //        }
            //        else
            //        {
            //            if (Convert.IsDBNull(pDbReader[fieldName]))
            //            {
            //                throw new NoNullAllowedException("Cannot assign null value to Non-null field!");
            //            }
            //            val = ConvertDataToType(targetProp.PropertyType, pDbReader[fieldName]);

            //        }

            //        //	Set the value in the destination
            //        if (targetProp.CanWrite)
            //        {
            //            targetProp.SetValue(destObject, val, null);
            //        }
            //    }
            //}
            //return destObject;
        }

        public static void ConvertObjectToDataRow(object pObj, DataRow pDataRow)
        {
            ConvertObjectToDataRow(pObj, pDataRow, pObj.GetType().GetProperties());
        }

        public static void ConvertObjectToDataRow(object pObj, DataRow pDataRow, PropertyInfo[] pObjProperties)
        {
            DBBase.DBMap.ConvertObjectToDataRow(pObj, pDataRow, pObjProperties);

            //if (pObj == null)
            //{
            //    throw new ArgumentNullException("Object cannot be null");
            //}
            //if (pDataRow == null)
            //{
            //    throw new ArgumentNullException("Data row cannot be null");
            //}

            //DataTable dtSchema = pDataRow.Table;
            //foreach (PropertyInfo objProp in pObjProperties)
            //{
            //    string fieldName = objProp.Name;
            //    if (dtSchema.Columns.Contains(fieldName))
            //    {
            //        object val = objProp.GetValue(objProp,null);
            //        if (val == null)
            //        {
            //            pDataRow[fieldName] = DBNull.Value;
            //        }
            //        else
            //        {
            //            pDataRow[fieldName] = val;
            //        }
            //    }
            //}
        }

        private static object ConvertDataToType(Type pType, object fData)
        {
            return DBBase.DBMap.ConvertDataToType(pType, fData);  
        }


        public static PG.Core.FormDataMode ConvertIntToEditMode(int pEditModeInt)
        {
            FormDataMode mode = FormDataMode.None;

            //int emInt = WebUtility.GetQueryStringInteger(pKey, HttpContext.Current);

            try
            {
                mode = (FormDataMode)pEditModeInt;
            }
            finally { }


            return mode;
        }

        public static Type ConvertTypeCodeToType(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Boolean:
                    return typeof(bool);

                case TypeCode.Byte:
                    return typeof(byte);

                case TypeCode.Char:
                    return typeof(char);

                case TypeCode.DateTime:
                    return typeof(DateTime);

                case TypeCode.DBNull:
                    return typeof(DBNull);

                case TypeCode.Decimal:
                    return typeof(decimal);

                case TypeCode.Double:
                    return typeof(double);

                case TypeCode.Empty:
                    return null;

                case TypeCode.Int16:
                    return typeof(short);

                case TypeCode.Int32:
                    return typeof(int);

                case TypeCode.Int64:
                    return typeof(long);

                case TypeCode.Object:
                    return typeof(object);

                case TypeCode.SByte:
                    return typeof(sbyte);

                case TypeCode.Single:
                    return typeof(Single);

                case TypeCode.String:
                    return typeof(string);

                case TypeCode.UInt16:
                    return typeof(UInt16);

                case TypeCode.UInt32:
                    return typeof(UInt32);

                case TypeCode.UInt64:
                    return typeof(UInt64);
            }

            return null;
        }

        public static Type ConvertDbTypeToType(DbType dbType)
        {
            TypeCode tc = ConvertDbTypeToTypeCode(dbType);
            return ConvertTypeCodeToType(tc);
        }

        public static TypeCode ConvertDbTypeToTypeCode(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                    return TypeCode.String;
                case DbType.Boolean:
                    return TypeCode.Boolean;
                case DbType.Byte:
                    return TypeCode.Byte;
                case DbType.VarNumeric:     // ???
                case DbType.Currency:
                case DbType.Decimal:
                    return TypeCode.Decimal;
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2: // new Katmai type
                case DbType.Time:      // new Katmai type - no TypeCode for TimeSpan
                    return TypeCode.DateTime;
                case DbType.Double:
                    return TypeCode.Double;
                case DbType.Int16:
                    return TypeCode.Int16;
                case DbType.Int32:
                    return TypeCode.Int32;
                case DbType.Int64:
                    return TypeCode.Int64;
                case DbType.SByte:
                    return TypeCode.SByte;
                case DbType.Single:
                    return TypeCode.Single;
                case DbType.UInt16:
                    return TypeCode.UInt16;
                case DbType.UInt32:
                    return TypeCode.UInt32;
                case DbType.UInt64:
                    return TypeCode.UInt64;
                case DbType.Guid:           // ???
                case DbType.Binary:
                case DbType.Object:
                case DbType.DateTimeOffset: // new Katmai type - no TypeCode for DateTimeOffset
                default:
                    return TypeCode.Object;
            }
        }

        public static DbType ConvertTypeToDbType(Type type)
        {
            return ConvertTypeCodeToDbType(Type.GetTypeCode(type));
        }

        public static DbType ConvertTypeCodeToDbType(TypeCode typeCode)
        {
            // no TypeCode equivalent for TimeSpan or DateTimeOffset
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return DbType.Boolean;
                case TypeCode.Byte:
                    return DbType.Byte;
                case TypeCode.Char:
                    return DbType.StringFixedLength;    // ???
                case TypeCode.DateTime: // Used for Date, DateTime and DateTime2 DbTypes
                    return DbType.DateTime;
                case TypeCode.Decimal:
                    return DbType.Decimal;
                case TypeCode.Double:
                    return DbType.Double;
                case TypeCode.Int16:
                    return DbType.Int16;
                case TypeCode.Int32:
                    return DbType.Int32;
                case TypeCode.Int64:
                    return DbType.Int64;
                case TypeCode.SByte:
                    return DbType.SByte;
                case TypeCode.Single:
                    return DbType.Single;
                case TypeCode.String:
                    return DbType.String;
                case TypeCode.UInt16:
                    return DbType.UInt16;
                case TypeCode.UInt32:
                    return DbType.UInt32;
                case TypeCode.UInt64:
                    return DbType.UInt64;
                case TypeCode.DBNull:
                case TypeCode.Empty:
                case TypeCode.Object:
                default:
                    return DbType.Object;
            }
        }




    }
}