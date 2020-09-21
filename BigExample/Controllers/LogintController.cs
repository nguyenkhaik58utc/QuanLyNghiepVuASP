using BigExample.Areas.Admin.Model;
using BigExample.Code;
using EntityFW.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigExample.Controllers
{
    public class LogintController : Controller
    {
        BigExampleDbContext bigExampleDb = new BigExampleDbContext();

        // GET: Logint
        [HttpGet]
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var result = new EntityFW.AccountModel().Login(model.UserName, model.PassWord);
            if (result && ModelState.IsValid)
            {
                UserSession.UserName = model.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng,");
            }
            return View(model);
        }
    }
}
