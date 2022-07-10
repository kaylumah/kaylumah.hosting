﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ServiceModel.Syndication;
using HtmlAgilityPack;
using Kaylumah.Ssg.Manager.Site.Service.SiteMap;

namespace Test.Specflow.Utilities;

public static class ArtifactAccessMockExtensions
{
    public static byte[] GetArtifactContents(this ArtifactAccessMock artifactAccess, string path)
    {
        var bytes = artifactAccess
            .Artifacts
            .GetArtifactContents(path);
        return bytes;
    }

    public static HtmlDocument GetHtmlDocument(this ArtifactAccessMock artifactAccess, string path)
    {
        var htmlBytes = artifactAccess.GetArtifactContents(path);
        return htmlBytes.ToHtmlDocument();
    }

    public static SyndicationFeed GetFeedArtifact(this ArtifactAccessMock artifactAccess, string path = "feed.xml")
    {
        var atomFeedXmlBytes = artifactAccess.GetArtifactContents(path);
        return atomFeedXmlBytes.ToSyndicationFeed();
    }

    public static SiteMap GetSiteMapArtifact(this ArtifactAccessMock artifactAccess, string path = "sitemap.xml")
    {
        var siteMapXmlBytes = artifactAccess.GetArtifactContents(path);
        return siteMapXmlBytes.ToSiteMap();
    }
}
