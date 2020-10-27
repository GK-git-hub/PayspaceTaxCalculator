using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI.Contants;
using RestAPI.Contracts.CalculateTax;
using RestAPI.Services;
using Serilog;

namespace RestAPI.Application
{
    public class CalculateTaxHandler : IRequestHandler<CalculateTaxRequest, CalculateTaxResponse>
    {
        private readonly IFetchCalculateTypeService _fetchCalculateTypeService;
        private readonly ICalculateTaxService _calculateTaxService;
        private readonly ISaveCalculationService _saveCalculationService;
        
        public CalculateTaxHandler(IFetchCalculateTypeService fetchCalculateTypeService, 
            ICalculateTaxService calculateTaxService, ISaveCalculationService saveCalculationService)
        {
            _fetchCalculateTypeService = fetchCalculateTypeService;
            _calculateTaxService = calculateTaxService;
            _saveCalculationService = saveCalculationService;
        }

        public async Task<CalculateTaxResponse> Handle(CalculateTaxRequest request, CancellationToken cancellationToken)
        {
            return CalculateTax(request);
        }

        private CalculateTaxResponse CalculateTax(CalculateTaxRequest request)
        {
            CalculateTaxResponse response = null;

            var getCalculationType = _fetchCalculateTypeService.FetchCalculationType(request.PostalCode);
            
            if (getCalculationType != null)
            {
                var calculationType = getCalculationType.CalculationType;
                
                switch (calculationType)
                {
                    case CalculationTypes.Progressive:
                        CalculateProgressive();
                        break;
                    case CalculationTypes.FlatRate:
                        CalculateFlatRate();
                        break;
                    case CalculationTypes.FlatValue:
                        CalculateFlatValue();
                        break;
                }
            }
            else
            {
                response = new CalculateTaxResponse()
                {
                    Message = "Unable to retrieve tax calculation type"
                };

                return response;
            }

            CalculateTaxResponse CalculateProgressive()
            {
                try
                {
                    var calculate = _calculateTaxService.CalculateProgressive(request.Income , request.PostalCode);
                        
                    _saveCalculationService.SaveCalculationValue(calculate.Income, calculate.TaxAmount, 
                        calculate.PostalCode, calculate.DateTimeOfCalculateCompleted, getCalculationType.CalculationType);

                    response = new CalculateTaxResponse()
                    {
                        Message = $"Success your tax amount is R{calculate.TaxAmount}"
                    };

                    return response;
                }
                catch (Exception exception)
                {
                    Log.Error("Failed to calculate Progressive, response : {exception}", exception);

                    response = new CalculateTaxResponse()
                    {
                        Message = "Unable to calculate your tax at this time."
                    };

                    return response;
                }
            }

            CalculateTaxResponse CalculateFlatRate()
            {
                try
                {
                    var calculate = _calculateTaxService.CalculateFlatRate(request.Income , request.PostalCode);
                        
                    _saveCalculationService.SaveCalculationValue(calculate.Income, calculate.TaxAmount, 
                        calculate.PostalCode, calculate.DateTimeOfCalculateCompleted, getCalculationType.CalculationType);

                    response = new CalculateTaxResponse()
                    {
                        Message = $"Success your tax amount is R{calculate.TaxAmount}"
                    };

                    return response;
                }
                catch (Exception exception)
                {
                    Log.Error("Failed to calculate Flat Rate, response : {exception}", exception);

                    response = new CalculateTaxResponse()
                    {
                        Message = "Unable to calculate your tax at this time."
                    };

                    return response;
                }
            }
            
            CalculateTaxResponse CalculateFlatValue()
            {
                try
                {
                    var calculate = _calculateTaxService.CalculateFlatValue(request.Income , request.PostalCode);
                        
                    _saveCalculationService.SaveCalculationValue(calculate.Income, calculate.TaxAmount, 
                        calculate.PostalCode, calculate.DateTimeOfCalculateCompleted, getCalculationType.CalculationType);

                    response = new CalculateTaxResponse()
                    {
                        Message = $"Success your tax amount is R{calculate.TaxAmount}"
                    };

                    return response;
                }
                catch (Exception exception)
                {
                    Log.Error("Failed to calculate Flat Value, response : {exception}", exception);

                    response = new CalculateTaxResponse()
                    {
                        Message = "Unable to calculate your tax at this time."
                    };

                    return response;
                }
            }

            return response;
        }
    }
}