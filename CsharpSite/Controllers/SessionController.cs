using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;

namespace CsharpSite.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterPost() {
            ActionResult result = null;
            string uname = Request.Form["username"];
            string passw = Request.Form["passw"];
            string cpassw= Request.Form["cpassw"];
            string email = Request.Form["email"];

            user user = new user();
            user.Id = 100;
            user.password = passw;
            user.username = uname;
            user.registration_date = DateTime.Now;

            try {
                DBEntities db = new DBEntities();
                db.users.Add( user );
                db.SaveChanges();
                result = Json( new { status = "success", message = "registered successfully" } );
            } catch (Exception e) {
                result = Json( new { status = "error", message = "registration failed: "+e.Message } );
            }

            return result;
        }

    }
}