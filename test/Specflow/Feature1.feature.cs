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
namespace Test.Specflow
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Feature1Feature : object, Xunit.IClassFixture<Feature1Feature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Feature1.feature"
#line hidden
        
        public Feature1Feature(Feature1Feature.FixtureData fixtureData, Test_Specflow_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Feature1", "A short summary of the feature", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="MyFirstScenario")]
        [Xunit.TraitAttribute("FeatureTitle", "Feature1")]
        [Xunit.TraitAttribute("Description", "MyFirstScenario")]
        [Xunit.TraitAttribute("Category", "tag1")]
        public void MyFirstScenario()
        {
            string[] tagsOfScenario = new string[] {
                    "tag1"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("MyFirstScenario", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "scope",
                            "path",
                            "key",
                            "value"});
                table1.AddRow(new string[] {
                            "<null>",
                            "",
                            "",
                            ""});
                table1.AddRow(new string[] {
                            "<null>",
                            "2022",
                            "",
                            ""});
                table1.AddRow(new string[] {
                            "posts",
                            "",
                            "",
                            ""});
#line 7
    testRunner.Given("the following default metadata:", ((string)(null)), table1, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "key",
                            "value"});
                table2.AddRow(new string[] {
                            ".md",
                            ".html"});
#line 12
    testRunner.And("the following extension mapping:", ((string)(null)), table2, "* ");
#line hidden
#line 15
    testRunner.Given("the extensions \'.md,.txt\' are targeted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 16
    testRunner.And("file \'_site/file1.md\' has the following contents:", "---\r\ntitle: my title\r\n---\r\n# Hello World", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
    testRunner.And("file \'_site/_posts/2019-09-07-file1.md\' has the following contents:", "---\r\ntitle: my title\r\n---\r\n# Hello World", ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "key",
                            "value"});
                table3.AddRow(new string[] {
                            "number",
                            "1"});
                table3.AddRow(new string[] {
                            "text",
                            "abc"});
                table3.AddRow(new string[] {
                            "expr",
                            "true"});
#line 30
 testRunner.Given("scope \'[string]\' has the following metadata:", ((string)(null)), table3, "Given ");
#line hidden
#line 35
    testRunner.When("something else", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 36
    testRunner.When("something", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "key",
                            "value"});
                table4.AddRow(new string[] {
                            "A",
                            ""});
                table4.AddRow(new string[] {
                            "B",
                            "\'\'"});
                table4.AddRow(new string[] {
                            "C",
                            "<value>"});
                table4.AddRow(new string[] {
                            "D",
                            "<null>"});
                table4.AddRow(new string[] {
                            "uri",
                            "2"});
#line 37
    testRunner.Then("something", ((string)(null)), table4, "Then ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "uri"});
                table5.AddRow(new string[] {
                            "file1.html"});
                table5.AddRow(new string[] {
                            "2019/09/07/file1.html"});
#line 44
    testRunner.Then("the following pages:", ((string)(null)), table5, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                Feature1Feature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                Feature1Feature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
