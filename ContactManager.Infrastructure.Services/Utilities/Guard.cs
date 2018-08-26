using ContactManager.Infrastructure.Services.Exceptions;

namespace ContactManager.Infrastructure.Services.Utilities
{
    public static class Guard
    {
        public static void ThrowIfDefaultValue<T>(T argumentValue, string argumentName)
        {
            if (argumentValue.Equals(default(T)))
            {
                throw new ValidateException(argumentName, "{0} is required.");
            }
        }

        public static void ThrowIfDefaultOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ValidateException(argumentName, "{0} is required.");
            }
        }
    }
}
