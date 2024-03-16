﻿// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

// using FluentAssertions;
// using Kaylumah.Ssg.Manager.Site.Service.Files.Metadata;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Ssg.Extensions.Metadata.Abstractions;
// using Test.Unit.FormerXunit.Mocks;
// using Xunit;

// namespace Test.Unit.FormerXunit
// {
//     public class FileMetadataParserTests
//     {
//         [Fact]
//         public void Test_FilemetadataParser_EmptyFileWithoutConfigOnlyGetsDefaultValues()
//         {
//             // Arange
//             MetadataParserOptions optionsMock = new MetadataParserOptions();
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();

//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, null));

//             LoggerMock<FileParser> loggerMock = new LoggerMock<FileParser>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, optionsMock);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(1, "Only URI is added by default");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("file.html");
//         }

//         [Fact]
//         public void Test_FilemetadataParser_EmptyFileWithConfigThatIsEmptyOnlyGetsDefaultValues()
//         {
//             // Arange
//             MetadataParserOptions options = new MetadataParserOptions
//             {
//                 Defaults = new DefaultMetadatas {
//                     new DefaultMetadata {
//                         Path = string.Empty,
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {}
//                     }
//                 }
//             };
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();

//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, null));

//             Mock<ILogger<FileParser>> loggerMock = new Mock<ILogger<FileParser>>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, options);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(1, "Only URI is added by default");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("file.html");
//         }

//         [Fact]
//         public void Test_FilemetadataParser_EmptyFileWithConfigTGetsDefaultValuesAndConfiguration()
//         {
//             // Arange
//             MetadataParserOptions options = new MetadataParserOptions
//             {
//                 Defaults = new DefaultMetadatas {
//                     new DefaultMetadata {
//                         Path = string.Empty,
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Layout = "default.html"
//                         }
//                     }
//                 }
//             };
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();

//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, null));

//             Mock<ILogger<FileParser>> loggerMock = new Mock<ILogger<FileParser>>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, options);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(2, "Defaults = 1 + Applied Config = 1, Makes 2 values");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("file.html");
//             result.FrontMatter.Layout.Should().NotBeNull();
//             result.FrontMatter.Layout.Should().Be("default.html");
//         }

//         [Fact]
//         public void Test_FilemetadataParser_EmptyFileWithConfigTGetsDefaultValuesAndMultipleConfigurations()
//         {
//             // Arange
//             MetadataParserOptions options = new MetadataParserOptions
//             {
//                 Defaults = new DefaultMetadatas {
//                     new DefaultMetadata {
//                         Path = string.Empty,
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Layout = "default.html"
//                         }
//                     },
//                     new DefaultMetadata {
//                         Path = "test",
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Collection = "test"
//                         }
//                     }
//                 }
//             };
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();
//             FileMetaData data = new FileMetaData
//             {
//                 OutputLocation = "test/:name:ext"
//             };
//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, data));

//             Mock<ILogger<FileParser>> loggerMock = new Mock<ILogger<FileParser>>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, options);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "test/file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(3, "Defaults = 1 + Applied Config = 2, Makes 3 values");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("test/file.html");
//             result.FrontMatter.Layout.Should().NotBeNull();
//             result.FrontMatter.Layout.Should().Be("default.html");
//             result.FrontMatter.Collection.Should().NotBeNull();
//             result.FrontMatter.Collection.Should().Be("test");
//         }

//         [Fact]
//         public void Test_FilemetadataParser_EmptyFileIfMultipleConfigurationsApplyLastOneWins()
//         {
//             // Arange
//             MetadataParserOptions options = new MetadataParserOptions
//             {
//                 Defaults = new DefaultMetadatas {
//                     new DefaultMetadata {
//                         Path = string.Empty,
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Layout = "default.html"
//                         }
//                     },
//                     new DefaultMetadata {
//                         Path = "test",
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Layout = "other.html",
//                             Collection = "test"
//                         }
//                     }
//                 }
//             };
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();

//             FileMetaData meta = new FileMetaData()
//             {
//                 OutputLocation = "test/:name:ext"
//             };
//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, meta));

//             LoggerMock<FileParser> loggerMock = new LoggerMock<FileParser>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, options);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "test/file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(3, "Defaults = 1 + Applied Config = 2, Makes 3 values");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("test/file.html");
//             result.FrontMatter.Layout.Should().NotBeNull();
//             result.FrontMatter.Layout.Should().Be("other.html");
//             result.FrontMatter.Collection.Should().NotBeNull();
//             result.FrontMatter.Collection.Should().Be("test");
//         }

//         [Fact]
//         public void Test_FilemetadataParser_MultipleLayers()
//         {
//             // Arange
//             MetadataParserOptions options = new MetadataParserOptions
//             {
//                 Defaults = new DefaultMetadatas {
//                     new DefaultMetadata {
//                         Path = string.Empty,
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Layout = "default.html"
//                         }
//                     },
//                     new DefaultMetadata {
//                         Path = "test",
//                         Extensions = [ ".html" ],
//                         Values = new FileMetaData {
//                             Collection = "test"
//                         }
//                     }
//                 }
//             };
//             Mock<IFrontMatterMetadataProvider> metadataProviderMock = new Mock<IFrontMatterMetadataProvider>();

//             FileMetaData data = new FileMetaData
//             {
//                 OutputLocation = "posts/2021/:name:ext"
//             };

//             metadataProviderMock
//                 .Setup(x => x.Retrieve<FileMetaData>(It.Is<string>(p => p.Equals(string.Empty))))
//                 .Returns(new ParsedFile<FileMetaData>(null, data));

//             Mock<ILogger<FileParser>> loggerMock = new Mock<ILogger<FileParser>>();
//             FileParser sut = new FileParser(loggerMock.Object, metadataProviderMock.Object, options);
//             MetadataCriteria criteria = new MetadataCriteria
//             {
//                 Content = string.Empty,
//                 FileName = "posts/2021/file.html"
//             };

//             // Act
//             ParsedFile<FileMetaData> result = sut.Parse(criteria);

//             // Assert
//             result.Should().NotBeNull();
//             result.FrontMatter.Should().NotBeNull();
//             result.FrontMatter.Count.Should().Be(2, "Defaults = 1 + Applied Config = 1, Makes 2 values");
//             result.FrontMatter.Uri.Should().NotBeNull();
//             result.FrontMatter.Uri.Should().Be("posts/2021/file.html");
//             result.FrontMatter.Layout.Should().NotBeNull();
//             result.FrontMatter.Layout.Should().Be("default.html");
//         }
//     }
// }
