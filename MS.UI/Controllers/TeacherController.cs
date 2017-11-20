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
            Teacher teacher = DataService.Service.teacherService.SelectOne(x => x.Id == id);

            if (teacher == null)
                return RedirectToAction("Index");

            return View(teacher);
        }
    }
}