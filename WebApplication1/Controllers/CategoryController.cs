using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        #region DependencyInjection
        private readonly NorthwindContext _db;

        public CategoryController(NorthwindContext db)
        {
            _db = db;
        } 
        #endregion

        public IActionResult Index()
        {
           return View(_db.Categories.ToList());
        }

        public IActionResult GetAll()
        {
            return Json(new { data = _db.Categories.Select(c=> new {c.CategoryId,c.Description,c.CategoryName}).ToList() });
        }


       
        public IActionResult Create()
        {
            
            return View();
        }

        //Data Annotation
        [HttpPost]
        public IActionResult Create(Category category)
        {



            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int categoryId)
        {
           Category category= _db.Categories.Find(categoryId);

            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
            }
            TempData["success"] = "Başarıyla düzenlendi";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int categoryId)
        {
            Category category = _db.Categories.Find(categoryId);
            _db.Categories.Remove(category);
            _db.SaveChanges();

            TempData["success"] = "Silme işlemi başarıyla gerçekleşmiştir !!!";

            return RedirectToAction("Index");
        }




       

    }
}
