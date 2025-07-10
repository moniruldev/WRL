using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.Utility
{
    public class NumberInWord
    {
        public static string GetInWord(string strAmount)
        {
            return GetInWord(strAmount, false);
        }
        public static string GetInWord(string strAmount, bool isAND)
        {
            int LoopSig = 1;
            int LoopCtr = 1;
            String OriString;
            String WorkString;
            String WorkString1 = "";

            int LofString;
            int ctr;
            int NOV = 0;
            int SP = 0;
            string NoToWrd;
            string FinalWord = "";
            string[] NumWord = new string[100];
            string[] Pos = new string[5];
            long var2 = 0;
            string var1 = "";
            string ProcessNoInWord = "";
            string strAND = isAND ? " And " : "";
            bool isMinus = false;


            NumWord[0] = "";
            NumWord[1] = "One";
            NumWord[2] = "Two";
            NumWord[3] = "Three";
            NumWord[4] = "Four";
            NumWord[5] = "Five";
            NumWord[6] = "Six";
            NumWord[7] = "Seven";
            NumWord[8] = "Eight";
            NumWord[9] = "Nine";
            NumWord[10] = "Ten";
            NumWord[11] = "Eleven";
            NumWord[12] = "Twelve";
            NumWord[13] = "Thirteen";
            NumWord[14] = "Fourteen";
            NumWord[15] = "Fifteen";
            NumWord[16] = "Sixteen";
            NumWord[17] = "Seventeen";
            NumWord[18] = "Eighteen";
            NumWord[19] = "Nineteen";
            NumWord[20] = "Twenty";
            NumWord[21] = "Twenty One";
            NumWord[22] = "Twenty Two";
            NumWord[23] = "Twenty Three";
            NumWord[24] = "Twenty Four";
            NumWord[25] = "Twenty Five";
            NumWord[26] = "Twenty Six";
            NumWord[27] = "Twenty Seven";
            NumWord[28] = "Twenty Eight";
            NumWord[29] = "Twenty Nine";
            NumWord[30] = "Thirty";
            NumWord[31] = "Thirty One";
            NumWord[32] = "Thirty Two";
            NumWord[33] = "Thirty Three";
            NumWord[34] = "Thirty Four";
            NumWord[35] = "Thirty Five";
            NumWord[36] = "Thirty Six";
            NumWord[37] = "Thirty Seven";
            NumWord[38] = "Thirty Eight";
            NumWord[39] = "Thirty Nine";
            NumWord[40] = "Forty";
            NumWord[41] = "Forty One";
            NumWord[42] = "Forty Two";
            NumWord[43] = "Forty Three";
            NumWord[44] = "Forty Four";
            NumWord[45] = "Forty Five";
            NumWord[46] = "Forty Six";
            NumWord[47] = "Forty Seven";
            NumWord[48] = "Forty Eight";
            NumWord[49] = "Forty Nine";
            NumWord[50] = "Fifty";
            NumWord[51] = "Fifty One";
            NumWord[52] = "Fifty Two";
            NumWord[53] = "Fifty Three";
            NumWord[54] = "Fifty Four";
            NumWord[55] = "Fifty Five";
            NumWord[56] = "Fifty Six";
            NumWord[57] = "Fifty Seven";
            NumWord[58] = "Fifty Eight";
            NumWord[59] = "Fifty Nine";
            NumWord[60] = "Sixty";
            NumWord[61] = "Sixty One";
            NumWord[62] = "Sixty Two";
            NumWord[63] = "Sixty Three";
            NumWord[64] = "Sixty Four";
            NumWord[65] = "Sixty Five";
            NumWord[66] = "Sixty Six";
            NumWord[67] = "Sixty Seven";
            NumWord[68] = "Sixty Eight";
            NumWord[69] = "Sixty Nine";
            NumWord[70] = "Seventy";
            NumWord[71] = "Seventy One";
            NumWord[72] = "Seventy Two";
            NumWord[73] = "Seventy Three";
            NumWord[74] = "Seventy Four";
            NumWord[75] = "Seventy Five";
            NumWord[76] = "Seventy Six";
            NumWord[77] = "Seventy Seven";
            NumWord[78] = "Seventy Eight";
            NumWord[79] = "Seventy Nine";
            NumWord[80] = "Eighty";
            NumWord[81] = "Eighty One";
            NumWord[82] = "Eighty Two";
            NumWord[83] = "Eighty three";
            NumWord[84] = "Eighty Four";
            NumWord[85] = "Eighty Five";
            NumWord[86] = "Eighty Six";
            NumWord[87] = "Eighty Seven";
            NumWord[88] = "Eighty Eight";
            NumWord[89] = "Eighty Nine";
            NumWord[90] = "Ninety";
            NumWord[91] = "Ninety One";
            NumWord[92] = "Ninety Two";
            NumWord[93] = "Ninety Three";
            NumWord[94] = "Ninety Four";
            NumWord[95] = "Ninety Five";
            NumWord[96] = "Ninety Six";
            NumWord[97] = "Ninety Seven";
            NumWord[98] = "Ninety Eight";
            NumWord[99] = "Ninety Nine";

            Pos[1] = " Hundred ";
            Pos[2] = " Thousand ";
            Pos[3] = " Lac ";
            Pos[4] = " Crore ";


            if(strAmount.Substring(0,1) == "-")
            {
                isMinus = true;
                strAmount = strAmount.Substring(1);
            }



            //OriString = strAmount;
            OriString = strAmount.Replace(",","");
            WorkString = OriString;
            LofString = WorkString.Length;

            int ctr1 = 0;

            if (LofString > 7)
            {
                ctr = 3;
                WorkString1 = OriString.Substring(0, LofString - 7);
                if (WorkString1.Length > 5)
                {
                    ctr1 = 3;

                }
                else if (WorkString1.Length > 3)
                {
                    ctr1 = 2;

                }
                else if (WorkString1.Length > 2)
                {
                    ctr1 = 1;
                }

                else
                {
                    ctr1 = 0;
                }

                WorkString = WorkString.Substring(OriString.Length - 7, 7);
                LoopSig = 2;
            }
            else if (LofString > 5)
            {
                ctr = 3;
            }
            else if (LofString > 3)
            {
                ctr = 2;
            }
            else if (LofString > 2)
            {
                ctr = 1;
            }
            else
            {
                ctr = 0;
            }

            while (LoopCtr <= LoopSig)
            {
                if (LoopCtr == 2)
                {
                    WorkString = WorkString1;

                    ctr = ctr1;

                    LofString = WorkString.Length;

                    if (FinalWord.Length >= 1)
                    {
                        FinalWord = " Crore " + strAND + FinalWord;
                    }
                    else
                    {
                        FinalWord = " Crore " + FinalWord;
                    }
                }

                SP = 0;

                string[] wdArr = new string[5];
                wdArr[0] = "";
                wdArr[1] = "";
                wdArr[2] = "";
                wdArr[3] = "";
                wdArr[4] = "";

                while (ctr >= 0)
                {
                    if (ctr == 1)
                    {
                        NOV = 1;
                    }
                    if (ctr == 0)
                    {
                        if (LofString >= 2)
                        {
                            NOV = 2;

                        }
                        else
                        {
                            NOV = 1;
                        }
                    }
                    if (ctr == 2)
                    {
                        if (LofString >= 5)
                        {
                            NOV = 2;
                        }
                        else
                        {
                            NOV = 1;
                        }
                    }

                    if (ctr == 3)
                    {
                        if (LofString >= 7)
                        {
                            NOV = 2;
                        }
                        else
                        {
                            NOV = 1;
                        }
                    }

                    try
                    {

                        var1 = WorkString.Substring(SP, NOV);
                        var2 = Convert.ToInt64(var1);
                        ProcessNoInWord = "";
                        if (NumWord[var2] != "")
                        {

                            ProcessNoInWord = NumWord[var2] + Pos[ctr];
                            wdArr[ctr + 1] = ProcessNoInWord;
                        }


                    }
                    catch
                    {
                        wdArr[ctr + 1] = "";
                    }
                    SP = SP + NOV;
                    ctr = ctr - 1;
                }


                int aSig = 0;

                if (!(wdArr[1] == ""))
                {
                    if (wdArr[2] != "" || wdArr[3] != "" || wdArr[4] != "")
                    {
                        aSig = 1;
                    }
                }
                else
                    aSig = 2;

                if (aSig == 2)
                {
                    if (wdArr[2] == "")
                    {
                        if (wdArr[4] != "")
                        {
                            aSig = 3;

                        }
                        else
                        {
                            aSig = 0;
                        }
                    }
                    else if (wdArr[4] == "" && wdArr[3] == "")
                    {
                        aSig = 0;
                    }
                }

                if (aSig == 3)
                {
                    if (wdArr[3] == "")
                        aSig = 0;
                }

                int Lp;
                Lp = 1;

                while (Lp <= 4)
                {
                    if (aSig == Lp)
                        //FinalWord = " " + strAND + " " + wdArr[Lp] + FinalWord;
                        FinalWord = strAND + wdArr[Lp] + FinalWord;
                    else
                        FinalWord = wdArr[Lp] + FinalWord;

                    Lp = Lp + 1;
                }
                LoopCtr = LoopCtr + 1;
            }
            //NoToWrd = "Tk. " + FinalWord + " Only.";
            //NoToWrd = FinalWord;
            NoToWrd = FinalWord.Replace("  "," ");
            if(isMinus)
            {
                NoToWrd = "Minus " + NoToWrd;
            }
            return NoToWrd;
        }
    }


   public class NumberToWords
    {
        private static String[] units = { "Zero", "One", "Two", "Three",  
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",  
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",  
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",  
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return Convert(amount_int) + " Only.";
                }
                else
                {
                    return Convert(amount_int) + " Point " + Convert(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String Convert(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + Convert(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        //+ ((i % 100 > 0) ? " And " + Convert(i % 100) : "");
                +((i % 100 > 0) ? " " + Convert(i % 100) : "");
            }
            if (i < 100000)
            {
                return Convert(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + Convert(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return Convert(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + Convert(i % 100000) : "");
            }
            //if (i < 1000000000)
            //{
                return Convert(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + Convert(i % 10000000) : "");
            //}
            //return Convert(i / 1000000000) + " Arab "
            //        + ((i % 1000000000 > 0) ? " " + Convert(i % 1000000000) : "");
        }
    } 
}
