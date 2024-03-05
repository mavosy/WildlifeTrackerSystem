using System.IO;

namespace WTS.Utilities
{
    /// <summary>
    /// Provides utility methods for inspecting objects with reflection.
    /// </summary>
    public static class ObjectInspector
    {
        /// <summary>
        /// Defines the order in which general properties should be written.
        /// </summary>
        private static readonly List<string> GeneralPropertiesOrder = new()
        {
            "Id",
            "Name",
            "Age",
            "Gender",
            "Category",
        };

        /// <summary>
        /// Generates a string representation of an object's properties and their values.
        /// The properties are listed in the order defined by GeneralPropertiesOrder, followed by any additional properties.
        /// If a property value cannot be retrieved, an error message is written instead of the value.
        /// </summary>
        /// <param name="animal">The object to inspect.</param>
        /// <returns>A string representation of the object's properties and their values, or an empty string if the object is null.</returns>
        public static string GetPropertiesToString(object animal)
        {
            if (animal == null) return string.Empty;

            using var propertyWriter = new StringWriter();
            var properties = animal.GetType().GetProperties();


            foreach (var propertyName in GeneralPropertiesOrder)
            {
                var property = animal.GetType().GetProperty(propertyName);
                if (property is not null)
                {
                    try
                    {
                        var value = property.GetValue(animal) ?? "null";
                        propertyWriter.WriteLine($"{property.Name.PadRight(15)}: {value}");
                    }
                    catch (Exception)
                    {
                        propertyWriter.WriteLine($"{property.Name.PadRight(15)}: [Error retrieving value]");
                    }
                }
            }


            foreach (var property in properties)
            {
                if (!GeneralPropertiesOrder.Contains(property.Name))
                {
                    try
                    {
                        var value = property.GetValue(animal) ?? "null";
                        propertyWriter.WriteLine($"{property.Name.PadRight(15)}: {value}");
                    }
                    catch (Exception)
                    {
                        propertyWriter.WriteLine($"{property.Name.PadRight(15)}: [Error retrieving value]");
                    }
                }
            }
            return propertyWriter.ToString();
        }
    }
}