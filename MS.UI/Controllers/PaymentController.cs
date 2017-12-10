using MS.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.UI.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student()
        {
            ViewData["Students"] = DataService.Service.studentService.SelectAll();
            ViewData["Programs"] = DataService.Service.programService.SelectAll();

            return View();
        }

        public ActionResult Teacher()
        {
            ViewData["Teachers"] = DataService.Service.teacherService.SelectAll();
            //ViewData["Programs"] = DataService.Service.programService.SelectAll();

            return View();
        }
    }
}