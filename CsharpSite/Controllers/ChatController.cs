using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class ChatController : BaseController
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetChat(int targetUserId ) {
            User user = getAuthUser();
            User targetUser = db.Users.Single( u => u.UserId == targetUserId );
            object json = null;
            if (user == null || targetUser == null) {
                json = new { status = "error", message = "either not logged in or target user is invalid" };
            }else {
                List<ChatMessage> msgs = user.GetMessagesWith( targetUserId );
                json = new { status = "success", data = msgs };
            }

            return Json( json );
        }

        [HttpPost]
        public ActionResult SendMessage() {
            object json = null;
            int target = int.Parse(Request.Form["targetID"]);
            string message = Request.Form["message"];
            User user = getAuthUser();
            if(user != null) {
                if (db.Users.Any( u => u.UserId == target )) {
                    if(message.Length > 0) {
                        ChatMessage newmessage = new ChatMessage() {
                            MessageId = 0,
                            SenderID = user.UserId,
                            ReceiverID = target,
                            Message = message};
                        try {
                            db.ChatMessages.Add( newmessage );
                            db.SaveChanges();
                            json = new { status = "success", message = "message sent" };
                        } catch(Exception e) {
                            json = new { status = "error", message = e.Message };
                        }
                    } else {
                        json = new { status = "error", message = "cannot send empty message" };
                    }
                } else {
                    json = new { status = "error", message = "target user does not exist" };
                }
            } else {
                json = new { status = "error", message = "not logged in" };
            }

            

            return Json(json);
        }
    }
}