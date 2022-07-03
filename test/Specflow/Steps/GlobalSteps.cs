﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Kaylumah.Ssg.Manager.Site.Service;
using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
using Kaylumah.Ssg.Manager.Site.Service.Files.Preprocessor;
using Kaylumah.Ssg.Manager.Site.Service.Files.Processor;
using Kaylumah.Ssg.Utilities;
using Microsoft.Extensions.Logging.Abstractions;
using Ssg.Extensions.Data.Yaml;
using Ssg.Extensions.Metadata.YamlFrontMatter;
using Test.Specflow.Entities;
using Test.Specflow.Extensions;
using Test.Specflow.Utilities;
using File = Kaylumah.Ssg.Manager.Site.Service.Files.Processor.File;

namespace Test.Specflow.Steps;

[Binding]
public class GlobalSteps
{
    private readonly MockFileSystem _mockFileSystem = new();
    private readonly IFileProcessor _fileProcessor;
    private readonly string _postsDirectory = Path.Combine("_site", "_posts");
    private readonly List<File> _files = new();

    public GlobalSteps(MetadataParserOptions metadataParserOptions, SiteInfo siteInfo)
    {
        var metadataParser = new FileMetadataParser(NullLogger<FileMetadataParser>.Instance,
            new YamlFrontMatterMetadataProvider(new YamlParser()),
            metadataParserOptions);
        IFileSystem fileSystem = new FileSystem(_mockFileSystem);
        _fileProcessor = new FileProcessor(fileSystem,
            NullLogger<FileProcessor>.Instance,
            Enumerable.Empty<IContentPreprocessorStrategy>(),
            siteInfo,
            metadataParser);
    }

    [Given("'(.*)' is an empty post:")]
    public void GivenIsAnEmptyPost(string fileName)
    {
        var articleDirectory = Path.Combine(_postsDirectory, fileName);
        _mockFileSystem.AddFile(articleDirectory, MockFileDataFactory.EmptyFile());
    }

    

    

    [Given("post '(.*)' has the following contents:")]
    public void GivenFileHasTheFollowingContents(string fileName, string contents)
    {
        var articleDirectory = Path.Combine(_postsDirectory, fileName);
        _mockFileSystem.AddFile(articleDirectory, MockFileDataFactory.PlainFile(contents));
    }

    [Given("a test post named '(.*)':")]
    public void GivenATestPost(string fileName)
    {
        var articleDirectory = Path.Combine(_postsDirectory, fileName);
        _mockFileSystem.AddFile(articleDirectory, MockFileDataFactory.EnrichedFile("Hello World!"));
    }

    [Given("a test post v2 named '(.*)':")]
    public void GivenATestPostV2(string fileName)
    {
        var articleDirectory = Path.Combine(_postsDirectory, fileName);
        _mockFileSystem.AddFile(articleDirectory, MockFileDataFactory.EnrichedFile("Hello World!", new Dictionary<string, object>()
        {
            ["output"] = true
        }));
    }

    [When("the files are retrieved:")]
    public async Task WhenTheFilesAreRetrieved(FileFilterCriteria criteria)
    {
        var result = await _fileProcessor.Process(criteria);
        _files.AddRange(result);
    }

    [Then("the following:")]
    public void ThenTheFollowing(Table table)
    {
        var actual = new List<(string Path, string Key, object Value)>();
        foreach (var file in _files)
        {
            foreach (var item in file.MetaData)
            {
                actual.Add(new(file.Name, item.Key, item.Value));
            }
        }

        var expected = table
            .CreateSet<(string Path, string Key, string Value)>()
            .ToList();
        actual.Should().BeEquivalentTo(expected);
    }

    [Then("the following V2:")]
    public void ThenTheFollowingV2(ArticleCollection articleCollection)
    {
        var actual = _files.ToPages(Guid.NewGuid());
        var actualTransformed = actual.ToArticles();
        actualTransformed.Should().BeEquivalentTo(articleCollection);
        // var expectedTransformed = expected.Select(x => new PageMetaData())
    }

    [StepArgumentTransformation]
    private static FileFilterCriteria ToFileFilterCriteria(Table table)
    {
        var criteria = table.CreateInstance<FileFilterCriteria>();
        return criteria;
    }
}
