using Moq;
using NUnit.Framework;
using RestAPI.DataAccess.DataTransferObjects.TaxCalculation;
using RestAPI.Services;

namespace RestAPITests
{
    [TestFixture]
    public class FetchCalculationTypeTests
    {
        Mock<IFetchCalculateTypeService> mockFetchCalculateTypeService;
        FetchCalc testFetchCaluclationServiceUnderTest;

        [TestCase("1000" , "Progressive")]
        [TestCase("7441" , "Progressive")]
        [TestCase("A100" , "Flat Value")]
        [TestCase("7000" , "Flat rate")]

        [Test]
        public void FetchCalculationTypes(string postalCode, string calculationType)
        {
            mockFetchCalculateTypeService = new Mock<IFetchCalculateTypeService>(MockBehavior.Strict);
            mockFetchCalculateTypeService.Setup(p => p.FetchCalculationType(postalCode)).Returns(new FetchCalculationTypeDto{CalculationType = calculationType});
            
            testFetchCaluclationServiceUnderTest = new FetchCalc(mockFetchCalculateTypeService.Object);
            var result = testFetchCaluclationServiceUnderTest.GetCalculationType(postalCode);
            var calculation = result.CalculationType;
            
            Assert.That(calculation, Is.EqualTo(calculationType));
            
            mockFetchCalculateTypeService.VerifyAll();
        }
    }
    
    public class FetchCalc
    {
        private IFetchCalculateTypeService fetchCalculateTypeService;

        public FetchCalc(IFetchCalculateTypeService fetchCalculateTypeService)
        {
            this.fetchCalculateTypeService = fetchCalculateTypeService;
        }

        public FetchCalculationTypeDto GetCalculationType(string postalCode)
        {
            return fetchCalculateTypeService.FetchCalculationType(postalCode);
        }
    }
}