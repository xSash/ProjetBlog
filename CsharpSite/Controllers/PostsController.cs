﻿using System;
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
        public ActionResult Create([Bind(Include = "Title,Contents")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserID = getAuthUser().UserId;
                post.Publication_date = DateTimeOffset.Now;
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

        /*
            REACT to a post, if you clic on the reaction you already made, will remove it, if you clic on another reaction, will replace it
         */
        [HttpPost, ActionName("React")]
        public ActionResult React(FormCollection collection) {
            User user = getAuthUser();
            if (user == null)
                return HttpNotFound();
            int postid = int.Parse( collection["PostID"]?? Request.Form["PostID"] );
            int reactionid = int.Parse( collection["ReactionId"] ?? Request.Form["ReactionId"] );
            PostReaction reaction = null;
            if (db.PostReactions.Any(p => p.PostID == postid && p.UserID == user.UserId )) {
                reaction = db.PostReactions.Single( p => p.PostID == postid && p.UserID == user.UserId );
                if (reaction.ReactionID == reactionid) {
                    db.PostReactions.Remove(reaction);
                    db.SaveChanges();
                    return Json( new { message = "unreacted", status = "success" } );
                } else {
                    return Json( new { status = "error", message = "you already reacted to that post" } );
                }
            } else {
                /*[Bind(Include = "ReactionId,PostID")] PostReaction reaction*/
                reaction = new PostReaction() { ReactionID = reactionid, PostID = postid, UserID = user.UserId, PostReactionId = 0 };
                //db.PostReactions.Attach( reaction );
                db.PostReactions.Add( reaction );
                db.SaveChanges();
                
            }
            db.Dispose();
            db = new DB();

            string format = Request?["format"];

            //if (format == "json")
                return Json( new { message = "reacted", status = "success" } );

            //return RedirectToAction("Details", new { id = reaction.PostID });
        }
        /*
            REACT to a comment, if you clic on the reaction you already made, will remove it, if you clic on another reaction, will replace it
         */
        [HttpPost, ActionName( "ReactComment" )]
        public ActionResult ReactComment( FormCollection collection ) {
            User user = getAuthUser();
            if (user == null)
                return HttpNotFound();
            int commentid = int.Parse( collection["CommentID"] ?? Request.Form["CommentID"] );
            int reactionid = int.Parse( collection["ReactionId"] ?? Request.Form["ReactionId"] );
            CommentReaction reaction = null;
            if (db.CommentReactions.Any( p => p.CommentID == commentid && p.UserID == user.UserId )) {
                reaction = db.CommentReactions.Single( p => p.CommentID == commentid && p.UserID == user.UserId );
                if (reaction.ReactionID == reactionid) {
                    db.CommentReactions.Remove( reaction );
                    db.SaveChanges();
                    return Json( new { message = "unreacted", status = "success" } );
                } else {
                    return Json( new { status = "error", message = "you already reacted to that post" } );

                }
            } else {
                reaction = new CommentReaction() { ReactionID = reactionid, CommentID = commentid, UserID = user.UserId, CommentReactionId = 0 };
                db.CommentReactions.Add( reaction );
                db.SaveChanges();

            }
            db.Dispose();
            db = new DB();

            string format = Request?["format"];

            //if (format == "json")
            return Json( new { message = "reacted", status = "success" } );

            //return RedirectToAction("Details", new { id = reaction.PostID });
        }
        [HttpPost]
        public ActionResult Comment([Bind(Include = "Contents,PostID")] Comment comment)
        {
            User user = getAuthUser();
            if (user == null)
                return HttpNotFound();

            Comment c = new Comment() { Contents = comment.Contents, PostID = comment.PostID, UserID = user.UserId };
            //db.Comments.Attach( comment );

            comment.Publication_date = DateTimeOffset.Now;
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
