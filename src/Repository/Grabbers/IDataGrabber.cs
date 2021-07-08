using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Repository.Grabbers
{
    public interface IDataGrabber
    {
        Task<(string FileName, Stream DataStream)> GetTodaysFileAsync(CnFileType fileType, ILogger logger);
    }
}
