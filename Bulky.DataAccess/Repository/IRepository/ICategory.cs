using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface ICategory: IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}
