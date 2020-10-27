using System.ComponentModel.DataAnnotations;
using MediatR;
using RestAPI.Contracts.CalculateTax;

namespace RestAPI.Application
{
    public class CalculateTaxRequest : IRequest<CalculateTaxResponse>
    {
        [Required]
        public float Income { get; set; }
        
        [Required]
        public string PostalCode { get; set; }
    }
}