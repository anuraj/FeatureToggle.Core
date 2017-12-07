namespace dotnetthoughts.AspNetCore
{
    public interface IFeature
    {
        bool IsFeatureEnabled(string feature);
    }
}