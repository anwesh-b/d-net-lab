using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using MvcMoviw.Models;

namespace MvcMoviw.Controllers;

public class PhonebookController : Controller
{
    private readonly string connStr =
        "server=localhost;user=anwesh;password=root;database=dnet;port=3306;";

    public IActionResult Index()
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "SELECT * FROM phonebook";

        MySqlCommand cmd = new MySqlCommand(query, conn);

        List<Phonebook> phoneBookList = new List<Phonebook>();

        conn.Open();

        MySqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Phonebook pBookData = new Phonebook();
            pBookData.id = Convert.ToInt32(rdr["id"]);
            pBookData.name = rdr["name"].ToString();
            pBookData.number = rdr["number"].ToString();

            phoneBookList.Add(pBookData);
        }
        conn.Close();

        Console.WriteLine("Hello World!");

        return View(phoneBookList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Phonebook toAddData)
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "INSERT INTO phonebook (name, number) VALUES (@name, @number)";

        MySqlCommand cmd = new MySqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@name", toAddData.name);
        cmd.Parameters.AddWithValue("@number", toAddData.number);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "SELECT * FROM phonebook WHERE ID=@id";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        MySqlDataReader rdr = cmd.ExecuteReader();

        Phonebook pBookData = new Phonebook();
        while (rdr.Read())
        {
            pBookData.id = Convert.ToInt32(rdr["id"]);
            pBookData.name = rdr["name"].ToString();
            pBookData.number = rdr["number"].ToString();
        }
        conn.Close();

        if (pBookData == null)
        {
            return NotFound();
        }

        return View(pBookData);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Phonebook updatedPhonebook)
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "UPDATE phonebook SET name=@name, number=@number WHERE id=@id";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", updatedPhonebook.id);
        cmd.Parameters.AddWithValue("@name", updatedPhonebook.name);
        cmd.Parameters.AddWithValue("@number", updatedPhonebook.number);

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        var query = "DELETE FROM phonebook WHERE id=@id";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();

        return RedirectToAction("Index");
    }
}
