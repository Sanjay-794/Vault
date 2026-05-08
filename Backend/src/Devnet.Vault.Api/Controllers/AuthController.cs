using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
using Devnet.Vault.Domain.Constants.AppSettings;
using Devnet.Vault.Domain.Constants.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Devnet.Vault.Api.Controllers;

/// <summary>
/// Authentication API endpoints using CQRS pattern
/// Handles OTP-based registration and login
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Request OTP for authentication
    /// </summary>
    [HttpPost(ApiEndpoints.AuthApiEndpoints.REQUEST_AUTH_OTP_ENDPOINT)]
    [ProducesResponseType(typeof(RequestOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RequestOtp([FromBody] RequestOtpRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new RequestOtpCommand(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Register or login user after OTP validation
    /// </summary>
    [HttpPost(ApiEndpoints.AuthApiEndpoints.LOGIN_WITH_OTP_ENDPOINT)]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginWithOtp([FromBody] RegisterOrLoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Identifier) ||
                string.IsNullOrWhiteSpace(request.Otp))
            {
                return BadRequest(new { message = "Identifier and OTP are required" });
            }

            // Get client IP if not provided
            var ipAddress = request.IpAddress ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            var modifiedRequest = request with { IpAddress = ipAddress };

            var command = new RegisterOrLoginCommand(modifiedRequest);
            var response = await _mediator.Send(command, cancellationToken);

            // Set secure cookies for tokens
            Response.Cookies.Append(AppConstants.APP_ACCESS_TOKEN_NAME, response.AccessToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(response.ExpiryMinutes)
            });

            Response.Cookies.Append(AppConstants.APP_REFRESH_TOKEN_NAME, response.RefreshToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(AppTimes.REFRESH_TOKEN_EXPIRY_TIME_IN_DAYS)
            });

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
