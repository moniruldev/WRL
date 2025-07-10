using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using PG.Core.DBBase;

namespace PG.Core.DBFilters
{
    public class DBFilterManager
    {
        public static DBFilterSettings DefaultFilterSettings = new DBFilterSettings();

        /// <summary>
        /// GetFilterResult(): Generates Linq/Sql string with values of the filters
        /// </summary>
        /// <param name="listFilter"></param>
        /// <returns></returns>
        /// 
        public static DBFilterResult GetFilterResult(List<DBFilter> listFilter)
        {
            return GetFilterResult(listFilter, DefaultFilterSettings);
        }
        //public static DBFilterResult GetFilterResultOLD(List<DBFilter> listFilter, DBFilterSettings fs)
        //{
        //    if (fs == null)
        //    {
        //        throw new ArgumentNullException("Filter Settings Cannot be NULL!");
        //    }

        //    string strSql = string.Empty;
        //    List<string> listParams = new List<string>();
        //    List<object> listValues = new List<object>();
        //    string strField = string.Empty;
        //    string strParam = string.Empty;
        //    string strParam2 = string.Empty;
        //    bool IsCombine = false;
        //    string strCompare = string.Empty;
        //    int counter = 0;
        //    string strIN = string.Empty;
        //    string strINVal = string.Empty;
        //    string paramvalue = string.Empty;
        //    string strComma = string.Empty;
        //    string strAndOr = string.Empty;
        //    string strFieldName = string.Empty;

        //    if (listFilter != null)
        //    {
        //        foreach (DBFilter filter in listFilter)
        //        {
        //            if (filter.FieldName == string.Empty || filter.Values.Count == 0)
        //            { continue; }

        //            strFieldName = GetFieldName(filter,fs);

        //            switch (filter.CompareType)
        //            {
        //                case DBFilterCompareTypeEnum.IN:
        //                    strComma = string.Empty;
        //                    strINVal = string.Empty;

