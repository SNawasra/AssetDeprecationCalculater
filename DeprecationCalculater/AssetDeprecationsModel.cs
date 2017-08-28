using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeprecationCalculater
{
    public class AssetBookToBeDeprecated
    {
        public Guid AssetDeprecationAccount { get; set; }
        public Guid AccumulatedDepreciationAccount { get; set; }
        public Guid AssetId { get; set; }
        public decimal LifeTimeYears { get; set; }
        public decimal CurrentCost { get; set; }
        public decimal Cost { get; set; }
        public decimal SalvageValue { get; set; }
        public Guid AssetGLAccountID { get; set; }
        public Guid AssetBookId { get; set; }
        public string BookType { get; set; }
        public string DepreciationMethod { get; set; }
        public string Percentage { get; set; }
        public DateTime DepreciationStartDate { get; set; }
    }

    public class AssetPosting
    {
        public Guid Id { get; set; }
        public Guid AssetId { get; set; }
        public Guid BookID { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public Guid LocationId { get; set; }
        public Guid AssetGLAccountID { get; set; }
        public Guid AccmltdDepGLAccountID { get; set; }
        public Guid DepExpenseGLAccountID { get; set; }
        public Guid GainLossGLAccountID { get; set; }
        public Guid PotentialAssetGLAccountID { get; set; }
        public string Status { get; set; }
        public Guid PostedJE { get; set; }
        public DateTime? PostedOn { get; set; }
        public Guid PostedBy { get; set; }
    }

    public class DepreciationMethod
    {
        public static string StraightLine { get { return "Straight Line"; } }
        public static string DecliningBalance { get { return "Declining Balance"; } }
        public static string DecliningBalancewithStraightlineSwitchover { get { return "Declining Balance with Straightline Switchover"; } }
        public static string SumofYearsDigits { get { return "Sum of Years Digits"; } }
        public static string None { get { return "None"; } }
        public static string getNameByValue(string value)
        {
            switch (value)
            {
                case "1": return StraightLine;
                case "2": return DecliningBalance;
                case "3": return DecliningBalancewithStraightlineSwitchover;
                case "4": return SumofYearsDigits;
                case "5": return None;
                default: return "";
            }
        }
    }

    public class AssetPostingTypes
    {
        public static string PlaceInService { get { return "Placce In Service"; } }
        public static string PlaceinServicefromCIP { get { return "Place in Service from CIP"; } }
        public static string CIP { get { return "CIP"; } }
        public static string Depreciation { get { return "Depreciation"; } }
        public static string Retirement { get { return "Retirement"; } }
    }

    public class DepreciationPercent
    {
        public static string Percent125 { get { return "125"; } }
        public static string percent150 { get { return "150"; } }
        public static string Percent175 { get { return "175"; } }
        public static string Percent200 { get { return "200"; } }
        public static string Percent300 { get { return "300"; } }
    }
}
