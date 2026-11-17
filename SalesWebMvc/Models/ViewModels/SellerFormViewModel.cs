namespace SalesWebMvc.Models.ViewModels;

public class SellerFormViewModel
{
    public Seller? Seller { get; init; }
    public IReadOnlyCollection<Department> Departments { get; init; } = [];
}
