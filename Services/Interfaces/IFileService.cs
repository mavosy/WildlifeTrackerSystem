using System.Windows.Media.Imaging;

namespace WTS.Services.Interfaces
{
    internal interface IFileService
    {
        public string OpenFileDialog(string filter);

        public BitmapImage FileToBitmapImage(string filePath);
    }
}
