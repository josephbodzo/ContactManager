using ContactManager.Infrastructure.Services.Exceptions;
using ContactManager.Infrastructure.Services.Paging;

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

        public static void ThrowIfInvalidPaging(PagingOptions pagingOptions)
        {
            if(pagingOptions == null) return;

            if (pagingOptions.Take < 0 || pagingOptions.Skip < 0)
                throw new ValidateException("Take and Skip can not be negative");

            if (pagingOptions.Take > Constants.Constants.ConstantMaxPageTake)
                throw new ValidateException($"Cannot return more than {Constants.Constants.ConstantMaxPageTake} items");
        }
    }
}
