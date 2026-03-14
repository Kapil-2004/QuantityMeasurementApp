using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    /// <summary>
    /// Repository interface defining the contract for persisting quantity measurement operations.
    /// Implementations handle storing and retrieving QuantityMeasurementEntity records.
    /// </summary>
    public interface IQuantityMeasurementRepository
    {
        /// <summary>
        /// Saves a quantity measurement operation record to the repository.
        /// </summary>
        /// <param name="entity">The entity containing operation details, result, or error information</param>
        void Save(QuantityMeasurementEntity entity);

        /// <summary>
        /// Retrieves all recorded quantity measurement operations from the repository.
        /// </summary>
        /// <returns>A list of all stored QuantityMeasurementEntity records</returns>
        List<QuantityMeasurementEntity> GetAll();
    }
}