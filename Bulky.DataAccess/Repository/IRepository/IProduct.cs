using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProduct: IRepository<Product>
    {
        void Update(Product obj);
    }
}



