using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeprecationCalculater
{
    public class DeprecatedCalculaterTest
    {
        [Test]
        public void DeprecatedCalculater_CalculateDeprecationForStraightLineMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.StraightLine, 30000m,
                30000m, 3000m, 5m, "");

            var assetPostingRecords = 
                deprecation.CalculateDeprecatiobForStraightLineMethod(assetToBeDeprecated);


            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }

            Assert.AreEqual(assetPostingRecords.Count, 5);
            Assert.AreEqual(totalDeprecation, 27000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 5400m);
            Assert.AreEqual(assetPostingRecords[1].Amount, 5400m);
            Assert.AreEqual(assetPostingRecords[2].Amount, 5400m);
            Assert.AreEqual(assetPostingRecords[0].Status, "unposted");
        }

        [Test]
        public void DeprecatedCalculater_CalculateDeprecationForStraightLineMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsNotInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.StraightLine, 30000m,
                30000m, 3000m, 5.5m, "");

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForStraightLineMethod(assetToBeDeprecated);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }

            Assert.AreEqual(assetPostingRecords.Count, 6);
            Assert.AreEqual(totalDeprecation, 27000m);
            Assert.AreEqual(assetToBeDeprecated.CurrentCost, 3000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 4500m);
            Assert.AreEqual(assetPostingRecords[1].Amount, 4500m);
            Assert.AreEqual(assetPostingRecords[2].Amount, 4500m);
            Assert.AreEqual(assetPostingRecords[3].Amount, 4500m);
            Assert.AreEqual(assetPostingRecords[4].Amount, 4500m);
            Assert.AreEqual(assetPostingRecords[5].Amount, 4500m);

            Assert.AreEqual(assetPostingRecords[0].Status, "unposted");
        }


        [Test]
        public void DeprecatedCalculater_CalculateDeprecatiobForDecliningBalanceMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.DecliningBalance, 30000m, 30000m,
                3000m, 5m, DepreciationPercent.Percent200);

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForDecliningBalanceMethod(assetToBeDeprecated);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }

            Assert.AreEqual(assetPostingRecords.Count, 5);
            Assert.AreEqual(totalDeprecation, 27000m);
            Assert.AreEqual(assetToBeDeprecated.CurrentCost, 3000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 12000m);
            Assert.AreEqual(assetPostingRecords[1].Amount, 7200m);
            Assert.AreEqual(assetPostingRecords[2].Amount, 4320m);
            Assert.AreEqual(assetPostingRecords[3].Amount, 2592m);
            Assert.AreEqual(assetPostingRecords[4].Amount, 888m);

            Assert.AreEqual(assetPostingRecords[0].Status, "unposted");
        }

        [Test]
        public void DeprecatedCalculater_CalculateDeprecatiobForDecliningBalanceMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsNotInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.DecliningBalance, 30000m, 30000m,
                3000m, 5.5m, DepreciationPercent.Percent200);

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForDecliningBalanceMethod(assetToBeDeprecated);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }

            Assert.AreEqual(assetPostingRecords.Count, 6);
            Assert.AreEqual(totalDeprecation, 27000m);
            Assert.AreEqual(assetToBeDeprecated.CurrentCost, 3000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 10909.090909090909090909090909m);
            Assert.AreEqual(assetPostingRecords[1].Amount, 6942.148760330578512396694215m);
            Assert.AreEqual(assetPostingRecords[2].Amount, 4417.7310293012772351615326822m);
            Assert.AreEqual(assetPostingRecords[3].Amount, 2811.2833822826309678300662524m);
            Assert.AreEqual(assetPostingRecords[4].Amount, 1788.9985159980378886191330698m);
            Assert.AreEqual(assetPostingRecords[5].Amount, 130.7474029965663050834828716m);

            Assert.AreEqual(assetPostingRecords[0].Status, "unposted");
        }


        [Test]
        public void DeprecatedCalculater_CalculateDecliningBalanceMethodWithSwitchOver_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.DecliningBalancewithStraightlineSwitchover, 30000m, 30000m,
                3000m, 5m, DepreciationPercent.Percent200);

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForDecliningBalanceMethodWithSwitchOver(assetToBeDeprecated);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }

            Assert.AreEqual(assetPostingRecords.Count, 5);
            Assert.AreEqual(totalDeprecation, 27000m);
            Assert.AreEqual(assetToBeDeprecated.CurrentCost, 3000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 12000m);
            Assert.AreEqual(assetPostingRecords[1].Amount, 7200m);
            Assert.AreEqual(assetPostingRecords[2].Amount, 5400m);
            Assert.AreEqual(assetPostingRecords[3].Amount, 2400);
            Assert.AreEqual(assetPostingRecords[4].Amount, 0);

        }


        [Test]
        public void DeprecatedCalculater_CalculateDeprecatiobForSumOfYearsMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.SumofYearsDigits, 30000m, 30000m,
                3000m, 5m, "");

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForSumOfYearsMethod(assetToBeDeprecated);

            Assert.AreEqual(assetPostingRecords.Count, 5);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }
            Assert.AreEqual(totalDeprecation, 27000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 8999.999999999999999999999999M);
            Assert.AreEqual(assetPostingRecords[1].Amount, 7200.0000000000000000000000009M);
            Assert.AreEqual(assetPostingRecords[2].Amount, 5400.0M);
            Assert.AreEqual(assetPostingRecords[3].Amount, 3599.9999999999999999999999991M);
            Assert.AreEqual(assetPostingRecords[4].Amount, 1800.0000000000000000000000009M);

        }


        [Test]
        public void DeprecatedCalculater_CalculateDeprecatiobForSumOfYearsMethod_ShouldReturnListOfAssetPosting_WhenNumberOfLifeTimeYearsIsNotInteger()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var assetToBeDeprecated =
                createAssetBookToBeDeprecated(DepreciationMethod.SumofYearsDigits, 30000m, 30000m,
                3000m, 5.65m, "");

            var assetPostingRecords =
                deprecation.CalculateDeprecatiobForSumOfYearsMethod(assetToBeDeprecated);

            Assert.AreEqual(assetPostingRecords.Count, 6);

            var totalDeprecation = 0m;
            foreach (var assetPosting in assetPostingRecords)
            {
                totalDeprecation += assetPosting.Amount;
            }
            Assert.AreEqual(totalDeprecation, 27000m);

            Assert.AreEqual(assetPostingRecords[0].Amount, 8120.300751879699248120300752M);
            Assert.AreEqual(assetPostingRecords[1].Amount, 6683.0793798655931865060882303M);
            Assert.AreEqual(assetPostingRecords[2].Amount, 5245.8580078514871248918757060M);
            Assert.AreEqual(assetPostingRecords[3].Amount, 3808.6366358373810632776631844M);
            Assert.AreEqual(assetPostingRecords[4].Amount, 2371.4152638232750016634506628M);
            Assert.AreEqual(assetPostingRecords[5].Amount, 934.1938918091689400492381385m);
        }

        [Test]
        public void DeprecatedCalculater_CalculateDeprecation()
        {
            AssetDeprecationCalculater deprecation = new AssetDeprecationCalculater();
            var straigtLineDeprecation =
                createAssetBookToBeDeprecated(DepreciationMethod.StraightLine, 30000m, 30000m,
                3000m, 4m, "");

            var decliningBalanceDeprecation =
                createAssetBookToBeDeprecated(DepreciationMethod.DecliningBalance, 30000m, 30000m,
                3000m, 6m, DepreciationPercent.Percent200);

            var decliningBalancewithStraightlineSwitchoverDeprecation =
                createAssetBookToBeDeprecated(DepreciationMethod.DecliningBalancewithStraightlineSwitchover, 30000m, 30000m,
                3000m, 3m, DepreciationPercent.Percent300);

            var sumofYearsDigitsDeprecation =
                createAssetBookToBeDeprecated(DepreciationMethod.SumofYearsDigits, 30000m, 30000m,
                3000m, 2m, "");

            List<AssetBookToBeDeprecated> assetBooksToBeDeprecated = new List<AssetBookToBeDeprecated>();
            assetBooksToBeDeprecated.Add(straigtLineDeprecation);
            assetBooksToBeDeprecated.Add(decliningBalanceDeprecation);
            assetBooksToBeDeprecated.Add(decliningBalancewithStraightlineSwitchoverDeprecation);
            assetBooksToBeDeprecated.Add(sumofYearsDigitsDeprecation);

            List<AssetPosting> postingRecords = 
                deprecation.CalculateDeprecation(assetBooksToBeDeprecated);

            Assert.AreEqual(postingRecords.Count, 15);
        }



        public AssetBookToBeDeprecated createAssetBookToBeDeprecated(string deprecationMethod ,
             decimal cost, decimal currentCost, decimal salvageValue, decimal years, string percentage)
        {
            return new AssetBookToBeDeprecated
            {
                AccumulatedDepreciationAccount = Guid.NewGuid(),
                AssetBookId = Guid.NewGuid(),
                AssetDeprecationAccount = Guid.NewGuid(),
                AssetGLAccountID = Guid.NewGuid(),
                AssetId = Guid.NewGuid(),
                DepreciationMethod = deprecationMethod,
                Percentage = percentage,
                Cost = cost,
                CurrentCost = currentCost,
                DepreciationStartDate = new DateTime(2017, 1, 1),
                LifeTimeYears = years,
                SalvageValue = salvageValue
            };
        }
    }
}
