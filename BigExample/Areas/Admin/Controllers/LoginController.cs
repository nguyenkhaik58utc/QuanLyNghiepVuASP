using BigExample.Areas.Admin.Model;
using BigExample.Code;
using EntityFW;
using EntityFW.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BigExample.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        BigExampleDbContext bigExampleDb = new BigExampleDbContext();


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
            var result = new AccountModel().Login(model.UserName, model.PassWord);
            if(result && ModelState.IsValid)
            {
                var selectAccount = bigExampleDb.Accounts.Where(a => a.User_emp == model.UserName).FirstOrDefault<Account>();
                var selectEmp = bigExampleDb.Employees.Where(s => s.User_emp == model.UserName).FirstOrDefault<Employee>();
                UserSession.idEmployee = selectEmp.Employee_ID;
                UserSession.roleId = selectAccount.Role_ID;
                UserSession.UserName = model.UserName;
                return RedirectToAction("Index", "Home");

            }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng,");
                }
            return View(model);
        }
        public ActionResult EditPassWord()
        {
            return View();
        }
        public Boolean ChangePw(string currentPW, string newPW)
        {
            var selectEmp = bigExampleDb.Accounts.Where(a => a.User_emp == UserSession.UserName).FirstOrDefault<Account>();
            if (selectEmp.Password_emp == currentPW)
            {
                selectEmp.Password_emp = newPW;
                bigExampleDb.SaveChanges();

                return true;
            }
            else
                return false;
        }
    }
}