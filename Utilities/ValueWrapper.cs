namespace WTS.Utilities
{
    /// <summary>
    /// Encapsulates a value of any type, allowing the value to be retrieved in a type-safe manner. 
    /// The value to be encapsulated can be null if a reference type or nullable value type.
    /// </summary>
    public class ValueWrapper
    {
        /// <summary>
        /// The encapsulated value.
        /// </summary>
        public object? Value { get; }

        /// <summary>
        /// A private constructor to make sure that an instance of ValueWrapper only can be created through the Create<T> method
        /// </summary>
        /// <param name="value">The value to be encapsulated.</param>
        private ValueWrapper(object? value) { Value = value; }

        /// <summary>
        /// Creates a new instance of ValueWrapper encapsulating the provided value as an object.
        /// </summary>
        /// <typeparam name="T">The type of the value to encapsulate, for example a string or int.</typeparam>
        /// <param name="value">The value to encapsulate.</param>
        /// <returns>A new instance of ValueWrapper encapsulating the value.</returns>
        public static ValueWrapper Create<T>(T value) => new ValueWrapper(value);

        /// <summary>
        /// Retrieves the encapsulated value, cast as the specified type T.
        /// </summary>
        /// <typeparam name="T">The type to which the value will be cast.</typeparam>
        /// <returns>The encapsulated value cast to the specified type T.</returns>
        public T Get<T>()
        {
            if (Value == null)
            {
                if (default(T) != null)
                {
                    throw new InvalidOperationException("Cannot cast null to a non-nullable type.");
                }
                return default(T);
            }

            try
            {
                return (T)Value;
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException($"Failed to cast the value as {typeof(T)}. The actual type is: {Value?.GetType().Name ?? "null"}. Error message: {ex.Message}");
            }
        }

        /// <summary>
        /// Overriding ToString to be able to convert the ValueWrapper 
        /// object to a string without first casting dynamically to the values original type
        /// </summary>
        /// <returns>The string representation of the encapsulated value</returns>
        public override string ToString()
        {
            return Value?.ToString() ?? "null";
        }
    }
}