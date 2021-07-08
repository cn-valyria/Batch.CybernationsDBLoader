using Repository.DataObjects;
using System;

namespace Repository.DbContexts.Internal
{
    public class TodaysWarData
    {
        public int WarId { get; set; }
        public int AttackingNationId { get; set; }
        public int AttackingAllianceId { get; set; }
        public int AttackingTeam { get; set; }
        public int DefendingNationId { get; set; }
        public int DefendingAllianceId { get; set; }
        public int DefendingTeam { get; set; }
        public int WarStatus { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public decimal Destruction { get; set; }
        public int AttackPercent { get; set; }
        public int DefendPercent { get; set; }
        public string FileName { get; set; }

        public TodaysWarData() { }

        public TodaysWarData(War warEntity, string fileName)
        {
            WarId = warEntity.Id;
            AttackingNationId = warEntity.AttackingNationId;
            AttackingAllianceId = warEntity.AttackingAllianceId;
            AttackingTeam = (int)warEntity.AttackingTeam;
            DefendingNationId = warEntity.DefendingNationId;
            DefendingAllianceId = warEntity.DefendingAllianceId;
            DefendingTeam = (int)warEntity.DefendingTeam;
            WarStatus = (int)warEntity.WarStatus;
            BeginDate = warEntity.BeginDate;
            EndDate = warEntity.EndDate;
            Reason = warEntity.Reason;
            Destruction = warEntity.Destruction;
            AttackPercent = warEntity.AttackPercent;
            DefendPercent = warEntity.DefendPercent;
            FileName = fileName;
        }
    }
}
