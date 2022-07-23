﻿// Copyright (c) Kaylumah, 2022. All rights reserved.
// See LICENSE file in the project root for full license information.

using Test.Specflow.Helpers;

namespace Test.Specflow.Entities;

public class Organization
{
    [GherkinTableHeader(0)]
    public string Id { get; set; }
}

public class OrganizationCollection : MockEntityCollection<string, Organization>
{
    public override string BuildKey(Organization item)
    {
        return item.Id;
    }
}
