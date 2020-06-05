using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GamingConsole.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            //Log the error!!
            //_Logger.Error(filterContext.Exception);

            string exceptionMessage = string.Empty;
            if (filterContext.Exception.Message.Length > 200)
            {
                exceptionMessage = filterContext.Exception.Message.Substring(0, 200);
            }
            else
            {
                exceptionMessage = filterContext.Exception.Message;
            }

            //Redirect or return a view, but not both.
            //filterContext.Result = RedirectToAction("ErrorHandler", new RouteValueDictionary(
            //                                        new { controller = "Home",
            //                                            action = "ErrorHandler",
            //                                            ErrorMessage = exceptionMessage }));
            filterContext.Result = RedirectToAction("ErrorHandler", "Home",new { ErrorMessage = exceptionMessage });
            // OR 
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/ErrorHandler/Index.cshtml"
            //};
        }
    }
}