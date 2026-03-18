using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementRepositoryLayer.Implementations;

namespace QuantityMeasurementApp.Tests.Repository
{
    [TestClass]
    public class QuantityMeasurementRepositoryTests
    {
        [TestMethod]
        public void Save_ShouldStoreOperationInRepository()
        {
            var repository = QuantityMeasurementCacheRepository.GetInstance();

            var entity = new QuantityMeasurementEntity
            {
                Operand1 = new QuantityModel<object>(10, LengthUnit.Inch),
                Operand2 = new QuantityModel<object>(5, LengthUnit.Inch),
                Operation = OperationType.Add,
                Result = 15,
                HasError = false
            };

            repository.Save(entity);

            var allOperations = repository.GetAll();

            Assert.IsTrue(allOperations.Count > 0);
        }

        [TestMethod]
        public void Repository_ShouldReturnSingletonInstance()
        {
            var repo1 = QuantityMeasurementCacheRepository.GetInstance();
            var repo2 = QuantityMeasurementCacheRepository.GetInstance();

            Assert.AreSame(repo1, repo2);
        }
    }
}