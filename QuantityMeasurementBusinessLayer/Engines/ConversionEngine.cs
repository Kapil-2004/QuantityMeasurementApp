using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementBusinessLayer.Engines
{
    public static class ConversionEngine
    {
        public static double ConvertToBase(QuantityDTO dto)
        {
            return dto.MeasurementType switch
            {
                "Length" => Enum.Parse<LengthUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Weight" => Enum.Parse<WeightUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Volume" => Enum.Parse<VolumeUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Temperature" => Enum.Parse<TemperatureUnit>(dto.Unit).ToBaseUnit(dto.Value),
                _ => throw new QuantityMeasurementException("Invalid Measurement Type")
            };
        }

        public static double ConvertFromBase(string measurementType, string unit, double baseValue)
        {
            return measurementType switch
            {
                "Length" => Enum.Parse<LengthUnit>(unit).FromBaseUnit(baseValue),
                "Weight" => Enum.Parse<WeightUnit>(unit).FromBaseUnit(baseValue),
                "Volume" => Enum.Parse<VolumeUnit>(unit).FromBaseUnit(baseValue),
                "Temperature" => Enum.Parse<TemperatureUnit>(unit).FromBaseUnit(baseValue),
                _ => throw new QuantityMeasurementException("Invalid Measurement Type")
            };
        }
    }
}