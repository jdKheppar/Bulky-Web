namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategory Category { get; }
        IProduct Product {get;}
        void Save();
    }
}
