using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Services.Tools
{
    public class ProductTools
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductTools(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<object> GetProductsByCategory(string categoryName)
        {
            var category = _unitOfWork.Category
                .Get(c => c.Name.ToLower() == categoryName.ToLower());

            if (category == null)
            {
                return new List<object>();
            }

            var products = _unitOfWork.Product
                .GetAll()
                .Where(p => p.CategoryId == category.Id)
                .Select(p => new
                {
                    p.Title,
                    p.Author,
                    p.ListPrice,
                    p.Price,
                    p.Price50,
                    p.Price100
                })
                .ToList();

            return products.Cast<object>().ToList();
        }
    }
}