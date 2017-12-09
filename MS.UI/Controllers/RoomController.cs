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
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            List<Room> roomList = DataService.Service.roomService.SelectAll();

            return View(roomList);
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Room room = DataService.Service.roomService.SelectOne(x => x.Id == id);

            if (room == null)
                return RedirectToAction("Index");

            // unMatchedLectures
            List<Lecture> UMLectures = DataService.Service.lectureService.UnmatchedLectures(room);
            ViewData["UMLectures"] = UMLectures;

            return View(room);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(RoomVM roomdetails)
        {
            int newRoomId = 0;

            if (ModelState.IsValid)
            {
                Room room = new Room
                {
                    isActive = true,
                    Name = roomdetails.Name
                };

                newRoomId = DataService.Service.roomService.InsertandReturnId(room).Id;
            }
            else
            {
                return View();
            }

            return RedirectToAction("Detail", new { id = newRoomId });
        }

        //Room-Lecture Match
        [HttpPost]
        public ActionResult AddOrRemoveLecture(RoomLecture rl)
        {
            if (ModelState.IsValid)
            {
                rl.AddedDate = DateTime.Now;

                RoomLecture item = DataService.Service.roomLectureService
                    .SelectOne(x => x.LectureId == rl.LectureId && x.RoomId == rl.RoomId);

                if (item != null)
                    DataService.Service.roomLectureService.Delete(item.Id);
                else
                    DataService.Service.roomLectureService.Insert(rl);
            }

            return RedirectToAction("Detail", new { id = rl.RoomId });
        }

        public ActionResult Update(int? id)
        {
            Room room = DataService.Service.roomService.SelectOne(x => x.Id == id);

            if (room == null)
                return RedirectToAction("Index");

            return View(room);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(RoomDTO roomdetails)
        {
            if (ModelState.IsValid)
            {
                Room room = new Room
                {
                    Id = roomdetails.Id,
                    Name = roomdetails.Name
                };

                int result = DataService.Service.roomService.Update(room);

                if (result != 0)
                    return RedirectToAction("Detail", new { id = room.Id });
                else
                    return RedirectToAction("Update", new { id = room.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}