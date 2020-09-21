using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFW.EF6;
using BigExample.Areas.Admin.Model;
using BigExample.Code;

namespace BigExample.Areas.Admin.Controllers
{


    public class HomeController : Controller
    {

        BigExampleDbContext bigExampleDb = new BigExampleDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View(bigExampleDb.Posts.ToList());
        }

        public ActionResult getPost(int Post_ID)
        {
            var selectPost = bigExampleDb.Posts.Where(p => p.Post_ID == Post_ID).FirstOrDefault<Post>();
            ViewData["chude"] = selectPost.theme;
            ViewData["noidung"] = selectPost.content;
            ViewData["link"] = selectPost.Urlfile;

            return View();
        }

        public ActionResult AddOT()
        {

            return View();
        }
        [HttpGet]


        public ActionResult dsNhanVien()
        {

            return View(bigExampleDb.Employees.Where(e => e.Delete_flag == 1).ToList());
        }
        public PartialViewResult Menu()
        {
            var model = from x in bigExampleDb.menus
                        join y in bigExampleDb.menu_mapping
                        on x.menu_id equals y.menu_id
                        where y.role_id == UserSession.roleId
                        select new MenuModel()
                        {
                            menu_id = x.menu_id,
                            menu_name = x.menu_name,
                            menu_url = x.menu_url,
                            menu_screen = x.menu_screen,

                        };
            var abc = model.ToList();
            return PartialView(model.ToList());
        }
        public PartialViewResult getRoles()
        {
            return PartialView(bigExampleDb.Roles.ToList());
        }
        public PartialViewResult InforEmp()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult inforUserLogin()
        {
            try
            {

                var selectEmp = bigExampleDb.Employees.Where(s => s.User_emp == UserSession.UserName).FirstOrDefault<Employee>();
                // đối tượng trả về
                EmployeeModel employee = new EmployeeModel
                {
                    Employee_ID = selectEmp.Employee_ID,
                    Employee_Name = selectEmp.Employee_Name,
                    Images = selectEmp.Images,
                    User_emp = selectEmp.User_emp,
                    Department = selectEmp.Department,
                    Date_Of_Birth = selectEmp.Date_Of_Birth,
                    Sex = selectEmp.Sex,
                    Address_emp = selectEmp.Address_emp,
                    Email_Address = selectEmp.Email_Address,
                    Phone_Number = selectEmp.Phone_Number,
                };
                return Json(new
                {
                    Employee_ID = selectEmp.Employee_ID,
                    Employee_Name = selectEmp.Employee_Name,
                    Images = selectEmp.Images,
                    User_emp = selectEmp.User_emp,
                    Department = selectEmp.Department,
                    Date_Of_Birth = selectEmp.Date_Of_Birth,
                    Sex = selectEmp.Sex,
                    Address_emp = selectEmp.Address_emp,
                    Email_Address = selectEmp.Email_Address,
                    Phone_Number = selectEmp.Phone_Number
                }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getEmpForUpdate(int employeeId)
        {
            try
            {
                if (employeeId > 0)
                {

                    var selectEmp = bigExampleDb.Employees.Where(s => s.Employee_ID == employeeId).FirstOrDefault<Employee>();
                    // đối tượng trả về
                    EmployeeModel employee = new EmployeeModel
                    {
                        Employee_ID = selectEmp.Employee_ID,
                        Employee_Name = selectEmp.Employee_Name,
                        Images = selectEmp.Images,
                        User_emp = selectEmp.User_emp,
                        Department = selectEmp.Department,
                        Date_Of_Birth = selectEmp.Date_Of_Birth,
                        Sex = selectEmp.Sex,
                        Address_emp = selectEmp.Address_emp,
                        Email_Address = selectEmp.Email_Address,
                        Phone_Number = selectEmp.Phone_Number,
                    };
                    return Json(new
                    {
                        Employee_ID = selectEmp.Employee_ID,
                        Employee_Name = selectEmp.Employee_Name,
                        Images = selectEmp.Images,
                        User_emp = selectEmp.User_emp,
                        Department = selectEmp.Department,
                        Date_Of_Birth = selectEmp.Date_Of_Birth,
                        Sex = selectEmp.Sex,
                        Address_emp = selectEmp.Address_emp,
                        Email_Address = selectEmp.Email_Address,
                        Phone_Number = selectEmp.Phone_Number
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public Boolean updateEmployee(int employeeID, string employeeName, string userEmp, string images, string department,
            DateTime dateOfBirth, string sex, string addressEmp, string emailAddress, string phoneNumber, int optionRoles)
        {

            var result = bigExampleDb.Employees.Where(s => s.Employee_ID == employeeID).FirstOrDefault<Employee>();
            if (result != null)
            {
                var account = bigExampleDb.Accounts.Where(a => a.User_emp == result.User_emp).FirstOrDefault<Account>();
                account.User_emp = userEmp;
                account.Role_ID = optionRoles;
                result.Employee_Name = employeeName;
                result.User_emp = userEmp;
                result.Images = images;
                result.Department = department;
                result.Date_Of_Birth = dateOfBirth;
                result.Sex = sex;
                result.Address_emp = addressEmp;
                result.Email_Address = emailAddress;
                result.Phone_Number = phoneNumber;
                bigExampleDb.SaveChanges();
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public Boolean addEmployee(string employeeName, string userEmp, string images, string department,
            DateTime dateOfBirth, string sex, string addressEmp, string emailAddress, string phoneNumber, int optionRoles)
        {


            var emp = new Employee()
            {
                Employee_Name = employeeName,
                User_emp = userEmp,
                Images = images,
                Department = department,
                Date_Of_Birth = dateOfBirth,
                Sex = sex,
                Address_emp = addressEmp,
                Email_Address = emailAddress,
                Phone_Number = phoneNumber,
                Delete_flag = 1
            };


            bigExampleDb.Employees.Add(emp);
            bigExampleDb.SaveChanges();

            var account = new Account()
            {
                User_emp = userEmp,
                Password_emp = "123456789Aabc",
                Role_ID = optionRoles
            };
            bigExampleDb.Accounts.Add(account);
            bigExampleDb.SaveChanges();

            var CheckAccount = bigExampleDb.Accounts.Where(a => a.User_emp == userEmp).FirstOrDefault<Account>();
            var CheckEmp = bigExampleDb.Employees.Where(s => s.User_emp == userEmp).FirstOrDefault<Employee>();

            if (CheckEmp != null && CheckAccount != null)
            {
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        public Boolean deleteEmployee(int employeeId)
        {

            var itemToRemove = bigExampleDb.Employees.SingleOrDefault(x => x.Employee_ID == employeeId); //returns a single item.

            if (itemToRemove != null)
            {
                itemToRemove.Delete_flag = 0;
                bigExampleDb.SaveChanges();
                return true;
            }
            else return false;
        }

        [HttpPost]
        public String Homepage()
        {
            if (UserSession.roleId == 1)
            {
                return "https://localhost:44364/Admin/Home";
            }
            else
                return "https://localhost:44364/Admin/Home";
        }


    }
}