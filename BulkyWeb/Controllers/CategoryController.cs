
using Bulky.DataAcess;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var objCategoryList = _db.JCategories.ToList(); in this way var can get its type based on the result or we can tell it explicitly
            List<Category> objCategoryList = _db.JCategories.ToList();
            //List<Category> objCategoryList = [.. _db.JCategories]; it is equal to the above expression, it's using the collection expression

            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            _db.JCategories.Add(obj);
            _db.SaveChanges();
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
            Category? categoryFromDb = _db.JCategories.Find(Id);
            //The below code will use linked operation
            //FirstOrDefault will work even if the attribute passed like in our case Id
            // is not the primary
            //Category? categoryFromDb1 = _db.JCategories.FirstOrDefault(u=>u.Id==Id);
            //Category categoryFromDb2 = _db.JCategories.Where(u=>u.Id==Id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Below function acts as a post function/request
        [HttpPost]
        public IActionResult Edit(Category obj)
            //If you click on the above Edit keyword in visual studio, you will get an option to add a new related to this Controller
        {
            _db.JCategories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            // RedirectToAction("ViewName","ControllerName") but since we are in the same controller we can skip that
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.JCategories.Find(Id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _db.JCategories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
