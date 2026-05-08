using Devnet.Vault.Application.Features.Auth.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Commands;

/// <summary>
/// Command to request OTP for authentication
/// </summary>
public record RequestOtpCommand(RequestOtpRequest Request) : IRequest<RequestOtpResponse>;
