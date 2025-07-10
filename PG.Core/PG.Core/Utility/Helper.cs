using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Web;
using System.Collections;
using System.IO;
using System.Globalization;




namespace PG.Core.Utility
{
    public class Helper
    {
        public static bool IsWebApp = false;

        public static bool IsCurrentAppWeb()
        {
            bool isWebApp = false;

            isWebApp = Process.GetCurrentProcess().ProcessName == "aspnet_wp";
            if (!isWebApp)
            {
                isWebApp = HttpContext.Current != null;
            }
            if (!isWebApp)
            {
                isWebApp = HttpRuntime.Cache != null;
            }
            if (!isWebApp)
            {
                isWebApp = System.Reflection.Assembly.GetEntryAssembly() == null;
            }
            return isWebApp;
        }



        public static bool IsNullableType(Type theType)
        {
            //use: IsNullableType(property.PropertyType)


            //The NullableConverter Class will allow you to convert a Nullable Type to its underlying type:
            // UnderlyingType will equal System.DateTime
            //NullableConverter nc = new NullableConverter(DateTime?);
            //Type underlyingType = nc.UnderlyingType;


            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        public static Type GetFieldType(string pDBType)
        {
            Type fieldType = typeof(string);


            switch (pDBType.ToLower())
            {
                case "boolean":
                    //bit
                    //dType = "bool";
                    fieldType = typeof(Boolean);
                    break;
                case "byte":
                    //tinyint
                    //dType = "byte";
                    fieldType = typeof(Byte);
                    break;

                case "image":
                case "byte[]":
                    //binary, varbinary,image,timestamp   
                    //dType = "byte[]";
                    fieldType = typeof(byte[]);
                    break;


                case "guid":
                    //uniqeidentifier
                    //dType = "Guid";
                    fieldType = typeof(Guid);
                    break;

                case "int16":
                    //dType = "int16";
                    fieldType = typeof(Int16);
                    break;
                case "int":
                case "int32":
                    //int
                    //dType = "int";
                    fieldType = typeof(Int32);
                    break;
                case "int64":
                    //bigint
                    //dType = "int64";
                    fieldType = typeof(Int64);
                    break;
                case "single":
                    //real
                    //dType = "single";
                    fieldType = typeof(Single);
                    break;
                case "double":
                    //dType = "double";
                    fieldType = typeof(Double);
                    break;
                case "decimal":
                    //money, smallmoney
                    //dType = "decimal";
                    fieldType = typeof(Decimal);
                    break;
                case "datetime":
                    //datetime, smalldatetime
                    //dType = "DateTime";
                    fieldType = typeof(DateTime);
                    break;

                case "char":
                case "string":
                    //also char,varchar,nvarchar,nchar,text,ntext
                    //dType = "string";
                    fieldType = typeof(string);
                    break;
                case "object":
                    //sqlvariant
                    //dType = "object";
                    fieldType = typeof(string);
                    break;

                default:
                    //dType = "string";
                    fieldType = typeof(string);
                    break;

            }

            //SqlDataReader r;


            return fieldType;
        }

        public static object GetPropertyValueByName(Object fromObj, string pPropertyName)
        {
            System.Reflection.PropertyInfo[] properties = fromObj.GetType().GetProperties();
            return GetPropertyValueByName(fromObj, pPropertyName, properties);
        }

        public static Object GetPropertyValueByName(Object fromObj, string pPropertyName, System.Reflection.PropertyInfo[] properties)
        {
            Object val = null;
            //System.Reflection.PropertyInfo[] properties = fromObj.GetType().GetProperties();
            System.Reflection.PropertyInfo propInfo = fromObj.GetType().GetProperty(pPropertyName);
            if (propInfo != null)
            {
                if (propInfo.CanRead)
                {
                    val = propInfo.GetValue(fromObj, null);
                }
            }
            return val;
        }

        public static void SetPropertyValueByName(Object toObj, string pPropertyName, object value)
        {
            System.Reflection.PropertyInfo[] properties = toObj.GetType().GetProperties();
            SetPropertyValueByName(toObj, pPropertyName, value, properties);
        }

        public static void SetPropertyValueByName(Object toObj, string pPropertyName, object value, System.Reflection.PropertyInfo[] properties)
        {
            //System.Reflection.PropertyInfo[] properties = fromObj.GetType().GetProperties();
            System.Reflection.PropertyInfo propInfo = toObj.GetType().GetProperty(pPropertyName);
            if (propInfo != null)
            {
                if (propInfo.CanWrite)
                {
                    propInfo.SetValue(toObj, value, null);
                }
            }
        }

        /// <summary>
        /// Copies source objects property value to destination object proptery by matching property name and type
        /// </summary>
        /// <param name="fromObj"></param>
        /// <param name="toObj"></param>
        public static void CopyPropertyValueByName(Object fromObj, Object toObj)
        {
            //copys propery value from object > to object if property name mathched.
            //property data type must be same;
            System.Reflection.PropertyInfo[] properties = fromObj.GetType().GetProperties();
            System.Reflection.PropertyInfo propInfo;
            System.Reflection.PropertyInfo propInfo1;

            for (int i = 0; i < properties.Length; i++)
            {
                propInfo = properties[i];
                if (propInfo.CanRead)
                {
                    propInfo1 = toObj.GetType().GetProperty(propInfo.Name);
                    if (propInfo1 != null)
                    {
                        if (propInfo1.GetType() != propInfo.GetType())
                        {
                            throw new Exception("Property Data Type Not Matched!");
                        }

                        if (propInfo1.CanWrite)
                        {
                            Object vObj = propInfo.GetValue(fromObj, null);
                            propInfo1.SetValue(toObj, vObj, null);

                        }
                    }
                }
            } //for loop
        }
        /// <summary>
        /// Creates New distination object from coping source object property value by matching property name and type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject"></param>
        /// <param name="destObject"></param>
        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;

                //	Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }
        //public static T ConvertObject<T>(object sourceObject) where T : class, new()
        //{
        //    //	If either the source, or destination is null, return
        //    if (sourceObject == null)
        //        return null;

        //    //	Get the type of each object
        //    Type sourceType = sourceObject.GetType();

        //    //Type targetType = destObject.GetType();
        //    Type targetType = typeof(T);
        //    T destObject = new T();


        //    //	Loop through the source properties
        //    foreach (PropertyInfo p in sourceType.GetProperties())
        //    {
        //        //	Get the matching property in the destination object
        //        PropertyInfo targetObj = targetType.GetProperty(p.Name);
        //        //	If there is none, skip
        //        if (targetObj == null)
        //            continue;

        //        //	Set the value in the destination
        //        targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
        //    }
        //    return destObject;
        //}


        public static T ConvertObject<T>(object sourceObject) where T : class
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null)
                return null;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();