        //                    if (fs.FilterStyle == DBFilterStyleEnum.DirectString)
        //                    {
        //                        for (int i = 0; i < filter.Values.Count; i++)
        //                        {
        //                            strParam = CreateParamName(filter.FieldName + "_" + i.ToString(), counter, fs);
        //                            listParams.Add(strParam);
        //                            strINVal = strINVal + strComma + strParam;
        //                            listValues.Add(filter.Values[i]);
        //                            strComma = ", ";
        //                            counter++;
        //                        }
        //                        strINVal = " (" + strINVal + ")";
        //                        strField = strFieldName + " IN " + strINVal;
        //                        if (filter.NegateExpression)
        //                        {
        //                            strField = " NOT(" + strField + ")";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //TODO: do it dinamically -> not found yet
        //                        //for Now Concat by OR
        //                        strIN = filter.NegateExpression ? " != " : " = ";
        //                        for (int i = 0; i < filter.Values.Count; i++)
        //                        {
        //                            strParam = CreateParamName(filter.FieldName + "_" + i.ToString(), counter, fs);
        //                            strINVal = strINVal + strAndOr + "(" + strFieldName + strIN + strParam + ")";
        //                            //strField = strField +  strAndOr  + "(" + strFieldName + strIN + strParam + ")";
        //                            listParams.Add(strParam);
        //                            listValues.Add(filter.Values[i]);
        //                            counter++;
        //                            strAndOr = " OR ";
        //                        }
        //                        strField = strINVal;
        //                    }
        //                    break;
        //                case DBFilterCompareTypeEnum.StartsWith:
        //                case DBFilterCompareTypeEnum.EndsWith:
        //                case DBFilterCompareTypeEnum.Contains:
        //                    strAndOr = string.Empty;
        //                    for (int i = 0; i < filter.Values.Count; i++)
        //                    {
        //                        strParam = CreateParamName(filter.FieldName + "_" + i.ToString(), counter, fs);
        //                        if (fs.FilterStyle == DBFilterStyleEnum.DirectString) //sql or acces
        //                        {
        //                            strField = strAndOr + strFieldName + " LIKE " + strParam;
        //                            if (filter.NegateExpression)
        //                            {
        //                                strField = " NOT (" + strField + ")";
        //                            }
        //                            paramvalue = CreateLikeString(filter.Values[i].ToString(), filter.CompareType,fs);
        //                            listParams.Add(strParam);
        //                            listValues.Add(paramvalue);
        //                            //listValues.Add(filter.Values[i]);
        //                            counter++;
        //                            strAndOr = " OR ";
        //                        }
        //                        else
        //                        {
        //                            strField = CreateLikeStringLinq(strFieldName, strParam, filter.CompareType);
        //                            if (filter.NegateExpression)
        //                            {
        //                                strField = "!(" + strField + ") ";
        //                            }
        //                            paramvalue = CreateLikeString(filter.Values[i].ToString(), filter.CompareType,fs);
        //                            listParams.Add(strParam);
        //                            listValues.Add(paramvalue);
        //                            //listValues.Add(filter.Values[i]);
        //                            counter++;
        //                            strAndOr = " OR ";
        //                        }
        //                    }
        //                    break;
        //                case DBFilterCompareTypeEnum.Range:
        //                    if (filter.Values.Count > 1)
        //                    {
        //                        strParam = CreateParamName(filter.FieldName, counter,fs);
        //                        counter++;
        //                        strParam2 = CreateParamName(filter.FieldName + "_1", counter,fs);
        //                        if (fs.FilterStyle == DBFilterStyleEnum.DirectString) //sql or acces
        //                        {
        //                            strField = strFieldName + " BETWEEN " + strParam + " AND " + strParam2;
        //                            if (filter.NegateExpression)
        //                            {
        //                                strField = " NOT (" + strField + ")";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strField = strFieldName + " >= " + strParam + " AND " + strFieldName + " <= " + strParam2;
        //                            if (filter.NegateExpression)
        //                            {
        //                                strField = " !(" + strField + ")";
        //                            }
        //                        }
        //                        listParams.Add(strParam);
        //                        listParams.Add(strParam2);
        //                        listValues.Add(filter.Values[0]);
        //                        listValues.Add(filter.Values[1]);
        //                        counter++;
        //                        break;
        //                    }
        //                    break;
        //                case DBFilterCompareTypeEnum.EqualTo:
        //                case DBFilterCompareTypeEnum.NotEqualTo:
        //                    strAndOr = string.Empty;
        //                    for (int i = 0; i < filter.Values.Count; i++)
        //                    {
        //                        if ((fs.FilterStyle == DBFilterStyleEnum.DirectString) && filter.Values[i] == null)
        //                        {
        //                            strField = strField + strAndOr + strFieldName + (filter.CompareType == DBFilterCompareTypeEnum.EqualTo ? " IS" : " IS NOT") + " NULL ";
        //                        }
        //                        else
        //                        {
        //                            strParam = CreateParamName(filter.FieldName + "_" + i.ToString(), counter,fs);
        //                            strField = strField + strAndOr + strFieldName + GetCompareString(filter.CompareType,fs) + strParam;
        //                            listParams.Add(strParam);
        //                            listValues.Add(filter.Values[i]);
        //                            counter++;
        //                        }
        //                        strAndOr = " OR ";
        //                    }
        //                    break;
        //                default:        //all other
        //                    strAndOr = string.Empty;
        //                    for (int i = 0; i < filter.Values.Count; i++)
        //                    {
        //                        strParam = CreateParamName(filter.FieldName + "_" + i.ToString(), counter,fs);
        //                        strField = strField + strAndOr + strFieldName + GetCompareString(filter.CompareType,fs) + strParam;
        //                        listParams.Add(strParam);
        //                        listValues.Add(filter.Values[i]);
        //                        counter++;
        //                        strAndOr = " OR ";
        //                    }
        //                    break;
        //            }

        //            if (strField != string.Empty)
        //            {
        //                strField = "(" + strField + ")";
        //            }
        //            if (IsCombine)
        //            {
        //                strSql += GetCombineString(filter.CombineType) + strField;
        //            }
        //            else
        //            {
        //                strSql = strField;
        //            }
        //            IsCombine = true;

        //            ///reset string vars
        //            strFieldName = string.Empty;
        //            strField = string.Empty;
        //            strParam = string.Empty;
        //            strParam2 = string.Empty;
        //            strIN = string.Empty;
        //            strINVal = string.Empty;
        //            paramvalue = string.Empty;
        //            strComma = string.Empty;
        //            strAndOr = string.Empty;
        //        } //filter list loop
        //    } // lstfilter != null


        //    if (fs.IncludeOneFilter)
        //    {
        //        strSql = "(1=1)" + (strSql == string.Empty ? string.Empty : " AND ") + strSql;
        //    }
        //    if (fs.IncludeWhere)
        //    {
        //        strSql = " Where " + strSql;
        //    }

