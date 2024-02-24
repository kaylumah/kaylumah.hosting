﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ssg.Extensions.Metadata.Abstractions
{
    [DebuggerDisplay("PageMetaData '{Uri}'")]
    public class PageMetaData
    {
        public string Id
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Id));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Id), value);
            }
        }
        public string Title => _InternalData.GetValue<string>(nameof(Title));
        public string Description => _InternalData.GetValue<string>(nameof(Description));
        public string Language => _InternalData.GetValue<string>(nameof(Language));
        public string Author => _InternalData.GetValue<string>(nameof(Author));
        public string Organization => _InternalData.GetValue<string>(nameof(Organization));
        public bool SocialShare => _InternalData.GetBoolValue(nameof(SocialShare));
        public bool Sitemap => _InternalData.GetBoolValue(nameof(Sitemap));
        public bool Feed => _InternalData.GetBoolValue(nameof(Feed));
        public bool Featured => _InternalData.GetBoolValue(nameof(Featured));
        public string LdJson
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(LdJson));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(LdJson), value);
            }
        }
        public string MetaTags
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(MetaTags));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(MetaTags), value);
            }
        }
        public string Layout => _InternalData.GetValue<string>(nameof(Layout));
        public string Uri => _InternalData.GetValue<string>(nameof(Uri));
        public string Image => _InternalData.GetValue<string>(nameof(Image));
        public string CommentId => _InternalData.GetValue<string>(nameof(CommentId));

        public string Name
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Name));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Name), value);
            }
        }

        public string Content
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Content));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Content), value);
            }
        }

        public string Collection
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Collection));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Collection), value);
            }
        }

        public string Series
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Series));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Series), value);
            }
        }

        public List<string> Tags
        {
            get
            {
                List<string>? tags = _InternalData.GetValue<List<object>>(nameof(Tags))?.Cast<string>().ToList();
                return tags ?? new List<string>();
            }
            set
            {
                _InternalData.SetValue(nameof(Tags), value);
            }
        }

        public string Type
        {
            get
            {
                string result = _InternalData.GetValue<string>(nameof(Type));
                return result;
            }
            set
            {
                _InternalData.SetValue(nameof(Type), value);
            }
        }

        public DateTimeOffset Published => GetPublishedDate();
        public DateTimeOffset Modified => _InternalData.GetValue<DateTimeOffset>(nameof(Modified));

        readonly Dictionary<string, object?> _InternalData;

        public PageMetaData(Dictionary<string, object?> internalData)
        {
            _InternalData = internalData;
        }

        protected virtual DateTimeOffset GetPublishedDate()
        {
            DateTimeOffset result = _InternalData.GetValue<DateTimeOffset>(nameof(Published));
            return result;
        }

        public static implicit operator Dictionary<string, object?>(PageMetaData page) => page._InternalData;
    }

    public class Article : PageMetaData
    {
        public Article(Dictionary<string, object?> internalData) : base(internalData)
        {
        }
    }
}
