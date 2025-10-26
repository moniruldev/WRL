using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using PG.Core;
using PG.Core.Web;

namespace PG.Web
{
    public class Helper
    {



        public static ListItem FindByValue(ListBox lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }

        public static ListItem FindByValue(DropDownList lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }
        public static int FindIndexByValue(DropDownList lstBox, string value)
        {
            int i = 0;

            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value == value)
                    return i;
                i++;
            }
            return -1;
        }
        public static int FindIndexByValue(ListBox lstBox, string value)
        {
            int i = 0;

            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value == value)
                    return i;
                i++;
            }
            return -1;
        }
        public static ListItem FindByText(ListBox lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Text.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }
        public static ListItem FindByText(DropDownList lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Text.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }

        public static void SetStatusMessage(Label lblStatus, string pMsg, MessageTypeEnum pMessageType)
        {
            System.Drawing.Color color = System.Drawing.Color.Black;
            switch (pMessageType)
            {
                case MessageTypeEnum.None:
                    color = System.Drawing.Color.Black;
                    break;
                case MessageTypeEnum.Information:
                    color = System.Drawing.Color.Black;
                    break;
                case MessageTypeEnum.Successful:
                    //color = System.Drawing.Color.LightGreen;
                    //color = "#20b2aa";
                    color = System.Drawing.Color.MediumAquamarine;
                    break;
                case MessageTypeEnum.Error:
                    //color = System.Drawing.Color.Red;
                    color = System.Drawing.Color.Pink;
                    break;
                case MessageTypeEnum.Permission:
                    color = System.Drawing.Color.Red;
                    break;
                case MessageTypeEnum.InvalidData:
                    //color = System.Drawing.Color.Red;
                    color = System.Drawing.Color.Pink;
                    break;
                default:
                    color = System.Drawing.Color.White;
                    break;
            }
            lblStatus.Text = pMsg;
            ///lblStatus.ForeColor = color;
            lblStatus.BackColor = color;

            lblStatus.Visible = pMsg == string.Empty ? false : true;
        }

        public static float GetFitTextWidth(string strText, string fontName, float fontSize, decimal fitWidth)
        {
            //0.69743
            int widthPixel = Convert.ToInt32(Math.Ceiling(fitWidth * 300));
            int heightPixel = widthPixel;

            Bitmap gImg = new Bitmap(widthPixel, heightPixel);
            gImg.SetResolution(300, 300);
            Graphics graphics = Graphics.FromImage(gImg);

            float minFontSize = 4F;

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
                decimal widthInch = Convert.ToDecimal(size.Width / 72);

                if (widthInch < fitWidth)
                {
                    isFit = true;
                    break;
                }
                fontSizeTest = fontSizeTest - 1;
            }
            return fontSizeTest;
        }


        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }


        public static NameValueCollection ConvertJSonTextToList(string jsonString)
        {
            NameValueCollection dList = new NameValueCollection();
            string[] content = jsonString.Split('&');
            for (int i = 0; i < content.Length; i++)
            {
                string[] fields = content[i].Split('=');
                string sKey = fields[0].Trim();
                if (sKey != string.Empty)
                {
                    if (fields.Length > 1)
                    {
                        dList.Add(sKey, fields[1]);
                    }
                    else
                    {
                        dList.Add(sKey, string.Empty);
                    }
                }
            }
            return dList;
        }

        public static string NumberToWords(decimal number)
        {
            if (number == 0)
                return "Zero Taka Only";

            string words = "";

            long intPortion = (long)Math.Floor(number);
            int fraction = (int)((number - intPortion) * 100);

            words = NumberToWordsHelper(intPortion) + " Taka";

            if (fraction > 0)
                words += " and " + NumberToWordsHelper(fraction) + " Paisa";

            words += " Only";

            return words;
        }

        private static string NumberToWordsHelper(long number)
        {
            if (number == 0)
                return "";
            if (number < 0)
                return "Minus " + NumberToWordsHelper(Math.Abs(number));

            string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six",
                          "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
                          "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen",
                          "Eighteen", "Nineteen" };

            string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
                         "Sixty", "Seventy", "Eighty", "Ninety" };

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += NumberToWordsHelper(number / 10000000) + " Crore ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWordsHelper(number / 100000) + " Lakh ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += NumberToWordsHelper(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += NumberToWordsHelper(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

    }
}
