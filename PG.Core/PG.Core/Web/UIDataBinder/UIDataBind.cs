using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Reflection;



namespace PG.Core.Web.UIDataBinder
{
    public class UIDataBind
    {
        public static void SetDataObjectToForm(Object data, List<UIDataBindItem> dataBinderList)
        {
            if (data == null | dataBinderList == null)
            {
                throw new ArgumentNullException("SetFromData:data or list cannont be null");
            }

            //PropertyInfo[] propsData = data.GetType().GetProperties();

            foreach (UIDataBindItem dbind in dataBinderList)
            {
                if (dbind.DataBindMode == DataBindMode.ObjectToFrom || dbind.DataBindMode == DataBindMode.Both)
                {

                    PropertyInfo srcObj = data.GetType().GetProperty(dbind.BindingPropertyName);

                    //object control = page.FindControl(dbind.BindingControlName);


                    PropertyInfo targetObj = dbind.BindingControl.GetType().GetProperty(dbind.BindingControlPropertyName);

                    object objVal = srcObj.GetValue(data, null);

                    string strVal = string.Empty;
                    if (dbind.FormatString == string.Empty)
                    {
                        strVal = objVal == null ? string.Empty : objVal.ToString();
                    }
                    else
                    {
                        strVal = string.Format("{0:" + dbind.FormatString + "}", objVal);
                    }

                    if (targetObj != null)
                    {
                        if (targetObj.CanWrite)
                        {
                            targetObj.SetValue(dbind.BindingControl, strVal, null);
                        }
                    }
                }
            } //loop
        }

        public static void SetDataFromToObject(Object data, List<UIDataBindItem> dataBinderList)
        {
            if (data == null | dataBinderList == null)
            {
                throw new ArgumentNullException("GetFromData:data or list cannont be null");
            }

            foreach (UIDataBindItem dbind in dataBinderList)
            {
                if (dbind.DataBindMode == DataBindMode.FormToObject | dbind.DataBindMode == DataBindMode.Both)
                {
                    PropertyInfo targetObj = data.GetType().GetProperty(dbind.BindingPropertyName);
                    PropertyInfo srcObj = dbind.BindingControl.GetType().GetProperty(dbind.BindingControlPropertyName);
                    object objVal = srcObj.GetValue(dbind.BindingControl, null);

                    if (DBBase.DBMap.IsNullableType(targetObj.PropertyType))
                    {
                        objVal = DBBase.DBMap.ConvertDataToType(Nullable.GetUnderlyingType(targetObj.PropertyType), objVal);
                    }
                    else
                    {
                        objVal = DBBase.DBMap.ConvertDataToType(targetObj.PropertyType, objVal);
                    }
                    if (targetObj != null)
                    {
                        if (targetObj.CanWrite)
                        {
                            targetObj.SetValue(data, objVal, null);
                        }
                    }
                }

            }
        }

        private Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.ID == controlID) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                Control controlToReturn =
                    FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }

        public static List<UIDataBindItem> ValidateInputFormat(List<UIDataBindItem> dataBinderList)
        {
            List<UIDataBindItem> errorList = new List<UIDataBindItem>();


            return errorList;
        }
    }
}
