using System.ComponentModel.DataAnnotations;

namespace RestAPI.Contracts.CalculateTax
{
    public class FetchCalculationTypeModel
    {
        [Required]
        public string PostalCode { get; set; }
    }
}