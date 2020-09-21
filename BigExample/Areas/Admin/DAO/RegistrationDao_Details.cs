using BigExample.Areas.Admin.Model;
using EntityFW.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigExample.Areas.Admin.DAO
{
    interface RegistrationDao_Details
    {
        List<RegistrationModel> ListPaginationByMonth(int temp, int month, int year);
        List<Registration_Detail> ListBySearch(int statusFlag);
    }
}
