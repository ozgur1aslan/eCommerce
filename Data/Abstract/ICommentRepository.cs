using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface ICommentRepository{
        
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
    }
}