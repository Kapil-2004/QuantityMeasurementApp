using System;
using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Implementations
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private static QuantityMeasurementCacheRepository instance;

        private readonly List<QuantityMeasurementEntity> cache;

        private QuantityMeasurementCacheRepository()
        {
            cache = new List<QuantityMeasurementEntity>();
        }

        public static QuantityMeasurementCacheRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new QuantityMeasurementCacheRepository();
            }

            return instance;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            cache.Add(entity);
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return cache;
        }

        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(OperationType operationType)
        {
            return cache.Where(e => e.Operation == operationType).ToList();
        }

        public int GetTotalCount()
        {
            return cache.Count;
        }

        public void DeleteAll()
        {
            cache.Clear();
        }

        public void CloseResources()
        {
            // In-memory cache has no external resources to close.
        }
    }
}