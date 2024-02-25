﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
using Kaylumah.Ssg.Manager.Site.Service.Files.Processor;
using Ssg.Extensions.Metadata.Abstractions;

namespace Test.Unit.Extensions
{
    public static partial class MappingExtensions
    {
        public static File ToFile(this PageMetaData pageMetaData)
        {
            FileMetaData fileMetaData = new FileMetaData();
            Dictionary<string, object> data = pageMetaData;
            foreach (KeyValuePair<string, object> item in data)
            {
                fileMetaData.Add(item.Key, item.Value);
            }

            File file = new TextFile(fileMetaData, [], string.Empty);
            return file;
        }

        public static IEnumerable<File> ToFile(this IEnumerable<PageMetaData> pageMetaData)
        {
            return pageMetaData.Select(ToFile);
        }

        public static PageMetaData ToPageMetaData(this Entities.Article article)
        {
            List<object> tags = article.Tags.Cast<object>().ToList();
            Dictionary<string, object> pageDictionary = new Dictionary<string, object>();
            pageDictionary.SetValue(nameof(PageMetaData.Uri), article.Uri);
            pageDictionary.SetValue(nameof(PageMetaData.Name), article.Uri);
            pageDictionary.SetValue(nameof(PageMetaData.Title), article.Title);
            pageDictionary.SetValue(nameof(PageMetaData.Description), article.Description);
            pageDictionary.SetValue(nameof(PageMetaData.Author), article.Author);
            pageDictionary.SetValue("PublishedDate", article.Created.GetValueOrDefault().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            pageDictionary.SetValue("ModifiedDate", article.Modified.GetValueOrDefault().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            pageDictionary.SetValue(nameof(PageMetaData.Type), "Article");
            pageDictionary.SetValue(nameof(PageMetaData.Collection), "posts");
            pageDictionary.SetValue(nameof(Ssg.Extensions.Metadata.Abstractions.Article.Feed), "true");
            pageDictionary.SetValue(nameof(PageMetaData.Sitemap), "true");
            pageDictionary.SetValue(nameof(PageMetaData.Layout), "default.html");
            pageDictionary.SetValue(nameof(PageMetaData.Tags), tags);
            return new PageMetaData(pageDictionary);
        }

        public static IEnumerable<PageMetaData> ToPageMetaData(this IEnumerable<Entities.Article> article)
        {
            return article.Select(ToPageMetaData);
        }

        public static IEnumerable<Entities.Article> ToArticles(this IEnumerable<File> files, Guid siteGuid = default)
        {
            List<PageMetaData> pageMetas = new List<PageMetaData>();
            IEnumerable<TextFile> textFiles = files.OfType<TextFile>();
            foreach (TextFile file in textFiles)
            {
                pageMetas.Add(file.ToPage(siteGuid));
            }

            return pageMetas.ToArticles();
        }

        public static IEnumerable<Entities.Article> ToArticles(this IEnumerable<PageMetaData> pageMetaData)
        {
            return pageMetaData.Select(ToArticle);
        }

        public static Entities.Article ToArticle(this PageMetaData pageMetaData)
        {
            return new Entities.Article()
            {
                Uri = pageMetaData.Uri,
                Title = pageMetaData.Title,
                Description = pageMetaData.Description,
                Author = pageMetaData.Author,
                Created = pageMetaData.Published != DateTimeOffset.MinValue ? pageMetaData.Published : null,
                Modified = pageMetaData.Modified != DateTimeOffset.MinValue ? pageMetaData.Modified : null
            };
        }
    }
}
