﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ServiceModel.Syndication;
using Kaylumah.Ssg.Engine.Transformation.Interface;
using Kaylumah.Ssg.Utilities;
using Microsoft.Extensions.Logging;

namespace Kaylumah.Ssg.Manager.Site.Service.Feed
{
    public class FeedGenerator
    {
        private readonly ILogger _logger;

        public FeedGenerator(ILogger<FeedGenerator> logger)
        {
            _logger = logger;
        }

        private IEnumerable<PageMetaData> RetrievePostPageMetaDatas(SiteMetaData siteMetaData)
        {
            if (siteMetaData.Collections.TryGetValue("posts", out var posts))
            {
                return posts
                    .Where(x => bool.Parse((string)x["feed"]));
            }
            return Enumerable.Empty<PageMetaData>();
        }

        public SyndicationFeed Create(SiteMetaData siteMetaData)
        {
            var build = siteMetaData.Build;
            var generatorVersion = build.ShortGitHash;
            var copyrightClaim = build.Copyright;
            var generatedAtBuildTime = build.Time;

            var feed = new SyndicationFeed();
            feed.Language = siteMetaData.Language;
            feed.Title = new TextSyndicationContent(siteMetaData.Title);
            feed.Description = new TextSyndicationContent(siteMetaData.Description);
            feed.Id = GlobalFunctions.AbsoluteUrl("feed.xml");
            feed.Copyright = new TextSyndicationContent(copyrightClaim);
            feed.LastUpdatedTime = generatedAtBuildTime;
            feed.ImageUrl = new Uri(GlobalFunctions.AbsoluteUrl("assets/logo_alt.svg"));
            feed.Generator = "Kaylumah Site Generator";

            feed.Links.Add(new SyndicationLink(new Uri(GlobalFunctions.AbsoluteUrl("feed.xml")))
            {
                RelationshipType = "self",
                MediaType = "application/atom+xml",
            });

            feed.Links.Add(new SyndicationLink(new Uri(GlobalFunctions.AbsoluteUrl("blog.html")))
            {
                RelationshipType = "alternate",
                MediaType = "text/html",
            });

            feed.Links.Add(new SyndicationLink(new Uri(GlobalFunctions.AbsoluteUrl("archive.html")))
            {
                RelationshipType = "related",
                MediaType = "text/html",
            });

            var persons = siteMetaData.ToPersons();
            var tags = siteMetaData.ToCategories();
            var posts = RetrievePostPageMetaDatas(siteMetaData);
            if (posts.Any())
            {
                var items = new List<SyndicationItem>();
                foreach (var pageMetaData in posts)
                {
                    var author = persons[pageMetaData.Author];
                    var pageUrl = GlobalFunctions.AbsoluteUrl(pageMetaData.Url);
                    var item = new SyndicationItem
                    {
                        Id = pageUrl,
                        Title = new TextSyndicationContent(pageMetaData.Title),
                        Summary = new TextSyndicationContent(pageMetaData.Description),
                        Content = new CDataSyndicationContent(new TextSyndicationContent(pageMetaData.Content, TextSyndicationContentKind.Html)),
                        PublishDate = (DateTimeOffset)pageMetaData["date"],
                        LastUpdatedTime = pageMetaData.LastModified
                    };

                    var itemCategories = pageMetaData
                        .Tags
                        .Where(tag => tags.ContainsKey(tag))
                        .Select(tag => tags[tag])
                        .ToList();
                    itemCategories.ForEach(category => item.Categories.Add(category));
                    item.Links.Add(new SyndicationLink(new Uri(pageUrl)));
                    item.Authors.Add(author);
                    items.Add(item);

                }
                feed.Items = items;
            }

            feed.Items = feed.Items.OrderByDescending(x => x.PublishDate);
            return feed;
        }
    }
}
