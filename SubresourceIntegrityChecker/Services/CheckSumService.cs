using System.Security.Cryptography;
using ErrorOr;
using Microsoft.Extensions.Logging;
using SubresourceIntegrityChecker.Interfaces;

namespace SubresourceIntegrityChecker.Services;

public class CheckSumService : ICheckSumService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CheckSumService> _logger;

    public CheckSumService(IHttpClientFactory httpClientFactory, ILogger<CheckSumService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<ErrorOr<string>> CreateChecksumFromUrl(string url)
    {
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogError("Failed to create checksum from url: {Url}, reason: {Reason}", url,
                    response.ReasonPhrase);
                return Error.Conflict(description: "Failed to create checksum from url");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var checksum = Convert.ToBase64String(SHA384.HashData(bytes));

            return checksum;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create checksum from url: {Url}", url);
            return Error.Unexpected(description: "An unexpected error occurred please try again later");
        }
    }
}