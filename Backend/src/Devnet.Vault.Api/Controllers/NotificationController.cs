using Devnet.Vault.Application.Features.Shared.Otp.Commands;
using Devnet.Vault.Application.Features.Shared.Otp.DTOs;
using Devnet.Vault.Domain.Constants.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Devnet.Vault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(IMediator _mediator) : ControllerBase
{
    #region Request OTP

    /// <summary>
    /// Request OTP
    /// </summary>
    [HttpPost(ApiEndpoints.AuthApiEndpoints.REQUEST_AUTH_OTP_ENDPOINT)]
    [ProducesResponseType(typeof(RequestOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
    public async Task<IActionResult> RequestOtp([FromBody] RequestOtpRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(new RequestOtpCommand(request), cancellationToken);

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    #endregion
}
