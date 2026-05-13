using Devnet.Vault.Application.Features.Shared.FileUpload.Models;

namespace Devnet.Vault.Application.Features.Shared.FileUpload.Interfaces;

public interface IR2FileUploadService
{
    Task<string> UploadAsync(FileUploadTask uploadTask, CancellationToken cancellationToken = default);
}
