using Devnet.Vault.Application.Features.Account.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Account.Commands;

public record UpdateProfileDetailsCommand(UpdateProfileDetailsRequest Request, long UserId, long UpdatedBy) : IRequest<bool>;

public record UpdateUserEmailAddressCommand(UpdateEmailAddressRequest Request, long UserId, long UpdatedBy) : IRequest<bool>;

public record UpdateUserPhoneNumberCommand(UpdatePhoneNumberRequest Request, long UserId, long UpdatedBy) : IRequest<bool>;

public record DeactivateAccountCommand(DeactivateAccountRequest Request, long UserId, long DeactivatedBy) : IRequest<bool>;

public record DeleteAccountCommand(DeleteAccountRequest Request, long UserId, long DeletedBy) : IRequest<bool>;