using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface ISeasonRepository{
        IQueryable<Season> Seasons {get;}

        
    }
}