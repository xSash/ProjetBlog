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
        // GET: indexDefault
        public ActionResult Index()
        {
            D db = new DBEntities();
            user[] user = db.users.ToArray();
            ViewBag.users = user;
            return View();
        }
    }
}