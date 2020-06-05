using GamingConsole.Hubs;
using GamingConsole.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamingConsole.Controllers
{
    public class MainBoardController : BaseController
    {
        private GamingConsoleDbContext db = new GamingConsoleDbContext();

        // GET: MainBoard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMainBoardData()
        {
            return PartialView("_MainBoardData", db.MainBoards.ToList());
        }

        public ActionResult PullDataToMainBoard(int  consoleNumber)
        {
            return PartialView("_PullDataToMainBoard", db.MainBoards.FirstOrDefault(c=>c.ConsoleNumber == consoleNumber));
        }

        [HttpPost]
        public JsonResult CreateTeam(BasketBallBoard mainBoard)
        {
            mainBoard.Status = "InActive";
            //if (ModelState.IsValid)
            //{
            if (db.MainBoards.Any(c => c.ConsoleName == mainBoard.ConsoleName))
            {
                return Json(new { mainBoard, status = "500", message = "Team already exsist. \n Please change Console name and retry." }, JsonRequestBehavior.AllowGet);
            }
            db.MainBoards.Add(mainBoard);
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return Json(new { mainBoard, status = "200", message = "Team created successfully." }, JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { mainBoard, status = "500", message = "Data is not valid" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Activate(int consoleNumber)
        {
            if (consoleNumber == 0)
            {
                return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
            }
            BasketBallBoard basketBallBoard = db.MainBoards.Find(consoleNumber);
            basketBallBoard.Status = "Active";
            db.Entry(basketBallBoard).State = EntityState.Modified;
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return Json(new { status = "200", message = "Console is Activated successfully." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeActivate(int consoleNumber)
        {
            if (consoleNumber == 0)
            {
                return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
            }
            BasketBallBoard basketBallBoard = db.MainBoards.Find(consoleNumber);
            basketBallBoard.Status = "InActive";
            db.Entry(basketBallBoard).State = EntityState.Modified;
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return Json(new { status = "200", message = "Console is DeActivated successfully." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResetScores(int consoleNumber)
        {
            if (consoleNumber == 0)
            {
                return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
            }
            BasketBallBoard BasketBallBoard = db.MainBoards.Find(consoleNumber);
            if (BasketBallBoard == null)
            {
                return Json(new { status = "404", message = "Invalid data." }, JsonRequestBehavior.AllowGet);
            }
            BasketBallBoard.TeamOneScore = 0;
            BasketBallBoard.TeamTwoScore = 0;
            db.Entry(BasketBallBoard).State = EntityState.Modified;
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return Json(new { status = "200", message = "Scores reseted successfully." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int consoleNumber)
        {
            if (consoleNumber == 0)
            {
                return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
            }
            BasketBallBoard mainBoard = db.MainBoards.Find(consoleNumber);
            if (mainBoard == null)
            {
                return Json(new { status = "404", message = "Invalid data." }, JsonRequestBehavior.AllowGet);
            }
            db.MainBoards.Remove(mainBoard);
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return Json(new { status = "200", message = "Deleted successfully." }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}