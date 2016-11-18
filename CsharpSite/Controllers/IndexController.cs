using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;


namespace CsharpSite.Controllers
{
    public class IndexController : Controller
    {
        private DB db = new DB();
        // GET: indexDefault
        public ActionResult Index()
        {

            ViewBag.users = db.Users.ToList();
            ViewBag.posts = db.Posts.ToList();
            Session.Add( "test", "hi!" );

            db.Groups.First().Members.Add(db.Users.First());
            ViewBag.groups = db.Groups.ToList();



            return View();
        }
    }
}