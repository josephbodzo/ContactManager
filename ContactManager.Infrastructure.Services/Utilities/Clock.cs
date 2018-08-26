using System;

namespace ContactManager.Common.Utilities
{
    public class Clock : IClock
    {
        private readonly Func<DateTime> _currentDateFunc = () => DateTime.Now;

        public Clock()
        {
        }

        public Clock(DateTime now)
        {
            _currentDateFunc = () => now;
        }

        public DateTime Now => _currentDateFunc();
    }
}
