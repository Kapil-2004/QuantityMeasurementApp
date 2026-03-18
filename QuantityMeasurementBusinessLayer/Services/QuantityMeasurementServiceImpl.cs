using System.Collections.Generic;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository repository;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        private double ConvertToBase(QuantityDTO dto)
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

        private double ConvertFromBase(string measurementType, string unit, double baseValue)
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

        private QuantityModel<object> MapToEntityModel(QuantityDTO dto)
        {
            object unitEnum = dto.MeasurementType switch
            {
                "Length" => Enum.Parse<LengthUnit>(dto.Unit),
                "Weight" => Enum.Parse<WeightUnit>(dto.Unit),
                "Volume" => Enum.Parse<VolumeUnit>(dto.Unit),
                "Temperature" => Enum.Parse<TemperatureUnit>(dto.Unit),
                _ => throw new QuantityMeasurementException("Invalid Measurement Type")
            };
            
            return new QuantityModel<object>(dto.Value, unitEnum);
        }

        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot compare different measurement types");

            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            bool result = base1 == base2;

            var entity = new QuantityMeasurementEntity(result);
            entity.Operation = OperationType.Compare;
            entity.Operand1 = MapToEntityModel(q1);
            entity.Operand2 = MapToEntityModel(q2);
            repository.Save(entity);

            return result;
        }

        public QuantityDTO Convert(QuantityDTO input, string targetUnit)
        {
            double baseValue = ConvertToBase(input);

            double convertedValue = ConvertFromBase(input.MeasurementType, targetUnit, baseValue);

            var result = new QuantityDTO(convertedValue, targetUnit, input.MeasurementType);

            var entity = new QuantityMeasurementEntity(result);
            entity.Operation = OperationType.Convert;
            entity.Operand1 = MapToEntityModel(input);
            entity.Operand2 = new QuantityModel<object>(0, MapToEntityModel(new QuantityDTO(0, targetUnit, input.MeasurementType)).Unit);
            repository.Save(entity);

            return result;
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot add different measurement types");

            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature addition not supported");

            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            double resultBase = base1 + base2;

            double resultValue = ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

            var result = new QuantityDTO(resultValue, q1.Unit, q1.MeasurementType);

            var entity = new QuantityMeasurementEntity(result);
            entity.Operation = OperationType.Add;
            entity.Operand1 = MapToEntityModel(q1);
            entity.Operand2 = MapToEntityModel(q2);
            repository.Save(entity);

            return result;
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot subtract different measurement types");

            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature subtraction not supported");

            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            double resultBase = base1 - base2;

            double resultValue = ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

            var result = new QuantityDTO(resultValue, q1.Unit, q1.MeasurementType);

            var entity = new QuantityMeasurementEntity(result);
            entity.Operation = OperationType.Subtract;
            entity.Operand1 = MapToEntityModel(q1);
            entity.Operand2 = MapToEntityModel(q2);
            repository.Save(entity);

            return result;
        }

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot divide different measurement types");

            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature division not supported");

            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            if (base2 == 0)
                throw new QuantityMeasurementException("Division by zero");

            double result = base1 / base2;

            var entity = new QuantityMeasurementEntity(result);
            entity.Operation = OperationType.Divide;
            entity.Operand1 = MapToEntityModel(q1);
            entity.Operand2 = MapToEntityModel(q2);
            repository.Save(entity);

            return result;
        }

        public List<QuantityMeasurementEntity> GetHistory()
        {
            return repository.GetAll();
        }
    }
}