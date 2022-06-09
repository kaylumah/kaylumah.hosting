﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Kaylumah.Ssg.Engine.Transformation.Interface;

[DebuggerDisplay("TagMetaData '{Name}'")]
public class TagMetaData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class TagMetaDataCollection : KeyedCollection<string, TagMetaData>
{
    protected override string GetKeyForItem(TagMetaData item)
    {
        return item.Id;
    }

    public new IDictionary<string, TagMetaData> Dictionary => base.Dictionary;

    public IEnumerable<string> Keys => base.Dictionary?.Keys ?? Enumerable.Empty<string>();

}
