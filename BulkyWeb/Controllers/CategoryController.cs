using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
