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
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            List<Teacher> teacherList = DataService.Service.teacherService.SelectAll();

            return View(teacherList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(TeacherVM teacherdetails)
        {
            int newTeacherId = 0;

            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher
                {
                    Name = teacherdetails.Name,
                    Surname = teacherdetails.Surname,
                    AddedDate = DateTime.Now,
                    Birthday = teacherdetails.BirthDay,
                    UserId = HttpContext.User.Identity.Name.Split('-')[0],
                };

                newTeacherId = DataService.Service.teacherService.InsertandReturnId(teacher).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newTeacherId });
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Teacher teacher = DataService.Service.teacherService.SelectOne(x => x.Id == id);

            if (teacher == null)
                return RedirectToAction("Index");

            // unmatchedLectures
            List<Lecture> UMLectures = DataService.Service.lectureService.UnmatchedLectures(teacher);
            ViewData["UMLectures"] = UMLectures;

            return View(teacher);
        }

        //Teacher-Lecture Match
        [HttpPost]
        public ActionResult AddOrRemoveLecture(TeacherLecture tl)
        {
            if (ModelState.IsValid)
            {
                tl.AddedDate = DateTime.Now;

                TeacherLecture item = DataService.Service.teacherLectureService
                    .SelectOne(x => x.LectureId == tl.LectureId && x.TeacherId == tl.TeacherId);

                if (item != null)
                    DataService.Service.teacherLectureService.Delete(item.Id);
                else
                    DataService.Service.teacherLectureService.Insert(tl);
            }

            return RedirectToAction("Detail", new { id = tl.TeacherId });
        }

        public ActionResult Update(int? id)
        {
            Teacher teacher = DataService.Service.teacherService.SelectOne(x => x.Id == id);

            if (teacher == null)
                return RedirectToAction("Index");

            return View(teacher);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(TeacherDTO teacherdetails)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher
                {
                    Id = teacherdetails.Id,
                    Name = teacherdetails.Name,
                    Surname = teacherdetails.Surname,
                    Birthday = teacherdetails.BirthDay,
                    UserId = HttpContext.User.Identity.Name.Split('-')[0],
                };

                int result = DataService.Service.teacherService.Update(teacher);

                if (result != 0)
                    return RedirectToAction("Detail", new { id = teacher.Id });
                else
                    return RedirectToAction("Update", new { id = teacher.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}