using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Example.Data;
using Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(
            ApplicationDbContext db,
            ILogger<ExpenseController> logger
        )
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Requested the Index Page");
            IEnumerable<Expense> objList = _db.Expenses;
            return View(objList);
        }

        //GET-Create
        public IActionResult Create()
        {
            _logger.LogInformation("Requested the Create New Page");
            return View();
        }

        //POST-Create
        [HttpPost]
        public IActionResult Create(Expense obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Expenses.Add (obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine (ex);
            }
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Delete-POST
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove (obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST- Update
        [HttpPost]
        public IActionResult Update(Expense obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update (obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
