using Amazon.S3;
using Amazon.S3.Model;
using Devnet.Vault.Application.Configurations;
using Devnet.Vault.Application.Features.Shared.FileUpload.Interfaces;
using Devnet.Vault.Application.Features.Shared.FileUpload.Models;
using Microsoft.Extensions.Options;

namespace Devnet.Vault.Infrastructure.Storage.CloudFareR2.Services;

public class R2FileUploadService(AmazonS3Client _r2Client, IOptions<CloudFareR2Settings> _options) : IR2FileUploadService
{

    private readonly CloudFareR2Settings _r2Settings = _options.Value;
    private const int MaxRetryCount = 3;

    public async Task<string> UploadAsync(FileUploadTask uploadTask, CancellationToken cancellationToken = default)
    {
        var sanitizedFileName = Path.GetFileName(uploadTask.FileName);

        var fileKey = $"uploads/{uploadTask.UploadId}/{sanitizedFileName}";

        var retryCount = 0;

        while (true)
        {
            try
            {
                await using var stream =
                    uploadTask.File.OpenReadStream();

                var request = new PutObjectRequest
                {
                    BucketName = _r2Settings.BucketName,
                    Key = fileKey,
                    InputStream = stream,
                    ContentType = uploadTask.ContentType
                };

                await _r2Client.PutObjectAsync(
                    request,
                    cancellationToken);

                return fileKey;
            }
            catch
            {
                retryCount++;

                if (retryCount >= MaxRetryCount)
                {
                    throw;
                }

                var delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));

                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}