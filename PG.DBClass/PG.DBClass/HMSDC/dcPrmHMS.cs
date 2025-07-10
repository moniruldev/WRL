using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.DBClass.HMSDC
{
   public class dcPrmHMS
    {
       public int TRANS_ID { get; set; }
       public int TRANS_NO { get; set; }
       public int GUEST_ID { get; set; }
       public int RESERVATION_ID { get; set; }
       public string GUEST_NAME { get; set; }
       public string MOBILE_NO { get; set; }
       public string IDENTITY_NO { get; set; }
       public DateTime? FROM_DATE { get; set; }
       public DateTime? TO_DATE { get; set; }
       public string TITLE { get; set; }
       public int MAX_PERSON { get; set; }
       public string IS_ACTIVE { get; set; }

    }
}
