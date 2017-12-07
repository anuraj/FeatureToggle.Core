using System.Collections.Generic;
using dotnetthoughts.AspNetCore;
using Xunit;

namespace FeatureToggle.Core.Tests
{
    public class FeatureTests
    {
        private UnitTestConfiguration _configuration;
        public FeatureTests()
        {
            var configuration = new Dictionary<string, string>(2);
            configuration.Add("Features:Feature1", "true");
            configuration.Add("Features:Feature2", "false");
            _configuration = new UnitTestConfiguration(configuration);
        }
        [Fact]
        public void VerifyFeature1IsTrue()
        {
            var feature = new Feature(_configuration);
            Assert.True(feature.IsFeatureEnabled("Feature1"));
        }

        [Fact]
        public void VerifyFeature2IsFalse()
        {
            var feature = new Feature(_configuration);
            Assert.False(feature.IsFeatureEnabled("Feature2"));
        }

        [Fact]
        public void VerifyFeature3ThrowsException()
        {
            var feature = new Feature(_configuration);
            Assert.Throws<FeatureNotFoundException>(() => feature.IsFeatureEnabled("Feature3"));
        }
    }
}
