using FirebaseCoreAdmin.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FirebaseCoreAdmin.Tests
{
    public class DataExtensionsTest
    {
        [Fact]
        public void Beyon_Unix_Epoch()
        {
            DateTime date = new DateTime(1969, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => date.ToUnixSeconds());
        }

        [Theory]
        [InlineData(1970, 1, 1, 0, 0, 10, 10)]
        [InlineData(1970, 1, 1, 0, 0, 40, 40)]
        [InlineData(1970, 1, 1, 0, 5, 0, 300)]
        [InlineData(1970, 1, 1, 2, 0, 0, 7200)]
        [InlineData(1970, 1, 3, 0, 0, 0, 172800)]
        [InlineData(2017, 5, 1, 0, 0, 0, 1493596800)]
        public void Unix_Seconds(int year, int month, int day, int hour, int minute, int second, long output)
        {
            var dateParam = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            var secondsInUnixEpoch = dateParam.ToUnixSeconds();
            Assert.Equal(output, secondsInUnixEpoch);
        }
    }
}
