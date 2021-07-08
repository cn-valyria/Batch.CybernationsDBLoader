using Repository.DataObjects;
using System;

namespace Repository.DbContexts.Internal
{
    public class TodaysAllianceData
    {
        public int AllianceId { get; set; }
        public string Name { get; set; }
        public DateTime Updated { get; set; }
        public int TotalNations { get; set; }
        public int ActiveNations { get; set; }
        public int PercentActive { get; set; }
        public int Strength { get; set; }
        public int AverageStrength { get; set; }
        public decimal Score { get; set; }
        public int Land { get; set; }
        public int Infrastructure { get; set; }
        public int Technology { get; set; }
        public int War { get; set; }
        public int Peace { get; set; }
        public int Soldiers { get; set; }
        public int Tanks { get; set; }
        public int Cruise { get; set; }
        public int Nukes { get; set; }
        public int Aircraft { get; set; }
        public int Navy { get; set; }
        public int Anarchy { get; set; }
        public string FileName { get; set; }

        public TodaysAllianceData() { }

        public TodaysAllianceData(Alliance allianceEntity, string fileName)
        {
            AllianceId = allianceEntity.Id;
            Name = allianceEntity.Name;
            Updated = allianceEntity.Updated;
            TotalNations = allianceEntity.TotalNations;
            ActiveNations = allianceEntity.ActiveNations;
            PercentActive = allianceEntity.PercentActive;
            Strength = allianceEntity.Strength;
            AverageStrength = allianceEntity.AverageStrength;
            Score = allianceEntity.Score;
            Land = allianceEntity.Land;
            Infrastructure = allianceEntity.Infrastructure;
            Technology = allianceEntity.Technology;
            War = allianceEntity.War;
            Peace = allianceEntity.Peace;
            Soldiers = allianceEntity.Soldiers;
            Tanks = allianceEntity.Tanks;
            Cruise = allianceEntity.Cruise;
            Nukes = allianceEntity.Nukes;
            Aircraft = allianceEntity.Aircraft;
            Navy = allianceEntity.Navy;
            Anarchy = allianceEntity.Anarchy;
            FileName = fileName;
        }
    }
}
