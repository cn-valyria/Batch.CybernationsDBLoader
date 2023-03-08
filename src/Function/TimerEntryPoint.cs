using Function.Services;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Repository.Grabbers;
using Repository.Infrastructure;

namespace Function;

public class TimerEntryPoint
{
    private readonly IDataGrabber _cnFileGrabber;
    private readonly IFileImporter _cnFileImporter;
    private readonly IBlobManager _blobManager;

    public TimerEntryPoint(IDataGrabber cnFileGrabber, IFileImporter cnFileImporter, IBlobManager blobManager) 
        => (_cnFileGrabber, _cnFileImporter, _blobManager) = (cnFileGrabber, cnFileImporter, blobManager);

    [FunctionName(nameof(ImportAllianceFile))]
    public async Task ImportAllianceFile(
        [TimerTrigger("0 0 1,13 * * *")] TimerInfo myTimer,
        [Blob("alliances", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(ImportAllianceFile)} function started execution at: {DateTime.Now}");

        await RunCnFileImportProcess(CnFileType.Alliances, outputContainer, _cnFileImporter.ImportAlliancesAsync, log);

        log.LogInformation($"{nameof(ImportAllianceFile)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(ImportNationsFile))]
    public async Task ImportNationsFile(
        [TimerTrigger("0 5 1,13 * * *")] TimerInfo myTimer,
        [Blob("nations", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(ImportNationsFile)} function started execution at: {DateTime.Now}");

        await RunCnFileImportProcess(CnFileType.Nations, outputContainer, _cnFileImporter.ImportNationsAsync, log);

        log.LogInformation($"{nameof(ImportNationsFile)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(ImportAidFile))]
    public async Task ImportAidFile(
        [TimerTrigger("0 10 1,13 * * *")] TimerInfo myTimer,
        [Blob("aid", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(ImportAidFile)} function started execution at: {DateTime.Now}");

        await RunCnFileImportProcess(CnFileType.Aid, outputContainer, _cnFileImporter.ImportAidAsync, log);

        log.LogInformation($"{nameof(ImportAidFile)} function completed execution at: {DateTime.Now}");
    }

    [FunctionName(nameof(ImportWarFile))]
    public async Task ImportWarFile(
        [TimerTrigger("0 15 1,13 * * *")] TimerInfo myTimer,
        [Blob("war", Connection = "AzureWebJobsStorage")] CloudBlobContainer outputContainer,
        ILogger log)
    {
        log.LogInformation($"{nameof(ImportWarFile)} function started execution at: {DateTime.Now}");

        await RunCnFileImportProcess(CnFileType.War, outputContainer, _cnFileImporter.ImportWarsAsync, log);

        log.LogInformation($"{nameof(ImportWarFile)} function completed execution at: {DateTime.Now}");
    }

    private async Task RunCnFileImportProcess(
        CnFileType fileType,
        CloudBlobContainer outputContainer,
        Func<Stream, string, Task> fileImporterFunc,
        ILogger log)
    {
        var (fileName, dataStream) = await _cnFileGrabber.GetTodaysFileAsync(fileType, log);
        log.LogInformation($"{fileName}.txt downloaded from CN.");

        // Upload the file first. If anything goes wrong with the import, we want the file preserved in blob storage
        await _blobManager.UploadFileAsync(outputContainer, fileName, await dataStream.CopyAsync());
        log.LogInformation($"{fileName}.txt uploaded to {outputContainer.Name} Azure blob");

        await fileImporterFunc(await dataStream.CopyAsync(), fileName);
        log.LogInformation($"{fileName}.txt successfully imported to cybernations_db");
    }
}
