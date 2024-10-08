using System.Windows.Media.Imaging;

namespace WTS.Services.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Converts a file by filepath to a BitmapImage.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        BitmapImage FileToBitmapImage(string filePath);

        /// <summary>
        /// Saves a string to a text file by filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        void SaveDataToTextFile(string filePath, string content);

        /// <summary>
        /// Loads a text file by filepath to a string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        string LoadDataFromTextFile(string filePath);

        /// <summary>
        /// Serializes the given list to json using System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToSerialize"></param>
        /// <returns>A json string</returns>
        string SerializeListToJson<T>(List<T> listToSerialize);

        /// <summary>
        /// Deserializes the given list to json using System.Text.Json
        /// </summary>
        /// <returns>A deserialized generic list</returns>
        List<T> DeserializeListFromJson<T>(string jsonString);

        /// <summary>
        /// Serializes the given data to xml using xmlSerializer
        /// </summary>
        void SaveDataToXmlFile<T>(string filePath, T data);

        /// <summary>
        /// Deserializes the given data to xml using xmlSerializer
        /// </summary>
        public T LoadDataFromXmlFile<T>(string filePath);
    }
}