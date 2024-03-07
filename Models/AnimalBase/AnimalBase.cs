using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.AnimalBase
{
    /// <summary>
    /// Base class for all animal models
    /// </summary>
    internal abstract class Animal : IAnimal
    {
        protected Animal(string id, CategoryType categoryType, GenderType genderType = GenderType.Unknown, string? name = "No name", int? age = null)
        {
            Name = name;
            Id = id;
            Age = age;
            Gender = genderType;
            Category = categoryType;
        }

        /// <summary>
        /// Set automatically at creation
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nullable, defaults to "No Name"
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Nullable, defaults to null
        /// </summary>
        public int? Age { get; set; }

        public GenderType Gender { get; set; }

        /// <summary>
        /// Set automatically at the category level of inheritance
        /// </summary>
        public CategoryType Category { get; set; }

        /// <summary>
        /// Retrieves a collection of properties for the current object represented as key-value pairs.
        /// This method is overridden in the child classes of AnimalBase, and implemented to include additional properties specific to the derived class.
        /// Is used to get information of an Animal object to the GUI.
        /// </summary>
        public virtual IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            yield return new KeyValuePair<string, ValueWrapper>("ID", ValueWrapper.Create(Id));
            yield return new KeyValuePair<string, ValueWrapper>("Name", ValueWrapper.Create(Name));
            yield return new KeyValuePair<string, ValueWrapper>("Age", ValueWrapper.Create(Age));
            yield return new KeyValuePair<string, ValueWrapper>("Gender", ValueWrapper.Create(Gender));
            yield return new KeyValuePair<string, ValueWrapper>("Category", ValueWrapper.Create(Category));
        }

        /// <summary>
        /// An abstract method that gets the sound of the specific species of animal.
        /// If the aninmal is known to be mostly silent or not have an characteristic sound it will return "Silent".
        /// </summary>
        /// <returns>A string representation of the animals sound</returns>
        public abstract string GetAnimalSoundAsString();
    }
}