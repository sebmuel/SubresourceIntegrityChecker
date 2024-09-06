using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SubresourceIntegrityChecker.Interfaces;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Controllers;

namespace SubresourceIntegrityChecker.Controller;

public class SubresourceIntegrityCheckerController : UmbracoAuthorizedApiController
{
    private readonly ICheckSumService _checkSumService;

    public SubresourceIntegrityCheckerController(ICheckSumService checkSumService)
    {
        _checkSumService = checkSumService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateChecksum(CreateChecksumRequest request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            return BadRequest();
        }

        ErrorOr<string> checksum = await _checkSumService.CreateChecksumFromUrl(request.Url);

        if (checksum.IsError)
        {
            return Problem(
                statusCode: checksum.FirstError.NumericType,
                title: checksum.FirstError.Description
            );
        }
        
        return Ok(checksum);
    }
}

public record CreateChecksumRequest
{
    public string? Url { get; init; }
}