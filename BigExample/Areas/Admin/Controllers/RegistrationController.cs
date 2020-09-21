using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigExample.Code;
using EntityFW.EF6;

namespace BigExample.Areas.Admin.Controllers
{
    public class RegistrationController : Controller
    {

        BigExampleDbContext bigExampleDb = new BigExampleDbContext();

        // GET: Admin/Registration
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult tableRegistration(int month, int year)
        {
            int m = month;
            int y = year;
            IList<Registration_Detail> registrations = new List<Registration_Detail>();
            var selectRegistration = bigExampleDb.Registrations.Where(r => r.Employee_ID == UserSession.idEmployee).ToList();
            for (int i = 0; i < selectRegistration.Count; i++)
            {
                int otId = selectRegistration[i].OT_ID;
                var selectRegistrationDetail = bigExampleDb.Registration_Detail.Where(r => r.OT_ID == otId && r.Registration_Date.Value.Year == year && r.Registration_Date.Value.Month ==  month  ).FirstOrDefault<Registration_Detail>();
                if(selectRegistrationDetail != null)
                registrations.Add(selectRegistrationDetail);
            }
            return Json(registrations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public Boolean deleteResgitration(int otId,string LyDoHuy)
        {
            var itemToRemove = bigExampleDb.Registration_Detail.SingleOrDefault(r => r.OT_ID == otId); //returns a single item.
            var itemRemoveRegistration = bigExampleDb.Registrations.SingleOrDefault(r => r.OT_ID == otId);
            if (itemToRemove != null && itemRemoveRegistration != null)
            {
                bigExampleDb.Registration_Detail.Remove(itemToRemove);
                bigExampleDb.SaveChanges();
                bigExampleDb.Registrations.Remove(itemRemoveRegistration);
                bigExampleDb.SaveChanges();
                return true;
            }
            else return false;
        }

        public Boolean addRegistrationDetail(DateTime NgayDangKy,TimeSpan TGBatDau,TimeSpan TGKetThuc,string LyDo)
        {

            int maxOtId = bigExampleDb.Registration_Detail.Max(r => r.OT_ID);

            var registrationDetail = new Registration_Detail()
            {
                OT_ID = maxOtId + 1,
                Registration_Date = NgayDangKy,
                Time_Start = TGBatDau,
                Time_Finish = TGKetThuc,
                Reason = LyDo,
                Status_flag = 2
            };
            

            bigExampleDb.Registration_Detail.Add(registrationDetail);
            bigExampleDb.SaveChanges();

            var registration  = new Registration()
            {
                OT_ID = maxOtId + 1,
                Employee_ID = UserSession.idEmployee
            };
            bigExampleDb.Registrations.Add(registration);
            bigExampleDb.SaveChanges();

            var CheckRegistrationDetail = bigExampleDb.Registration_Detail.Where(r => r.OT_ID == maxOtId + 1).FirstOrDefault<Registration_Detail>();
            var CheckRegistration = bigExampleDb.Registrations.Where(r => r.OT_ID == maxOtId + 1).FirstOrDefault<Registration>();

            if (CheckRegistration != null && CheckRegistrationDetail != null)
            {
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public Boolean editRegistrationDetail(int OtId,DateTime NgayDangKy,TimeSpan TGBatDau,TimeSpan TGKetThuc,string LyDo)
        {
            var result = bigExampleDb.Registration_Detail.Where(s => s.OT_ID == OtId).FirstOrDefault<Registration_Detail>();
            if (result != null)
            {
                result.Registration_Date = NgayDangKy;
                result.Time_Start = TGBatDau;
                result.Time_Finish = TGKetThuc;
                result.Reason = LyDo;
                bigExampleDb.SaveChanges();
                return true;
            }
            else
                return false;
        }


    }
}