using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models;

public class Seller
{
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} required")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "{0} required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Birth Date")]
    [Required(ErrorMessage = "{0} required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Base Salary")]
    [Required(ErrorMessage = "{0} required")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
    public double BaseSalary { get; set; }

    // Anulável de propósito: com <Nullable>enable</Nullable>, uma navegação não anulável
    // vira [Required] implícito no MVC, e o formulário só envia DepartmentId.
    public Department? Department { get; set; }

    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    public ICollection<SalesRecord> Sales { get; set; } = [];

    public Seller()
    {
    }

    public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        BaseSalary = baseSalary;
        Department = department;
    }

    public void AddSales(SalesRecord sr) => Sales.Add(sr);

    public void RemoveSales(SalesRecord sr) => Sales.Remove(sr);

    public double TotalSales(DateTime initial, DateTime final) =>
        Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
}
