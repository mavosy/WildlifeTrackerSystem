namespace WTS.Models
{
    /// <summary>
    /// Provides functionality to generate unique IDs for different animal species.
    /// </summary>
    public static class AnimalIdGenerator
    {
        /// <summary>
        /// Stores the current count for each category of animal ID prefixes.
        /// </summary>
        private static readonly Dictionary<string, int> _prefixCounters = new()
        {
            { "AM", 1 }, // Amphibian
            { "AR", 1 },  // Arachnid
            { "B", 1 },  // Bird 
            { "F", 1 },  // Fish
            { "I", 1 },  // Insect
            { "M", 1 }, // Mammal
            { "R", 1 }, // Reptile
        };

        /// <summary>
        /// Maps each species to its corresponding ID prefix.
        /// </summary>
        private static readonly Dictionary<string, string> _speciesToPrefixMap = new()
        {
            { "Axolotl", "AM" },
            { "Frog", "AM" },
            { "Spider", "AR" },
            { "Scorpion", "AR" },
            { "Raven", "B" },
            { "Falcon", "B" },
            { "Salmon", "F" },
            { "Shark", "F" },
            { "Bee", "I" },
            { "Ladybug", "I" },
            { "Cat", "M" },
            { "Elephant", "M" },
            { "Snake", "R" },
            { "Tortoise", "R" },
        };

        /// <summary>
        /// Generates a unique ID for the given species. The ID consists of a prefix based on the species category and a number that increments for each new ID within the same category.
        /// </summary>
        /// <param name="species">The name of the species for which to generate an ID.</param>
        /// <returns>A unique ID for the given species.</returns>
        /// <exception cref="ArgumentException">Thrown when the species is not recognized or when the prefix for the species is not found.</exception>
        public static string GenerateId(string species)
        {
            if (!_speciesToPrefixMap.TryGetValue(species, out var categoryPrefix))
            {
                throw new ArgumentException($"Invalid species: {species}");
            }

            if (!_prefixCounters.TryGetValue(categoryPrefix, out var number))
            {
                throw new ArgumentException($"Invalid prefix for species {species}: {categoryPrefix}");
            }

            _prefixCounters[categoryPrefix] = number + 1;
            return $"{categoryPrefix}{number:D3}"; // D3 formats the number to a string with three characters 
        }
    }
}