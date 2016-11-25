using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;
using System.Security.Cryptography;

namespace CsharpSite.Controllers
{
    public class SessionController : BaseController
    {
        
        // GET: Session
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Session");
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult RegisterGet() {
            if (Session[Auth.AUTH_USER_SESSION_NAME] != null) {
                return RedirectToAction("Index", "Feed");
            }
            return View("Register");
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterPost() {
            if (Session[Auth.AUTH_USER_SESSION_NAME] != null) {
                RedirectToAction("Index", "Feed");
            }
            ActionResult result = null;
            string first_name = Request.Form["firstName"];
            string last_name = Request.Form["lastName"];
            string uname = Request.Form["username"];
            string profile_picture = Request["profilePicture"];
            string banner_picture = Request["bannerPicture"];
            string email = Request.Form["email"];
            string mobile_number = Request.Form["mobileNumber"];
            string passw = Request.Form["password"];
            string birthday = Request.Form["date"];
            //int country = Request.Form["country"];
            //int city = Request.Form["city"];
            char gender = Request.Form["gender"].ToUpper()[0];


            User user = new User();
            user.First_name = first_name;
            user.Last_name = last_name;
            user.Username = uname;
            user.Email = email;
            user.Phone_number = mobile_number;
            user.Password = passw;
            user.Birthday = DateTimeOffset.Parse(birthday);
            //user.CountryID = country;
            //user.CityID = city;
            user.Gender = gender;
            
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


        [HttpGet]
        [ActionName("Login")]
        public ActionResult LoginGet() {
            if (Session[Auth.AUTH_USER_SESSION_NAME] != null) {
                return RedirectToAction("Index", "Feed");
            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost() {
            string username = Request.Form["usr"];
            string password = Request.Form["pw"];

            if (Session[Auth.AUTH_USER_SESSION_NAME] != null) {
                return RedirectToAction("Index","Feed");
            }
            bool loginResult = UserManager.IsValid(username, password);

            if ( loginResult ) {
                User loginUser = db.Users.Where(u => u.Username == username
                    && u.Password == password).First();
                bool rememberCred = bool.Parse( Request.Cookies[Auth.REMEMBER_CREDENTIALS_COOKIE_NAME]?.Value ?? "false" );
                string usr = Request.Cookies[Auth.USRNAME_COOKIE_NAME]?.Value;
                string pw = Request.Cookies[Auth.PW_COOKIE_NAME]?.Value;

                Session.Add(Auth.AUTH_USER_SESSION_NAME, new Auth { User = loginUser });

                if (rememberCred && (usr == null || pw == null) ) {
                    Response.SetCookie(new HttpCookie(Auth.USRNAME_COOKIE_NAME, username));
                    Response.SetCookie(new HttpCookie(Auth.PW_COOKIE_NAME, password));
                }

            }else {
                ViewBag.SuccessMessage = TempData["SuccessMessage"]?.ToString();
                ViewBag.ErrorMessage = "Invalid Password or Username";
                return View();
            }

            return RedirectToAction("Index", "Index");
        }

        [HttpGet]
        [ActionName("Logout")]
        public ActionResult LogoutGet() {
            Session.Remove(Auth.AUTH_USER_SESSION_NAME);
            TempData["SuccessMessage"] = "Logged out successfully";
            return RedirectToAction("Login", "Session");
        }
        [HttpPost]
        [ActionName("Logout")]
        public ActionResult LogoutPost() {
            Session.Remove(Auth.AUTH_USER_SESSION_NAME);
            TempData["SuccessMessage"] = "Logged out successfully";
            return RedirectToAction("Login", "Session");
        }
    }

    

    
}