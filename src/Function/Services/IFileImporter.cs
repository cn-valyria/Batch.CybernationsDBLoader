namespace Function.Services;

public interface IFileImporter
{
    Task ImportAidAsync(Stream fileBlob, string fileName);
}