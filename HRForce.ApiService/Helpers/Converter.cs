namespace HRForce.ApiService.Helpers
{
    public static class Converter
    {
        /// <summary>
        /// Converts a string to the specified enum type.
        /// Throws ArgumentException if the string is invalid.
        /// </summary>
        public static TEnum ConvertStringToEnum<TEnum>(string value) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty.");

            if (!Enum.TryParse<TEnum>(value, true, out var parsedEnum))
                throw new ArgumentException($"Invalid {typeof(TEnum).Name} value: {value}");

            return parsedEnum;
        }
    }

}
