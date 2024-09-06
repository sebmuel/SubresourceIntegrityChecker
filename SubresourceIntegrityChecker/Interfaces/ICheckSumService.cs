using ErrorOr;

namespace SubresourceIntegrityChecker.Interfaces;

public interface ICheckSumService
{
    public Task<ErrorOr<string>> CreateChecksumFromUrl(string url);
}