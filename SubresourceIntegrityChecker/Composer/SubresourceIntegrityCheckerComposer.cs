using Microsoft.Extensions.DependencyInjection;
using SubresourceIntegrityChecker.Interfaces;
using SubresourceIntegrityChecker.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace SubresourceIntegrityChecker.Composer;

public class SubresourceIntegrityCheckerComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.ManifestFilters().Append<SubresourceIntegrityCheckerManifest>();
        builder.Services.AddScoped<ICheckSumService, CheckSumService>();
    }
}