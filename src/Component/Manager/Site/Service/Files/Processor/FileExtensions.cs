﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using Kaylumah.Ssg.Engine.Transformation.Interface;

namespace Kaylumah.Ssg.Manager.Site.Service.Files.Processor;

public static class FileExtensions
{
    public static PageMetaData ToPage(this File file)
    {
        return new PageMetaData(file.MetaData, file.Name, file.Content, file.LastModified);
    }

    public static PageMetaData ToPage(this File file, Guid siteGuid)
    {
        var page = file.ToPage();
        page.Id = siteGuid.CreatePageGuid(file.MetaData.Uri).ToString();
        return page;
    }

    public static PageMetaData[] ToPages(this File[] files, Guid siteGuid)
    {
        return files.Select(x => ToPage(x, siteGuid)).ToArray();
    }
}