using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigExample.Areas.Admin.Model
{
    public class RegistrationModel
    {
        public int ot_Id { get; set; }
        public string employee_Name { get; set; }
        public DateTime? Registration_Date { get; set; }
        public TimeSpan? Time_Start { get; set; }
        public TimeSpan? Time_Finish { get; set; }
        public string Reason { get; set; }
        public string Reason_For_Cancel { get; set; }
        public string Status_flag { get; set; }
    }
}