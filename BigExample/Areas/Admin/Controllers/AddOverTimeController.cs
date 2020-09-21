using BigExample.Areas.Admin.DAOImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigExample.Areas.Admin.Controllers
{
    public class AddOverTimeController : Controller
    {
        // GET: Admin/AddOverTime
        public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public JsonResult SearchOverTime(int statusFlag)
        {
            RegistrationDetails_DAOImpl details_DAOImpl = new RegistrationDetails_DAOImpl();
            var selectRegistrationDetail = details_DAOImpl.ListBySearch(statusFlag);
            return Json(selectRegistrationDetail, JsonRequestBehavior.AllowGet);
        }
    }
}