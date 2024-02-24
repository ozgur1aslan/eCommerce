using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface ITagRepository{
        IQueryable<Tag> Tags {get;}

        void CreateTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(Tag tag);
    }
}