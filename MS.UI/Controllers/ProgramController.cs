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
            ViewData["Rooms"] = DataService.Service.roomService.SelectByCondition(x => x.isActive == true).OrderBy(x => x.Id);

            return View(DayProgram);
        }

        public ActionResult Detail(int id)
        {
            WeeklyProgram ProgramDetail = DataService.Service.programService.SelectOne(x => x.Id == id);

            return View(ProgramDetail);
        }

        // GET: Add
        public ActionResult Add(int? roomid, int? hour, int? day)
        {
            ViewData["RoomId"] = roomid ?? 0;
            ViewData["Hour"] = hour ?? 0;
            ViewData["Day"] = day ?? 0;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(ProgramVM programdetails)
        {
            int newProgramId = 0;

            if (ModelState.IsValid)
            {
                WeeklyProgram program = new WeeklyProgram
                {
                    
                };

                newProgramId = DataService.Service.programService.InsertandReturnId(program).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newProgramId });
        }
    }
}