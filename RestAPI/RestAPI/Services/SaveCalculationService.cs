using System;
using RestAPI.Contracts.CalculateTax;
using RestAPI.DataAccess.QueryProvider;
using Dapper;

namespace RestAPI.Services
{
    public class SaveCalculationService : ISaveCalculationService
    {
                
        private readonly IDataQueryProvider _dataQueryProvider;

        public SaveCalculationService(IDataQueryProvider dataQueryProvider)
        {
            _dataQueryProvider = dataQueryProvider;
        }
        
        public void SaveCalculationValue(float income, float taxAmount, string postalCode, DateTime dateTimeOfCalculateCompleted,
            string calculationType)
        {
            _dataQueryProvider.Query(connection => connection.QueryFirstOrDefault<SaveCalculationModel>(
                "INSERT [dbo].[CompletedTaxCalculation] " +
                "([postalCode] ,[calculationType] ,[income] ,[dateTimeOfCalculation] ,[calculatedTaxAmount]) " +
                "VALUES (@PostalCode, @CalculationType, @Income, @DateTimeOfCalculation, @CalculatedTaxAmount);",
                new
                {
                    PostalCode = postalCode,
                    CalculationType = calculationType,
                    Income = income,
                    DateTimeOfCalculation = dateTimeOfCalculateCompleted,
                    CalculatedTaxAmount = taxAmount

                }));
        }
    }
}