using Repository.DataObjects;
using System;

namespace Repository.DbContexts.Internal
{
    public class TodaysAidData
    {
        public int AidId { get; set; }
        public int SendingNationId { get; set; }
        public int SendingAllianceId { get; set; }
        public int SendingTeam { get; set; }
        public int ReceivingNationId { get; set; }
        public int ReceivingAllianceId { get; set; }
        public int ReceivingTeam { get; set; }
        public int Status { get; set; }
        public int Money { get; set; }
        public int Technology { get; set; }
        public int Soldiers { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string FileName { get; set; }

        public TodaysAidData() { }

        public TodaysAidData(Aid aidEntity, string fileName)
        {
            AidId = aidEntity.Id;
            SendingNationId = aidEntity.SendingNationId;
            SendingAllianceId = aidEntity.SendingAllianceId;
            SendingTeam = (int)aidEntity.SendingTeam;
            ReceivingNationId = aidEntity.ReceivingNationId;
            ReceivingAllianceId = aidEntity.ReceivingAllianceId;
            ReceivingTeam = (int)aidEntity.ReceivingTeam;
            Status = (int)aidEntity.Status;
            Money = aidEntity.Money;
            Technology = aidEntity.Technology;
            Soldiers = aidEntity.Soldiers;
            Date = aidEntity.Date;
            Reason = aidEntity.Reason;
            FileName = fileName;
        }
    }
}
