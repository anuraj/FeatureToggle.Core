using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetthoughts.AspNetCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace FeatureToggle.Core.Tests
{
    public class FeatureTagHelperTests
    {
        private UnitTestConfiguration _configuration;
        private TagHelperContext _tagHelperContext;
        private TagHelperOutput _tagHelperOutput;
        private string _tagContent;
        public FeatureTagHelperTests()
        {
            var configuration = new Dictionary<string, string>(2);
            configuration.Add("Features:Feature1", "true");
            configuration.Add("Features:Feature2", "false");
            _configuration = new UnitTestConfiguration(configuration);

            _tagHelperContext = new TagHelperContext(
                            new TagHelperAttributeList(),
                            new Dictionary<object, object>(),
                            Guid.NewGuid().ToString("N"));

            _tagContent = "<Button>Hello</Button>";
            _tagHelperOutput = new TagHelperOutput("feature",
             new TagHelperAttributeList(),
            (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });

            _tagHelperOutput.Content.SetHtmlContent(_tagContent);
        }

        [Fact]
        public void VerifyEnabledFeatureRenderingValues()
        {
            var feature = new Feature(_configuration);
            var featureTagHelper = new FeatureTagHelper(feature);
            featureTagHelper.Name = "Feature1";
            featureTagHelper.Process(_tagHelperContext, _tagHelperOutput);
            Assert.NotNull(_tagHelperOutput.TagName);
            Assert.Equal("DIV", _tagHelperOutput.TagName);
            Assert.Equal(_tagContent, _tagHelperOutput.Content.GetContent());
        }
        [Fact]
        public void VerifyDisabledFeatureRenderingEmpty()
        {
            var feature = new Feature(_configuration);
            var featureTagHelper = new FeatureTagHelper(feature);
            featureTagHelper.Name = "Feature2";
            featureTagHelper.Process(_tagHelperContext, _tagHelperOutput);
            Assert.Null(_tagHelperOutput.TagName);
        }

        [Fact]
        public void VerifyNotExistingFeatureThrowsException()
        {
            var feature = new Feature(_configuration);
            var featureTagHelper = new FeatureTagHelper(feature);
            featureTagHelper.Name = "Feature3";
            Assert.Throws<FeatureNotFoundException>(() => featureTagHelper.Process(_tagHelperContext, _tagHelperOutput));
        }
    }
}