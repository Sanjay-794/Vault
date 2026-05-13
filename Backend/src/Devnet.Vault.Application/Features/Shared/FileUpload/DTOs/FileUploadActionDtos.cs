using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Shared.FileUpload.DTOs;


public record UploadStatusResponse(Guid UploadId, string FileName, FileUploadStatus Status, string? FileKey, string? ErrorMessage, DateTime CreatedAt, DateTime? CompletedAt = null);
