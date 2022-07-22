﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
using Kaylumah.Ssg.Manager.Site.Service.Files.Processor;
using Test.Specflow.Entities;

namespace Test.Specflow;

[Binding]
public class Transforms
{
    [StepArgumentTransformation]
    public static string ToNullableString(string value)
    {
        return Constants.NullIndicator.Equals(value, System.StringComparison.Ordinal) ? null : value;
    }
    
    [StepArgumentTransformation]
    public List<String> TransformToListOfString(string commaSeparatedList)
    {
        return commaSeparatedList.Split(",").ToList();
    }
    
    [StepArgumentTransformation]
    public static ArticleCollection ToArticles(Table table)
    {
        var articles = table.CreateSet<Article>();
        var collection = new ArticleCollection();
        collection.AddRange(articles);
        return collection;
    }

    [StepArgumentTransformation]
    public static DefaultMetadatas ToDefaultMetadatas(Table table)
    {
        var defaultMetaDatas = new DefaultMetadatas();
        var items = table
            .CreateSet<(string Scope, string Path, string Key, string Value)>()
            .ToList();
        var groupedByScope = items.GroupBy(x => x.Scope);
        foreach (var scopeGroup in groupedByScope)
        {
            var scope = scopeGroup.Key;
            var groupedByPath = scopeGroup.GroupBy(x => x.Path);
            foreach (var pathGroup in groupedByPath)
            {
                var path = pathGroup.Key;
                var fileMetaData = new FileMetaData();
                foreach (var item in pathGroup)
                {
                    fileMetaData.Add(item.Key, item.Value);
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

    [StepArgumentTransformation]
    private static FileFilterCriteria ToFileFilterCriteria(Table table)
    {
        var criteria = table.CreateInstance<FileFilterCriteria>();
        return criteria;
    }
}
