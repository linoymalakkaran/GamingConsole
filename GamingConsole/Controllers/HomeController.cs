using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingConsole.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorHandler(string ErrorMessage)
        {
            ViewBag.ErrorMessage = ErrorMessage;
            return View();
        }
        

    }
}