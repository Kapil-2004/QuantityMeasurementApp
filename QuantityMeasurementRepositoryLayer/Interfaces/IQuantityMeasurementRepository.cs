using System.Collections.Generic;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAll();

        List<QuantityMeasurementEntity> GetMeasurementsByOperation(OperationType operationType);

        int GetTotalCount();

        void DeleteAll();

        void CloseResources();
    }
}