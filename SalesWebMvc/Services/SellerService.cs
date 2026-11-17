using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services;

public class SellerService(SalesWebMvcContext context)
{
    public async Task<List<Seller>> FindAllAsync() =>
        await context.Seller.Include(obj => obj.Department).ToListAsync();

    public async Task InsertAsync(Seller obj)
    {
        context.Add(obj);
        await context.SaveChangesAsync();
    }

    public async Task<Seller?> FindByIdAsync(int id) =>
        await context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

    public async Task RemoveAsync(int id)
    {
        var obj = await context.Seller.FindAsync(id)
            ?? throw new NotFoundException("Id not found");

        try
        {
            context.Seller.Remove(obj);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new IntegrityException("Can't delete seller: the database rejected the operation", e);
        }
    }

    public async Task UpdateAsync(Seller obj)
    {
        if (!await context.Seller.AnyAsync(x => x.Id == obj.Id))
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            context.Update(obj);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            // Converte a exceção do EF (camada de dados) na exceção da camada de serviço.
            throw new DBConcurrencyException(e.Message, e);
        }
    }
}
