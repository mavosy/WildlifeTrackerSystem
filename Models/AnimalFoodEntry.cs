using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WTS.Models
{
    [Serializable]
    public class AnimalFoodEntry
    {
        [XmlAttribute("AnimalName")]
        public string AnimalName { get; set; }

        [XmlElement("FoodItem")]
        public List<string> FoodItems { get; set; } = new List<string>();
    }
}