        //    DBFilterResult result = new DBFilterResult();
        //    result.FilterString = strSql;
        //    result.FilterParams = listParams;
        //    result.FilterValues = listValues;

        //    return result;
        //}


        public static DBFilterResult GetFilterResult(List<DBFilter> listFilter, DBFilterSettings fs)
        {
            if (fs == null)
            {
                throw new ArgumentNullException("Filter Settings Cannot be NULL!");
            }

            bool IsCombine = false;
            int setNumber = -1;
            int paramCounter = -1;

            DBFilterResult dbFilterResult = new DBFilterResult();

            if (listFilter != null)
            {
                foreach (DBFilter filter in listFilter)
                {
                    setNumber++;
                    //paramCounter++;
                    List<DBFilter> dbFilterList = new List<DBFilter>();

                    if (filter.DBFilterList != null && filter.DBFilterList.Count > 0)
                    {
                        //dbFilterResult = GetFilterResultBySet(filter.DBFilterList, setNumber, paramCounter, fs);
                        dbFilterList = filter.DBFilterList;
                    }
                    else
                    {
                        dbFilterList.Add(filter);
                    }

                    DBFilterResult dbFilterResultLocal = GetFilterResultBySet(dbFilterList, setNumber, ref paramCounter, fs);

                    dbFilterResult.DBParametersInfo.AddRange(dbFilterResultLocal.DBParametersInfo);

                    if (dbFilterResultLocal.FilterString != string.Empty)
                    {
                        dbFilterResultLocal.FilterString = "(" + dbFilterResultLocal.FilterString + ")";
                    }
                    if (IsCombine)
                    {
                        dbFilterResult.FilterString += GetCombineString(filter.CombineType) + dbFilterResultLocal.FilterString;
                    }
                    else
                    {
                        dbFilterResult.FilterString = dbFilterResultLocal.FilterString;
                    }
                    IsCombine = true;
                    
                } //filter list loop
            } // lstfilter != null

            if (fs.IncludeOneFilter)
            {
                dbFilterResult.FilterString = "(1=1)" + (dbFilterResult.FilterString == string.Empty ? string.Empty : " AND ") + dbFilterResult.FilterString;
            }
            if (fs.IncludeWhere)
            {
                dbFilterResult.FilterString = " Where " + dbFilterResult.FilterString;
            }
            return dbFilterResult;
        }



