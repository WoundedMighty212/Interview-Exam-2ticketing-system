// Controllers/BugController.cs
using Interview_Exam_2ticketing_system;
using Interview_Exam_2ticketing_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class BugController : Controller
{
    private readonly ApplicationDbContext _context;

    public BugController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Bug
    public IActionResult Index()
    {
        var bugs = _context.Bugs.ToList();
        return View(bugs);
    }

    // GET: Bug/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Bug/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Summary,Description")] Bug bug)
    {
        if (ModelState.IsValid)
        {
            bug.CreatedByUserId = 1; // Assuming user ID 1 for now, you can replace this with actual user authentication
            _context.Add(bug);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(bug);
    }

    // GET: Bug/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bug = _context.Bugs.Find(id);
        if (bug == null)
        {
            return NotFound();
        }
        return View(bug);
    }

    // POST: Bug/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("BugId,Summary,Description")] Bug bug)
    {
        if (id != bug.BugId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bug);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(bug.BugId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(bug);
    }

    // GET: Bug/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bug = _context.Bugs
            .FirstOrDefault(m => m.BugId == id);
        if (bug == null)
        {
            return NotFound();
        }

        return View(bug);
    }

    // POST: Bug/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var bug = _context.Bugs.Find(id);
        _context.Bugs.Remove(bug);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool BugExists(int id)
    {
        return _context.Bugs.Any(e => e.BugId == id);
    }

    // POST: Bug/Resolve/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Resolve(int id)
    {
        var bug = _context.Bugs.Find(id);
        if (bug == null)
        {
            return NotFound();
        }

        bug.Status = "Resolved";
        _context.Update(bug);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
