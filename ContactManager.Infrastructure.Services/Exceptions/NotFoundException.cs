using System;

namespace ContactManager.Infrastructure.Services.Exceptions
{
    public class NotFoundException: ValidateException
    {
        public NotFoundException(string message, string fieldName) : base(message, fieldName)
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Exception inner, string message, string fieldName) : base(inner, message, fieldName)
        {
        }
    }
}
