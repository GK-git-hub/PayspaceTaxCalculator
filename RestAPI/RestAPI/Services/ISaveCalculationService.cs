using System;

namespace RestAPI.Services
{
    public interface ISaveCalculationService
    {
        void SaveCalculationValue(float income, float taxAmount, string postalCode, DateTime dateTimeOfCalculateCompleted, string CalculationType);
    }
}