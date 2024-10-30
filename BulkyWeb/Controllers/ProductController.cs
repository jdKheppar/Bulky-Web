
using Bulky.DataAcess;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BulkyWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var objProductList = _db.JProducts.ToList(); in this way var can get its type based on the result or we can tell it explicitly
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            //List<Product> objProductList = [.. _db.JProducts]; it is equal to the above expression, it's using the collection expression
            
            return View(objProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                   .GetAll().Select(u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.Id.ToString()
                   });
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
            // RedirectToAction("ViewName","ControllerName") but since we are in the same controller we can skip that
        }
        //Below function acts as a get function/request
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id==Id);
            
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        //Below function acts as a post function/request
        [HttpPost]
        public IActionResult Edit(Product obj)
            //If you click on the above Edit keyword in visual studio, you will get an option to add a new related to this Controller
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
            // RedirectToAction("ViewName","ControllerName") but since we are in the same controller we can skip that
        }
        
        //Delete View
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == Id); ;
            
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        //Delete API
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == Id); ;
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}



/*
 The LINQ query _db.JProducts.FirstOrDefault(u => u.Id == Id) is a concise and expressive way to search for a specific record in a database table using Entity Framework Core. It leverages the power of LINQ to filter and retrieve data efficiently.
FirstOrDefault: This method returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
u => u.Id == Id: This is a lambda expression used to specify the condition. Here, u is a parameter that represents each element in the collection, and u.Id == Id is the condition that the element must satisfy.
 */