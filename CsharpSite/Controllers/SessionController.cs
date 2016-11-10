using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;
using System.Data.Entity;

namespace CsharpSite.Controllers
{
    public class SessionController : Controller
    {
        private DB db = new DB();
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult RegisterGet() {

            return View("Register");
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterPost() {
            ActionResult result = null;
            string uname = Request.Form["username"];
            string passw = Request.Form["passw"];
            string cpassw= Request.Form["cpassw"];
            string email = Request.Form["email"];

            User user = new User();
            user.Password = passw;
            user.Username = uname;
            user.Email = email;
            user.Registration_date = DateTimeOffset.Now;

            try {
                
                db.Users.Add( user );
                db.SaveChanges();
                result = Json( new { status = "success", message = "registered successfully" } );
            } catch (Exception e) {
                result = Json( new { status = "error", message = "registration failed: "+e.Message } );
            }
            
            return result;
        }

    }
}