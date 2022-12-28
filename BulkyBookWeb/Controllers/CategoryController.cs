using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Composition;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{

    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET 
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "O campo DisplayOrder não pode ser o mesmo que o Nome.");
        }
        if (ModelState.IsValid) 
        { 
        _db.Categories.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Categoria inserida com sucesso!";
        return RedirectToAction("Index");
        }
        return View(obj);
    }

    // EDIT
    public IActionResult Edit(int? id)
    {
        if(id==null || id==0)
        {
            return NotFound();
        }
        var categoryFromDB = _db.Categories.Find(id);
       // var categoryFromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
       // var categoryFromDBSecond = _db.Categories.SingleOrDefault(u => u.Id == id);

        if(categoryFromDB == null)
        {
            return NotFound();
        }
        return View(categoryFromDB);
    }

    // EDIT POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "O campo DisplayOrder não pode ser o mesmo que o Nome.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Categoria alterada com sucesso!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //DELETE
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDB = _db.Categories.Find(id);
        // var categoryFromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
        // var categoryFromDBSecond = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (categoryFromDB == null)
        {
            return NotFound();
        }
        return View(categoryFromDB);
    }

    //DELETE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Categories.Find(id);
        if (id == null || id == 0)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Categoria excluída com sucesso!";
        return RedirectToAction("Index");
    }
}
