using Bulky.DataAcess;
using Bulky.Models;
using System.Linq.Expressions;
namespace Bulky.DataAccess.Repository.IRepository
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

       

        public void Update(Product obj)
        {
            _db.JProducts.Update(obj);
        }
    }
}
