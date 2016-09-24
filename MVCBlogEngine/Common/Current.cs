using MVCBlogEngine.Controllers;
using MVCBlogEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogEngine.Common
{
    public class Current
    {
        private static int _userId;
        public static int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        
    }
}