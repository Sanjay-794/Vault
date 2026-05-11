using Devnet.Vault.Application.Features.Account.DTOs;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Account.Queries;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Handlers;

public class GetProfileDetailsQueryHandler(IUserRepository _userRepository) : IRequestHandler<GetProfileDetailsQuery, ProfileDetailsResponse>
{
    public async Task<ProfileDetailsResponse> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        var userDetails = await _userRepository.GetUserDetailsByUserIdAsync(request.UserId, cancellationToken);

        return userDetails == null
            ? throw new InvalidOperationException(UserInfoMessages.USER_NOT_FOUND)
            : new ProfileDetailsResponse
            {
                Name = userDetails.Name,
                Email = userDetails.Email,
                PhoneNumber = userDetails.PhoneNumber,
                CountryName = userDetails.Country?.CountryName,
                CountryCallingCode = userDetails.Country?.CountryCallingCode,
                RoleName = userDetails.Role?.RoleName,
                LastLoginDate = userDetails.LastLoginDate
            };
    }
}