            //Type targetType = destObject.GetType();
            Type targetType = typeof(T);
            T destObject = Activator.CreateInstance<T>();


            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;

                //	Set the value in the destination
                if (targetObj.CanWrite)
                {
                    targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
                }
            }
            return destObject;
        }


        public static int GetMonthNo(string pMonthName)
        {
            int rMonthNo = 0;

            switch (pMonthName.ToLower())
            {
                case "jan":
                case "january":
                    rMonthNo = 1;
                    break;
                case "fab":
                case "fabruary":
                    rMonthNo = 2;
                    break;
                case "mar":
                case "march":
                    rMonthNo = 3;
                    break;
                case "apr":
                case "april":
                    rMonthNo = 4;
                    break;
                case "may":
                    rMonthNo = 5;
                    break;
                case "jun":
                case "june":
                    rMonthNo = 6;
                    break;
                case "jul":
                case "july":
                    rMonthNo = 7;
                    break;
                case "aug":
                case "august":
                    rMonthNo = 8;
                    break;
                case "sep":
                case "september":
                    rMonthNo = 9;
                    break;
                case "oct":
                case "october":
                    rMonthNo = 10;
                    break;
                case "nov":
                case "november":
                    rMonthNo = 11;
                    break;
                case "dec":
                case "december":
                    rMonthNo = 12;
                    break;
                default:
                    rMonthNo = 0;
                    break;
            }
            return rMonthNo;
        }
        public static void InsertIntoArray(Array target, object value, int index)
        {
            if (index < target.GetLowerBound(0) || index > target.GetUpperBound(0))
            {
                throw (new ArgumentOutOfRangeException("index", index,
                  "Array index out of bounds."));
            }
            else
            {
                Array.Copy(target, index, target, index + 1,
                           target.Length - index - 1);
            }

            target.SetValue(value, index);
        }

        public static void InsertIntoArray(Array target, object value)
        {
            int index = target.GetUpperBound(0);

            Array.Copy(target, index, target, index + 1, target.Length - index - 1);

            target.SetValue(value, index);
        }


        public static void RemoveFromArray(Array target, int index)
        {
            if (index < target.GetLowerBound(0) ||
                index > target.GetUpperBound(0))
            {
                throw (new ArgumentOutOfRangeException("index", index,
                  "Array index out of bounds."));
            }
            else if (index < target.GetUpperBound(0))
            {
                Array.Copy(target, index + 1, target, index,
                           target.Length - index - 1);
            }

            target.SetValue(null, target.GetUpperBound(0));
        }


        public static string RepeatString(string repeatString, int repeatCount)
        {
            string strRepeat = string.Empty;

            if (repeatCount > 0)
            {
                ///strRepeat = string.Concat(System.Collections.ArrayList.Repeat(repeatString, repeatCount).ToArray());
                strRepeat = (new StringBuilder().Insert(0, repeatString, repeatCount)).ToString();
            }


            return strRepeat;
        }



        public static bool ErrorHandler()
        {
            //messagebox
            //message
            //to file
            //to db
            //to winlog
            //to mail


            return true;
        }

        public static bool MailManager()
        {
            //send mail
            //receive mail
            return true;
        }

        public static bool AlertManager()
        {
            //create reminder
            //respond to reminder

            return true;
        }

        public static bool DBLogManager()
        {
            //log add/edit/delete by module
            //

            return true;
        }

        public static float GetFitTextWidth(string strText, string fontName, float fontSize, float fitWidth, float minFontSize)
        {
            return GetFitTextWidth(strText, fontName, fontSize, fitWidth, minFontSize, 72);
        }
        public static float GetFitTextWidth(string strText, string fontName, float fontSize, float fitWidth, float minFontSize, int pDpi)
        {

            //0.69743
            int widthPixel = Convert.ToInt32(Math.Ceiling(fitWidth * pDpi));
            int heightPixel = widthPixel;

            Bitmap gImg = new Bitmap(widthPixel, heightPixel);
            //gImg.SetResolution(300, 300);
            gImg.SetResolution(pDpi, pDpi);
            Graphics graphics = Graphics.FromImage(gImg);

            //float minFontSize = 4F;

            float fontSizeTest = fontSize;
            bool isFit = false;
            while (!isFit)
            {
                if (fontSizeTest <= minFontSize)
                {
                    fontSizeTest = minFontSize;
                    isFit = true;
                    break;
                }

                //Font TestFont = GetFontFromPath(fontFilePath, OrgFontFamilyName, (float)AdjustedSize);
                Font TestFont = new Font(fontName, fontSizeTest, GraphicsUnit.Point); // GetFontFromPath(fontFilePath, OrgFontFamilyName, (float)AdjustedSize);
                SizeF size = graphics.MeasureString(strText, TestFont);
                float widthInch = size.Width / 72;


                if (widthInch < fitWidth)
                {
                    isFit = true;
                    break;
                }
                //fontSizeTest = fontSizeTest - 1;
                fontSizeTest = fontSizeTest - .5F;
            }
            return fontSizeTest;
        }



        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


        public static string NumberFormatInBD(string pValue)
        {
            return NumberFormatInBD(pValue,"#,#");
        }

        public static string NumberFormatInBD(string pValue, string strFormat)
        {
            string strVal = string.Empty;

            //CultureInfo cInfo = new CultureInfo("hi-IN");
            //CultureInfo cInfo = new CultureInfo("bn");
            CultureInfo cInfo = new CultureInfo("bn-BD");

            decimal data = Utility.Conversion.StringToDecimal(pValue);

            //strVal = decimal.Parse(pValue).ToString("N2", cInfo);
            //strVal = decimal.Parse(pValue).ToString(strFormat, cInfo);
            strVal = data.ToString(strFormat, cInfo);

            return strVal;
        }

    }
}
