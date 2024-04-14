// Copyright (c) Kaylumah, 2024. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Text;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Kaylumah.Ssg.Utilities
{

    public static class MarkdownUtil
    {
        public static string ToHtml(string source)
        {
            MarkdownPipeline pipeline = BuildPipeline();
            string intermediateResult = Markdown.ToHtml(source, pipeline);
            string result = intermediateResult.Trim();
            return result;
        }

        public static string ToText(string source)
        {
            MarkdownPipeline pipeline = BuildPipeline();
            MarkdownDocument document = Markdown.Parse(source, pipeline);
            string textExcludingCodeBlocksAndImages = GetTextExcludingCodeBlocks(document);
            string result = textExcludingCodeBlocksAndImages.Trim();
            return result;
        }

        static string GetTextExcludingCodeBlocks(MarkdownDocument document)
        {
            StringBuilder textBuilder = new StringBuilder();
            foreach (Block block in document)
            {
                string textFromBlock = GetTextFromBlock(block);
                textBuilder.AppendLine(textFromBlock);
            }

            string result = textBuilder.ToString();
            return result;
        }

        static string GetTextFromBlock(Block block)
        {
            StringBuilder textBuilder = new StringBuilder();
            if (block is LeafBlock leafBlock && !(block is FencedCodeBlock))
            {
                if (leafBlock.Inline == null)
                {
                    return string.Empty;
                }

                foreach (Inline inline in leafBlock.Inline!)
                {
                    if (inline is LiteralInline literal)
                    {
                        textBuilder.Append(literal.Content);
                    }
                    else if (inline is ContainerInline nestedContainer)
                    {
                        string textFromContainer = GetTextFromContainer(nestedContainer);
                        textBuilder.Append(textFromContainer);
                    }
                }
            }
            else if (block is ContainerBlock containerBlock)
            {
                foreach (Block subBlock in containerBlock)
                {
                    string textFromSubBlock = GetTextFromBlock(subBlock);
                    textBuilder.Append(textFromSubBlock);
                }
            }
            string result = textBuilder.ToString();
            return result;
        }

        static string GetTextFromContainer(ContainerInline container)
        {
            StringBuilder textBuilder = new StringBuilder();
            foreach (Inline inline in container)
            {
                if (inline is LiteralInline literal)
                {
                    textBuilder.Append(literal.Content);
                }
                else if (inline is ContainerInline nestedContainer)
                {
                    string textFromNestedContainer = GetTextFromContainer(nestedContainer);
                    textBuilder.Append(textFromNestedContainer);
                }
            }
            string result = textBuilder.ToString();
            return result;
        }
       
        static MarkdownPipeline BuildPipeline()
        {
            // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/YamlSpecs.md
            // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/AutoIdentifierSpecs.md
            // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/PipeTableSpecs.md
            // https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/GenericAttributesSpecs.md

            MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
                .UseYamlFrontMatter() // needed to remove any frontmatter
                .UseAutoIdentifiers() // used for clickable headers
                .UsePipeTables() // support for tables
                .UseGenericAttributes() // support for inline attributes (like width, height)
                .Use<MarkdownExtensionEnsureAbsoluteLink>()
                .Use<MarkdownExtensionEnsureExternalLink>()
                .Use<MarkdownExtensionClickableHeaderLink>()
                .Use<MarkdownExtensionUsePictures>()
                .Build();
            return pipeline;
        }
    }
}