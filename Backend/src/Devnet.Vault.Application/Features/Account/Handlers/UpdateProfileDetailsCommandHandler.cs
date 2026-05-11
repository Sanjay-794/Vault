using Devnet.Vault.Application.Features.Account.Commands;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Domain.Constants.Messages;
using MediatR;

namespace Devnet.Vault.Application.Features.Account.Handlers;

public class UpdateProfileDetailsCommandHandler(IUserRepository _userRepository) : IRequestHandler<UpdateProfileDetailsCommand, bool>
{
    public async Task<bool> Handle(UpdateProfileDetailsCommand request, CancellationToken cancellationToken)
    {
        var req = request?.Request;
        if (request == null || req == null)
            throw new InvalidOperationException(ExceptionMessages.GENERIC_NULL_ERROR);

        var url = req?.ProfilePicture?.FileName ?? string.Empty; // TO DO : Replace with image url stored on cloud
        var success = await _userRepository.UpdateUserDetailsAsync(request.UserId, request.UpdatedBy, url, req?.Name, cancellationToken);

        return success;
    }
}