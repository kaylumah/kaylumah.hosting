// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.Ssg.Manager.Site.Service.Search
{
    public class SearchIndex
    {
        public IndexItem[] IndexItems
        { get; set; }

        public SearchIndex(IndexItem[] indexItems)
        {
            IndexItems = indexItems;
        }
    }
}