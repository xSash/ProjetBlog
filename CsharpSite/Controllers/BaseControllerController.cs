using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CsharpSite.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting( ActionExecutingContext filterContext ) {
            //check for loggedin
            //string name = FormsAuthentication.Decrypt( Request.Cookies[FormsAuthentication.FormsCookieName].Value ).Name.ToString();

            //redirect if auth failed to login
        }
    }
}