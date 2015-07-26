using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LabProject.Models;
using LabProject.Infrastructure.Services;
using LabProject.Infrastructure;
using System.Threading.Tasks;

namespace LabProject.Controllers
{
    public class HomeController : Controller
    {
        private PlayerService playerService = new PlayerService();
        private static readonly List<string> comments = new List<string>();

        private int itemsPerPage = 10;

        public ActionResult Index(int page = 1)
        {
            ViewBag.Page = page;

            return View(comments);
        }

        [HttpPost]
        public ActionResult AddComment(string comment)
        {
            if (comment != string.Empty)
            {
                comments.Add(comment);

                if (Request.IsAjaxRequest())
                    return PartialView("_Comment", comment);
                else
                    return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Comment can't be empty");

                return PartialView("_InvalidComment");
            }
        }

        public ActionResult ShowPlayers(int page)
        {
            if(!Request.IsAjaxRequest())
            {
                int redirectToPage = page;

                return RedirectToAction("Index", new { page = redirectToPage });
            }

            return PartialView("_Players", playerService.GetPlayersWithPage(page, itemsPerPage));
        }

        [ChildActionOnly]
        public ActionResult ShowPlayersNotAjax(int page)
        {
            return PartialView("_Players", playerService.GetPlayersWithPage(page, itemsPerPage));
        }
    }
}
