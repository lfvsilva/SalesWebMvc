using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models.Enums;
using SalesWebMvc.Models.ViewModels;
using System.Diagnostics;

namespace SalesWebMvc.Controllers;

public class HomeController(SalesWebMvcContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = new DashboardViewModel
        {
            DepartmentCount = await context.Department.CountAsync(),
            SellerCount = await context.Seller.CountAsync(),
            SalesCount = await context.SalesRecords.CountAsync(),
            TotalBilled = await context.SalesRecords
                .Where(x => x.Status == SaleStatus.Billed)
                .SumAsync(x => (double?)x.Amount) ?? 0,
            RecentSales = await context.SalesRecords
                .AsNoTracking()
                .Include(x => x.Seller)
                .ThenInclude(s => s!.Department)
                .OrderByDescending(x => x.Date)
                .Take(5)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
