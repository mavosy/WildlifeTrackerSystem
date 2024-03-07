using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.Mammals
{
    public class Cat : Mammal
    {
        public Cat(string id, string? name, int? age, GenderType gender, int numberOfLegs, string breed)
            : base(id, name, age, gender, numberOfLegs)
        {
            Breed = breed;
        }

        public string Breed { get; set; }

        public override IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            foreach (KeyValuePair<string, ValueWrapper> keyValuePair in base.GetPropertiesAsKeyValuePairs())
            {
                yield return keyValuePair;
            }
            yield return new KeyValuePair<string, ValueWrapper>("Breed", ValueWrapper.Create(Breed));
        }

        public override string GetAnimalSoundAsString()
        {
            return "Meow";
        }
    }
}