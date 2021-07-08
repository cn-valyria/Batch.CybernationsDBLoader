using CsvHelper.Configuration.Attributes;
using System;
using System.Globalization;

namespace Repository.Parsers.DataObjects
{
    public class CnNation
    {
        [Name("Nation ID")]
        public int NationId { get; set; }

        [Name("Ruler Name")]
        public string RulerName { get; set; }

        [Name("Nation Name")]
        public string NationName { get; set; }

        [Name("Alliance")]
        public string Alliance { get; set; }

        [Name("Alliance ID")]
        public string AllianceId { get; set; }

        [Name("Alliance Date")]
        public DateTime AllianceDate { get; set; }

        [Name("Alliance Status")]
        public string AllianceStatus { get; set; }

        [Name("Government Type")]
        public string GovernmentType { get; set; }

        [Name("Religion")]
        public string Religion { get; set; }

        [Name("Team")]
        public string Team { get; set; }

        [Name("Created")]
        public DateTime Created { get; set; }

        [Name("Technology")]
        public decimal Technology { get; set; }

        [Name("Infrastructure")]
        public decimal Infrastructure { get; set; }

        [Name("Base Land")]
        public decimal BaseLand { get; set; }

        [Name("War Status")]
        public string WarStatus { get; set; }

        [Name("Resource 1")]
        public string Resource1 { get; set; }

        [Name("Resource 2")]
        public string Resource2 { get; set; }

        [Name("Votes")]
        public int Votes { get; set; }

        [Name("Strength")]
        [NumberStyles(NumberStyles.Number)]
        public decimal Strength { get; set; }

        [Name("DEFCON")]
        public int Defcon { get; set; }

        [Name("Base Soldiers")]
        [NumberStyles(NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign)]
        public int BaseSoldiers { get; set; }

        [Name("Tanks")]
        [NumberStyles(NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign)]
        public int Tanks { get; set; }

        [Name("Cruise")]
        [NumberStyles(NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign)]
        public int Cruise { get; set; }

        [Name("Nukes")]
        public int Nukes { get; set; }

        [Name("Activity")]
        public string Activity { get; set; }

        [Name("Connected Resource 1")]
        public string ConnectedResource1 { get; set; }

        [Name("Connected Resource 2")]
        public string ConnectedResource2 { get; set; }

        [Name("Connected Resource 3")]
        public string ConnectedResource3 { get; set; }

        [Name("Connected Resource 4")]
        public string ConnectedResource4 { get; set; }

        [Name("Connected Resource 5")]
        public string ConnectedResource5 { get; set; }

        [Name("Connected Resource 6")]
        public string ConnectedResource6 { get; set; }

        [Name("Connected Resource 7")]
        public string ConnectedResource7 { get; set; }

        [Name("Connected Resource 8")]
        public string ConnectedResource8 { get; set; }

        [Name("Connected Resource 9")]
        public string ConnectedResource9 { get; set; }

        [Name("Connected Resource 10")]
        public string ConnectedResource10 { get; set; }







    }
}
