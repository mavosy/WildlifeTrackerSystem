using System.Windows.Media.Imaging;

namespace WTS.Services.Interfaces
{
    public interface IFileService
    {
        public string? OpenFileDialog(string filter);

        public BitmapImage FileToBitmapImage(string filePath);
    }
}