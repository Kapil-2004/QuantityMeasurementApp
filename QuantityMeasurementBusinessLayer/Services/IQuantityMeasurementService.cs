using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementBusinessLayer.Services
{
    public interface IQuantityMeasurementService
    {
        bool Compare(QuantityDTO q1, QuantityDTO q2, bool saveHistory = false);

        QuantityDTO Convert(QuantityDTO input, string targetUnit, bool saveHistory = false);

        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, bool saveHistory = false);

        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, bool saveHistory = false);

        double Divide(QuantityDTO q1, QuantityDTO q2, bool saveHistory = false);

        List<QuantityMeasurementEntity> GetHistory();
    }
}
