﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.Ssg.Manager.Site.Service.SiteMap;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace System
{
    public static partial class ByteExtensions
    {
        public static SiteMap ToSiteMap(this byte[] bytes)
        {
            using MemoryStream stream = new MemoryStream(bytes);
            using XmlReader xmlReader = XmlReader.Create(stream);
            XmlDocument document = new XmlDocument();
            document.Load(xmlReader);
            XmlNode root = document.DocumentElement?.SelectSingleNode("//*[local-name()='urlset']");
            XmlNodeList children = root?.SelectNodes("//*[local-name()='url']");

            List<SiteMapNode> nodes = new List<SiteMapNode>();
            foreach (XmlNode child in children)
            {
                // TODO better solution does not work
                //                 string location = child.SelectSingleNode("//*[local-name()='loc']")?.InnerText;
                string location = child.ChildNodes[0]?.InnerText;
                string lastModified = child.ChildNodes[1]?.InnerText;
                nodes.Add(new SiteMapNode()
                {
                    Url = location,
#pragma warning disable
                    LastModified = DateTimeOffset.Parse(lastModified)
                });
            }

            SiteMap sitemap = new SiteMap()
            {
                Items = nodes
            };

            return sitemap;
        }
    }
}