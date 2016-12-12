using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profil
        public ActionResult Index(){
            User user = getAuthUser();
            List<Post> posts = user.Posts.ToList();
            
            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;
            ViewBag.targetUser = user;

            return View();
        }

        public ActionResult View(int userid) {
            User user = getAuthUser();
            if(user.UserId == userid) {
                RedirectToAction( "Index", "Profile" );
            }
            User target = db.Users.Single( u => u.UserId == userid );
            List<Post> posts = target.Posts.ToList();

            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;
            ViewBag.targetUser = target;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateImage() {
            User user = getAuthUser();
            var filecontent = Request.Files[0];
            var stream = filecontent.InputStream;
            var path = Path.Combine( Server.MapPath( "~/Content/images/" ), user.UserId + ".jpg" );
            using (var fileStream = System.IO.File.Create( path )) {
                stream.CopyTo( fileStream );
            }
            return RedirectToAction( "Index", "Profile" );
        }
        [HttpPost]
        public ActionResult UpdateBGImage() {
            User user = getAuthUser();
            var filecontent = Request.Files[0];
            var stream = filecontent.InputStream;
            var path = Path.Combine( Server.MapPath( "~/Content/images/" ), user.UserId + "_background.jpg" );
            using (var fileStream = System.IO.File.Create( path )) {
                stream.CopyTo( fileStream );
            }            
            return RedirectToAction("Index", "Profile");
        }


        [HttpPost]
        public ActionResult Edit() {
            object json = new { status = "warning", message = "no changes were made" };
            string first = Request.Form["firstname"];
            string last = Request.Form["lastname"];
            string email = Request.Form["email"];
            string pw = Request.Form["pw"];
            string country = Request.Form["countryId"];
            string city = Request.Form["cityId"];
            bool changed = false;
            User user = getAuthUser();

            if (pw != null && pw != "") {
                user.Password = pw;
                changed = true;
            }
            if (first != user.First_name) {
                user.First_name = first;
                changed = true;
            }
            if (last != user.Last_name) {
                user.Last_name = last;
                changed = true;
            }
            if (email != user.Email) {
                if (db.Users.Any( u => u.Email == email )) {
                    return Json( new { status = "error", message = "email already exists" } );
                }
                user.Email = email;
                changed = true;
            }
            if (country != user.CountryID + "") {
                try {
                    user.CountryID = int.Parse( country );
                    changed = true;
                } catch (Exception e) {
                    return Json( new { status = "error", message = "something went wrong with the country" + e.Message } );
                }
            }
            if (city != "") {
                try {
                    user.CountryID = int.Parse( city );
                    changed = true;
                } catch (Exception e) {
                    return Json( new { status = "error", message = "something went wrong with the city" } );
                }
            }
            if (changed) { 
                db.SaveChanges();
                json = new { status = "success", message = "profile edited suceessfully" };
            }
            
            return Json( json );

        }

        [HttpPost]
        public ActionResult GetCitiesByCountry() {
            object json = null;
            try {
                int country = int.Parse( Request.Form["countryId"] );
                if(db.Countries.Any(c => c.CountryId == country )) {
                    json = new { status = "success", data = db.Countries.Single( c => c.CountryId == country ).SerializeCities() };
                }else {
                    throw new Exception();
                }

            } catch (Exception e) {
                json = new { status = "error", message = "wrong country id format or unknow id" };
            }


            return Json(json);
        }

    }

    
}