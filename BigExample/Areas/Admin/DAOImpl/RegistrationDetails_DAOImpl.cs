using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BigExample.Areas.Admin.DAO;
using BigExample.Areas.Admin.Model;
using EntityFW.EF6;

namespace BigExample.Areas.Admin.DAOImpl
{
    public class RegistrationDetails_DAOImpl : RegistrationDao_Details
    {
        private BigExampleDbContext bigExampleDb = new BigExampleDbContext();

        public List<Registration_Detail> ListBySearch(int statusFlag)
        {
            List<Registration_Detail> lst = new List<Registration_Detail>();
            if (statusFlag == 1)
            {
                lst = bigExampleDb.Registration_Detail.Where(r => r.Status_flag == 1).ToList();
            }
            else
            {
                if (statusFlag == 2)
                {
                    lst = bigExampleDb.Registration_Detail.Where(r => r.Status_flag == 2).ToList();
                }
                else
                    lst = bigExampleDb.Registration_Detail.Where(r => r.Status_flag == 3).ToList();
            }
            return lst;

        }

        public List<RegistrationModel> ListPaginationByMonth(int temp, int month, int year)
        {
            List<RegistrationModel> lst = new List<RegistrationModel>();
            if (temp == 1)
            {
                //lst = bigExampleDb.Registration_Detail.Where(r => r.Registration_Date.Value.Year == year && r.Registration_Date.Value.Month == month && r.Status_flag == 3).ToList();
                var model = from a in bigExampleDb.Registration_Detail
                        join b in bigExampleDb.Registrations
                        on a.OT_ID equals b.OT_ID
                        join c in bigExampleDb.Employees on b.Employee_ID equals c.Employee_ID
                        join d in bigExampleDb.Statusses on a.Status_flag equals d.Status_flag
                        where a.Registration_Date.Value.Year == year && a.Registration_Date.Value.Month == month && a.Status_flag == 3
                        select new RegistrationModel()
                        {
                            ot_Id = a.OT_ID,
                            employee_Name = c.Employee_Name,
                            Registration_Date = a.Registration_Date,
                            Time_Start = a.Time_Start,
                            Time_Finish = a.Time_Finish,
                            Reason = a.Reason,
                            Reason_For_Cancel = a.Reason_For_Cancel,
                            Status_flag = d.Status_Name,

                        };
                lst = model.ToList();
            }
            else
            {
                if (temp == 2)
                {
                    //lst = bigExampleDb.Registration_Detail.Where(r => r.Registration_Date.Value.Year == year && r.Registration_Date.Value.Month == month && r.Status_flag == 1).ToList();
                    var model = from a in bigExampleDb.Registration_Detail
                            join b in bigExampleDb.Registrations
                            on a.OT_ID equals b.OT_ID
                            join c in bigExampleDb.Employees on b.Employee_ID equals c.Employee_ID
                            join d in bigExampleDb.Statusses on a.Status_flag equals d.Status_flag
                            where a.Registration_Date.Value.Year == year && a.Registration_Date.Value.Month == month && a.Status_flag == 1
                            select new RegistrationModel()
                            {
                                ot_Id = a.OT_ID,
                                employee_Name = c.Employee_Name,
                                Registration_Date = a.Registration_Date,
                                Time_Start = a.Time_Start,
                                Time_Finish = a.Time_Finish,
                                Reason = a.Reason,
                                Reason_For_Cancel = a.Reason_For_Cancel,
                                Status_flag = d.Status_Name,

                            };
                    lst = model.ToList();

                }
                else
                {
                    //lst = bigExampleDb.Registration_Detail.Where(r => r.Registration_Date.Value.Year == year && r.Registration_Date.Value.Month == month).ToList();
                    var model = from a in bigExampleDb.Registration_Detail
                            join b in bigExampleDb.Registrations
                            on a.OT_ID equals b.OT_ID
                            join c in bigExampleDb.Employees on b.Employee_ID equals c.Employee_ID
                            join d in bigExampleDb.Statusses on a.Status_flag equals d.Status_flag
                            where a.Registration_Date.Value.Year == year && a.Registration_Date.Value.Month == month
                            select new RegistrationModel()
                            {
                                ot_Id = a.OT_ID,
                                employee_Name = c.Employee_Name,
                                Registration_Date = a.Registration_Date,
                                Time_Start = a.Time_Start,
                                Time_Finish = a.Time_Finish,
                                Reason = a.Reason,
                                Reason_For_Cancel = a.Reason_For_Cancel,
                                Status_flag = d.Status_Name,

                            };
                    lst = model.ToList();
                }

            }
            return lst;
        }
    }
}