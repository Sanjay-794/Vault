using Devnet.Vault.Api.Extensions;
using Devnet.Vault.Application.Features.Account.Commands;
using Devnet.Vault.Application.Features.Account.DTOs;
using Devnet.Vault.Application.Features.Account.Queries;
using Devnet.Vault.Application.Features.Shared.Otp.Commands;
using Devnet.Vault.Application.Features.Shared.Otp.DTOs;
using Devnet.Vault.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;
using static Devnet.Vault.Domain.Constants.Routes.ApiEndpoints;

namespace Devnet.Vault.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Get current user's profile details
    /// </summary>
    [HttpGet(AccountApiEndpoints.USER_PROFILE_ENDPOINT)]
    [ProducesResponseType(typeof(ProfileDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfileDetails(CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            var response = await _mediator.Send(new GetProfileDetailsQuery(userId), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update current user's profile details
    /// </summary>
    [HttpPut(AccountApiEndpoints.USER_PROFILE_UPDATE_ENDPOINT)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProfileDetails([FromBody] UpdateProfileDetailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            var response = await _mediator.Send(new UpdateProfileDetailsCommand(request, userId, userId), cancellationToken);
            if (response)
                return Ok(response);
            return BadRequest(new { message = ProfileMessages.PROFILE_UPDATE_FAILED });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Request OTP for account deactivation
    /// </summary>
    [HttpPost(AccountApiEndpoints.DEACTIVATE_OTP_REQUEST_ENDPOINT)]
    [ProducesResponseType(typeof(RequestOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RequestDeactivationOtp(CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            // Get user contact details for OTP
            var userDetails = await _mediator.Send(new GetProfileDetailsQuery(userId), cancellationToken);
            if (userDetails == null)
                return NotFound(UserInfoMessages.USER_NOT_FOUND);

            var otpRequest = new RequestOtpRequest
            {
                Identifier = userDetails.Email ?? userDetails.PhoneNumber ?? throw new InvalidOperationException(OtpValidationMessages.CHANNEL_VALUE_NULL),
                ChannelType = userDetails.Email == null ? NotificationChannel.Email : NotificationChannel.SMS,
                Purpose = OtpPurpose.AccountDeactivation
            };

            var response = await _mediator.Send(new RequestOtpCommand(otpRequest), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deactivate current user's account
    /// </summary>
    [HttpPost(AccountApiEndpoints.DEACTIVATE_ACCOUNT_ENDPOINT)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            var response = await _mediator.Send(new DeactivateAccountCommand(request, userId, userId), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Request OTP for account deletion
    /// </summary>
    [HttpPost(AccountApiEndpoints.DELETE_ACCOUNT_OTP_REQUEST_ENDPOINT)]
    [ProducesResponseType(typeof(RequestOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RequestDeletionOtp(CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            // Get user contact details for OTP
            var userDetails = await _mediator.Send(new GetProfileDetailsQuery(userId), cancellationToken);
            if (userDetails == null)
                return NotFound(UserInfoMessages.USER_NOT_FOUND);

            var otpRequest = new RequestOtpRequest
            {
                Identifier = userDetails.Email ?? userDetails.PhoneNumber ?? throw new InvalidOperationException(OtpValidationMessages.CHANNEL_VALUE_NULL),
                ChannelType = userDetails.Email == null ? NotificationChannel.Email : NotificationChannel.SMS,
                Purpose = OtpPurpose.AccountDeletion
            };

            var response = await _mediator.Send(new RequestOtpCommand(otpRequest), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Permanently delete current user's account
    /// </summary>
    [HttpPost(AccountApiEndpoints.DELETE_ACCOUNT_ENDPOINT)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = HttpContext.GetUserId();
            if (userId <= 0)
                return Unauthorized(new { message = UserInfoMessages.REQUEST_USER_ID_INVALID });

            var response = await _mediator.Send(new DeleteAccountCommand(request, userId, userId), cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
