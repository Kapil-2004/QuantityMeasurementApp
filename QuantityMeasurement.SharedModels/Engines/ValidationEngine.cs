namespace QuantityMeasurement.SharedModels.Engines
{
    public static class ValidationEngine
    {
        public static void ValidateSameMeasurement(string measurementType1, string measurementType2)
        {
            if (measurementType1 != measurementType2)
                throw new ArgumentException("Different measurement types not allowed");
        }
    }
}
