using System.ComponentModel.DataAnnotations;
using System.Text;
using WTS.Enums;
using WTS.Utilities;

namespace WTS.Models.AnimalBase
{
    /// <summary>
    /// Base class for all animal models
    /// </summary>
    internal abstract class Animal
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
        [StringLength(5)]
        protected string Id { get; set; }

        /// <summary>
        /// Nullable, defaults to "No Name"
        /// </summary>
        [StringLength(50, ErrorMessage = "Name must be 50 characters or less")]
        protected string? Name { get; set; }

        /// <summary>
        /// Nullable, defaults to null
        /// </summary>
        [Range(0, 600, ErrorMessage = "Must be between 0 and 600")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Age must be a numeric value")]
        protected int? Age { get; set; }

        [Required(ErrorMessage = "This information is required")]
        protected GenderType Gender { get; set; }

        /// <summary>
        /// Set automatically at the category level of inheritance
        /// </summary>
        [Required(ErrorMessage = "This information is required")]
        protected CategoryType Category { get; set; }

        public virtual IEnumerable<KeyValuePair<string, ValueWrapper>> GetPropertiesAsKeyValuePairs()
        {
            yield return new KeyValuePair<string, ValueWrapper>("ID", ValueWrapper.Create(Id));
            yield return new KeyValuePair<string, ValueWrapper>("Name", ValueWrapper.Create(Name));
            yield return new KeyValuePair<string, ValueWrapper>("Age", ValueWrapper.Create(Age));
            yield return new KeyValuePair<string, ValueWrapper>("Gender", ValueWrapper.Create(Gender));
            yield return new KeyValuePair<string, ValueWrapper>("Category", ValueWrapper.Create(Category));
        }




        //protected virtual List<string> GetPropertyStrings()
        //{
        //    return
        //    [
        //        $"ID: {Id}",
        //        $"Name: {Name}",
        //        $"Age: {Age}",
        //        $"Gender: {Gender}",
        //        $"Category: {Category}"
        //    ];
        //}

        //public override string ToString()
        //{
        //    List<string> propertyStrings = GetPropertyStrings();
        //    int maxKeyLength = propertyStrings.Max(s => s.IndexOf(':'));

        //    StringBuilder stringBuilder = new();

        //    foreach (string propertyString in propertyStrings)
        //    {
        //        int colonIndex = propertyString.IndexOf(':');
        //        //int paddingNeeded = maxKeyLength - colonIndex; // Remove?
        //        string paddedKeyString = propertyString.Substring(0, colonIndex).PadRight(maxKeyLength);
        //        string valueString = propertyString.Substring(colonIndex + 2);
        //        string paddedString = $"{paddedKeyString}{valueString}";
        //        stringBuilder.AppendLine(paddedString);
        //    }
        //    return stringBuilder.ToString();
        //}
             
        //public override string ToString()
        //{
        //    return 
        //        $"ID:\t\t{Id}\n" +
        //        $"Name:\t\t{Name}\n" +
        //        $"Age:\t\t{Age}\n" +
        //        $"Gender:\t\t{Gender}\n" +
        //        $"Category:\t{Category}";
        //}


        //protected virtual string GetParentAnimalInfo()
        //{
        //    int keyWidth = CalculateKeyWidth();
        //    StringBuilder builder = new();

        //    builder.AppendLine($"{PadKey("ID:", keyWidth)}{Id}");
        //    builder.AppendLine($"{PadKey("Name:", keyWidth)}{Name}");
        //    builder.AppendLine($"{PadKey("Age:", keyWidth)}{Age}");
        //    builder.AppendLine($"{PadKey("Gender:", keyWidth)}{Gender}");
        //    builder.AppendLine($"{PadKey("Category:", keyWidth)}{Category}");

        //    return builder.ToString();
        //}

        //protected string PadKey(string key, int width)
        //{
        //    return key.PadRight(width + 1);
        //}

        //protected virtual int CalculateKeyWidth()
        //{
        //    return new[] { "ID:", "Name:", "Age:", "Gender:", "Category:" }.Max(k => k.Length);
        //}

        //public override string ToString()
        //{
        //    return GetParentAnimalInfo();
        //}
    }
}