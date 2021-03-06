﻿using MS.BLL.Helper;
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
            ViewData["yesterday"] = date.Value.AddDays(-1);
            ViewData["today"] = date.Value;
            ViewData["tomorrow"] = date.Value.AddDays(1);

            //sınıf listesi
            ViewData["Rooms"] = DataService.Service.roomService
                .SelectByCondition(x => x.isActive == true)
                .OrderBy(x => x.Id);

            return View(DayProgram);
        }

        public ActionResult Detail(int id)
        {
            WeeklyProgram programDetail = DataService.Service.programService.SelectOne(x => x.Id == id);

            if (programDetail != null)
            {
                ViewData["Debt"] = DataService.Service.programService.GetDebt(id);
                ViewData["paymentDate"] = DataService.Service.programService.GetPaymentDate(id);
                return View(programDetail);
            }
            else
                return RedirectToAction("Index");
        }

        // GET: Add
        public ActionResult Add(int? roomid, int? hour, int? day)
        {
            //eğer gelen verilerden biri boşsa ana sayfaya dön
            if (roomid == null || hour == null || day == null)
                return RedirectToAction("Index");

            // eğer sınıf, o saat ve günde dolu ise ana sayfaya dön
            if (!DataService.Service.programService.isRoomAvailable(roomid.Value, day.Value, hour.Value))
                return RedirectToAction("Index");

            //??buradaki kod düzgün mü? nasıl yapmalıyım?
            WeeklyProgram model = new WeeklyProgram
            {
                RoomId = roomid.Value,
                Hour = hour.Value,
                Day = day.Value
            };

            model.Room = DataService.Service.roomService.SelectOne(x => x.Id == model.RoomId);
            model.WeekDay = DataService.Service.weekDayService.SelectOne(x => x.Id == model.Day);

            ViewData["Students"] = DataService.Service.studentService.SelectAll();
            ViewData["Teachers"] = DataService.Service.teacherService.SelectAll();
            ViewData["Lectures"] = DataService.Service.lectureService.SelectAll();

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
                    isActive = true,
                    AddedDate = DateTime.Now,
                    TeacherId = programdetails.TeacherId,
                    StudentId = programdetails.StudentId,
                    LectureId = programdetails.LectureId,
                    RoomId = programdetails.RoomId,
                    Day = programdetails.Day,
                    Hour = programdetails.Hour,
                    StartDate = programdetails.StartDate,
                    EndDate = programdetails.EndDate,
                    Note = programdetails.Note,
                    Price = programdetails.Price,
                    UserId = HttpContext.User.Identity.Name.Split('-')[0]
                };

                newProgramId = DataService.Service.programService.InsertandReturnId(program).Id;
            }
            else
            {
                WeeklyProgram model = new WeeklyProgram
                {
                    RoomId = programdetails.RoomId,
                    Hour = programdetails.Hour,
                    Day = programdetails.Day
                };

                model.Room = DataService.Service.roomService.SelectOne(x => x.Id == model.RoomId);
                model.WeekDay = DataService.Service.weekDayService.SelectOne(x => x.Id == model.Day);

                ViewData["Students"] = DataService.Service.studentService.SelectAll();
                ViewData["Teachers"] = DataService.Service.teacherService.SelectAll();
                ViewData["Lectures"] = DataService.Service.lectureService.SelectAll();

                return View(model);
            }

            return RedirectToAction("Detail", new { id = newProgramId });
        }

        [HttpPost]
        public ActionResult AddEndDate(int programid, DateTime enddate)
        {
            if (ModelState.IsValid)
            {
                DataService.Service.programService.SetEndDate(programid, enddate);
            }

            return RedirectToAction("Detail", new { id = programid });
        }

        // Öğrenci isimleri autocomplete ile gelsin istediğimden bunu yaptım ama kullanamadım.
        [HttpPost]
        public JsonResult FetchStudents(string prefix)
        {
            List<Student> students = new List<Student>();

            students = DataService.Service.studentService.AutoComplete(prefix);

            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}