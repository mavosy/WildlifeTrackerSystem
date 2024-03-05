using System.ComponentModel.DataAnnotations;
using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Amphibians
{
    internal class Axolotl : Amphibian
    {
        public Axolotl(string id, string? name, int? age, GenderType gender, bool landliving, double regenerationRate) 
            : base(id, name, age, gender, landliving)
        {
            RegenerationRate = regenerationRate;
        }

        /// <summary>
        /// Regrowth in millimeters per day
        /// </summary>
        [Required]
        [Range(0.0, 1.0, ErrorMessage ="Must be between 0.0 and 1.0 mm/day")]
        public double RegenerationRate { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Regeneration", ValueWrapper.Create(RegenerationRate));
        }

        //public override string ToString()
        //{
        //    return 
        //        $"{base.ToString()}\n" +
        //        $"Regeneration:\t{RegenerationRate}";
        //}
    }
}