using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI.Contracts.CalculateTax;
using RestAPI.Services;
using Serilog;

namespace RestAPI.Application
{
    public class FetchCalculationTypeHandler : IRequestHandler<FetchCalculationTypeRequest, FetchCalculationTypeResponse>
    {
        private readonly IFetchCalculateTypeService _fetchCalculateTypeService;


        public FetchCalculationTypeHandler(IFetchCalculateTypeService fetchCalculateTypeService)
        {
            _fetchCalculateTypeService = fetchCalculateTypeService;
        }

        public async Task<FetchCalculationTypeResponse> Handle(FetchCalculationTypeRequest request,
            CancellationToken cancellationToken)
        {
            return FetchCalculationType(request);

        }

        private FetchCalculationTypeResponse FetchCalculationType(FetchCalculationTypeRequest request)
        {
            FetchCalculationTypeResponse response = null;

            try
            {
                var calculationType = _fetchCalculateTypeService.FetchCalculationType(request.PostalCode);

                if (calculationType != null)
                {
                    response = new FetchCalculationTypeResponse
                    {
                        CalculationType = calculationType.CalculationType
                    };
                }
                else
                {
                    response = new FetchCalculationTypeResponse
                    {
                        CalculationType = "Unable to retrieve tax calculation type"
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Error("Failed to calculate Progressive, response : {exception}", exception);

                response = new FetchCalculationTypeResponse()
                {
                    CalculationType = "Unable to fetch your tax calculation type."
                };

                return response;
            }
            
            return response;
        }
    }
}