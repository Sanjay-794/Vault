using Devnet.Vault.Application.Features.Auth.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Commands;

/// <summary>
/// Command to refresh the access token using a refresh token
/// </summary>
public record RefreshTokenCommand(RefreshTokenRequest Request, long UserIdClaim) : IRequest<AuthResponse>;
