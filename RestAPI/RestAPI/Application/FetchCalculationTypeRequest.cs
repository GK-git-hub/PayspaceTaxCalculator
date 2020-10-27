using System.ComponentModel.DataAnnotations;
using MediatR;
using RestAPI.Contracts.CalculateTax;

namespace RestAPI.Application
{
    public class FetchCalculationTypeRequest : IRequest<FetchCalculationTypeResponse>
    {
        [Required]
        public string PostalCode { get; set; }
    }
}