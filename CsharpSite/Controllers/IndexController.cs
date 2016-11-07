using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class IndexController : Controller
    {
        // GET: indexDefault
        public ActionResult Index()
        {
            return View();
        }
    }
}