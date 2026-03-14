using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementRepositoryLayer.Implementations;

namespace QuantityMeasurementApp.Tests.Repository
{
    /// <summary>
    /// Unit tests for QuantityMeasurementCacheRepository validating data persistence and Singleton pattern.
    /// Tests repository save/retrieve operations and verifies Singleton instance uniqueness.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementRepositoryTests
    {
        /// <summary>
        /// TEST: Repository should store operation entity in cache and retrieve it.
        /// SCENARIO: Save operation (10 Inch + 5 Inch = 15 Inch)
        /// EXPECTED: GetAll() returns non-empty list containing saved entity
        /// PURPOSE: Verify operation persistence in in-memory cache
        /// </summary>
        [TestMethod]
        public void Save_ShouldStoreOperationInRepository()
        {
            // Arrange: Get singleton repository instance
            var repository = QuantityMeasurementCacheRepository.GetInstance();

            // Create operation entity to persist
            var entity = new QuantityMeasurementEntity
            {
                Operand1 = new QuantityModel<object>(10, LengthUnit.Inch),
                Operand2 = new QuantityModel<object>(5, LengthUnit.Inch),
                Operation = OperationType.Add,
                Result = 15,
                HasError = false
            };

            // Act: Save entity to repository
            repository.Save(entity);

            // Retrieve all saved operations
            var allOperations = repository.GetAll();

            // Assert: Repository contains saved operation
            Assert.IsTrue(allOperations.Count > 0);
        }

        /// <summary>
        /// TEST: Repository should implement Singleton pattern returning same instance.
        /// SCENARIO: Call GetInstance() twice
        /// EXPECTED: Both calls return same object reference
        /// PURPOSE: Verify Singleton implementation ensures single repository instance
        /// </summary>
        [TestMethod]
        public void Repository_ShouldReturnSingletonInstance()
        {
            // Act: Get two repository instances
            var repo1 = QuantityMeasurementCacheRepository.GetInstance();
            var repo2 = QuantityMeasurementCacheRepository.GetInstance();

            // Assert: Both should reference same object (Singleton pattern)
            Assert.AreSame(repo1, repo2);
        }
    }
}