using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlogEngine.DB
{
    public partial class Post
    {
        public int[] CategoryIds { get; set; }
    }
}