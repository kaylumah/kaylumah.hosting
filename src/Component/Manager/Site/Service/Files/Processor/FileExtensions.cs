﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Ssg.Extensions.Metadata.Abstractions;

namespace Kaylumah.Ssg.Manager.Site.Service.Files.Processor
{
    public static class FileExtensions
    {
        public static Dictionary<string, object?> ToDictionary(this File file)
        {
            Dictionary<string, object?> result = new Dictionary<string, object?>(file.MetaData);
            // result.SetValue(nameof(file.LastModified), file.LastModified);
            result.SetValue(nameof(file.Content), file.Content);
            result.SetValue(nameof(file.Name), file.Name);
            return result;
        }

        public static PageMetaData ToPage(this File file)
        {
            Dictionary<string, object?> data = file.ToDictionary();
            PageMetaData result = new PageMetaData(data);
            return result;
        }

        public static PageMetaData ToPage(this File file, Guid siteGuid)
        {
            PageMetaData page = file.ToPage();
            page.Id = file.ToPageId(siteGuid);
            return page;
        }

        internal static string ToPageId(this File file, Guid siteGuid)
        {
            Guid pageGuid = file.ToPageGuid(siteGuid);
            string id = pageGuid.ToString();
            return id;
        }

        internal static Guid ToPageGuid(this File file, Guid siteGuid)
        {
            Guid pageGuid = siteGuid.CreatePageGuid(file.MetaData.Uri);
            return pageGuid;
        }
    }
}
