using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PG.Core.Windows
{
    public class WinUtility
    {
        public static string MDIFormName = "MDIMain";


        public static bool CheckAndInitForm(ref Form pFrm, string pFormName)
        {
            pFrm = Application.OpenForms[pFormName];
            if (pFrm == null)
            { return false; }
            else
            { return true; }
        }

        public static int FindListViewData(ListView lvw, string sText, bool isPartial, bool searchTag, int subItemIndex)
        {
            ///returns index of 
            ///
            int i = -1;
            bool isMatched = false;
            string lvwData = string.Empty;

            foreach (ListViewItem lItem in lvw.Items)
            {
                if (subItemIndex > -1)
                {
                    //search sub item
                    lvwData = searchTag ? lItem.SubItems[subItemIndex].Tag.ToString() : lItem.SubItems[subItemIndex].Text;
                }
                else
                {
                    lvwData = searchTag ? lItem.Tag.ToString() : lItem.Text;
                }

                if (isPartial)
                {
                    if (lvwData.StartsWith(sText, StringComparison.OrdinalIgnoreCase))
                    {
                        isMatched = true;
                    }
                }
                else
                {
                    if (string.Compare(lvwData, sText, true) == 0)
                    {
                        isMatched = true;
                    }
                }

                if (isMatched)
                {
                    i = lItem.Index;
                    break;
                }

                //lItem
            }
            return i;
        }

        public static System.Windows.Forms.Form GetMdiForm()
        {
            Form frm = System.Windows.Forms.Application.OpenForms[MDIFormName];
            return frm;
        }

        public static System.Windows.Forms.Form GetMdiForm(string pMDIFormName)
        {
            Form frm = System.Windows.Forms.Application.OpenForms[pMDIFormName];
            return frm;
        }
    }
}
