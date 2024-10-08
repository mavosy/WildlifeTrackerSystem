using System.Xml.Serialization;

namespace WTS.Models
{
    [Serializable]
    public class AnimalFoodData
    {
        [XmlElement("AnimalFoodEntry")]
        public List<AnimalFoodEntry> Entries { get; set; } = new List<AnimalFoodEntry>();
    }
}
