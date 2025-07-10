using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Security;

namespace PG.Core.Reporting
{
    [CLSCompliant(true)]
    public class ReportHelper
    {
        [CLSCompliant(true)]
        public static string Test(string strText)
        {
            return "Report Test: " + strText;
        }

        [CLSCompliant(true)]
        public string Test2(string strText)
        {
            return "Report Test 2: " + strText;
        }


        [CLSCompliant(true)]
        public static float GetFitTextWidth(string strText, string fontName, float fontSize, float fitWidth, float minFontSize)
        {
            return Utility.Helper.GetFitTextWidth(strText, fontName, fontSize, fitWidth, minFontSize, 72);
        }

        [CLSCompliant(true)]
        public static float GetFitTextWidth(string strText, string fontName, float fontSize, float fitWidth, float minFontSize, int pDpi)
        {
            return Utility.Helper.GetFitTextWidth(strText, fontName, fontSize, fitWidth, minFontSize, pDpi);

            ////0.69743
            //int widthPixel = Convert.ToInt32(Math.Ceiling(fitWidth * pDpi));
            //int heightPixel = widthPixel;

            //Bitmap gImg = new Bitmap(widthPixel, heightPixel);
            ////gImg.SetResolution(300, 300);
            //gImg.SetResolution(pDpi, pDpi);
            //Graphics graphics = Graphics.FromImage(gImg);

            ////float minFontSize = 4F;

            //float fontSizeTest = fontSize;
            //bool isFit = false;
            //while (!isFit)
            //{
            //    if (fontSizeTest <= minFontSize)
            //    {
            //        fontSizeTest = minFontSize;
            //        isFit = true;
            //        break;
            //    }

            //    //Font TestFont = GetFontFromPath(fontFilePath, OrgFontFamilyName, (float)AdjustedSize);
            //    Font TestFont = new Font(fontName, fontSizeTest, GraphicsUnit.Point); // GetFontFromPath(fontFilePath, OrgFontFamilyName, (float)AdjustedSize);
            //    SizeF size = graphics.MeasureString(strText, TestFont);
            //    float widthInch = size.Width / 72;


            //    if (widthInch < fitWidth)
            //    {
            //        isFit = true;
            //        break;
            //    }
            //    fontSizeTest = fontSizeTest - 1;
            //}
            //return fontSizeTest;
        }



        [CLSCompliant(true)]
        public static string NumberInWord(string strText)
        {
            return Utility.NumberInWord.GetInWord(strText);
        }


        [CLSCompliant(true)]
        public static string NumberInWord(string strText, bool isAnd)
        {
            return Utility.NumberInWord.GetInWord(strText, isAnd);
        }



        [CLSCompliant(true)]
        public static string NumberFormatInBD(string strText)
        {
            return Utility.Helper.NumberFormatInBD(strText);
        }


        [CLSCompliant(true)]
        public static string NumberFormatInBD(string strText, string strFormat)
        {
            return Utility.Helper.NumberFormatInBD(strText, strFormat);
        }


    }
}
