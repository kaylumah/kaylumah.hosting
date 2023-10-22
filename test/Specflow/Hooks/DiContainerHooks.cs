﻿// Copyright (c) Kaylumah, 2023. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.IO.Abstractions.TestingHelpers;
using BoDi;
using TechTalk.SpecFlow;

namespace Test.Specflow
{
    [Binding]
    public class DiContainerHooks
    {
        readonly IObjectContainer _ObjectContainer;

        public DiContainerHooks(IObjectContainer objectContainer)
        {
            _ObjectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            MockFileSystem mockFileSystem = new MockFileSystem();
            _ObjectContainer.RegisterInstanceAs(mockFileSystem);
        }
    }
}
