using BigExample.Code;
using EntityFW.EF6;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace BigExample.Areas.Admin.Controllers
{
    public class TinhLuongController : Controller
    {
        BigExampleDbContext bigExample = new BigExampleDbContext();
        // GET: Admin/TinhLuong
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TinhLuong()
        {
            return View();
        }
        [HttpPost]
        public Boolean getSalary(int Time_id, int Employee_Id, DateTime DateWork, TimeSpan timeStartAM, TimeSpan timeFinishAM, TimeSpan timeStartPM, TimeSpan timeFinishPM, TimeSpan timeStartOT, TimeSpan timeFinishOT)
        {

            var check = bigExample.Timekeepings.Where(t => t.Employee_ID == Employee_Id && t.Timekeeping_Date.Year == DateWork.Year && t.Timekeeping_Date.Month == DateWork.Month && t.Timekeeping_Date.Day == DateWork.Day).ToList();
            if (check.Count == 0)
            {
                int maxPostId;
                var selectMax = bigExample.Timekeepings.ToList();
                if (selectMax.Count == 0)
                {
                    maxPostId = 0;
                }
                else
                {
                    maxPostId = bigExample.Timekeepings.Max(r => r.Time_ID);
                }

                var newTime = new Timekeeping()
                {
                    Employee_ID = Employee_Id,
                    Timekeeping_Date = DateWork,
                    time1 = timeStartAM,
                    time2 = timeFinishAM,
                    time3 = timeStartPM,
                    time4 = timeFinishPM,
                    time5 = timeStartOT,
                    time6 = timeFinishOT,
                };
                bigExample.Timekeepings.Add(newTime);
                bigExample.SaveChanges();
                var checkAdd = bigExample.Timekeepings.Where(p => p.Time_ID == maxPostId + 1).FirstOrDefault<Timekeeping>();
                if (checkAdd != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var checkEdit = bigExample.Timekeepings.Where(t => t.Employee_ID == Employee_Id && t.Timekeeping_Date.Year == DateWork.Year && t.Timekeeping_Date.Month == DateWork.Month && t.Timekeeping_Date.Day == DateWork.Day).FirstOrDefault<Timekeeping>();
                if (checkEdit != null)
                {
                    checkEdit.Timekeeping_Date = DateWork;
                    checkEdit.time1 = timeStartAM;
                    checkEdit.time2 = timeFinishAM;
                    checkEdit.time3 = timeStartPM;
                    checkEdit.time4 = timeFinishPM;
                    checkEdit.time5 = timeStartOT;
                    checkEdit.time6 = timeFinishOT;

                    bigExample.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public ActionResult XemLuong()
        {
            DateTime getDate = DateTime.Now;
            int monthNow = 0;
            int yearNow = 0;
            Double tongGioLamNhanVien = 0;
            Double tongGioLamChinhThuc = 0;
            Double tongGioLAmOverTime = 0;

            int ngay = 0;
            if (getDate.Day < 10 && getDate.Month == 1)
            {
                monthNow = 12;
                yearNow = getDate.Year - 1;
            }
            else
            {
                if (getDate.Day < 10)
                {
                    monthNow = getDate.Month - 1;
                    yearNow = getDate.Year;
                }
                else
                {
                    monthNow = getDate.Month;
                    yearNow = getDate.Year;
                }
            }

            if (monthNow == 2 && monthNow % 4 == 0)
            {
                ngay = 29;
            }
            else
            {
                if (monthNow == 2 && monthNow % 4 != 0)
                {
                    ngay = 28;
                }
                else
                {
                    if (monthNow % 2 == 0)
                    {
                        ngay = 30;
                    }
                    else
                        ngay = 31;
                }
            }

            ViewData["Date"] = monthNow + "/" + yearNow;

            var selectSalary = bigExample.Timekeepings.Where(t => t.Employee_ID == UserSession.idEmployee && t.Timekeeping_Date.Year == yearNow && t.Timekeeping_Date.Month == monthNow).ToList();
            for (int i = 0; i < selectSalary.Count; i++)
            {
                Double minutes = (selectSalary[i].time2.Hours * 60 + selectSalary[i].time2.Minutes) - (selectSalary[i].time1.Hours * 60 + selectSalary[i].time1.Minutes) + (selectSalary[i].time4.Hours * 60 + selectSalary[i].time4.Minutes) - (selectSalary[i].time3.Hours * 60 + selectSalary[i].time3.Minutes) + (selectSalary[i].time6.Hours * 60 + selectSalary[i].time6.Minutes) * 1.25 - (selectSalary[i].time5.Hours * 60 + selectSalary[i].time1.Minutes) * 1.25;
                tongGioLamChinhThuc += ((selectSalary[i].time2.Hours * 60 + selectSalary[i].time2.Minutes) - (selectSalary[i].time1.Hours * 60 + selectSalary[i].time1.Minutes) + (selectSalary[i].time4.Hours * 60 + selectSalary[i].time4.Minutes) - (selectSalary[i].time3.Hours * 60 + selectSalary[i].time3.Minutes)) / 60;
                tongGioLAmOverTime += ((selectSalary[i].time6.Hours * 60 + selectSalary[i].time6.Minutes) - (selectSalary[i].time5.Hours * 60 + selectSalary[i].time1.Minutes)) / 60;

                tongGioLamNhanVien += minutes / 60;
            }
            string ngayDau = monthNow + "/" + 01 + "/" + yearNow;
            string ngayCuoi = monthNow + "/" + ngay + "/" + yearNow;
            DateTime ngayBatDau = Convert.ToDateTime(ngayDau);
            DateTime ngayKetThuc = Convert.ToDateTime(ngayCuoi);
            Double tongGioLam = (GetWorkingDays(ngayBatDau, ngayKetThuc) + 1) * 8;
            Double luong = (tongGioLamNhanVien / tongGioLam) * 10000000;
            ViewData["tongGioLamNhanVien"] = tongGioLamNhanVien;
            ViewData["tongGioLamChinhThuc"] = tongGioLamChinhThuc;
            ViewData["tongGioLAmOverTime"] = tongGioLAmOverTime;
            ViewData["tongGioLam"] = tongGioLam;
            ViewData["luong"] = luong;
            return View();
        }

        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var dayDifference = (int)to.Subtract(from).TotalDays;
            return Enumerable
                .Range(1, dayDifference)
                .Select(x => from.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
        }


    }
}