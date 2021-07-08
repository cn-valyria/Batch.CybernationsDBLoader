using CsvHelper.Configuration.Attributes;
using System;

namespace Repository.Parsers.DataObjects
{
    public class CnWar
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

        [Name("War Status")]
        public string WarStatus { get; set; }

        [Name("Begin Date")]
        public DateTime BeginDate { get; set; }

        [Name("End Date")]
        public DateTime EndDate { get; set; }

        [Name("Reason")]
        public string Reason { get; set; }

        [Name("War ID")]
        public int WarId { get; set; }

        [Name("Destruction")]
        public decimal Destruction { get; set; }

        [Name("Attack Percent")]
        public int AttackPercent { get; set; }

        [Name("Defend Percent")]
        public int DefendPercent { get; set; }
    }
}
