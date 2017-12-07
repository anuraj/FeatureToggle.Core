using Microsoft.AspNetCore.Razor.TagHelpers;

namespace dotnetthoughts.AspNetCore
{
        public class FeatureTagHelper : TagHelper
    {
        private readonly IFeature _feature;
        [HtmlAttributeName("name")]
        public string Name { get; set; }
        public FeatureTagHelper(IFeature feature)
        {
            _feature = feature;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "DIV";
            try
            {
                if (!_feature.IsFeatureEnabled(Name))
                {
                    output.SuppressOutput();
                }
            }
            catch (FeatureNotFoundException)
            {

                var warningStyle = @"background: url('data:image/png;base64,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') no-repeat; display:inline-block;width: 32px";

                output.Content.SetHtmlContent($"<h2 style=\"text-align:center; border: solid 3px red; padding:20px;text-transform:uppercase;\"><span style=\"{warningStyle}\">&nbsp;</span>Feature Toggle Exception : \"{Name}\" - feature not configured.</h2>");
            }
        }
    }
}