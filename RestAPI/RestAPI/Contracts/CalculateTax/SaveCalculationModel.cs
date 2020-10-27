using System;

namespace RestAPI.Contracts.CalculateTax
{
    public class SaveCalculationModel
    {
        public float Income { get; set; }
        public float TaxAmount { get; set; }
        public DateTime DateTimeOfCalculateCompleted { get; set; }
        public string PostalCode { get; set; }
        
        public string CalculationType { get; set; }
    }
}