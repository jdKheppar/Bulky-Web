using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category 
        IEnumerable<T> GetAll();
        //Below is the template for receiving a linq query expression
        // like u => u.id = id in our controller
        T Get(Expression<Func<T, bool>> filter);//Gor getting a single item
        void Add(T entity);
        //void Update(T entity); it's complex operation so we are not writing it here
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        //saveChanges() it's also a complex operation which needs a separate repository etc
    }
}
