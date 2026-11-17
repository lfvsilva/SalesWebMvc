using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services;

public class DepartmentService(SalesWebMvcContext context)
{
    public async Task<List<Department>> FindAllAsync() =>
        await context.Department.OrderBy(x => x.Name).ToListAsync();
}
