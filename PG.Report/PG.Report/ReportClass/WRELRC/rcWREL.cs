using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.Report.ReportClass.WRELRC
{
     [Serializable]
    public class rcWREL
    {
        public string CN_NUMBER { get; set; }
      

        private byte[] m_img;
        public byte[] img
        {
            get { return this.m_img; }
            set { this.m_img = value; }
        }
    }
}
