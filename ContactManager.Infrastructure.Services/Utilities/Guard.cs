using System.Text.RegularExpressions;
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

        public static void ThrowIfLessThanMinLength(string argumentValue, string argumentName, int minLength)
        {
            if (argumentValue.Length < minLength)
            {
                throw new ValidateException(argumentName, "{0} must have a minimum of " + minLength + " characters");
            }
        }

        public static void ThrowIfGreaterThanMaxLength(string argumentValue, string argumentName, int maxLength)
        {
            if (argumentValue.Length > maxLength)
            {
                throw new ValidateException(argumentName, "{0} must have a maximum of " + maxLength + " characters");
            }
        }

        public static void ThrowIfRegexNotMatch(string argumentValue, string argumentName, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(argumentValue);

            if (!match.Success)
            {
                throw new ValidateException(argumentName, "{0} is not in valid format");
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
