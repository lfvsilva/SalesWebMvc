using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers;

public class SellersController(SellerService sellerService, DepartmentService departmentService) : Controller
{
    // GET: Sellers
    public async Task<IActionResult> Index() => View(await sellerService.FindAllAsync());

    // GET: Sellers/Create
    public async Task<IActionResult> Create() => View(await BuildFormViewModelAsync());

    // POST: Sellers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormViewModelAsync(seller));
        }

        await sellerService.InsertAsync(seller);
        return RedirectToAction(nameof(Index));
    }

    // GET: Sellers/Delete/5
    public Task<IActionResult> Delete(int? id) => ShowSellerAsync(id);

    // POST: Sellers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e) when (e is IntegrityException or NotFoundException)
        {
            return RedirectToError(e.Message);
        }
    }

    // GET: Sellers/Details/5
    public Task<IActionResult> Details(int? id) => ShowSellerAsync(id);

    // GET: Sellers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return RedirectToError("Id not provided");
        }

        var seller = await sellerService.FindByIdAsync(id.Value);
        if (seller is null)
        {
            return RedirectToError("Id not found");
        }

        return View(await BuildFormViewModelAsync(seller));
    }

    // POST: Sellers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormViewModelAsync(seller));
        }
        if (id != seller.Id)
        {
            return RedirectToError("Id mismatch");
        }

        try
        {
            await sellerService.UpdateAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e) when (e is NotFoundException or DBConcurrencyException)
        {
            return RedirectToError(e.Message);
        }
    }

    public IActionResult Error(string? message) => View(new ErrorViewModel
    {
        Message = message,
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });

    private async Task<IActionResult> ShowSellerAsync(int? id)
    {
        if (id is null)
        {
            return RedirectToError("Id not provided");
        }

        var seller = await sellerService.FindByIdAsync(id.Value);
        return seller is null ? RedirectToError("Id not found") : View(seller);
    }

    private async Task<SellerFormViewModel> BuildFormViewModelAsync(Seller? seller = null) => new()
    {
        Seller = seller,
        Departments = await departmentService.FindAllAsync()
    };

    private RedirectToActionResult RedirectToError(string message) =>
        RedirectToAction(nameof(Error), new { message });
}
