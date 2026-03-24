using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementAPI.Data;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementAPI.Repositories
{
    /// <summary>
    /// EF Core implementation of QuantityMeasurement Repository
    /// This replaces ADO.NET with ORM for cleaner database access
    /// </summary>
    public class EFCoreQuantityMeasurementRepository : IQuantityMeasurementRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCoreQuantityMeasurementRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Save a measurement operation to the database
        /// </summary>
        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                _context.QuantityMeasurements.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving to database", ex);
            }
        }

        /// <summary>
        /// Get all measurement operations from the database
        /// </summary>
        public List<QuantityMeasurementEntity> GetAll()
        {
            try
            {
                return _context.QuantityMeasurements
                    .OrderByDescending(e => e.CreatedAt)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data from database", ex);
            }
        }

        /// <summary>
        /// Get measurements by operation type
        /// </summary>
        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(OperationType operationType)
        {
            try
            {
                return _context.QuantityMeasurements
                    .Where(e => e.Operation == operationType)
                    .OrderByDescending(e => e.CreatedAt)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error filtering data from database", ex);
            }
        }

        /// <summary>
        /// Get total count of measurements
        /// </summary>
        public int GetTotalCount()
        {
            try
            {
                return _context.QuantityMeasurements.Count();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting count from database", ex);
            }
        }

        /// <summary>
        /// Delete all measurements from the database
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                _context.QuantityMeasurements.RemoveRange(_context.QuantityMeasurements);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting data from database", ex);
            }
        }

        /// <summary>
        /// Close database resources (no-op for EF Core as it handles resource management)
        /// </summary>
        public void CloseResources()
        {
            // EF Core DbContext handles resource management automatically
            // No explicit cleanup needed
        }
    }
}
