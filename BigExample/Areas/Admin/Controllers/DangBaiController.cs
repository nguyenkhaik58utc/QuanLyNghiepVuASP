using BigExample.Areas.Admin.Model;
using BigExample.Code;
using EntityFW.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostModel = BigExample.Areas.Admin.Model.PostModel;

namespace BigExample.Areas.Admin.Controllers
{
    public class DangBaiController : Controller
    {
        BigExampleDbContext bigExample = new BigExampleDbContext();
        // GET: Admin/DangBai
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangBai()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetAll()
        {
            List<PostModel> lstPost = new List<PostModel>();
            var lst = bigExample.Posts.ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                int idEmp = lst[i].Employee_ID;
                var emp = bigExample.Employees.Where(e => e.Employee_ID == idEmp).FirstOrDefault<Employee>();
                lstPost.Add(new PostModel(lst[i].Post_ID, lst[i].theme, lst[i].content, lst[i].Urlfile, emp.Employee_Name));
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public Boolean deletePost(int postId)
        {
            var itemToRemove = bigExample.Posts.SingleOrDefault(x => x.Post_ID == postId); //returns a single item.

            if (itemToRemove != null)
            {
                bigExample.Posts.Remove(itemToRemove);
                bigExample.SaveChanges();
                return true;
            }
            else return false;
        }

        [HttpPost]
        public Boolean addPost(string valueTheme,string valueContent,string valueUrl)
        {
            int maxPostId = bigExample.Posts.Max(r => r.Post_ID);

            var newPost = new Post()
            {
                theme = valueTheme,
                content = valueContent,
                Urlfile = valueUrl,
                Employee_ID = UserSession.idEmployee,
            };
            bigExample.Posts.Add(newPost);
            bigExample.SaveChanges();
            var checkAdd = bigExample.Posts.Where(p => p.Post_ID == maxPostId + 1).FirstOrDefault<Post>();
            if(checkAdd != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
        [HttpPost]
        public Boolean editPost(int postId,string valueTheme, string valueContent, string valueUrl)
        {
            var selectPost = bigExample.Posts.Where(p => p.Employee_ID == postId).FirstOrDefault<Post>();

            if (selectPost != null)
            {
                selectPost.content = valueContent;
                selectPost.theme = valueTheme;
                selectPost.Urlfile = valueUrl;
                bigExample.SaveChanges();
                return true;
            }
            else
                return false;
        }

    }
}