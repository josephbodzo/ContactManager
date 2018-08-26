using System;

namespace ContactManager.Common.Utilities
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}