using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlogEngine.Controllers
{
    public class PostCommentsController : Controller
    {
        public ActionResult CreateComment()
        {
            return View();
        }
    }
}