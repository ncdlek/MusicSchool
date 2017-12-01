using MS.BLL.Helper;
using MS.DAL;
using MS.UI.Models;
using MS.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.UI.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program
        public ActionResult Index(DateTime? date)
        {
            if (!date.HasValue)
                date = DateTime.Now;

            int day = ((int)date.Value.DayOfWeek == 0) ? 7 : (int)date.Value.DayOfWeek;

            // günlük programı al
            List<PivotTable> DayProgram = DataService.Service.programService.GetDayProgram(day, date);

            // navigasyon bağlantıları için tarih bilgisi
            ViewData["yesterday"] = date.Value.AddDays(-1).ToString("MM-dd-yyyy");
            ViewData["today"] = date.Value.ToString("dd MMMM yyyy") + " " + (Day)day;
            ViewData["tomorrow"] = date.Value.AddDays(1).ToString("MM-dd-yyyy");

            //sınıf listesi
            ViewData["Rooms"] = DataService.Service.roomService
                .SelectByCondition(x => x.isActive == true)
                .OrderBy(x => x.Id);

            return View(DayProgram);
        }

        public ActionResult Detail(int id)
        {
            WeeklyProgram ProgramDetail = DataService.Service.programService.SelectOne(x => x.Id == id);

            return View(ProgramDetail);
        }

        // GET: Add
        public ActionResult Add(int roomid, int hour, int day)
        {
            // eğer sınıf, o saat ve günde dolu ise ana sayfaya dön
            if (!DataService.Service.programService.isRoomAvailable(roomid, day, hour))
                return RedirectToAction("Index");

            //??buradaki kod düzgün mü? nasıl yapmalıyım?
            WeeklyProgram model = new WeeklyProgram
            {
                RoomId = roomid,
                Hour = hour,
                Day = day
            };

            model.Room = DataService.Service.roomService.SelectOne(x => x.Id == model.RoomId);
            model.WeekDay = DataService.Service.weekDayService.SelectOne(x => x.Id == model.Day);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(ProgramVM programdetails)
        {
            int newProgramId = 0;

            if (ModelState.IsValid)
            {
                WeeklyProgram program = new WeeklyProgram
                {
                    // model uygun ise view modelden bilgileri alalım.
                };

                newProgramId = DataService.Service.programService.InsertandReturnId(program).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newProgramId });
        }

        [HttpPost]
        public JsonResult FetchStudents(string prefix)
        {
            List<Student> students = new List<Student>();

            students = DataService.Service.studentService.AutoComplete(prefix);

            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}