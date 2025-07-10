using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.DBClass.General
{
    public class MailMaster
    {
      
        
            
            
        public string LOC_CODE { get; set; }
        public string TRANS_ID { get; set; }
        public string FROM_LOC { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime MailingDate { get; set; }
        public string Status { get; set; }
        public string Count { get; set; }
        public string UserID { get; set; }

        public string HostMailAddress { get; set; }
    }
}
