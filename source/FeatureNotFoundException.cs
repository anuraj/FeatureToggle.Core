using System;

namespace dotnetthoughts.AspNetCore
{
    public class FeatureNotFoundException : Exception
    {
        private readonly string _feature;
        public FeatureNotFoundException(string feature)
        {
            _feature = feature;
        }
    }
}