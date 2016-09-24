using MVCBlogEngine.Common;
using MVCBlogEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBlogEngine.Controllers
{
    public class PostCommentsController : BaseController
    {
        public enum CommentUserType
        {
            Guest,
            Registered
        }

        public ActionResult CreateComment(int postId, CommentUserType userType, string comment, string username)
        {
            PostComment pc = new DB.PostComment();
            pc.PostId = postId;
            pc.Comment = comment;
            pc.Source = userType.ToString();
            pc.Username = username;
            pc.Created = DateTime.Now;
            pc.CreatedBy = Current.UserId;
            pc.IsPublished = false;

            BlogEngineDB.PostComments.Add(pc);
            BlogEngineDB.SaveChanges();

            return View(); 
        }

        public List<PostComment> GetPostComments(int postId)
        {
            var postComments = BlogEngineDB.PostComments.Where(c => c.PostId == postId).ToList();
            return postComments;
        }

        public bool UpdateComment(PostComment comment)
        {
            var dbComment = BlogEngineDB.PostComments.FirstOrDefault(c => c.CommentId == comment.CommentId);
            dbComment.Comment = comment.Comment;
            dbComment.Username = comment.Username;
            dbComment.UpdatedBy = Current.UserId;
            dbComment.UpdatedDate = DateTime.Now;

            BlogEngineDB.SaveChanges();
            return true;
        }

        public bool PublishComment(int commentId)
        {
            var dbComment = BlogEngineDB.PostComments.FirstOrDefault(c => c.CommentId == commentId);
            dbComment.IsPublished = true;
            dbComment.ApprovedBy = Current.UserId;
            dbComment.PublishedDate = DateTime.Now;

            BlogEngineDB.SaveChanges();
            return true;
        }

        public bool RemoveComment(int commentId)
        {
            var dbComment = BlogEngineDB.PostComments.FirstOrDefault(c => c.CommentId == commentId);

            if (dbComment.CreatedBy == Current.UserId)
            {
                BlogEngineDB.PostComments.Remove(dbComment);
                BlogEngineDB.SaveChanges();
                return true;
            }

            return false;
        }
    }
}