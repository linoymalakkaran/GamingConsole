using GamingConsole.Hubs;
using GamingConsole.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingConsole.Controllers
{
    public class BasketBallConsoleController : BaseController
    {
        private GamingConsoleDbContext db = new GamingConsoleDbContext();

        // GET: BasketBallConsole
        public ActionResult New(int ConsoleNumber = 0)
        {
            if (ConsoleNumber == 0)
            {
                throw new Exception("No such team created under console number: " + ConsoleNumber);
            }
            var model = db.MainBoards.Where(c => c.ConsoleNumber == ConsoleNumber).FirstOrDefault();
            model.Status = "Active";
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return View(model);
        }

        public ActionResult Old(int ConsoleNumber)
        {
            if (ConsoleNumber == 0)
            {
                throw new Exception("No such team created.");
            }
            var model = db.MainBoards.Where(c => c.ConsoleNumber == ConsoleNumber).FirstOrDefault();
            model.Status = "Active";
            db.SaveChanges();
            MainBoardHub.PushDataToMainBoard();
            return View(model);
        }

        [HttpPost]
        public JsonResult PushDataToMainBoard(UpdateScoreModel mainBoard)
        {
            try
            {
                if (mainBoard.ConsoleNumber == 0)
                {
                    return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
                }
                var teamInfo = db.MainBoards.Where(c => c.ConsoleNumber == mainBoard.ConsoleNumber).FirstOrDefault();
                if (teamInfo.Status.ToLower() != "active")
                {
                    return Json(new { mainBoard, status = "500", message = "Console is inactive. \n Please activate the Console and retry." }, JsonRequestBehavior.AllowGet);
                }
                if (mainBoard.IsTeamOne)
                {
                    teamInfo.TeamOneScore = mainBoard.IsIncrease ? teamInfo.TeamOneScore + 1 : teamInfo.TeamOneScore - 1;
                }
                else
                {
                    teamInfo.TeamTwoScore = mainBoard.IsIncrease ? teamInfo.TeamTwoScore + 1 : teamInfo.TeamTwoScore - 1;
                }
                if (teamInfo.TeamOneScore < 0 || teamInfo.TeamTwoScore < 0)
                {
                    return Json(new { status = "404", message = "Score can not be less than Zero." }, JsonRequestBehavior.AllowGet);
                }

                db.Entry(teamInfo).State = EntityState.Modified;
                db.SaveChanges();
                MainBoardHub.PushDataToMainBoard();
                return Json(new { teamInfo, status = "200", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { mainBoard, status = "500", message = "Console is not found." }, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        public JsonResult PullDataFromClient(UpdateScoreModel mainBoard)
        {
            try
            {
                if (mainBoard.ConsoleNumber == 0)
                {
                    return Json(new { status = "404", message = "Missing console number." }, JsonRequestBehavior.AllowGet);
                }
                var teamInfo = db.MainBoards.Where(c => c.ConsoleNumber == mainBoard.ConsoleNumber).FirstOrDefault();
                if (teamInfo.Status.ToLower() != "active")
                {
                    return Json(new { mainBoard, status = "500", message = "Console is inactive. \n Please activate the Console and retry." }, JsonRequestBehavior.AllowGet);
                }
                if (mainBoard.IsTeamOne)
                {
                    teamInfo.TeamOneScore = mainBoard.IsIncrease ? teamInfo.TeamOneScore + 1 : teamInfo.TeamOneScore - 1;
                }
                else
                {
                    teamInfo.TeamTwoScore = mainBoard.IsIncrease ? teamInfo.TeamTwoScore + 1 : teamInfo.TeamTwoScore - 1;
                }
                if(teamInfo.TeamOneScore < 0 || teamInfo.TeamTwoScore< 0)
                {
                    return Json(new { status = "404", message = "Score can not be less than Zero." }, JsonRequestBehavior.AllowGet);
                }

                db.Entry(teamInfo).State = EntityState.Modified;
                db.SaveChanges();
                MainBoardHub.PullDataFromClient(mainBoard.ConsoleNumber);
                return Json(new { teamInfo, status = "200", }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { mainBoard, status = "500", message = "Console is not found." }, JsonRequestBehavior.AllowGet);
            }
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