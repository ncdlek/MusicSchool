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
    public class LectureController : Controller
    {
        // GET: Lecture
        public ActionResult Index()
        {
            List<Lecture> lectureList = DataService.Service.lectureService.SelectAll();

            return View(lectureList);
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Lecture lecture = DataService.Service.lectureService.SelectOne(x => x.Id == id);

            return View(lecture);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(LectureVM lectureDetails)
        {
            int newLectureId = 0;

            if (ModelState.IsValid)
            {
                Lecture lecture = new Lecture
                {
                    Name = lectureDetails.Name,
                    Description = lectureDetails.Description
                };

                newLectureId = DataService.Service.lectureService.InsertandReturnId(lecture).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newLectureId });
        }

        public ActionResult Update(int? id)
        {
            Lecture lecture = DataService.Service.lectureService.SelectOne(x => x.Id == id);

            if (lecture == null)
                return RedirectToAction("Index");

            return View(lecture);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(LectureDTO lecturedetails)
        {
            if (ModelState.IsValid)
            {
                Lecture lecture = new Lecture
                {
                    Id = lecturedetails.Id,
                    Name = lecturedetails.Name,
                    Description = lecturedetails.Description
                };

                int result = DataService.Service.lectureService.Update(lecture);

                if (result != 0)
                    return RedirectToAction("Detail", new { id = lecture.Id });
                else
                    return View(lecture);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}