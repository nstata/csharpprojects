﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UserFxCurrencyConverterIntegrationTests.UserFxCurrencyConversion
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("UserFxCurrencyConversionUserSettings", Description="\tSimple library for currency conversion based on user settings", SourceFile="UserFxCurrencyConversion\\UserFxCurrencyConversionUserSettings.feature", SourceLine=0)]
    public partial class UserFxCurrencyConversionUserSettingsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "UserFxCurrencyConversionUserSettings.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "UserFxCurrencyConversion", "UserFxCurrencyConversionUserSettings", "\tSimple library for currency conversion based on user settings", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with Inactive Status places a request the conversion should not succe" +
            "ed", SourceLine=4)]
        public virtual void WhenAUserWithInactiveStatusPlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with Inactive Status places a request the conversion should not succe" +
                    "ed", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table65 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "IsActive",
                            "MinTradingAmount",
                            "MaxTradingAmount",
                            "AvailableBalance",
                            "UserCcy"});
                table65.AddRow(new string[] {
                            "1",
                            "200",
                            "false",
                            "100",
                            "10000",
                            "4000",
                            "GBP"});
#line 8
 testRunner.And("user has below settings:", ((string)(null)), table65, "And ");
#line hidden
                TechTalk.SpecFlow.Table table66 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table66.AddRow(new string[] {
                            "1",
                            "200",
                            "87217cb6-bad5-4759-aabd-7e72d9b41c0e",
                            "GBP/USD",
                            "Buy",
                            "100"});
#line 13
 testRunner.And("the request received is:", ((string)(null)), table66, "And ");
#line hidden
#line 18
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table67 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table67.AddRow(new string[] {
                            "1",
                            "200",
                            "87217cb6-bad5-4759-aabd-7e72d9b41c0e",
                            "UserInactive",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 20
 testRunner.Then("the expected results should be", ((string)(null)), table67, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table68 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table68.AddRow(new string[] {
                            "1",
                            "200",
                            "87217cb6-bad5-4759-aabd-7e72d9b41c0e",
                            "UserInactive",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 25
 testRunner.And("database should store", ((string)(null)), table68, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with incorrect Min Trading Amount places a request the conversion sho" +
            "uld not succeed", SourceLine=30)]
        public virtual void WhenAUserWithIncorrectMinTradingAmountPlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with incorrect Min Trading Amount places a request the conversion sho" +
                    "uld not succeed", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 32
 testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table69 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "IsActive",
                            "MinTradingAmount",
                            "MaxTradingAmount",
                            "AvailableBalance",
                            "UserCcy"});
                table69.AddRow(new string[] {
                            "2",
                            "300",
                            "true",
                            "100",
                            "10000",
                            "4000",
                            "GBP"});
#line 34
 testRunner.And("user has below settings:", ((string)(null)), table69, "And ");
#line hidden
                TechTalk.SpecFlow.Table table70 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table70.AddRow(new string[] {
                            "2",
                            "300",
                            "3e571810-9766-406a-88b1-9ce40c5df64d",
                            "GBP/USD",
                            "Buy",
                            "10"});
#line 39
 testRunner.And("the request received is:", ((string)(null)), table70, "And ");
#line hidden
#line 45
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table71 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table71.AddRow(new string[] {
                            "2",
                            "300",
                            "3e571810-9766-406a-88b1-9ce40c5df64d",
                            "ConversionFailedIncorrectMinimumTradingAmount",
                            "GBP/USD",
                            "10",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 47
 testRunner.Then("the expected results should be", ((string)(null)), table71, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table72 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table72.AddRow(new string[] {
                            "2",
                            "300",
                            "3e571810-9766-406a-88b1-9ce40c5df64d",
                            "ConversionFailedIncorrectMinimumTradingAmount",
                            "GBP/USD",
                            "10",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 52
 testRunner.And("database should store", ((string)(null)), table72, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with incorrect Available Balance places a request the conversion shou" +
            "ld not succeed", SourceLine=57)]
        public virtual void WhenAUserWithIncorrectAvailableBalancePlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with incorrect Available Balance places a request the conversion shou" +
                    "ld not succeed", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 58
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 59
 testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table73 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "IsActive",
                            "MinTradingAmount",
                            "MaxTradingAmount",
                            "AvailableBalance",
                            "UserCcy"});
                table73.AddRow(new string[] {
                            "4",
                            "500",
                            "true",
                            "100",
                            "10000",
                            "39",
                            "GBP"});
#line 61
 testRunner.And("user has below settings:", ((string)(null)), table73, "And ");
#line hidden
                TechTalk.SpecFlow.Table table74 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table74.AddRow(new string[] {
                            "4",
                            "500",
                            "58372ea6-0dc3-4252-8457-ff67f5b43049",
                            "GBP/USD",
                            "Buy",
                            "40"});
#line 66
 testRunner.And("the request received is:", ((string)(null)), table74, "And ");
#line hidden
#line 70
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table75 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table75.AddRow(new string[] {
                            "4",
                            "500",
                            "58372ea6-0dc3-4252-8457-ff67f5b43049",
                            "ConversionFailedInsufficientBalance",
                            "GBP/USD",
                            "40",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 72
 testRunner.Then("the expected results should be", ((string)(null)), table75, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table76 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "ConversionResult",
                            "CcyPair",
                            "OriginalAmount",
                            "Side",
                            "ConvertedAmountCurrency",
                            "ConvertedAmount",
                            "PxUsed",
                            "OriginalAmountCcy"});
                table76.AddRow(new string[] {
                            "4",
                            "500",
                            "58372ea6-0dc3-4252-8457-ff67f5b43049",
                            "ConversionFailedInsufficientBalance",
                            "GBP/USD",
                            "40",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 77
 testRunner.And("database should store", ((string)(null)), table76, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
