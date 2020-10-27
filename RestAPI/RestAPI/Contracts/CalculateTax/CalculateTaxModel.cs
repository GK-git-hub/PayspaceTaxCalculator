using System.ComponentModel.DataAnnotations;

namespace RestAPI.Contracts.CalculateTax
{
    public class CalculateTaxModel
    {
        [Required]
        public float Income { get; set; }
        
        [Required]
        public string PostalCode { get; set; }
    }
}