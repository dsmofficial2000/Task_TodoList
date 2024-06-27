using Microsoft.AspNetCore.Mvc;
using Task_TodoList.Data;
using Task_TodoList.Models;

namespace Task_TodoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the list of to-do items
        public IActionResult Index()
        {
            return View(_context.ToDos.ToList());
        }

        // Displays the form to create a new to-do item
        public IActionResult Create()
        {
            return View();
        }

        // Handles the HTTP POST request to create a new to-do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo todo)
        {
            if (_context.ToDos.Any(t => t.Email == todo.Email))
            {
                ModelState.AddModelError("Email", "Duplicate email is not accepted.");
            }

            if (ModelState.IsValid)
            {
                _context.ToDos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

 

        // Displays the form to edit a specific to-do item
        public IActionResult Edit(int id)
        {
            var toDo = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // Handles the HTTP POST request to edit a specific to-do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (_context.ToDos.Any(t => t.Email == toDo.Email && t.Id != id))
            {
                ModelState.AddModelError("Email", "Duplicate email is not accepted.");
            }

            if (ModelState.IsValid)
            {
                _context.ToDos.Update(toDo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

        // Displays a confirmation page for deleting a specific to-do item
        public IActionResult Delete(int id)
        {
            var toDo = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // Handles the HTTP POST request to delete a specific to-do item
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var toDo = _context.ToDos.FirstOrDefault(x => x.Id == id);
            if (toDo != null)
            {
                _context.ToDos.Remove(toDo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
