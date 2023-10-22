﻿// Copyright (c) Kaylumah, 2023. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Kaylumah.Ssg.Manager.Site.Service;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Test.Specflow.Steps
{
    [Binding]
    public class SiteInfoStepDefinitions
    {
        private readonly SiteInfo _siteInfo;

        public SiteInfoStepDefinitions(SiteInfo siteInfo)
        {
            _siteInfo = siteInfo;
        }

        [Given("the following collections:")]
        public void GivenTheFollowingCollections(Table table)
        {
            IEnumerable<Collection> collections = table.CreateSet<Collection>();
            _siteInfo.Collections = new Kaylumah.Ssg.Manager.Site.Service.Collections();
            foreach (Collection collection in collections)
            {
                _siteInfo.Collections.Add(collection);
            }
        }

        [Given("the following site info:")]
        public void GivenTheFollowingSiteInfo(Table table)
        {
            (string title, string description, string Language, string url, string baseUrl, string[] supportedFileExtensions) data = table.CreateInstance<(string title, string description, string Language, string url, string baseUrl, string[] supportedFileExtensions)>();
            _siteInfo.Url = data.url;
            _siteInfo.Title = data.title;
            _siteInfo.Description = data.description;
            _siteInfo.Lang = data.Language;
            _siteInfo.BaseUrl = data.baseUrl;
            _siteInfo.SupportedFileExtensions = new HashSet<string>(data.supportedFileExtensions);
            _siteInfo.SupportedDataFileExtensions = new HashSet<string>() { ".yml" };
        }
    }
}
