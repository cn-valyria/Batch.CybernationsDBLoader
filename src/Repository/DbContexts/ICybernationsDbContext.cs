using Microsoft.EntityFrameworkCore;
using Repository.DataObjects;
using Repository.DbContexts.Internal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.DbContexts
{
    public interface ICybernationsDbContext
    {
        DbSet<Nation> Nations { get; set; }

        Task InsertTempData(IReadOnlyCollection<TodaysNationData> todaysNationData);
        Task InsertTempData(IReadOnlyCollection<TodaysWarData> todaysWarData);
        Task InsertTempData(IReadOnlyCollection<TodaysAidData> todaysAidData);
        Task InsertTempData(IReadOnlyCollection<TodaysAllianceData> todaysAllianceData);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task ExecuteSqlCommand(string sql, params object[] parameters);
    }
}