        public static DBFilterResult GetFilterResultBySet(List<DBFilter> listFilter, int setNumber, ref int paramCounter)
        {
            return GetFilterResultBySet(listFilter, setNumber, ref paramCounter, DefaultFilterSettings);
        }
        public static DBFilterResult GetFilterResultBySet(List<DBFilter> listFilter, int setNumber, ref int paramCounter, DBFilterSettings fs)
        {
            if (fs == null)
            {
                throw new ArgumentNullException("Filter Settings Cannot be NULL!");
            }

            DBFilterResult result = new DBFilterResult();
            DBParameterInfoCollection dbParamsInfo = new DBParameterInfoCollection();
            string strSql = string.Empty;
            string strField = string.Empty;
            string strParam = string.Empty;
            string strParam2 = string.Empty;
            bool IsCombine = false;
            string strCompare = string.Empty;
            int counter = paramCounter;
            string strIN = string.Empty;
            string strINVal = string.Empty;
            string paramvalue = string.Empty;
            string strComma = string.Empty;
            string strAndOr = string.Empty;
            string strFieldName = string.Empty;

            if (listFilter != null)
            {
                foreach (DBFilter filter in listFilter)
                {
                    if (filter.FieldName == string.Empty || filter.Values.Count == 0)
                    { continue; }

                    strFieldName = GetFieldName(filter, fs);

                    switch (filter.CompareType)
                    {
                        case DBFilterCompareTypeEnum.IN:
                            strComma = string.Empty;
                            strINVal = string.Empty;

                            if (fs.FilterStyle == DBFilterStyleEnum.DirectString)
                            {
                                for (int i = 0; i < filter.Values.Count; i++)
                                {
                                    counter++;
                                    strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_" + i.ToString(), counter, fs);
                                    strINVal = strINVal + strComma + strParam;
                                    dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[i]));
                                    strComma = ", "; 
                                }
                                strINVal = " (" + strINVal + ")";
                                strField = strFieldName + " IN " + strINVal;
                                if (filter.NegateExpression)
                                {
                                    strField = " NOT(" + strField + ")";
                                }
                            }
                            else
                            {
                                //TODO: do it dinamically -> not found yet
                                //for Now Concat by OR
                                //strIN = filter.NegateExpression ? " != " : " = ";

                                for (int i = 0; i < filter.Values.Count; i++)
                                {
                                    counter++;
                                    strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_" + i.ToString(), counter, fs);
                                    strINVal = strINVal + strAndOr + "(" + strFieldName + strIN + strParam + ")";
                                    //strField = strField +  strAndOr  + "(" + strFieldName + strIN + strParam + ")";
                                    dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[i]));
                                    strAndOr = " OR ";
                                }
                                strField = strINVal;
                            }
                            break;
                        case DBFilterCompareTypeEnum.StartsWith:
                        case DBFilterCompareTypeEnum.EndsWith:
                        case DBFilterCompareTypeEnum.Contains:
                            strAndOr = string.Empty;
                            for (int i = 0; i < filter.Values.Count; i++)
                            {
                                counter++;
                                strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_" + i.ToString(), counter, fs);
                                if (fs.FilterStyle == DBFilterStyleEnum.DirectString) //sql or acces
                                {
                                    strField = strAndOr + strFieldName + " LIKE " + strParam;
                                    if (filter.NegateExpression)
                                    {
                                        strField = " NOT (" + strField + ")";
                                    }
                                    paramvalue = CreateLikeString(filter.Values[i].ToString(), filter.CompareType, fs);
                                    dbParamsInfo.Add(new DBParameterInfo(strParam, paramvalue));
                                    strAndOr = " OR ";
                                }
                                else
                                {
                                    strField = CreateLikeStringLinq(strFieldName, strParam, filter.CompareType);
                                    if (filter.NegateExpression)
                                    {
                                        strField = "!(" + strField + ") ";
                                    }
                                    paramvalue = CreateLikeString(filter.Values[i].ToString(), filter.CompareType, fs);
                                    dbParamsInfo.Add(new DBParameterInfo(strParam, paramvalue));
                                    strAndOr = " OR ";
                                }
                            }
                            break;
                        case DBFilterCompareTypeEnum.Range:
                            if (filter.Values.Count > 1)
                            { 
                                counter++;
                                strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString(), counter, fs);
                                counter++;
                                strParam2 = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_1", counter, fs);
                                if (fs.FilterStyle == DBFilterStyleEnum.DirectString) //sql or acces
                                {
                                    strField = strFieldName + " BETWEEN " + strParam + " AND " + strParam2;
                                    if (filter.NegateExpression)
                                    {
                                        strField = " NOT (" + strField + ")";
                                    }
                                }
                                else
                                {
                                    strField = strFieldName + " >= " + strParam + " AND " + strFieldName + " <= " + strParam2;
                                    if (filter.NegateExpression)
                                    {
                                        strField = " !(" + strField + ")";
                                    }
                                }
                                dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[0]));
                                dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[1]));

                                break;
                            }
                            break;
                        case DBFilterCompareTypeEnum.EqualTo:
                        case DBFilterCompareTypeEnum.NotEqualTo:
                            strAndOr = string.Empty;
                            for (int i = 0; i < filter.Values.Count; i++)
                            {
                                if ((fs.FilterStyle == DBFilterStyleEnum.DirectString) && filter.Values[i] == null)
                                {
                                    strField = strField + strAndOr + strFieldName + (filter.CompareType == DBFilterCompareTypeEnum.EqualTo ? " IS" : " IS NOT") + " NULL ";
                                }
                                else
                                {
                                    counter++;
                                    strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_" + i.ToString(), counter, fs);
                                    strField = strField + strAndOr + strFieldName + GetCompareString(filter.CompareType, fs) + strParam;
                                    dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[i]));
                                }
                                strAndOr = " OR ";
                            }
                            break;
                        default:        //all other
                            strAndOr = string.Empty;
                            for (int i = 0; i < filter.Values.Count; i++)
                            {
                                counter++;
                                strParam = CreateParamName(filter.FieldName + "_" + setNumber.ToString() + "_" + i.ToString(), counter, fs);
                                strField = strField + strAndOr + strFieldName + GetCompareString(filter.CompareType, fs) + strParam;
                                dbParamsInfo.Add(new DBParameterInfo(strParam, filter.Values[i]));
                                strAndOr = " OR ";
                            }
                            break;
                    }

                    if (strField != string.Empty)
                    {
                        strField = "(" + strField + ")";
                    }
                    if (IsCombine)
                    {
                        strSql += GetCombineString(filter.CombineType) + strField;
                    }
                    else
                    {
                        strSql = strField;
                    }
                    IsCombine = true;

                    ///reset string vars
                    strFieldName = string.Empty;
                    strField = string.Empty;
                    strParam = string.Empty;
                    strParam2 = string.Empty;
                    strIN = string.Empty;
                    strINVal = string.Empty;
                    paramvalue = string.Empty;
                    strComma = string.Empty;
                    strAndOr = string.Empty;
                } //filter list loop
            } // lstfilter != null


            //if (fs.IncludeOneFilter)
            //{
            //    strSql = "(1=1)" + (strSql == string.Empty ? string.Empty : " AND ") + strSql;
            //}

            //if (fs.IncludeWhere)
            //{
            //    strSql = " Where " + strSql;
            //}

            
            result.FilterString = strSql;
            result.DBParametersInfo = dbParamsInfo;

            paramCounter = counter;

            return result;
        }

        private static string GetFieldName(DBFilter filter, DBFilterSettings fs)
        {
            string fName = filter.FieldName;
            if (fs.FilterStyle == DBFilterStyleEnum.Linq)
            {
                fName = "it." + filter.FieldName;

            }
            else
            {
                if (filter.IncludeTableName && filter.TableName != string.Empty)
                {
                    fName = filter.TableName.Trim() + "." + filter.FieldName.Trim();
                }
            }
            return fName;
        }
        private static string CreateParamName(string fldName, int counter, DBFilterSettings fs)
        {
            string pName = string.Empty;
            switch (fs.ParamNameType)
            {
                case DBFilterParamNameTypeEnum.StringNumber:
                    pName = "{" + counter.ToString() + "}";
                    break;
                case DBFilterParamNameTypeEnum.ParamNumber:
                    pName = "@" + counter.ToString();
                    break;
                case DBFilterParamNameTypeEnum.ParamName:
                    pName = "@" + fldName.ToLower();
                    break;
                default:
                    pName = "@" + counter.ToString();
                    break;

            }
            return pName;
        }
        private static string CreateLikeString(string pValue, DBFilterCompareTypeEnum pCompType, DBFilterSettings fs)
        {
            string strLike = pValue.Trim();
            string likeChar = "%";
            switch (fs.DBContextSettings.DatabaseType)
            {
                case DatabaseTypeEnum.SQLServer:
                    likeChar = "%";
                    break;
                case DatabaseTypeEnum.Oracle:
                    likeChar = "%";
                    break;
                case DatabaseTypeEnum.MSAccess:
                    likeChar = "*";
                    break;
                default:
                    likeChar = "%";
                    break;
                    
            }



            switch (pCompType)
            {
                case DBFilterCompareTypeEnum.StartsWith:
                    if (!strLike.EndsWith(likeChar))
                    {
                        strLike = strLike + likeChar;
                    }
                    break;
                case DBFilterCompareTypeEnum.EndsWith:
                    if (!strLike.StartsWith(likeChar))
                    {
                        strLike = likeChar + strLike;
                    }
                    break;
                case DBFilterCompareTypeEnum.Contains:
                    if (!strLike.StartsWith(likeChar))
                    {
                        strLike = likeChar + strLike ;
                    }
                    if (!strLike.EndsWith(likeChar))
                    {
                        strLike = strLike + likeChar;
                    }
                    break;
                default:
                    if (!strLike.EndsWith(likeChar))
                    {
                        strLike = strLike + likeChar;
                    }
                    break;
            }
            return strLike;
        }
        private static string CreateLikeStringLinq(string pFieldName, string pParamName, DBFilterCompareTypeEnum pCompType)
        {
            //this function for linq Dynamic like by property
            string strLike = string.Empty;
            //query = query.Where(dimensionList[ddl.ID].Name + ".StartsWith(\"Other\")")

            switch (pCompType)
            {
                case DBFilterCompareTypeEnum.StartsWith:
                    strLike = pFieldName + ".StartsWith(" + pParamName + ")";
                    break;
                case DBFilterCompareTypeEnum.EndsWith:
                    strLike = pFieldName + ".EndsWith(" + pParamName + ")";
                    break;
                case DBFilterCompareTypeEnum.Contains:
                    strLike = pFieldName + ".Contains(" + pParamName + ")";
                    break;
                default:
                    strLike = pFieldName + ".StartsWith(" + pParamName + ")";
                    break;
            }
            return strLike;
        }

        private static string GetCombineString(DBFilterCombineTypeEnum combType)
        {
            string result = string.Empty;
            switch (combType)
            {
                case DBFilterCombineTypeEnum.OR:
                    result = " OR ";
                    break;
                case DBFilterCombineTypeEnum.AND:
                    result = " AND ";
                    break;
                default:
                    result = " AND ";
                    break;
            }
            return result;
        }

        private static string GetCompareString(DBFilterCompareTypeEnum compareType, DBFilterSettings fs)
        {
            string result = string.Empty;
            switch (compareType)
            {
                case DBFilterCompareTypeEnum.EqualTo:
                    result = " = ";
                    break;
                case DBFilterCompareTypeEnum.NotEqualTo:
                    result = fs.FilterStyle == DBFilterStyleEnum.DirectString ? " <> " : " != ";
                    break;
                case DBFilterCompareTypeEnum.GreaterThan:
                    result = " > ";
                    break;
                case DBFilterCompareTypeEnum.LessThan:
                    result = " < ";
                    break;
                case DBFilterCompareTypeEnum.GreaterThanEqualTo:
                    result = " >= ";
                    break;
                case DBFilterCompareTypeEnum.LessThanEqualTo:
                    result = " <= ";
                    break;
                case DBFilterCompareTypeEnum.IN:
                    result = " IN ";
                    break;
                case DBFilterCompareTypeEnum.Range:
                    result = "";
                    break;
                case DBFilterCompareTypeEnum.Contains:
                    result = "Contains";
                    break;
                case DBFilterCompareTypeEnum.StartsWith:
                    result = "StatsWith";
                    break;
                case DBFilterCompareTypeEnum.EndsWith:
                    result = "EndsWith";
                    break;
                default:
                    result = "";
                    break;

            }
            return result;
        }
        public static string GetFormattedString(object val, DBFilter filter)
        {
            string pVal = string.Empty;
            if (filter.FormatString == string.Empty)
            {
                return val.ToString();
            }
            switch (filter.DataType)
            {
                case DBFilterDataTypeEnum.String:
                    pVal = val.ToString();
                    break;
                case DBFilterDataTypeEnum.Integer:
                    break;
                case DBFilterDataTypeEnum.Decimal:
                    pVal = Convert.ToDecimal(val).ToString(filter.FormatString);
                    break;
                case DBFilterDataTypeEnum.DateTime:
                case DBFilterDataTypeEnum.Date:
                case DBFilterDataTypeEnum.Time:
                    pVal = Convert.ToDateTime(val).ToString(filter.FormatString);
                    break;
            }
            return pVal;
        }

        public static string CreateFilterText(DBFilter filter)
        {
            return CreateFilterText(filter, DefaultFilterSettings);
        }

        /// <summary>
        /// CreateFilterText(): Create filter text for UI 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string CreateFilterText(DBFilter filter, DBFilterSettings fs)
        {
            string strText = string.Empty;
            string val1 = string.Empty;
            string val2 = string.Empty;
            string comString = GetCompareString(filter.CompareType,fs).Trim();

            if (filter.Values.Count > 0)
            {
                val1 = GetFormattedString(filter.Values[0], filter);
                if (filter.CompareType == DBFilterCompareTypeEnum.Range)
                {
                    if (filter.Values.Count == 1)
                    {
                        strText = "BETWEEN " + val1 + " AND " + val1;
                    }
                    else
                    {
                        val2 = GetFormattedString(filter.Values[1], filter);
                        strText = "BETWEEN " + val1 + " AND " + val2;
                    }
                }
                else
                {
                    if (filter.CompareType == DBFilterCompareTypeEnum.IN)
                    {
                        if (filter.IsPreValue && filter.FilterTextPreValue != string.Empty)
                        {
                            strText = "IN (" + filter.FilterTextPreValue + ")";
                        }
                        else
                        {
                            string strComma = string.Empty;
                            string strVal = string.Empty;
                            foreach (object obj in filter.Values)
                            {
                                strVal += strComma + GetFormattedString(obj, filter);
                                strComma = ",";
                            }
                            strText = "IN (" + strVal + ")";
                        }
                    }
                    else
                    {
                        strText = comString + val1;
                    }
                }
            }
            return strText;
        }


        public static string CreateFilterTextFull(List<DBFilter> lstfilter)
        {
            //DBFilterSettings fs = GetDBFilterSettings();
            return CreateFilterTextFull(lstfilter, DefaultFilterSettings);
        }
        /// <summary>
        /// CreateFilterTextFull
        /// </summary>
        /// <param name="lstfilter"></param>
        /// <returns></returns>
        public static string CreateFilterTextFull(List<DBFilter> lstfilter, DBFilterSettings fs)
        {
            string fText = string.Empty;
            if (lstfilter == null | lstfilter.Count == 0)
            {
                return fText;
            }
            string txt1 = string.Empty;
            string strAndOr = string.Empty;
            foreach (DBFilter filter in lstfilter)
            {
                if (lstfilter.IndexOf(filter) > 0)
                {
                    strAndOr = filter.CombineType == DBFilterCombineTypeEnum.AND ? " AND " : " OR ";
                }

                txt1 = CreateFilterText(filter,fs);
                fText += strAndOr + "(" + filter.Name + " " + txt1 + ")";
            }
            return fText;
        }

        public static DataTable GetDBFilterFieldDataByID(int pDBFilterID)
        {
            return GetDBFilterFieldDataByID(pDBFilterID, null);
        }

        public static DataTable GetDBFilterFieldDataByID(int pDBFilterID, DBBase.DBContext dc)
        {
            bool isDCInit = false;
            isDCInit = DBBase.DBContextManager.CheckAndInitDBContext(ref dc);
            DbCommand cmd = dc.CreateDbCommand();
            cmd.Connection = dc.Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * From tblDBFilterField Where DBFilterID = @dbfilterid ORDER BY SLNo";
            dc.AddParamWithValue(cmd, "@dbfilterid", pDBFilterID);

            DataTable dtData = dc.GetDataTable(cmd);
            DBBase.DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            //DbCommand cmd = null;
            return dtData;
        }

        public static List<DBFilterField> GetDBFilterList(int pDBFilterID)
        {
            return GetDBFilterList(pDBFilterID,null);
        }
        public static List<DBFilterField> GetDBFilterList(int pDBFilterID, DBBase.DBContext dc)
        {
            return GetDBFilterList(GetDBFilterFieldDataByID(pDBFilterID,dc),dc);
        }
        public static List<DBFilterField> GetDBFilterList(DataTable dtData)
        {
            return GetDBFilterList(dtData, null);
        }
        public static List<DBFilterField> GetDBFilterList(DataTable dtData, DBBase.DBContext dc)
        {
            if (dtData == null)
            {
                throw new ArgumentNullException("Filter data not supplied");
            }
            List<DBFilterField> cObjList = new List<DBFilterField>();
            string data = string.Empty;
            int preValueMode = 0;
            List<string> lstData = null;
            bool isDCInit = false;
            
            foreach (DataRow dRow in dtData.Rows)
            {
                DBFilterField cObj = new DBFilterField();
                cObj.SLNo = Utility.Conversion.DBNullIntToZero(dRow["SLNo"]);
                cObj.Name = Utility.Conversion.DBNullStringToEmpty(dRow["Name"]);
                cObj.Description = Utility.Conversion.DBNullStringToEmpty(dRow["Description"]);
                cObj.TableName = Utility.Conversion.DBNullStringToEmpty(dRow["TableName"]);
                cObj.FieldName = Utility.Conversion.DBNullStringToEmpty(dRow["FieldName"]);
                cObj.FieldDataType = (DBFilterDataTypeEnum)Utility.Conversion.DBNullIntToZero(dRow["DataType"]);

                cObj.IsPreValue = Utility.Conversion.DBNullBoolToFalse(dRow["IsPreValue"]);
                if (cObj.IsPreValue)
                {
                    cObj.PresetValueDisplayColumnCount = Utility.Conversion.DBNullIntToZero(dRow["PreValueDisplayColumnCount"]);
                    //data = Utlity.DBNullStringToEmpty(dRow["PreValueDispalyColumnNames"]).Split(',');

                    cObj.PresetValueDispalyColumnNames = Utility.Conversion.DBNullStringToEmpty(dRow["PreValueDispalyColumnNames"]).Split(',').ToList();

                    lstData = Utility.Conversion.DBNullStringToEmpty(dRow["PreValueDisplayColumnWidths"]).Split(',').ToList();

                    foreach (string str in lstData)
                    {
                        cObj.PresetValueDisplayColumnWidths.Add(Convert.ToInt32(str == string.Empty ? "0" : str));
                    }

                    cObj.PresetValueDisplayWindowHeight = Utility.Conversion.DBNullIntToZero(dRow["PreValueDisplayWindowHeight"]);
                    cObj.PresetValueDisplayWindowWidth = Utility.Conversion.DBNullIntToZero(dRow["PreValueDisplayWindowWidth"]);

                    cObj.FormatString = Utility.Conversion.DBNullStringToEmpty(dRow["FormatString"]);

                    preValueMode = Utility.Conversion.DBNullIntToZero(dRow["PreValueLoadMode"]);
                    switch (preValueMode)
                    {
                        case 0:
                            break;
                        case 1:
                            lstData = Utility.Conversion.DBNullStringToEmpty(dRow["PreValues"]).Split(';').ToList();
                            foreach (string str in lstData)
                            {
                                //lstVals = str.Split(',').ToList();
                                cObj.DBFilterPresetValues.Add(new DBFilterPresetValue(str.Split(',').ToArray()));
                            }
                            break;
                        case 2:
                            //datatable
                            isDCInit = DBBase.DBContextManager.CheckAndInitDBContext(ref dc);
                            DbCommand cmd = dc.CreateDbCommand();
                            cmd.Connection = dc.Connection;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = Utility.Conversion.DBNullStringToEmpty(dRow["PreValueCommandString"]);
                            DataTable dtVal = dc.GetDataTable(cmd);
                            lstData = Utility.Conversion.DBNullStringToEmpty(dRow["PreValueFieldNames"]).Split(',').ToList();
                            List<string> lstVal = new List<string>();
                            foreach (DataRow dRowVal in dtVal.Rows)
                            {
                                lstVal.Clear();
                                foreach (string strCol in lstData)
                                {
                                    lstVal.Add(dRowVal[strCol].ToString());
                                }
                                cObj.DBFilterPresetValues.Add(new DBFilterPresetValue(lstVal.ToArray()));
                            }
                            DBBase.DBContextManager.ReleaseDBContext(ref dc, isDCInit);
                            break;
                        case 3:
                            //object list
                            //List<object> data2 = BLLibrary.VendorBL.GetVendorList();
                            break;
                    }
                } // isprevalue

                //cObj.PreValueDisplayColumnWidths = data.Split(',').ToList()

                cObjList.Add(cObj);
            }

            return cObjList;
        }

        public static void ApplyFilterListToCommand(List<DBFilter> lstfilter, DbCommand dbCommand)
        {
            ApplyFilterListToCommand(lstfilter, dbCommand, DefaultFilterSettings);
        }

        public static void ApplyFilterListToCommand(List<DBFilter> lstfilter, DbCommand dbCommand, DBFilterSettings filterSettings)
        {
            if (lstfilter != null && lstfilter.Count > 0)
            {
                DBFilterResult filterResult = DBFilterManager.GetFilterResult(lstfilter, filterSettings);
                ///string strWhere = string.Empty;

                //dbCommand.CommandText.Contains("Where")
                string strAND = filterResult.FilterString == string.Empty ? string.Empty : filterSettings.SQLStatementCombineBy; 
                dbCommand.CommandText = dbCommand.CommandText + strAND + filterResult.FilterString;

                DBQuery.AddDBParametersToDbCommand(dbCommand, filterResult.DBParametersInfo, filterSettings.DBContextSettings);
                
                
                
                //int i = 0;
                //foreach(string strParam in filterResult.FilterParams)
                //{
                //    //ZCore.DBBase.DBMap
                    
                //    DBQuery.AddDbCommandParamWithValue(dbCommand,strParam,filterResult.FilterValues[i],filterSettings.DBFilterDBType);
                //    i++;
                //}








               // query = query.Where(filterResult.FilterString, filterResult.FilterValues.ToArray());
            }
        }


        public static DBFilterCompareTypeEnum GetCompareTypeFormInt(int pCompareType)
        {
            DBFilterCompareTypeEnum compType = DBFilterCompareTypeEnum.EqualTo;
            try
            {
                compType = (DBFilterCompareTypeEnum)pCompareType;
            }
            finally { }
            
            return compType;

        }
    }
}
