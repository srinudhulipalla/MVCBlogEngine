using MVCBlogEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlogEngine.Controllers
{
    public class PostController : BaseController
    {
        public ActionResult CreatePost(string title, string contents)
        {
            return View();
        }

        public ActionResult GetUserPosts()
        {
            var model = BlogEngineDB.Posts.Where(p => p.AuthorId == Current.UserId).ToList();

            return View(model);
        }
    }
}