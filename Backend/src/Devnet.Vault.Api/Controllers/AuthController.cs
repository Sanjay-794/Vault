using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
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
    /// Request OTP for authentication (Step 1)
    /// </summary>
    [HttpPost("request-otp")]
    [ProducesResponseType(typeof(RequestOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RequestOtp(
        [FromBody] RequestOtpRequest request,
        CancellationToken cancellationToken)
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
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }

    /// <summary>
    /// Register or login user after OTP validation (Step 2)
    /// </summary>
    [HttpPost("register-or-login")]
    [HttpPost("login-with-otp")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginWithOtp(
        [FromBody] RegisterOrLoginRequest request,
        CancellationToken cancellationToken)
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
            Response.Cookies.Append("AccessToken", response.AccessToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(response.ExpiryMinutes)
            });

            Response.Cookies.Append("RefreshToken", response.RefreshToken, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", error = ex.Message });
        }
    }
}
