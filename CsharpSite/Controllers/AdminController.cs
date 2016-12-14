using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class AdminController : BaseController {

        [HttpGet]
        public ActionResult Index() {
            return View();
        }
        
    }

    
}
