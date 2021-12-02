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
    [TechTalk.SpecRun.FeatureAttribute("UserFxCurrencyConversionInvalidSettings", Description="\tSimple library for currency conversion based on user settings", SourceFile="UserFxCurrencyConversion\\UserFxCurrencyConversionNegative.feature", SourceLine=0)]
    public partial class UserFxCurrencyConversionInvalidSettingsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "UserFxCurrencyConversionNegative.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "UserFxCurrencyConversion", "UserFxCurrencyConversionInvalidSettings", "\tSimple library for currency conversion based on user settings", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with invalid RequestID places a request the conversion should not suc" +
            "ceed", SourceLine=4)]
        public virtual void WhenAUserWithInvalidRequestIDPlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with invalid RequestID places a request the conversion should not suc" +
                    "ceed", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
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
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "UserId",
                            "TradingStatus",
                            "MinTradingAmt",
                            "MaxTradingAmt",
                            "CurrentBalance",
                            "UserCcy",
                            "AllowedTradingCcy"});
                table1.AddRow(new string[] {
                            "100010",
                            "Active",
                            "1",
                            "1000",
                            "500",
                            "GBP",
                            "GBP/USD,EUR/GBP"});
                table1.AddRow(new string[] {
                            "100020",
                            "Active",
                            "10000",
                            "1000000",
                            "45000",
                            "GBP",
                            "GBP/USD"});
#line 13
 testRunner.And("user has below settings:", ((string)(null)), table1, "And ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table2.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "GBP/USD",
                            "Buy",
                            "100"});
                table2.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "GBP/USD",
                            "Sell",
                            "100"});
#line 21
 testRunner.And("the request received is:", ((string)(null)), table2, "And ");
#line hidden
#line 26
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
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
                table3.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidUserId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "",
                            "",
                            "",
                            ""});
                table3.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "",
                            "",
                            "",
                            ""});
#line 28
 testRunner.Then("the expected results should be", ((string)(null)), table3, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
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
                table4.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "",
                            "",
                            "",
                            ""});
                table4.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "",
                            "",
                            "",
                            ""});
#line 33
 testRunner.And("database should store", ((string)(null)), table4, "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
