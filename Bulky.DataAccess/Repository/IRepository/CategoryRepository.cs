using Bulky.DataAcess;
using Bulky.Models;
using System.Linq.Expressions;
namespace Bulky.DataAccess.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

       

        public void Update(Category obj)
        {
            _db.JCategories.Update(obj);
        }
    }
}
