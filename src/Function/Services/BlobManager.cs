using Microsoft.Azure.Storage.Blob;

namespace Function.Services;

public class BlobManager : IBlobManager
{
    public async Task UploadFileAsync(CloudBlobContainer outputContainer, string fileName, Stream dataStream)
    {
        await outputContainer.CreateIfNotExistsAsync();
        var cloudBlockBlob = outputContainer.GetBlockBlobReference($"{fileName}.txt");
        await cloudBlockBlob.UploadFromStreamAsync(dataStream);
    }
}