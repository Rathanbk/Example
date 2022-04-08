using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Example.Models;
using Example.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;


namespace Example.Controllers;

public class ItemController : Controller
{
        //to get the database in controller
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ItemController> _logger;
    public ItemController(ApplicationDbContext db, ILogger<ItemController> logger)
    {
        _db=db;
        _logger=logger;
    }
    

    public IActionResult Index()
    {
        IEnumerable<Item> objList= null;
        try
        {
            _logger.LogInformation("this is a log");
            
           objList = _db.Items; 
          
        }
      
        catch (System.Exception ex)
        {
            
            Console.WriteLine(ex);
        }
          return View(objList);
         
        
    }
     public IActionResult Create()
    {   
        return View();
    }
    [HttpPost]
    public IActionResult Create(Item obj)
    {
        try
        {
            if(obj.Borrower.Length<2)
            {
                throw new Exception("Enter proper input");
            }

        _db.Items.Add(obj);
        _db.SaveChanges();
       
        }
        catch (System.Exception ex)
        {
            //Logger.Error(ex.Message);
            _logger.LogError(ex.Message);
            return View("Error");
        }
         return RedirectToAction("Index");
    }
    
}