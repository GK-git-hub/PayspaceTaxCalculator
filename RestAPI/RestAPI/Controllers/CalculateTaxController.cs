using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Application;
using RestAPI.Contracts.CalculateTax;

namespace RestAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/")]
    [ApiController]
    public class CalculateTaxController : Controller
    {
        private readonly IMediator _mediator;

        public CalculateTaxController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [Route("calculate-tax")]
        public async Task<ActionResult<CalculateTaxResponse>> CalculateTax([FromBody] CalculateTaxModel model)
        {
            var request = new CalculateTaxRequest
            {
                Income = model.Income,
                PostalCode = model.PostalCode
            };

            return await _mediator.Send(request);
        }
    }
}