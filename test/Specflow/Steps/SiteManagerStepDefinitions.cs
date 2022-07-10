﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Kaylumah.Ssg.Manager.Site.Interface;
using Kaylumah.Ssg.Manager.Site.Service;
using Kaylumah.Ssg.Manager.Site.Service.Feed;
using Kaylumah.Ssg.Manager.Site.Service.Seo;
using Kaylumah.Ssg.Manager.Site.Service.SiteMap;
using Kaylumah.Ssg.Utilities.Time;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Ssg.Extensions.Data.Yaml;
using Test.Specflow.Entities;
using Test.Specflow.Utilities;

namespace Test.Specflow.Steps;

[Binding]
[Scope(Feature = "SiteManager")]
public class SiteManagerStepDefinitions
{
    private readonly ISiteManager _siteManager;
    private readonly ArticleCollection _articleCollection;
    private readonly ValidationContext _validationContext;

    public SiteManagerStepDefinitions(MockFileSystem mockFileSystem, ArticleCollection articleCollection, ValidationContext validationContext, SiteInfo siteInfo)
    {
        _articleCollection = articleCollection;
        _validationContext = validationContext;
        var clock = new Mock<ISystemClock>();
        var fileProcessor = new FileProcessorMock(_articleCollection);
        var artifactAccess = new ArtifactAccessMock();
        var logger = NullLogger<SiteManager>.Instance;
        var transformationEngine = new TransformationEngineMock();
        var yamlParser = new YamlParser();
        var siteMetadataFactory = new SiteMetadataFactory(clock.Object, siteInfo, yamlParser, mockFileSystem, NullLogger<SiteMetadataFactory>.Instance);
        var feedGenerator = new FeedGenerator(NullLogger<FeedGenerator>.Instance);
        var metaTagGenerator = new MetaTagGenerator(NullLogger<MetaTagGenerator>.Instance);
        var structureDataGenerator = new StructureDataGenerator(NullLogger<StructureDataGenerator>.Instance);
        var seoGenerator = new SeoGenerator(metaTagGenerator, structureDataGenerator);
        var siteMapGenerator = new SiteMapGenerator(NullLogger<SiteMapGenerator>.Instance);
        _siteManager = new SiteManager(
            fileProcessor.Object,
            artifactAccess.Object,
            mockFileSystem,
            logger,
            siteInfo,
            transformationEngine.Object,
            siteMetadataFactory,
            feedGenerator,
            seoGenerator,
            siteMapGenerator,
            clock.Object);
    }

    [When("the site is generated:")]
    public async Task WhenTheSiteIsGenerated()
    {
        try
        {
            await _siteManager.GenerateSite(new GenerateSiteRequest()
            {
                Configuration = new SiteConfiguration() { Source = "_site", AssetDirectory = "assets", DataDirectory = "data" }
            });
        }
        catch (Exception ex)
        {
            _validationContext.TestServiceException = ex;
        }
    }

    [Then("the scenario executed successfully:")]
    public void ThenTheScenarioExecutedSuccessfully()
    {
        _validationContext.TestServiceException.Should().BeNull();
    }
}
