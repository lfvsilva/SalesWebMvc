using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data;

public class SeedingService(SalesWebMvcContext context)
{
    public void Seed()
    {
        if (context.Department.Any() || context.Seller.Any() || context.SalesRecords.Any())
        {
            return; // DB has been seeded
        }

        Department d1 = new("Computers");
        Department d2 = new("Electronics");
        Department d3 = new("Fashion");
        Department d4 = new("Books");

        Seller s1 = new("Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
        Seller s2 = new("Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3500.0, d2);
        Seller s3 = new("Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 2200.0, d1);
        Seller s4 = new("Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, d4);
        Seller s5 = new("Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 4000.0, d3);
        Seller s6 = new("Alex Pink", "alexpink@gmail.com", new DateTime(1997, 3, 4), 3000.0, d2);

        SalesRecord[] records =
        [
            new(new DateTime(2018, 09, 25), 11000.0, SaleStatus.Billed, s1),
            new(new DateTime(2018, 09, 4), 7000.0, SaleStatus.Billed, s5),
            new(new DateTime(2018, 09, 13), 4000.0, SaleStatus.Canceled, s4),
            new(new DateTime(2018, 09, 1), 8000.0, SaleStatus.Billed, s1),
            new(new DateTime(2018, 09, 21), 3000.0, SaleStatus.Billed, s3),
            new(new DateTime(2018, 09, 15), 2000.0, SaleStatus.Billed, s1),
            new(new DateTime(2018, 09, 28), 13000.0, SaleStatus.Billed, s2),
            new(new DateTime(2018, 09, 11), 4000.0, SaleStatus.Billed, s4),
            new(new DateTime(2018, 09, 14), 11000.0, SaleStatus.Pending, s6),
            new(new DateTime(2018, 09, 7), 9000.0, SaleStatus.Billed, s6),
            new(new DateTime(2018, 09, 13), 6000.0, SaleStatus.Billed, s2),
            new(new DateTime(2018, 09, 25), 7000.0, SaleStatus.Pending, s3),
            new(new DateTime(2018, 09, 29), 10000.0, SaleStatus.Billed, s4),
            new(new DateTime(2018, 09, 4), 3000.0, SaleStatus.Billed, s5),
            new(new DateTime(2018, 09, 12), 4000.0, SaleStatus.Billed, s1),
            new(new DateTime(2018, 10, 5), 2000.0, SaleStatus.Billed, s4),
            new(new DateTime(2018, 10, 1), 12000.0, SaleStatus.Billed, s1),
            new(new DateTime(2018, 10, 24), 6000.0, SaleStatus.Billed, s3),
            new(new DateTime(2018, 10, 22), 8000.0, SaleStatus.Billed, s5),
            new(new DateTime(2018, 10, 15), 8000.0, SaleStatus.Billed, s6),
            new(new DateTime(2018, 10, 17), 9000.0, SaleStatus.Billed, s2),
            new(new DateTime(2018, 10, 24), 4000.0, SaleStatus.Billed, s4),
            new(new DateTime(2018, 10, 19), 11000.0, SaleStatus.Canceled, s2),
            new(new DateTime(2018, 10, 12), 8000.0, SaleStatus.Billed, s5),
            new(new DateTime(2018, 10, 31), 7000.0, SaleStatus.Billed, s3),
            new(new DateTime(2018, 10, 6), 5000.0, SaleStatus.Billed, s4),
            new(new DateTime(2018, 10, 13), 9000.0, SaleStatus.Pending, s1),
            new(new DateTime(2018, 10, 7), 4000.0, SaleStatus.Billed, s3),
            new(new DateTime(2018, 10, 23), 12000.0, SaleStatus.Billed, s5),
            new(new DateTime(2018, 10, 12), 5000.0, SaleStatus.Billed, s2),
        ];

        context.Department.AddRange(d1, d2, d3, d4);
        context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
        context.SalesRecords.AddRange(records);

        context.SaveChanges();
    }
}
