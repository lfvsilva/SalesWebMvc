
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

public class SellersController : Controller
{
    private readonly SalesWebMvcContext _context;

    public SellersController(SalesWebMvcContext context)
    {
        _context = context;
    }

    // GET: SELLERS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Seller.ToListAsync());
    }

    // GET: SELLERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seller = await _context.Seller
            .FirstOrDefaultAsync(m => m.Id == id);
        if (seller == null)
        {
            return NotFound();
        }

        return View(seller);
    }

    // GET: SELLERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SELLERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary,Department,Sales")] Seller seller)
    {
        if (ModelState.IsValid)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(seller);
    }

    // GET: SELLERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seller = await _context.Seller.FindAsync(id);
        if (seller == null)
        {
            return NotFound();
        }
        return View(seller);
    }

    // POST: SELLERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Email,BirthDate,BaseSalary,Department,Sales")] Seller seller)
    {
        if (id != seller.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(seller.Id))
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
        return View(seller);
    }

    // GET: SELLERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seller = await _context.Seller
            .FirstOrDefaultAsync(m => m.Id == id);
        if (seller == null)
        {
            return NotFound();
        }

        return View(seller);
    }

    // POST: SELLERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var seller = await _context.Seller.FindAsync(id);
        if (seller != null)
        {
            _context.Seller.Remove(seller);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SellerExists(int? id)
    {
        return _context.Seller.Any(e => e.Id == id);
    }
}
