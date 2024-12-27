﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.Ssg.Manager.Site.Service;
using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
using Kaylumah.Ssg.Manager.Site.Service.Files.Preprocessor;
using Kaylumah.Ssg.Manager.Site.Service.Files.Processor;
using Microsoft.Extensions.Logging.Abstractions;
using Reqnroll;
using Ssg.Extensions.Data.Yaml;
using Ssg.Extensions.Metadata.YamlFrontMatter;
using Test.Unit.Entities;
using Test.Unit.Extensions;

namespace Test.Unit.Steps
{
    [Binding]
    public class FileProcessorStepDefinitions
    {
        readonly IFileProcessor _FileProcessor;
        readonly List<BinaryFile> _Files;

        public FileProcessorStepDefinitions(MockFileSystem mockFileSystem, MetadataParserOptions metadataParserOptions, SiteInfo siteInfo)
        {
            _FileProcessor = new FileProcessor(mockFileSystem,
                NullLogger<FileProcessor>.Instance,
                Enumerable.Empty<IContentPreprocessorStrategy>(),
                siteInfo,
                new YamlFrontMatterMetadataProvider(new YamlParser()),
                metadataParserOptions);
            _Files = new();
        }
        [When("the files are retrieved:")]
        public async Task WhenTheFilesAreRetrieved(FileFilterCriteria criteria)
        {
            IEnumerable<BinaryFile> result = await _FileProcessor.Process(criteria);
            _Files.AddRange(result);
        }

        [Then("the following articles are returned:")]
        public void ThenTheFollowingArticlesAreReturned(ArticleCollection articleCollection)
        {
            IEnumerable<Article> actual = _Files.ToArticles();
            actual.Should().BeEquivalentTo(articleCollection);
        }

        [Then("no articles are returned:")]
        public void ThenNoArticlesAreReturned()
        {
            _Files.Should().BeEmpty();
        }
    }
}
