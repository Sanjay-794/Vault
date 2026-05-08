using Devnet.Vault.Application.Features.Auth.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Commands;

/// <summary>
/// Command to logout a user by revoking their current session
/// </summary>
public record LogoutCommand(long UserId, string RefreshToken) : IRequest<LogoutResponse>;
