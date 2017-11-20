﻿using MS.DAL;
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

        ////Planning to search 
        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult Index(int? page, string name)
        //{
        //    List<Student> studentList = DataService.Service.studentService.SelectOnePage(page, name, out int pageCount);

        //    ViewData["pageCount"] = pageCount;
        //    ViewData["page"] = page ?? 1;

        //    return View(studentList);
        //}

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
                    isFemale = studentdetails.Gender == "Kadın" ? true : false,
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
    }
}