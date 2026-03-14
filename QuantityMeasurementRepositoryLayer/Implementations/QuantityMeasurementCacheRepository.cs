using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Implementations
{
    /// <summary>
    /// Singleton implementation of IQuantityMeasurementRepository using in-memory cache (List).
    /// Stores quantity measurement operation records for the current session.
    /// Uses the Singleton pattern to ensure only one instance exists throughout the application.
    /// </summary>
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        /// <summary>Singleton instance of the repository</summary>
        private static QuantityMeasurementCacheRepository instance;

        /// <summary>In-memory cache (List) storing all operation records</summary>
        private readonly List<QuantityMeasurementEntity> cache;

        /// <summary>
        /// Private constructor to prevent direct instantiation.
        /// Initializes the in-memory cache for storing operation records.
        /// </summary>
        private QuantityMeasurementCacheRepository()
        {
            cache = new List<QuantityMeasurementEntity>();
        }

        /// <summary>
        /// Gets the singleton instance of the repository.
        /// Creates it on first call, returns existing instance on subsequent calls.
        /// </summary>
        /// <returns>The singleton instance of QuantityMeasurementCacheRepository</returns>
        public static QuantityMeasurementCacheRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new QuantityMeasurementCacheRepository();
            }

            return instance;
        }

        /// <summary>
        /// Saves a quantity measurement operation entity to the in-memory cache.
        /// </summary>
        /// <param name="entity">The entity containing operation details or result</param>
        public void Save(QuantityMeasurementEntity entity)
        {
            cache.Add(entity);
        }

        /// <summary>
        /// Retrieves all stored operation records from the in-memory cache.
        /// </summary>
        /// <returns>A list of all QuantityMeasurementEntity records in the cache</returns>
        public List<QuantityMeasurementEntity> GetAll()
        {
            return cache;
        }
    }
}