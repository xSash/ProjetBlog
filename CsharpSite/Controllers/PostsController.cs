using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace CsharpSite.Controllers
{

    public class PostsController : BaseController
    {

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            Comment comment = new Comment();
            comment.PostID = post.PostId;

            ViewModel mymodel = new ViewModel();
            
            mymodel.post = post;
            mymodel.comment = comment;

            //ViewBag.comment = comment;

            ViewBag.PostId = post.PostId;
            if (Request?["format"] == "json")
                return Json(post.Serialize());

            ViewBag.reactions = db.ReactionTypes;

            return View(mymodel);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.Users, "UserId", "Username");
            ViewBag.UserID = getAuthUser();

            return View();
        }

        // POST: Posts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Contents,Publication_date,UserID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();

                if (Request?["format"] == "json")
                    return Json(post.Serialize());

                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", post.UserID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("React")]
        public ActionResult React(FormCollection collection) {
            User user = getAuthUser();
            /*[Bind(Include = "ReactionId,PostID")] PostReaction reaction*/
            PostReaction reaction = new PostReaction();
            db.PostReactions.Attach( reaction );
            reaction.ReactionID = int.Parse(collection["ReactionId"]);
            reaction.PostID = int.Parse(collection["PostID"]);

            if (user == null)
                return HttpNotFound();

            
            reaction.UserID = user.UserId;
            db.PostReactions.Add(reaction);
            
            db.SaveChanges();

            String format = Request?["format"];

            if (format == "json")
                return Json(reaction.Serialize());

            return RedirectToAction("Details", new { id = reaction.PostID });
        }
        [HttpPost]
        public ActionResult Comment([Bind(Include = "Contents,PostID")] Comment comment)
        {
            User user = getAuthUser();
            if (user == null)
                return HttpNotFound();

            Comment c = new Comment() { CommentId = 0, Contents = comment.Contents, PostID = comment.PostID, UserID = user.UserId };
            //db.Comments.Attach( comment );

            comment.Publication_date = new DateTimeOffset();
            db.Comments.Add(c);
            db.SaveChanges();

            String format = Request?["format"];

            if (format == "json")
                return Json(comment.Serialize());

            return RedirectToAction("Details", new { id = comment.PostID });
        }

        // POST: Posts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Contents,Publication_date,UserID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                if (Request?["format"] == "json")
                    return Json(post.Serialize());

                return RedirectToAction("Index");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            dynamic errors = new System.Dynamic.ExpandoObject();

            String[] fields = {"PostId", "Title", "Contents", "Publication_date", "UserID"};
            foreach (String field in fields)
            {
                if (!ModelState.IsValidField(field))
                    errors.field = "Not valid";
            }

            if (Request?["format"] == "json")
                return Json(errors);

            ViewBag.errors = errors;
                
            //ViewBag.UserID = new SelectList(db.Users, "UserId", "Username", post.UserID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            
            db.Posts.Remove(post);
            db.SaveChanges();

            if (Request?["format"] == "json")
            {
                return Json(new
                {
                    status = "success",
                    message = "Post " + id + "deleted successfully"
                });
            }

            return RedirectToAction("Index");
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
