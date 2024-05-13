namespace WTS.Services.Interfaces
{
    public interface IDialogService
    {
        /// <summary>
        /// Opens a file dialog for the user to select a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        string? OpenFileDialog(string filter);

        /// <summary>
        /// Opens a file dialog for the user to save a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        string? SaveFileDialog(string filter);

        /// <summary>
        /// Displays a dialog window with the given title and message.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void DataDisplayDialog(string title, string message);
    }
}