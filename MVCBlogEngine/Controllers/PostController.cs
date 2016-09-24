using MVCBlogEngine.Common;
using MVCBlogEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlogEngine.Controllers
{
    public class PostController : BaseController
    {
        public ActionResult CreatePost(string title, string contents, string imagePath, bool allowComments, int[] categoryIds)
        {
            var post = new Post();
            post.Title = title;
            post.Contents = contents;
            post.ImagePath = imagePath;
            post.AllowComments = allowComments;
            post.AuthorId = Current.UserId;
            post.CreatedDate = DateTime.Now;
            post.IsPublished = false;

            BlogEngineDB.Posts.Add(post);
            BlogEngineDB.SaveChanges();

            SetPostCategories(post.PostId, categoryIds); 

            return View();
        }

        public ActionResult GetUserPosts()
        {
            var model = BlogEngineDB.Posts.Where(p => p.AuthorId == Current.UserId).ToList();
            return View(model);
        }

        public Post ViewPost(int postId)
        {
            var post = BlogEngineDB.Posts.FirstOrDefault(p => p.PostId == postId);
            return (post.AuthorId == Current.UserId) ? post : null;
        }

        public bool UpdatePost(Post post)
        {
            var dbPost = BlogEngineDB.Posts.FirstOrDefault(p => p.PostId == post.PostId);
            dbPost.Title = post.Title;
            dbPost.Contents = post.Contents;
            dbPost.ImagePath = post.ImagePath;
            dbPost.UpdatedBy = Current.UserId;
            dbPost.UpdatedDate = DateTime.Now;
            dbPost.IsPublished = post.IsPublished;
            dbPost.AllowComments = post.AllowComments;
            BlogEngineDB.SaveChanges();

            SetPostCategories(dbPost.PostId, post.CategoryIds);

            return true;
        }

        public bool RemovePost(int postId)
        {
            var post = BlogEngineDB.Posts.FirstOrDefault(p => p.PostId == postId);

            if (post.AuthorId == Current.UserId)
            {
                BlogEngineDB.Posts.Remove(post);
                BlogEngineDB.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SetPostCategories(int postId, int[] categoryIds)
        {
            var oldPosts = BlogEngineDB.PostCategories.Where(pc => pc.PostId == postId);
            BlogEngineDB.PostCategories.RemoveRange(oldPosts);

            foreach (int categoryId in categoryIds)
            {
                BlogEngineDB.PostCategories.Add(new PostCategory()
                {
                    PostId = postId,
                    CategoryId = categoryId
                });
            }

            BlogEngineDB.SaveChanges();
            return true;
        }

        public bool PublishPost(int postId)
        {
            var post = BlogEngineDB.Posts.FirstOrDefault(p => p.PostId == postId);
            post.IsPublished = true;
            post.ApprovedBy = Current.UserId;
            post.PublishedDate = DateTime.Now;
            BlogEngineDB.SaveChanges();

            return true;
        }
    }
}