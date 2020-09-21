using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFW.EF6;

namespace BigExample.Areas.Admin.Controllers
{
    public class EmpoyeeController : Controller
    {
        BigExampleDbContext bigExampleDb = new BigExampleDbContext();
        // GET: Admin/Empoyee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult PaggingNumber(int numberpagging)
        {

            var selectEmp = bigExampleDb.Registration_Detail.Skip(numberpagging).Take(5).ToList();
            return Json(selectEmp, JsonRequestBehavior.AllowGet);
        }
    }
}