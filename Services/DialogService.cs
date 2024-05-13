using Microsoft.Win32;
using WTS.Services.Interfaces;
using WTS.Views;

namespace WTS.Services
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Opens a file dialog for the user to select a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        public string? OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        /// <summary>
        /// Opens a file dialog for the user to save a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        public string? SaveFileDialog(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = filter };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }

        /// <summary>
        /// Displays a dialog window with the given title and message.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void DataDisplayDialog(string title, string message)
        {
            var displayWindow = new LoadedTextDataDisplayWindow();
            displayWindow.LoadedDataText.Text = message;
            displayWindow.Title = title;
            displayWindow.ShowDialog();
        }
    }
}