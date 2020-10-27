using Dapper;
using RestAPI.DataAccess.DataTransferObjects.TaxCalculation;
using RestAPI.DataAccess.QueryProvider;
using Serilog;

namespace RestAPI.Services
{
    public class FetchCalculateTypeService : IFetchCalculateTypeService
    {
        private readonly IDataQueryProvider _dataQueryProvider;

        public FetchCalculateTypeService(IDataQueryProvider dataQueryProvider)
        {
            _dataQueryProvider = dataQueryProvider;
        }
        public FetchCalculationTypeDto FetchCalculationType(string postalCode)
        {
            Log.Information("Fetch Calculation Type");
            
            var calculationType = _dataQueryProvider
                .Query(connection =>
                connection.QueryFirstOrDefault<FetchCalculationTypeDto>(
                    "SELECT TOP 1 calculationType " +
                    "From dbo.TaxBrackets " +
                    "WHERE postalCode = @PostalCode",
                    new
                    {
                        PostalCode = postalCode
                    }));
            
            return calculationType;
        }
    }
}