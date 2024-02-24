using Microsoft.Win32;
using System.Windows.Media.Imaging;
using WTS.Services.Interfaces;

namespace WTS.Services
{
    internal class FileService : IFileService
    {
        /// <summary>
        /// Converts a file by filepath to a BitmapImage.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public BitmapImage FileToBitmapImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }
            return new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Opens a file dialog for the user to select a file based on the filters.
        /// </summary>
        /// <param name="filter">File filters as strings</param>
        /// <returns>Filepath to the chosen file</returns>
        public string OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}
