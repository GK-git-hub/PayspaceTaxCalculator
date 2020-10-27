using System;
using Microsoft.Extensions.Configuration;
using RestAPI.Contracts.CalculateTax;
using Serilog;

namespace RestAPI.Services
{
    public class CalculateTaxService : ICalculateTaxService
    {
        
        private readonly IConfiguration _configuration;

        public CalculateTaxService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public SaveCalculationModel CalculateProgressive(float income, string postalCode)
        {
            Log.Information("Calculate progressive tax on : R {income} and postal code: {postalCode}", income , postalCode);
            
            // Tax Bracket percentage
            // Can be added to a table or config file so that it can be changed without changing the method
            const float taxBracketOne = 10;
            const float taxBracketTwo = 15;
            const float taxBracketThree = 25;
            const float taxBracketFour = 28;
            const float taxBracketFive = 33;
            const float taxBracketSix = 35;

            float taxAmount = 0;
            
            var taxBrackets = new[]
            {
                new {Lower = 0, Upper = 8350, Rate = taxBracketOne},
                new {Lower = 8351, Upper = 33950, Rate = taxBracketTwo},
                new {Lower = 33951, Upper = 82250, Rate = taxBracketThree},
                new {Lower = 82251, Upper = 171550, Rate = taxBracketFour},
                new {Lower = 171551, Upper = 372950, Rate = taxBracketFive},
                new {Lower = 372951, Upper = 9000000, Rate = taxBracketSix},
            };

            foreach (var bracket in taxBrackets)
            {
                if(income > bracket.Lower)
                {
                    var taxableAtThisRate = Math.Min(bracket.Upper - bracket.Lower, income - bracket.Lower);
                    var taxThisBand = (taxableAtThisRate * bracket.Rate) / 100;
                    taxAmount += taxThisBand;
                }
            }
            
            var calculationMessage = new SaveCalculationModel
            {
                Income = income,
                TaxAmount = taxAmount,
                PostalCode = postalCode,
                DateTimeOfCalculateCompleted = DateTime.Now,
            };

            return calculationMessage;
        }

        public SaveCalculationModel CalculateFlatRate(float income, string postalCode)
        {
            
            Log.Information("Calculate flat rate tax on : R {income} and postal code: {postalCode}", income , postalCode);
            
            
            const float taxPercentage = (float) 17.5;
            
            var taxValue = (income * taxPercentage / 100);
                
            var calculationMessage = new SaveCalculationModel
            {
                Income = income,
                TaxAmount = taxValue,
                PostalCode = postalCode,
                DateTimeOfCalculateCompleted = DateTime.Now,
            };
                
            return calculationMessage;
        }

        public SaveCalculationModel CalculateFlatValue(float income, string postalCode)
        {
            Log.Information("Calculate flat value tax on : R {income} and postal code: {postalCode}", income , postalCode);
            
            // Add to a config file or table so that it can be changed without changing the method
            const float flatValueMinIncome = 200000;
            const float flatValueTaxAmount = 10000;
            const float flatValueTaxPercentage = (float) 5;
            
            if (income > flatValueMinIncome)
            {
                var calculationMessage = new SaveCalculationModel
                {
                    Income = income,
                    TaxAmount = flatValueTaxAmount,
                    PostalCode = postalCode,
                    DateTimeOfCalculateCompleted = DateTime.Now,
                };

                return calculationMessage;
            }
            else
            {
                var taxValue = (income * flatValueTaxPercentage / 100);
                
                var calculationMessage = new SaveCalculationModel
                {
                    Income = income,
                    TaxAmount = taxValue,
                    PostalCode = postalCode,
                    DateTimeOfCalculateCompleted = DateTime.Now,
                };
                
                return calculationMessage;
            }
        }
    }
}