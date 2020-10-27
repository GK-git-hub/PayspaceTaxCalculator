using RestAPI.Contracts.CalculateTax;

namespace RestAPI.Services
{
    public interface ICalculateTaxService
    {
        SaveCalculationModel CalculateProgressive(float income, string postalCode);

        SaveCalculationModel CalculateFlatRate(float income, string postalCode);

        SaveCalculationModel CalculateFlatValue(float income, string postalCode);

    }
}