using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services;

public class SalesRecordService(SalesWebMvcContext context)
{
    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) =>
        await QueryByDate(minDate, maxDate).ToListAsync();

    public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
    {
        // O EF Core não traduz para SQL um GroupBy cuja chave é uma entidade,
        // então a consulta é executada no banco e o agrupamento é feito em memória.
        var records = await QueryByDate(minDate, maxDate).ToListAsync();

        return records
            .GroupBy(x => x.Seller!.Department!)
            .ToList();
    }

    private IQueryable<SalesRecord> QueryByDate(DateTime? minDate, DateTime? maxDate)
    {
        IQueryable<SalesRecord> result = context.SalesRecords;

        if (minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value);
        }

        return result
            .Include(x => x.Seller)
            .ThenInclude(s => s!.Department)
            .OrderByDescending(x => x.Date);
    }
}
