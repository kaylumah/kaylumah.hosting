﻿// Copyright (c) Kaylumah, 2023. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using System.IO.Abstractions.TestingHelpers;
using Ssg.Extensions.Metadata.Abstractions;
using TechTalk.SpecFlow;
using Test.Specflow.Entities;

namespace Test.Specflow.Steps.Collections
{
    [Binding]
    public class TagCollectionStepDefinitions
    {
        readonly TagCollection _TagCollection;
        readonly MockFileSystem _FileSystem;

        public TagCollectionStepDefinitions(MockFileSystem fileSystem, TagCollection tagCollection)
        {
            _FileSystem = fileSystem;
            _TagCollection = tagCollection;
        }

        [Given("the following tags:")]
        public void GivenTheFollowingTags(TagCollection tagCollection)
        {
            _TagCollection.AddRange(tagCollection);
            TagMetaDataCollection tagMetaDataCollection = new TagMetaDataCollection();
            tagMetaDataCollection.AddRange(_TagCollection.ToTagMetadata());
            _FileSystem.AddYamlDataFile(Constants.Files.Tags, tagMetaDataCollection);
        }
    }
}
