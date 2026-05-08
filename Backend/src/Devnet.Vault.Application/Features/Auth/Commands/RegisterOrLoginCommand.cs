using Devnet.Vault.Application.Features.Auth.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Commands;

/// <summary>
/// Command to register a new user or login existing user after OTP validation
/// </summary>
public record RegisterOrLoginCommand(RegisterOrLoginRequest Request) : IRequest<AuthResponse>;
