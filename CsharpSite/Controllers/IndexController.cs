using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;
using System.Threading;

namespace CsharpSite.Controllers
{
    public class IndexController : BaseController
    {
        private DB db = new DB();
        // GET: indexDefault
        public ActionResult Index()
        {
            return RedirectToAction( "Index", "Feed" );
            /*ViewBag.users = db.Users.ToList();
            ViewBag.posts = db.Posts.ToList();

            db.Groups.First().Members.Add(db.Users.First());
            ViewBag.groups = db.Groups.ToList();

            return View();*/
        }


    }
}