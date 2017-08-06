
namespace FirebaseCoreAdmin.Extensions
{
    using System;

    public static class DateExtensions
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixSeconds(this DateTime value)
        {
            value = value.ToUniversalTime();

            if (value <= UnixEpoch)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value is beyond unix epoch");
            }

            TimeSpan diff = value - UnixEpoch;
            return (long)Math.Floor(diff.TotalSeconds);
        }
    }
}
