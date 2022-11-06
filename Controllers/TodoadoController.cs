using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using MvcMoviw.Models;

namespace MvcMoviw.Controllers;

public class TodoadoController : Controller
{
    private readonly string connStr =
        "server=localhost;user=anwesh;password=root;database=dnet;port=3306;";

    public IActionResult Index()
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "SELECT * FROM todo";

        MySqlCommand cmd = new MySqlCommand(query, conn);

        List<Todo> todolist = new List<Todo>();

        conn.Open();

        MySqlDataReader rdr = cmd.ExecuteReader();

        // Console.WriteLine(rdr.Read());

        while (rdr.Read())
        {
            //     Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            // }
            Todo todo = new Todo();
            todo.id = Convert.ToInt32(rdr["id"]);
            todo.title = rdr["title"].ToString();
            todo.description = rdr["description"].ToString();

            todolist.Add(todo);
        }
        conn.Close();

        HttpContext.Session.SetString("name", "TodoADo");
        TempData["mytemp"] = "mero temo data from TodoAdo";

        return View(todolist);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Todo todoData)
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "INSERT INTO todo (title, description) VALUES (@title, @description)";

        MySqlCommand cmd = new MySqlCommand(query, conn);

        // SQL Injection bata bachauna
        cmd.Parameters.AddWithValue("@title", todoData.title);
        cmd.Parameters.AddWithValue("@description", todoData.description);

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();

        return RedirectToAction("Index");
    }
}
