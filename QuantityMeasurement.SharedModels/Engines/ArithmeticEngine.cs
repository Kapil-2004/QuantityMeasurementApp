namespace QuantityMeasurement.SharedModels.Engines
{
    public static class ArithmeticEngine
    {
        public static double Add(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new ArgumentException("Temperature addition not supported");

            return v1 + v2;
        }

        public static double Subtract(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new ArgumentException("Temperature subtraction not supported");

            return v1 - v2;
        }

        public static double Divide(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new ArgumentException("Temperature division not supported");

            if (v2 == 0)
                throw new ArgumentException("Division by zero");

            return v1 / v2;
        }
    }
}
