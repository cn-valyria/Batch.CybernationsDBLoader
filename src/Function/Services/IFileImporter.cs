namespace Function.Services;

public interface IFileImporter
{
    Task ImportNationsAsync(Stream fileBlob, string fileName);
    Task ImportAlliancesAsync(Stream fileBlob, string fileName);
    Task ImportAidAsync(Stream fileBlob, string fileName);
    Task ImportWarsAsync(Stream fileBlob, string fileName);
}