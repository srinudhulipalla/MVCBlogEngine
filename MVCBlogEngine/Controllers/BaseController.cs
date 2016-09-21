using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlogEngine.DB;

namespace MVCBlogEngine.Controllers
{
    public class BaseController : Controller
    {
        BlogEngineEntities _blogEngineDB = new BlogEngineEntities();

        public BlogEngineEntities BlogEngineDB
        {
            get
            {
                return _blogEngineDB;
            }
            set
            {
                _blogEngineDB = value;
            }
        }

        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}