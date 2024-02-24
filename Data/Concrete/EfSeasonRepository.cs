using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;

namespace eCommerce.Data.Concrete
{
    public class EfSeasonRepository : ISeasonRepository
    {
        private eCommerceContext _context;
        public EfSeasonRepository(eCommerceContext context){
            _context = context;
        }
        public IQueryable<Season> Seasons => _context.Seasons;

        
    }
}