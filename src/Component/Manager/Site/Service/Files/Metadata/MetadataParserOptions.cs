﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Kaylumah.Ssg.Manager.Site.Service.Files.Metadata
{
    public class MetadataParserOptions
    {
        public static readonly string Options;

        static MetadataParserOptions()
        {
            Options = "Metadata";
        }

        public DefaultMetadatas Defaults
        { get; set; } = new DefaultMetadatas();
        public Dictionary<string, string> ExtensionMapping
        { get; set; } = new Dictionary<string, string>();
        public string FallbackOutputLocation
        { get; set; } = string.Empty;
    }
}
