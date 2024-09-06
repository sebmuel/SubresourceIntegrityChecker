using Umbraco.Cms.Core.Manifest;

namespace SubresourceIntegrityChecker;

public class SubresourceIntegrityCheckerManifest : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        manifests.Add(new PackageManifest()
        {
            PackageName = "SubresourceIntegrityChecker",
            Version = typeof(SubresourceIntegrityCheckerManifest).Assembly.GetName().Version?.ToString() ??
                      throw new NullReferenceException(),
            Dashboards =
            [
                new ManifestDashboard()
                {
                    Alias = "SubresourceIntegrityChecker",
                    Sections = ["content"],
                    View = "/App_Plugins/SubresourceIntegrityChecker/dashboard.html",
                    Weight = 100
                }
            ],
            Scripts =
            [
                "/App_Plugins/SubresourceIntegrityChecker/js/subresource-integrity-checker.js"
            ],
            
            Stylesheets = [
                "/App_Plugins/SubresourceIntegrityChecker/css/subresource-integrity-checker.css"
            ]
        });
    }
}