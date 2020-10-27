using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Application;
using RestAPI.Contracts.CalculateTax;

namespace RestAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/")]
    [ApiController]
    public class FetchTaxCalculationController : Controller
    {
        private readonly IMediator _mediator;

        public FetchTaxCalculationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("fetch-tax-calculation-type")]
        public async Task<ActionResult<FetchCalculationTypeResponse>> FetchCalculationType([FromBody] FetchCalculationTypeModel model)
        {
            var request = new FetchCalculationTypeRequest
            {
                PostalCode = model.PostalCode
            };

            return await _mediator.Send(request);
        }
    }
}