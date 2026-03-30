using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Engines
{
    public static class ValidationEngine
    {
        public static void ValidateSameMeasurement(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Different measurement types not allowed");
        }
    }
}
