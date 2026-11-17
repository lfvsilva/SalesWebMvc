using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers;

public class DepartmentsController(SalesWebMvcContext context) : Controller
{
    // GET: Departments
    public async Task<IActionResult> Index() => View(await context.Department.ToListAsync());

    // GET: Departments/Details/5
    public Task<IActionResult> Details(int? id) => ShowDepartmentAsync(id);

    // GET: Departments/Create
    public IActionResult Create() => View();

    // POST: Departments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Department department)
    {
        if (!ModelState.IsValid)
        {
            return View(department);
        }

        context.Add(department);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var department = await context.Department.FindAsync(id);
        return department is null ? NotFound() : View(department);
    }

    // POST: Departments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(department);
        }

        try
        {
            context.Update(department);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await DepartmentExistsAsync(department.Id))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Departments/Delete/5
    public Task<IActionResult> Delete(int? id) => ShowDepartmentAsync(id);

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var department = await context.Department.FindAsync(id);
        if (department is not null)
        {
            context.Department.Remove(department);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> ShowDepartmentAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var department = await context.Department.FirstOrDefaultAsync(m => m.Id == id);
        return department is null ? NotFound() : View(department);
    }

    private Task<bool> DepartmentExistsAsync(int id) => context.Department.AnyAsync(e => e.Id == id);
}
