using Repository.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICnDbRepository
    {
        Task UpsertNations(IReadOnlyCollection<Nation> data, string dataFileName);
        Task UpsertWar(IReadOnlyCollection<War> data, string dataFileName);
        Task UpsertAid(IReadOnlyCollection<Aid> data, string dataFileName);
        Task UpsertAlliances(IReadOnlyCollection<Alliance> data, string dataFileName);
    }
}
