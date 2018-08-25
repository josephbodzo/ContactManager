using System;

namespace ContactManager.Infrastructure.Services.Utilities
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}