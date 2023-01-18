using Repository.DataObjects;
using Repository.DbContexts;
using Repository.DbContexts.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CnDbRepository : ICnDbRepository
    {
        private readonly ICybernationsDbContext _cnDbContext;

        public CnDbRepository(ICybernationsDbContext cnDbContext) => _cnDbContext = cnDbContext;

        public async Task UpsertNations(IReadOnlyCollection<Nation> data, string dataFileName)
        {
            // Load today's data into the temp table for storing this data
            await _cnDbContext.InsertTempData(data.Select(rec => new TodaysNationData(rec, dataFileName)).ToList());

            // Execute the proc that merges the data into the main table
            await _cnDbContext.ExecuteSqlCommand("CALL update_nation_data();");
        }

        public async Task UpsertWar(IReadOnlyCollection<War> data, string dataFileName)
        {
            // Load today's data into the temp table for quicker uploading
            await _cnDbContext.InsertTempData(data.Select(rec => new TodaysWarData(rec, dataFileName)).ToList());

            // Execute the proc that merges the data into the main table
            await _cnDbContext.ExecuteSqlCommand("CALL update_war_data();");
        }

        public async Task UpsertAid(IReadOnlyCollection<Aid> data, string dataFileName)
        {
            // Load today's data into the temp table for quicker uploading
            await _cnDbContext.InsertTempData(data.Select(rec => new TodaysAidData(rec, dataFileName)).ToList());

            // Execute the proc that merges the data into the main table
            await _cnDbContext.ExecuteSqlCommand("CALL update_aid_data();");

            // Sometimes aid is deleted before the CN file can report that it expired, so if we do nothing, they 
            // will end up showing as an un-expired status forever. This query will make sure that these missed 
            // aid records get their status updated correctly whenever this happens
            await _cnDbContext.ExecuteSqlCommand(@"
update aid
set status = 4
where date < curdate() - interval 10 day
and status <> 4;");
        }

        public async Task UpsertAlliances(IReadOnlyCollection<Alliance> data, string dataFileName)
        {
            // Load today's data into the temp table for quicker uploading
            await _cnDbContext.InsertTempData(data.Select(rec => new TodaysAllianceData(rec, dataFileName)).ToList());

            // Execute the proc that merges the data into the main table
            await _cnDbContext.ExecuteSqlCommand("CALL update_alliance_data();");
        }
    }
}
