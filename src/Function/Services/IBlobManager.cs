using Microsoft.Azure.Storage.Blob;

namespace Function.Services;

public interface IBlobManager
{
    Task UploadFileAsync(CloudBlobContainer outputContainer, string fileName, Stream dataStream);
}