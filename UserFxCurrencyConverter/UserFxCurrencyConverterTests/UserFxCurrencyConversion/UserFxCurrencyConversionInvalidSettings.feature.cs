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
    [TechTalk.SpecRun.FeatureAttribute("UserFxCurrencyConversionInvalidSettings", Description="\tSimple library for currency conversion based on user settings", SourceFile="UserFxCurrencyConversion\\UserFxCurrencyConversionInvalidSettings.feature", SourceLine=0)]
    public partial class UserFxCurrencyConversionInvalidSettingsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "UserFxCurrencyConversionInvalidSettings.feature"
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
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with Invalid RequestId places a request the conversion should not suc" +
            "ceed", SourceLine=4)]
        public virtual void WhenAUserWithInvalidRequestIdPlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with Invalid RequestId places a request the conversion should not suc" +
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
                TechTalk.SpecFlow.Table table49 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table49.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "GBP/USD",
                            "Buy",
                            "100"});
                table49.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "GBP/USD",
                            "Sell",
                            "100"});
#line 8
 testRunner.And("the request received is:", ((string)(null)), table49, "And ");
#line hidden
#line 13
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table50 = new TechTalk.SpecFlow.Table(new string[] {
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
                table50.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table50.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 15
 testRunner.Then("the expected results should be", ((string)(null)), table50, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table51 = new TechTalk.SpecFlow.Table(new string[] {
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
                table51.AddRow(new string[] {
                            "1",
                            "100010",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table51.AddRow(new string[] {
                            "2",
                            "100020",
                            "00000000-0000-0000-0000-000000000000",
                            "ConversionFailedInvalidRequestId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 20
 testRunner.And("database should store", ((string)(null)), table51, "And ");
#line hidden
#line 25
 testRunner.And("user settings are not called", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user with Invalid UserId places a request the conversion should not succee" +
            "d", SourceLine=27)]
        public virtual void WhenAUserWithInvalidUserIdPlacesARequestTheConversionShouldNotSucceed()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user with Invalid UserId places a request the conversion should not succee" +
                    "d", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 28
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
#line 29
 testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table52 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table52.AddRow(new string[] {
                            "1",
                            "0",
                            "eb63fd00-71c8-41e5-9d67-809d7ae95aa2",
                            "GBP/USD",
                            "Buy",
                            "100"});
                table52.AddRow(new string[] {
                            "2",
                            "-10",
                            "8efaa640-bd2c-4d6d-8563-47a3784d9e8b",
                            "GBP/USD",
                            "Sell",
                            "100"});
#line 31
 testRunner.And("the request received is:", ((string)(null)), table52, "And ");
#line hidden
#line 36
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table53 = new TechTalk.SpecFlow.Table(new string[] {
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
                table53.AddRow(new string[] {
                            "1",
                            "0",
                            "eb63fd00-71c8-41e5-9d67-809d7ae95aa2",
                            "ConversionFailedInvalidUserId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table53.AddRow(new string[] {
                            "2",
                            "-10",
                            "8efaa640-bd2c-4d6d-8563-47a3784d9e8b",
                            "ConversionFailedInvalidUserId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 38
 testRunner.Then("the expected results should be", ((string)(null)), table53, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table54 = new TechTalk.SpecFlow.Table(new string[] {
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
                table54.AddRow(new string[] {
                            "1",
                            "0",
                            "eb63fd00-71c8-41e5-9d67-809d7ae95aa2",
                            "ConversionFailedInvalidUserId",
                            "GBP/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table54.AddRow(new string[] {
                            "2",
                            "-10",
                            "8efaa640-bd2c-4d6d-8563-47a3784d9e8b",
                            "ConversionFailedInvalidUserId",
                            "GBP/USD",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 43
 testRunner.And("database should store", ((string)(null)), table54, "And ");
#line hidden
#line 48
 testRunner.And("user settings are not called", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When the user is using invalid Currency pair conversion should not succeed.", SourceLine=50)]
        public virtual void WhenTheUserIsUsingInvalidCurrencyPairConversionShouldNotSucceed_()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When the user is using invalid Currency pair conversion should not succeed.", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 51
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
#line 52
 testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table55 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table55.AddRow(new string[] {
                            "1",
                            "1001",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "GBP/USA",
                            "Buy",
                            "100"});
                table55.AddRow(new string[] {
                            "2",
                            "1002",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "",
                            "Buy",
                            "100"});
                table55.AddRow(new string[] {
                            "3",
                            "1003",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "null",
                            "Buy",
                            "100"});
                table55.AddRow(new string[] {
                            "4",
                            "1004",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "GBP/",
                            "Sell",
                            "100"});
                table55.AddRow(new string[] {
                            "5",
                            "1005",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "/USD",
                            "Buy",
                            "100"});
                table55.AddRow(new string[] {
                            "6",
                            "1006",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "GBP/INR",
                            "Buy",
                            "100"});
#line 54
 testRunner.And("the request received is:", ((string)(null)), table55, "And ");
#line hidden
#line 63
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table56 = new TechTalk.SpecFlow.Table(new string[] {
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
                table56.AddRow(new string[] {
                            "1",
                            "1001",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/USA",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table56.AddRow(new string[] {
                            "2",
                            "1002",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table56.AddRow(new string[] {
                            "3",
                            "1003",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "null",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table56.AddRow(new string[] {
                            "4",
                            "1004",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
                table56.AddRow(new string[] {
                            "5",
                            "1005",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table56.AddRow(new string[] {
                            "6",
                            "1006",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/INR",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 65
 testRunner.Then("the expected results should be", ((string)(null)), table56, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table57 = new TechTalk.SpecFlow.Table(new string[] {
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
                table57.AddRow(new string[] {
                            "1",
                            "1001",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/USA",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table57.AddRow(new string[] {
                            "2",
                            "1002",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table57.AddRow(new string[] {
                            "3",
                            "1003",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "null",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table57.AddRow(new string[] {
                            "4",
                            "1004",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/",
                            "100",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
                table57.AddRow(new string[] {
                            "5",
                            "1005",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "/USD",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table57.AddRow(new string[] {
                            "6",
                            "1006",
                            "96a9cc7c-d978-4b07-ae0d-958e406adb5d",
                            "ConversionFailedInvalidCcyPair",
                            "GBP/INR",
                            "100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
#line 74
 testRunner.And("database should store", ((string)(null)), table57, "And ");
#line hidden
#line 83
 testRunner.And("user settings are not called", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("When a user enters invalid (zero or negative) amount, conversion should not succe" +
            "ed.", SourceLine=85)]
        public virtual void WhenAUserEntersInvalidZeroOrNegativeAmountConversionShouldNotSucceed_()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a user enters invalid (zero or negative) amount, conversion should not succe" +
                    "ed.", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 86
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
#line 87
    testRunner.Given("the database is clean", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table58 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "UserId",
                            "RequestId",
                            "CcyPair",
                            "Side",
                            "Amount"});
                table58.AddRow(new string[] {
                            "1",
                            "101",
                            "cae799bd-4f58-4ab8-a2bd-686e424ef8ba",
                            "EUR/GBP",
                            "Buy",
                            "-100"});
                table58.AddRow(new string[] {
                            "2",
                            "102",
                            "3b32c1a2-d066-4d1f-a687-22deab727828",
                            "GBP/USD",
                            "Sell",
                            "0"});
#line 89
 testRunner.And("the request received is:", ((string)(null)), table58, "And ");
#line hidden
#line 94
 testRunner.When("we run the calculation with latest market price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table59 = new TechTalk.SpecFlow.Table(new string[] {
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
                table59.AddRow(new string[] {
                            "1",
                            "101",
                            "cae799bd-4f58-4ab8-a2bd-686e424ef8ba",
                            "ConversionFailedInvalidAmount",
                            "EUR/GBP",
                            "-100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table59.AddRow(new string[] {
                            "2",
                            "102",
                            "3b32c1a2-d066-4d1f-a687-22deab727828",
                            "ConversionFailedInvalidAmount",
                            "GBP/USD",
                            "0",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 96
 testRunner.Then("the expected results should be", ((string)(null)), table59, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table60 = new TechTalk.SpecFlow.Table(new string[] {
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
                table60.AddRow(new string[] {
                            "1",
                            "101",
                            "cae799bd-4f58-4ab8-a2bd-686e424ef8ba",
                            "ConversionFailedInvalidAmount",
                            "EUR/GBP",
                            "-100",
                            "Buy",
                            "null",
                            "",
                            "",
                            "null"});
                table60.AddRow(new string[] {
                            "2",
                            "102",
                            "3b32c1a2-d066-4d1f-a687-22deab727828",
                            "ConversionFailedInvalidAmount",
                            "GBP/USD",
                            "0",
                            "Sell",
                            "null",
                            "",
                            "",
                            "null"});
#line 101
 testRunner.And("database should store", ((string)(null)), table60, "And ");
#line hidden
#line 106
 testRunner.And("user settings are not called", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
