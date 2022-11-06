using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMoviw.Models;
using MvcMoviw.Data;

namespace MvcMoviw.Controllers;

public class TodoController : Controller
{
    private readonly ApplicationDbContext _db;

    public TodoController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var todolist  = _db.todo.ToList();

        HttpContext.Session.SetString("name", "todo");
        return View(todolist);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPut]
    public async Task<IActionResult> Create(Todo todoData) //Should be same as the form, esle submit le yesma pathaudaina
    {
        if (ModelState.IsValid)
        {
            _db.todo.Add(todoData);
            await _db.SaveChangesAsync();

            return RedirectToAction("");
        }

        return View(todoData);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var todo = _db.todo.Find(id);

        if (todo == null)
        {
            return NotFound();
        }

        return View(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Todo todoData)
    {
        Console.WriteLine(todoData.id);
        if (ModelState.IsValid)
        {
            _db.todo.Update(todoData);
            await _db.SaveChangesAsync();

            return RedirectToAction("");
        }

        return View(todoData);
    }


    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine(id);
        Console.WriteLine("Delete");
        if(id == null || id == 0)
        {
            return NotFound();
        }

        var todo = _db.todo.Find(id);

        if (todo == null)
        {
            return NotFound();
        }

        _db.todo.Remove(todo);
        await _db.SaveChangesAsync();

        return RedirectToAction("");
    }


    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
