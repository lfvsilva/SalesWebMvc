
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

public class SellersController : Controller
{
    //private readonly SalesWebMvcContext _sellerService;
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentService;

    public SellersController(SellerService sellerService, DepartmentService departmentService)
    {
        _sellerService = sellerService;
        _departmentService = departmentService;
    }

    // GET: SELLERS
    public async Task<IActionResult> Index()
    {
        return View(_sellerService.FindAll());
    }

    // GET: SELLERS/Create
    public IActionResult Create()
    {
        var departments = _departmentService.FindAll();
        var viewModel = new SellerFormViewModel { Departments = departments };
        return View(viewModel);
    }

    // POST: SELLERS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
        if (!ModelState.IsValid)
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }
        _sellerService.Insert(seller);
        return RedirectToAction(nameof(Index));
    }
    // GET: SELLERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var seller = _sellerService.FindById(id.Value);
        if (seller == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }

        return View(seller);
    }
    // POST: SELLERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var seller = _sellerService.FindById(id.Value);
        if (seller != null)
        {
            _sellerService.Remove(id.Value);
        }
        return RedirectToAction(nameof(Index));
    }
    // GET: SELLERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var seller = _sellerService.FindById(id.Value);
        if (seller == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
        }

        return View(seller);
    }// GET: SELLERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        var seller = _sellerService.FindById(id.Value);
        if (seller == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }
        List<Department> departments = _departmentService.FindAll();
        SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        return View(viewModel);
    }

    // POST: SELLERS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Seller seller)
    {
        if (!ModelState.IsValid)
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }
        if (id != seller.Id)
        {
            return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
        }
        try
        {
            _sellerService.Update(seller);
        }
        catch (NotFoundException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }
        catch (DBConcurrencyException e)
        {
            if (!(_sellerService.FindById(seller.Id) is null))
            {
                RedirectToAction(nameof(Error), new { message = e.Message });
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    //Error
    public IActionResult Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }
}
