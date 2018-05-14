using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace FeatureToggle.Core.Tests
{
    public class UnitTestConfiguration : IConfiguration
    {
        private Dictionary<string, string> _configuration;
        public UnitTestConfiguration(Dictionary<string, string> configuration)
        {
            _configuration = configuration;
        }

        public string this[string key]
        {
            get
            {
                if (_configuration.ContainsKey(key))
                {
                    return _configuration[key];
                }

                return string.Empty;
            }
            set => throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
