using RestAPI.DataAccess.DataTransferObjects.TaxCalculation;

namespace RestAPI.Services
{
    public interface IFetchCalculateTypeService
    {
        FetchCalculationTypeDto FetchCalculationType(string postalCode);
    }
}