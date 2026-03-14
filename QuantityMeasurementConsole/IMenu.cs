namespace QuantityMeasurementConsole.Interfaces
{
    /// <summary>
    /// Interface defining the contract for the console menu.
    /// Implementations provide user interaction functionality.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Starts the interactive menu loop for user interaction.
        /// Displays options and processes user input.
        /// </summary>
        void Start();
    }
}