using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers;

public class SalesRecordsController(SalesRecordService salesRecordService) : Controller
{
    public IActionResult Index() => View();

    public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
        var (min, max) = ApplyDefaultDates(minDate, maxDate);
        return View(await salesRecordService.FindByDateAsync(min, max));
    }

    public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
    {
        var (min, max) = ApplyDefaultDates(minDate, maxDate);
        return View(await salesRecordService.FindByDateGroupingAsync(min, max));
    }

    // Padrão: de 1º de janeiro do ano atual até hoje. Os valores também vão para a view preencher os filtros.
    private (DateTime Min, DateTime Max) ApplyDefaultDates(DateTime? minDate, DateTime? maxDate)
    {
        var min = minDate ?? new DateTime(DateTime.Now.Year, 1, 1);
        var max = maxDate ?? DateTime.Now;

        ViewData["minDate"] = min.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = max.ToString("yyyy-MM-dd");

        return (min, max);
    }
}
