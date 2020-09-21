using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFW.EF6;
using BigExample.Code;
using BigExample.Areas.Admin.DAOImpl;
using BigExample.Areas.Admin.Model;

namespace BigExample.Areas.Admin.Controllers
{
    public class ConfirmRegistrationController : Controller
    {
        private BigExampleDbContext bigExampleDb = new BigExampleDbContext();
        // GET: Admin/ConfirmRegistration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllList(int month, int year)
        {
            int m = month;
            int y = year;
            var selectRegistrationDetail = bigExampleDb.Registration_Detail.Where(r => r.Registration_Date.Value.Year == year && r.Registration_Date.Value.Month == month).ToList();

            var model = from a in bigExampleDb.Registration_Detail
                        join b in bigExampleDb.Registrations
                        on a.OT_ID equals b.OT_ID
                        join c in bigExampleDb.Employees on b.Employee_ID equals c.Employee_ID
                        join d in bigExampleDb.Statusses on a.Status_flag equals d.Status_flag
                        where a.Registration_Date.Value.Year == y && a.Registration_Date.Value.Month == m
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
            var abc = model.ToList();

            return Json(abc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public Boolean SubmitRegistration(int[] arrId)
        {
            int check = 0;
            for (int i = 0; i < arrId.Length; i++)
            {
                int index = arrId[i];
                var submitRegistration = bigExampleDb.Registration_Detail.Where(r => r.OT_ID == index).FirstOrDefault<Registration_Detail>();
                submitRegistration.Status_flag = 1;
                bigExampleDb.SaveChanges();
            }
            for (int i = 0; i < arrId.Length; i++)
            {
                int index = arrId[i];
                var submitRegistration = bigExampleDb.Registration_Detail.Where(r => r.OT_ID == index).FirstOrDefault<Registration_Detail>();
                if (submitRegistration.Status_flag == 1)
                {
                    check = 1;
                }
                else
                {
                    check = 0;
                    break;
                }
            }
            if (check == 1)
                return true;
            else return false;
        }

        [HttpPost]
        public Boolean RefuseRegistration(int otId, string reasonForCancel)
        {
            var selectRefuse = bigExampleDb.Registration_Detail.Where(r => r.OT_ID == otId).FirstOrDefault<Registration_Detail>();
            selectRefuse.Reason_For_Cancel = reasonForCancel;
            selectRefuse.Status_flag = 3;
            bigExampleDb.SaveChanges();
            var checkRefuse = bigExampleDb.Registration_Detail.Where(r => r.Reason_For_Cancel == reasonForCancel).FirstOrDefault<Registration_Detail>();

            if (checkRefuse != null)
            {
                return true;
            }
            else
                return false;

        }
        [HttpPost]
        public JsonResult PreviousByMonth(int temp, int month, int year)
        {
            RegistrationDetails_DAOImpl details_DAOImpl = new RegistrationDetails_DAOImpl();
            var selectRegistrationDetail = details_DAOImpl.ListPaginationByMonth(temp, month, year);
            return Json(selectRegistrationDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NextByMonth(int temp, int month, int year)
        {
            RegistrationDetails_DAOImpl details_DAOImpl = new RegistrationDetails_DAOImpl();
            var selectRegistrationDetail = details_DAOImpl.ListPaginationByMonth(temp, month, year);
            return Json(selectRegistrationDetail, JsonRequestBehavior.AllowGet);
        }
    }
}