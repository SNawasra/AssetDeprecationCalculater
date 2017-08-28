using DeprecationCalculater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeprecationCalculater
{
    public class AssetDeprecationCalculater
    {

        public AssetPosting CreateAssetPosting(AssetBookToBeDeprecated deprecation, decimal amount, int year)
        {
            AssetPosting assetPostingRecord = new AssetPosting
            {
                AssetId = deprecation.AssetId,
                Id = Guid.NewGuid(),
                AccmltdDepGLAccountID = deprecation.AccumulatedDepreciationAccount,
                DepExpenseGLAccountID = deprecation.AssetDeprecationAccount,
                AssetGLAccountID = deprecation.AssetGLAccountID,
                Amount = amount,
                BookID = deprecation.AssetBookId,
                Type = AssetPostingTypes.Depreciation,
                Year = deprecation.DepreciationStartDate.Year + year,
                Status = "unposted"
            };
            return assetPostingRecord;
        }

        public List<AssetPosting> CalculateDeprecatiobForStraightLineMethod(AssetBookToBeDeprecated deprecation)
        {
            var assetTotalValue = deprecation.CurrentCost;
            var depreciableValue = assetTotalValue - deprecation.SalvageValue;
            var years = deprecation.LifeTimeYears;
            var fYears = years > (int)years ? ((int)years + 1) : years;
            List<AssetPosting> assetPosting = new List<AssetPosting>();

            for (int i = 1; i <= fYears; i++)
            {
                var depForEachYear = depreciableValue / fYears;
                deprecation.CurrentCost -= depForEachYear;
                AssetPosting assetPostingRecord = CreateAssetPosting(deprecation, depForEachYear, (i - 1));
                assetPosting.Add(assetPostingRecord);
            }
            return assetPosting;
        }

        public List<AssetPosting> CalculateDeprecatiobForDecliningBalanceMethod(AssetBookToBeDeprecated deprecation)
        {
            var assetTotalValue = deprecation.CurrentCost;
            var accumulatedDepreciation = 0m;
            var multiplier = Convert.ToInt32(deprecation.Percentage)/100;
            var totalYearsOfLife = deprecation.LifeTimeYears;
            var fYears = totalYearsOfLife > (int)totalYearsOfLife ? ((int)totalYearsOfLife + 1) : totalYearsOfLife;
            List<AssetPosting> assetPosting = new List<AssetPosting>();

            for (int i = 1; i <= fYears; i++)
            {
                var remainingAssetValue = assetTotalValue - accumulatedDepreciation;
                var depForEachYear = (remainingAssetValue / totalYearsOfLife) * multiplier;
                //var depForEachYear = annualEquation / totalYearsOfLife;

                if(deprecation.CurrentCost - depForEachYear < deprecation.SalvageValue  )
                {
                    depForEachYear = deprecation.CurrentCost - deprecation.SalvageValue;
                }

                deprecation.CurrentCost -= depForEachYear;
                accumulatedDepreciation += depForEachYear;
                AssetPosting assetPostingRecord = CreateAssetPosting(deprecation, depForEachYear, (i - 1));
                assetPosting.Add(assetPostingRecord);
            }
            return assetPosting;
        }

        public List<AssetPosting> CalculateDeprecatiobForDecliningBalanceMethodWithSwitchOver(AssetBookToBeDeprecated deprecation)
        {
            var assetTotalValue = deprecation.CurrentCost;
            var depreciableValue = deprecation.CurrentCost - deprecation.SalvageValue;
            var accumulatedDepreciation = 0m;
            var multiplier = Convert.ToInt32(deprecation.Percentage) / 100;
            var totalYearsOfLife = deprecation.LifeTimeYears;
            var fYears = totalYearsOfLife > (int)totalYearsOfLife ? ((int)totalYearsOfLife + 1) : totalYearsOfLife;
            List<AssetPosting> assetPosting = new List<AssetPosting>();

            for (int i = 1; i <= fYears; i++)
            {
                var remainingAssetValue = assetTotalValue - accumulatedDepreciation;
                var deprecatiobForDecliningBalance = (remainingAssetValue / totalYearsOfLife) * multiplier;
                var deprecatiobForStraightLine = depreciableValue / totalYearsOfLife;

                var depForEachYear = deprecatiobForDecliningBalance > deprecatiobForStraightLine ?
                    deprecatiobForDecliningBalance : deprecatiobForStraightLine;

                if (deprecation.CurrentCost - depForEachYear < deprecation.SalvageValue)
                {
                    depForEachYear = deprecation.CurrentCost - deprecation.SalvageValue;
                }

                deprecation.CurrentCost -= depForEachYear;
                accumulatedDepreciation += depForEachYear;
                AssetPosting assetPostingRecord = CreateAssetPosting(deprecation, depForEachYear, (i - 1));
                assetPosting.Add(assetPostingRecord);
            }
            return assetPosting;
        }

        public List<AssetPosting> CalculateDeprecatiobForSumOfYearsMethod(AssetBookToBeDeprecated deprecation)
        {
            var assetTotalValue = deprecation.CurrentCost;
            var totalYearsOfLife = deprecation.LifeTimeYears;
            var depreciableValue = assetTotalValue - deprecation.SalvageValue;
            var yearsLeft = totalYearsOfLife;
            var SumOfYearsOfTotalLife = totalYearsOfLife * (totalYearsOfLife + 1) / 2;
            var fYears = totalYearsOfLife > (int)totalYearsOfLife ? ((int)totalYearsOfLife + 1) : totalYearsOfLife;

            List<AssetPosting> assetPosting = new List<AssetPosting>();

            for ( int i = 1; i <= fYears; i++)
            {
                var depForEachYear = depreciableValue * (yearsLeft / SumOfYearsOfTotalLife);
                //var depForEachYear = annualEquation / totalYearsOfLife;
                yearsLeft = --totalYearsOfLife;
                deprecation.CurrentCost -= depForEachYear;
                AssetPosting assetPostingRecord = CreateAssetPosting(deprecation, depForEachYear, (i - 1));
                assetPosting.Add(assetPostingRecord);

            }
            return assetPosting;
        }

        public List<AssetPosting> CalculateDeprecation(List<AssetBookToBeDeprecated> listOfBooksToBeDeprecated)
        {
            List<AssetPosting> DeprecatedRecords = new List<AssetPosting>();

            foreach (var bookToBeDeprecated in listOfBooksToBeDeprecated)
            {
                if(bookToBeDeprecated.DepreciationMethod == DepreciationMethod.StraightLine)
                {
                    DeprecatedRecords.AddRange(CalculateDeprecatiobForStraightLineMethod(bookToBeDeprecated));
                }
                else if (bookToBeDeprecated.DepreciationMethod == DepreciationMethod.DecliningBalance)
                {
                    DeprecatedRecords.AddRange(CalculateDeprecatiobForDecliningBalanceMethod(bookToBeDeprecated));
                }
                else if (bookToBeDeprecated.DepreciationMethod == DepreciationMethod.SumofYearsDigits)
                {
                    DeprecatedRecords.AddRange(CalculateDeprecatiobForSumOfYearsMethod(bookToBeDeprecated));
                }
                else if (bookToBeDeprecated.DepreciationMethod == DepreciationMethod.DecliningBalancewithStraightlineSwitchover)
                {
                    DeprecatedRecords.AddRange(CalculateDeprecatiobForDecliningBalanceMethodWithSwitchOver(bookToBeDeprecated));
                }
            }
            return DeprecatedRecords;
        }
    }
}
