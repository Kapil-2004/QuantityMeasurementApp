namespace QuantityMeasurementModelLayer.Enums
{
    /// <summary>
    /// Enumeration defining all supported measurement operations.
    /// Used to track what type of operation was performed in the repository.
    /// </summary>
    public enum OperationType
    {
        /// <summary>Compare two quantities to check if they are equal</summary>
        Compare,
        
        /// <summary>Convert a quantity from one unit to another</summary>
        Convert,
        
        /// <summary>Add two quantities together</summary>
        Add,
        
        /// <summary>Subtract one quantity from another</summary>
        Subtract,
        
        /// <summary>Divide one quantity by another</summary>
        Divide
    }
}