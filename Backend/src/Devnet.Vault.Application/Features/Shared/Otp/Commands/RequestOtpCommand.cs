using Devnet.Vault.Application.Features.Shared.Otp.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Shared.Otp.Commands;

/// <summary>
/// Command to request OTP for authentication
/// </summary>
public record RequestOtpCommand(RequestOtpRequest Request) : IRequest<RequestOtpResponse>;
