using Devnet.Vault.Application.Features.Groups.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.Groups.Commands;

public record CreateGroupCommand(CreateGroupRequest Request, long UserId) : IRequest<long>;
public record UpdateGroupNameCommand(UpdateGroupNameRequest Request, long GroupOwnerId, long UserId) : IRequest<bool>;
public record UpdateGroupFavouriteCommand(UpdateGroupFavouriteRequest Request, long GroupOwnerId, long UserId) : IRequest<bool>;
public record UpdateGroupParentCommand(UpdateGroupParentRequest Request, long GroupOwnerId, long UserId) : IRequest<bool>;
public record UpdateGroupMetaDataJsonCommand(UpdateGroupMetaDataJsonRequest Request, long GroupOwnerId, long UserId) : IRequest<bool>;
public record DeleteGroupCommand(DeleteGroupRequest Request, long GroupOwnerId, long UserId) : IRequest<bool>;
