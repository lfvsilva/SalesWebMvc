using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models;

public class SalesRecord
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Date { get; set; }

    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Amount { get; set; }

    public SaleStatus Status { get; set; }

    // Propriedade de navegação: pode não estar carregada (sem Include), por isso é anulável.
    // A FK explícita (int, não anulável) mantém o relacionamento obrigatório no banco.
    public Seller? Seller { get; set; }
    public int SellerId { get; set; }

    public SalesRecord()
    {
    }

    public SalesRecord(DateTime date, double amount, SaleStatus status, Seller seller)
    {
        Date = date;
        Amount = amount;
        Status = status;
        Seller = seller;
    }
}
