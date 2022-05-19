﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Test.Specflow
{
    [Binding]
    public class Mapping
    {
        private readonly MetadataParserOptions _metadataParserOptions;

        public Mapping(MetadataParserOptions metadataParserOptions)
        {
            _metadataParserOptions = metadataParserOptions;
        }

        [Given("the following default metadata:")]
        public void GivenTheFollowingDefaultMetadata(Table table)
        {
            var set = table.CreateSet<MetaItem>();
            _metadataParserOptions.Defaults = set.ToDefaultMetadatas();
        }

        [Given("the following extension mapping:")]
        public void GivenTheFollowingExtensionMapping(Table table)
        {
            var set = table.CreateSet<(string key, string value)>();
            var dictionary = new Dictionary<string, string>();
            foreach(var item in set)
            {
                dictionary.Add(item.key, item.value);
            }
            _metadataParserOptions.ExtensionMapping = dictionary;
        }
    }

    public static partial class Extensions
    {
        public static DefaultMetadatas ToDefaultMetadatas(this IEnumerable<MetaItem> metaItems)
        {
            var defaultMetaDatas = new DefaultMetadatas();

            var groupedByScope = metaItems.GroupBy(x => x.scope);

            foreach (var scopeGroup in groupedByScope)
            {
                var scope = scopeGroup.Key;
                var groupedByPath = scopeGroup.GroupBy(x => x.path);

                foreach (var pathGroup in groupedByPath)
                {
                    var path = pathGroup.Key;

                    var fileMetaData = new FileMetaData();
                    foreach (var item in pathGroup)
                    {
                        fileMetaData.Add(item.key, item.value);
                    }

                    defaultMetaDatas.Add(new DefaultMetadata
                    {
                        Path = path,
                        Scope = scope,
                        Values = fileMetaData
                    });
                }
            }
            return defaultMetaDatas;
        }
    }

    public readonly record struct MetaItem(string scope, string path, string key, string value);
}
