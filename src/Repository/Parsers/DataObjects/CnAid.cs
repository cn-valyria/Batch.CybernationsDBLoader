using CsvHelper.Configuration.Attributes;
using System;

namespace Repository.Parsers.DataObjects
{
    public class CnAid
    {
        [Name("Declaring ID")]
        public int DeclaringId { get; set; }

        [Name("Declaring Ruler")]
        public string DeclaringRuler { get; set; }

        [Name("Declaring Nation")]
        public string DeclaringNation { get; set; }

        [Name("Declaring Alliance")]
        public string DeclaringAlliance { get; set; }

        [Name("Declaring Alliance ID")]
        public int DeclaringAllianceId { get; set; }

        [Name("Declaring Team")]
        public string DeclaringTeam { get; set; }

        [Name("Receiving ID")]
        public int ReceivingId { get; set; }

        [Name("Receiving Ruler")]
        public string ReceivingRuler { get; set; }

        [Name("Receiving Nation")]
        public string ReceivingNation { get; set; }

        [Name("Receiving Alliance")]
        public string ReceivingAlliance { get; set; }

        [Name("Receiving Alliance ID")]
        public int ReceivingAllianceId { get; set; }

        [Name("Receiving Team")]
        public string ReceivingTeam { get; set; }

        [Name("Status")]
        public string Status { get; set; }

        [Name("Money")]
        public int Money { get; set; }

        [Name("Technology")]
        public int Technology { get; set; }

        [Name("Soldiers")]
        public int Soldiers { get; set; }

        [Name("Date")]
        public DateTime Date { get; set; }

        [Name("Reason")]
        public string Reason { get; set; }

        [Name("Aid ID")]
        public int AidId { get; set; }
    }
}
