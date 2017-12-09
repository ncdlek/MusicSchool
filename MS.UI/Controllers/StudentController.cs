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
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index(int? id)
        {
            List<Student> studentList = DataService.Service.studentService.SelectOnePage(id, out int pageCount);

            ViewData["pageCount"] = pageCount;
            ViewData["page"] = id ?? 1;

            return View(studentList);
        }

        public ActionResult Detail(int? id)
        {
            Student student = DataService.Service.studentService.SelectOne(x => x.Id == id);

            if (student == null)
                return RedirectToAction("Index");

            return View(student);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(StudentVM studentdetails)
        {
            int newStudentId = 0;

            if (ModelState.IsValid)
            {
                Student student = new Student
                {
                    Name = studentdetails.Name,
                    Surname = studentdetails.Surname,
                    AddedDate = DateTime.Now,
                    Birthday = studentdetails.BirthDay,
                    Email = studentdetails.Email,
                    isFemale = studentdetails.Gender == "female" ? true : false,
                    Phone = studentdetails.Phone,
                    UserId = HttpContext.User.Identity.Name.Split('-')[0],
                    Reference = studentdetails.Reference
                };

                newStudentId = DataService.Service.studentService.InsertandReturnId(student).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newStudentId });
        }

        public ActionResult Update(int? id)
        {
            Student student = DataService.Service.studentService.SelectOne(x => x.Id == id);

            if (student == null)
                return RedirectToAction("Index");

            return View(student);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(StudentDTO studentdetails)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student
                {
                    Id = studentdetails.Id,
                    Name = studentdetails.Name,
                    Surname = studentdetails.Surname,
                    Birthday = studentdetails.BirthDay,
                    Email = studentdetails.Email,
                    isFemale = studentdetails.Gender == "female" ? true : false,
                    Phone = studentdetails.Phone,
                    UserId = HttpContext.User.Identity.Name.Split('-')[0],
                    Reference = studentdetails.Reference
                };

                int result = DataService.Service.studentService.Update(student);

                if (result != 0)
                    return RedirectToAction("Detail", new { id = student.Id });
                else
                    return RedirectToAction("Update", new { id = student.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}