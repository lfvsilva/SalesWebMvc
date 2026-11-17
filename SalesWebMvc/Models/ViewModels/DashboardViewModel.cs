namespace SalesWebMvc.Models.ViewModels;

public class DashboardViewModel
{
    public int DepartmentCount { get; init; }
    public int SellerCount { get; init; }
    public int SalesCount { get; init; }
    public double TotalBilled { get; init; }
    public IReadOnlyList<SalesRecord> RecentSales { get; init; } = [];
}
