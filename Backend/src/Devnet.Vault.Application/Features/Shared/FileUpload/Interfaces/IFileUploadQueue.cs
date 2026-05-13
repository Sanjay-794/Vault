using Devnet.Vault.Application.Features.Shared.FileUpload.Models;

namespace Devnet.Vault.Application.Features.Shared.FileUpload.Interfaces;

public interface IFileUploadQueue
{
    void Enqueue(FileUploadTask uploadTask);

    Task<FileUploadTask> DequeueAsync(CancellationToken cancellationToken);
}
