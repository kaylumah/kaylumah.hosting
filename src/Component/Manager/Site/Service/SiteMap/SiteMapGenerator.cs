﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Kaylumah.Ssg.Utilities;
using Microsoft.Extensions.Logging;
using Ssg.Extensions.Metadata.Abstractions;

namespace Kaylumah.Ssg.Manager.Site.Service.SiteMap
{
    public partial class SiteMapGenerator
    {
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Information,
            Message = "Generate SiteMap")]
        private partial void LogGenerateSiteMap();

        readonly ILogger _Logger;
        public SiteMapGenerator(ILogger<SiteMapGenerator> logger)
        {
            _Logger = logger;
        }

        public SiteMap Create(SiteMetaData siteMetaData)
        {
            LogGenerateSiteMap();

            IEnumerable<PageMetaData> sitePages = siteMetaData.GetPages();
            IEnumerable<PageMetaData> htmlPages = sitePages.Where(file => file.IsHtml());
            IEnumerable<PageMetaData> without404 = htmlPages.Where(file => {
                bool is404 = file.IsUrl("404.html");
                bool result = is404 == false;
                return result;
            });

            List<PageMetaData> pages = without404.ToList();

            List<SiteMapNode> siteMapNodes = new List<SiteMapNode>();
            foreach (PageMetaData page in pages)
            {
                SiteMapNode node = new SiteMapNode();
                Uri siteMapUri = page.CanonicalUri;
                node.Url = siteMapUri.ToString();
                node.LastModified = page.Modified;
                siteMapNodes.Add(node);
            }

            SiteMap siteMap = new SiteMap("sitemap.xml", siteMapNodes);
            return siteMap;
        }
    }
}
