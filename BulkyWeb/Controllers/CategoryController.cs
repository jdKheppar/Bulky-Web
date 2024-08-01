﻿
using Bulky.DataAcess;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepo;
        public CategoryController(ICategory db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            //var objCategoryList = _db.JCategories.ToList(); in this way var can get its type based on the result or we can tell it explicitly
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
            _categoryRepo.Add(obj);
            _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==Id);
            
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
            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            return RedirectToAction("Index");
            // RedirectToAction("ViewName","ControllerName") but since we are in the same controller we can skip that
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == Id); ;
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == Id); ;
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            return RedirectToAction("Index");
        }
    }
}



/*
 The LINQ query _db.JCategories.FirstOrDefault(u => u.Id == Id) is a concise and expressive way to search for a specific record in a database table using Entity Framework Core. It leverages the power of LINQ to filter and retrieve data efficiently.
FirstOrDefault: This method returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
u => u.Id == Id: This is a lambda expression used to specify the condition. Here, u is a parameter that represents each element in the collection, and u.Id == Id is the condition that the element must satisfy.
 */