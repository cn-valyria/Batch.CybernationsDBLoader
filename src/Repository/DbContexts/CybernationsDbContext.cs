using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.DataObjects;
using Repository.DbContexts.Internal;
using Repository.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DbContexts
{
    public class CybernationsDbContext : DbContext, ICybernationsDbContext
    {
        private readonly string _connectionString;

        public DbSet<Nation> Nations { get; set; }
        private DbSet<TodaysNationData> TodaysNationData { get; set; }
        private DbSet<TodaysWarData> TodaysWarData { get; set; }
        private DbSet<TodaysAidData> TodaysAidData { get; set; }
        private DbSet<TodaysAllianceData> TodaysAllianceData { get; set; }

        public CybernationsDbContext(IOptions<FunctionOptions> options) => _connectionString = options.Value.DbConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Nation>(entity =>
            {
                entity.ToTable("nation");
                entity.HasKey(e => e.NationId);
                entity.Property(e => e.NationId).HasColumnName("id");
                entity.Property(e => e.NationName).HasColumnName("nation_name");
                entity.Property(e => e.RulerName).HasColumnName("ruler_name");
                entity.Property(e => e.AllianceId).HasColumnName("alliance_id");
                entity.Property(e => e.AllianceStatus).HasColumnName("alliance_status");
                entity.Property(e => e.AllianceDate).HasColumnName("alliance_date");
                entity.Property(e => e.GovernmentType).HasColumnName("government_type");
                entity.Property(e => e.Religion).HasColumnName("religion");
                entity.Property(e => e.Team).HasColumnName("team");
                entity.Property(e => e.Created).HasColumnName("created");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.Infrastructure).HasColumnName("infrastructure");
                entity.Property(e => e.BaseLand).HasColumnName("base_land");
                entity.Property(e => e.WarStatus).HasColumnName("war_status");
                entity.Property(e => e.Resource1).HasColumnName("resource_1");
                entity.Property(e => e.Resource2).HasColumnName("resource_2");
                entity.Property(e => e.Votes).HasColumnName("votes");
                entity.Property(e => e.Strength).HasColumnName("strength");
                entity.Property(e => e.Defcon).HasColumnName("defcon");
                entity.Property(e => e.BaseSoldiers).HasColumnName("base_soldiers");
                entity.Property(e => e.Tanks).HasColumnName("tanks");
                entity.Property(e => e.CruiseMissiles).HasColumnName("cruise_missiles");
                entity.Property(e => e.Nukes).HasColumnName("nukes");
                entity.Property(e => e.RecentActivity).HasColumnName("recent_activity");
                entity.Property(e => e.ConnectedResource1).HasColumnName("connected_resource_1");
                entity.Property(e => e.ConnectedResource2).HasColumnName("connected_resource_2");
                entity.Property(e => e.ConnectedResource3).HasColumnName("connected_resource_3");
                entity.Property(e => e.ConnectedResource4).HasColumnName("connected_resource_4");
                entity.Property(e => e.ConnectedResource5).HasColumnName("connected_resource_5");
                entity.Property(e => e.ConnectedResource6).HasColumnName("connected_resource_6");
                entity.Property(e => e.ConnectedResource7).HasColumnName("connected_resource_7");
                entity.Property(e => e.ConnectedResource8).HasColumnName("connected_resource_8");
                entity.Property(e => e.ConnectedResource9).HasColumnName("connected_resource_9");
                entity.Property(e => e.ConnectedResource10).HasColumnName("connected_resource_10");
                entity.Property(e => e.UpdatedBy).HasColumnName("audit_updated_by");
                entity.Property(e => e.UpdatedOn).HasColumnName("audit_updated_on");
            });

            modelBuilder.Entity<War>(entity =>
            {
                entity.ToTable("war");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AttackingNationId).HasColumnName("attacking_nation_id");
                entity.Property(e => e.AttackingAllianceId).HasColumnName("attacking_alliance_id");
                entity.Property(e => e.AttackingTeam).HasColumnName("attacking_team");
                entity.Property(e => e.DefendingNationId).HasColumnName("defending_nation_id");
                entity.Property(e => e.DefendingAllianceId).HasColumnName("defending_alliance_id");
                entity.Property(e => e.DefendingTeam).HasColumnName("defending_team");
                entity.Property(e => e.WarStatus).HasColumnName("war_status");
                entity.Property(e => e.BeginDate).HasColumnName("begin_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.Destruction).HasColumnName("destruction");
                entity.Property(e => e.AttackPercent).HasColumnName("attack_percent");
                entity.Property(e => e.DefendPercent).HasColumnName("defend_percent");
                entity.Property(e => e.UpdatedBy).HasColumnName("audit_updated_by");
                entity.Property(e => e.UpdatedOn).HasColumnName("audit_updated_on");
            });

            modelBuilder.Entity<Aid>(entity =>
            {
                entity.ToTable("aid");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("aid_id");
                entity.Property(e => e.SendingNationId).HasColumnName("sending_nation_id");
                entity.Property(e => e.SendingAllianceId).HasColumnName("sending_alliance_id");
                entity.Property(e => e.SendingTeam).HasColumnName("sending_team");
                entity.Property(e => e.ReceivingNationId).HasColumnName("receiving_nation_id");
                entity.Property(e => e.ReceivingAllianceId).HasColumnName("receiving_alliance_id");
                entity.Property(e => e.ReceivingTeam).HasColumnName("receiving_team");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Money).HasColumnName("money");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.Soldiers).HasColumnName("soldiers");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.UpdatedBy).HasColumnName("audit_updated_by");
                entity.Property(e => e.UpdatedOn).HasColumnName("audit_updated_on");
            });

            modelBuilder.Entity<Alliance>(entity =>
            {
                entity.ToTable("alliance");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Updated).HasColumnName("updated");
                entity.Property(e => e.TotalNations).HasColumnName("total_nations");
                entity.Property(e => e.ActiveNations).HasColumnName("active_nations");
                entity.Property(e => e.PercentActive).HasColumnName("percent_active");
                entity.Property(e => e.Strength).HasColumnName("strength");
                entity.Property(e => e.AverageStrength).HasColumnName("average_strength");
                entity.Property(e => e.Score).HasColumnName("score");
                entity.Property(e => e.Land).HasColumnName("land");
                entity.Property(e => e.Infrastructure).HasColumnName("infrastructure");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.War).HasColumnName("war");
                entity.Property(e => e.Peace).HasColumnName("peace");
                entity.Property(e => e.Soldiers).HasColumnName("soldiers");
                entity.Property(e => e.Tanks).HasColumnName("tanks");
                entity.Property(e => e.Cruise).HasColumnName("cruise");
                entity.Property(e => e.Nukes).HasColumnName("nukes");
                entity.Property(e => e.Aircraft).HasColumnName("aircraft");
                entity.Property(e => e.Navy).HasColumnName("navy");
                entity.Property(e => e.Anarchy).HasColumnName("anarchy");
                entity.Property(e => e.UpdatedBy).HasColumnName("audit_updated_by");
                entity.Property(e => e.UpdatedOn).HasColumnName("audit_updated_on");
            });

            #region Internal Tables

            modelBuilder.Entity<TodaysNationData>(entity =>
            {
                entity.ToTable("todays_file_nation");
                entity.HasKey(e => e.NationId);
                entity.Property(e => e.NationId).HasColumnName("nation_id");
                entity.Property(e => e.NationName).HasColumnName("nation_name");
                entity.Property(e => e.RulerName).HasColumnName("ruler_name");
                entity.Property(e => e.AllianceId).HasColumnName("alliance_id");
                entity.Property(e => e.AllianceStatus).HasColumnName("alliance_status");
                entity.Property(e => e.AllianceDate).HasColumnName("alliance_date");
                entity.Property(e => e.GovernmentType).HasColumnName("government_type");
                entity.Property(e => e.Religion).HasColumnName("religion");
                entity.Property(e => e.Team).HasColumnName("team");
                entity.Property(e => e.Created).HasColumnName("created");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.Infrastructure).HasColumnName("infrastructure");
                entity.Property(e => e.BaseLand).HasColumnName("base_land");
                entity.Property(e => e.WarStatus).HasColumnName("war_status");
                entity.Property(e => e.Resource1).HasColumnName("resource_1");
                entity.Property(e => e.Resource2).HasColumnName("resource_2");
                entity.Property(e => e.Votes).HasColumnName("votes");
                entity.Property(e => e.Strength).HasColumnName("strength");
                entity.Property(e => e.Defcon).HasColumnName("defcon");
                entity.Property(e => e.BaseSoldiers).HasColumnName("base_soldiers");
                entity.Property(e => e.Tanks).HasColumnName("tanks");
                entity.Property(e => e.CruiseMissiles).HasColumnName("cruise_missiles");
                entity.Property(e => e.Nukes).HasColumnName("nukes");
                entity.Property(e => e.RecentActivity).HasColumnName("recent_activity");
                entity.Property(e => e.ConnectedResource1).HasColumnName("connected_resource_1");
                entity.Property(e => e.ConnectedResource2).HasColumnName("connected_resource_2");
                entity.Property(e => e.ConnectedResource3).HasColumnName("connected_resource_3");
                entity.Property(e => e.ConnectedResource4).HasColumnName("connected_resource_4");
                entity.Property(e => e.ConnectedResource5).HasColumnName("connected_resource_5");
                entity.Property(e => e.ConnectedResource6).HasColumnName("connected_resource_6");
                entity.Property(e => e.ConnectedResource7).HasColumnName("connected_resource_7");
                entity.Property(e => e.ConnectedResource8).HasColumnName("connected_resource_8");
                entity.Property(e => e.ConnectedResource9).HasColumnName("connected_resource_9");
                entity.Property(e => e.ConnectedResource10).HasColumnName("connected_resource_10");
                entity.Property(e => e.FileName).HasColumnName("file_name");
            });

            modelBuilder.Entity<TodaysWarData>(entity =>
            {
                entity.ToTable("todays_file_war");
                entity.HasKey(e => e.WarId);
                entity.Property(e => e.WarId).HasColumnName("war_id");
                entity.Property(e => e.AttackingNationId).HasColumnName("attacking_nation_id");
                entity.Property(e => e.AttackingAllianceId).HasColumnName("attacking_alliance_id");
                entity.Property(e => e.AttackingTeam).HasColumnName("attacking_team");
                entity.Property(e => e.DefendingNationId).HasColumnName("defending_nation_id");
                entity.Property(e => e.DefendingAllianceId).HasColumnName("defending_alliance_id");
                entity.Property(e => e.DefendingTeam).HasColumnName("defending_team");
                entity.Property(e => e.WarStatus).HasColumnName("war_status");
                entity.Property(e => e.BeginDate).HasColumnName("begin_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.Destruction).HasColumnName("destruction");
                entity.Property(e => e.AttackPercent).HasColumnName("attack_percent");
                entity.Property(e => e.DefendPercent).HasColumnName("defend_percent");
                entity.Property(e => e.FileName).HasColumnName("file_name");
            });

            modelBuilder.Entity<TodaysAidData>(entity =>
            {
                entity.ToTable("todays_file_aid");
                entity.HasKey(e => e.AidId);
                entity.Property(e => e.AidId).HasColumnName("aid_id");
                entity.Property(e => e.SendingNationId).HasColumnName("sending_nation_id");
                entity.Property(e => e.SendingAllianceId).HasColumnName("sending_alliance_id");
                entity.Property(e => e.SendingTeam).HasColumnName("sending_team");
                entity.Property(e => e.ReceivingNationId).HasColumnName("receiving_nation_id");
                entity.Property(e => e.ReceivingAllianceId).HasColumnName("receiving_alliance_id");
                entity.Property(e => e.ReceivingTeam).HasColumnName("receiving_team");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Money).HasColumnName("money");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.Soldiers).HasColumnName("soldiers");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.Reason).HasColumnName("reason");
                entity.Property(e => e.FileName).HasColumnName("file_name");
            });

            modelBuilder.Entity<TodaysAllianceData>(entity =>
            {
                entity.ToTable("todays_file_alliance");
                entity.HasKey(e => e.AllianceId);
                entity.Property(e => e.AllianceId).HasColumnName("alliance_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Updated).HasColumnName("updated");
                entity.Property(e => e.TotalNations).HasColumnName("total_nations");
                entity.Property(e => e.ActiveNations).HasColumnName("active_nations");
                entity.Property(e => e.PercentActive).HasColumnName("percent_active");
                entity.Property(e => e.Strength).HasColumnName("strength");
                entity.Property(e => e.AverageStrength).HasColumnName("average_strength");
                entity.Property(e => e.Score).HasColumnName("score");
                entity.Property(e => e.Land).HasColumnName("land");
                entity.Property(e => e.Infrastructure).HasColumnName("infrastructure");
                entity.Property(e => e.Technology).HasColumnName("technology");
                entity.Property(e => e.War).HasColumnName("war");
                entity.Property(e => e.Peace).HasColumnName("peace");
                entity.Property(e => e.Soldiers).HasColumnName("soldiers");
                entity.Property(e => e.Tanks).HasColumnName("tanks");
                entity.Property(e => e.Cruise).HasColumnName("cruise");
                entity.Property(e => e.Nukes).HasColumnName("nukes");
                entity.Property(e => e.Aircraft).HasColumnName("aircraft");
                entity.Property(e => e.Navy).HasColumnName("navy");
                entity.Property(e => e.Anarchy).HasColumnName("anarchy");
                entity.Property(e => e.FileName).HasColumnName("file_name");
            });

            #endregion
        }

        public async Task InsertTempData(IReadOnlyCollection<TodaysNationData> todaysNationData)
        {
            // Truncate the table since it's only for temp data
            await Database.ExecuteSqlRawAsync("TRUNCATE TABLE todays_file_nation;");

            // Turn of change detection to speed up EF performance
            ChangeTracker.AutoDetectChangesEnabled = false;

            // Add the data and save
            TodaysNationData.AddRange(todaysNationData);
            await SaveChangesAsync();
        }

        public async Task InsertTempData(IReadOnlyCollection<TodaysWarData> todaysWarData)
        {
            // Truncate the table since it's only for temp data
            await Database.ExecuteSqlRawAsync("TRUNCATE TABLE todays_file_war;");

            // Turn of change detection to speed up EF performance
            ChangeTracker.AutoDetectChangesEnabled = false;

            // Add the data and save
            TodaysWarData.AddRange(todaysWarData);
            await SaveChangesAsync();
        }

        public async Task InsertTempData(IReadOnlyCollection<TodaysAidData> todaysAidData)
        {
            // Truncate the table since it's only for temp data
            await Database.ExecuteSqlRawAsync("TRUNCATE TABLE todays_file_aid;");

            // Turn of change detection to speed up EF performance
            ChangeTracker.AutoDetectChangesEnabled = false;

            // Add the data and save
            TodaysAidData.AddRange(todaysAidData);
            await SaveChangesAsync();
        }

        public async Task InsertTempData(IReadOnlyCollection<TodaysAllianceData> todaysAllianceData)
        {
            // Truncate the table since it's only for temp data
            await Database.ExecuteSqlRawAsync("TRUNCATE TABLE todays_file_alliance;");

            // Turn of change detection to speed up EF performance
            ChangeTracker.AutoDetectChangesEnabled = false;

            // Add the data and save
            TodaysAllianceData.AddRange(todaysAllianceData);
            await SaveChangesAsync();
        }

        public async Task ExecuteSqlCommand(string sql, params object[] parameters)
        {
            await Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
