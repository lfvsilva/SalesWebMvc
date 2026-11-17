namespace SalesWebMvc.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Seller> Sellers { get; set; } = [];

    public Department()
    {
    }

    public Department(string name)
    {
        Name = name;
    }

    public void AddSeller(Seller seller) => Sellers.Add(seller);

    public double TotalSales(DateTime initial, DateTime final) =>
        Sellers.Sum(seller => seller.TotalSales(initial, final));
}
