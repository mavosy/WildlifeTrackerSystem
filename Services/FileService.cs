using System.IO;
using System.Text.Json;
using System.Windows.Media.Imaging;
using WTS.Services.Interfaces;
using WTS.ViewModels;

namespace WTS.Services
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Converts a file by filepath to a BitmapImage.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public BitmapImage FileToBitmapImage(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("File path cannot be null or empty.");
                }
                return new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid file path", ex);
            }
        }

        /// <summary>
        /// Saves a string to a text file by filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void SaveDataToTextFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {

                throw new Exception("Data could not be saved", ex);
            }
        }

        /// <summary>
        /// Loads a text file by filepath to a string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string LoadDataFromTextFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading the file", ex);
            }
        }

        /// <summary>
        /// Serializes the given list to json using System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToSerialize"></param>
        /// <returns>A json string</returns>
        public string SerializeListToJson<T>(List<T> listToSerialize)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(listToSerialize, options);
        }

        /// <summary>
        /// Deserializes the given list to json using System.Text.Json
        /// </summary>
        /// <returns>A deserialized generic list</returns>
        public List<T> DeserializeListFromJson<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<List<T>>(jsonString) ?? throw new NullReferenceException();
        }
    }
}