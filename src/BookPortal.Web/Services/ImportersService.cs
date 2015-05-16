using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using BookPortal.Web.Domain.Models.Types;
using BookPortal.Web.Models;
using HtmlAgilityPack;
using Microsoft.Framework.Logging;

namespace BookPortal.Web.Services
{
    public class ImportersService
    {
        private readonly ILogger _logger;

        public ImportersService(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<ImportersService>();
        }

        public ImportEditionDto ParseOzonPage(string html)
        {
            ImportEditionDto importEdition = new ImportEditionDto();

#if DNX451
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            try
            {
                // book name
                var name = document.DocumentNode.SelectSingleNode("//h1[@itemprop='name']");
                importEdition.Name = name?.InnerText.Trim();

                // book authors
                var authors = document.DocumentNode.SelectNodes("//p[@itemprop='author']/a");
                if (authors != null)
                    importEdition.Authors = string.Join(", ", authors.Select(c => c.InnerText));

                // publishers
                var publishers = document.DocumentNode.SelectNodes("//p[@itemprop='publisher']/a");
                if (publishers != null)
                    importEdition.Publishers = string.Join(", ", publishers.Select(c => c.InnerText));

                // ISBN and year
                var isbns = document.DocumentNode.SelectSingleNode("//p[@itemprop='isbn']");
                if (isbns != null)
                {
                    var matchYearIsbn = Regex.Match(isbns.InnerText, @"ISBN(?<isbn>.+?);(?<year>.*)г\.");

                    if (matchYearIsbn.Success)
                    {
                        importEdition.Isbn = matchYearIsbn.Groups["isbn"].Value.Trim();
                        importEdition.Year = int.Parse(matchYearIsbn.Groups["year"].Value);
                    }
                    else
                    {
                        var matchYear = Regex.Match(isbns.InnerText, @"(?<year>.*)г\.");
                        if (matchYear.Success)
                            importEdition.Year = int.Parse(matchYear.Groups["year"].Value);
                    }
                }

                // pages
                var pages = document.DocumentNode.SelectSingleNode("//span[@itemprop='numberOfPages']");
                if (pages != null)
                {
                    importEdition.Pages = int.Parse(pages.InnerText);
                }

                // language
                var language = document.DocumentNode.SelectSingleNode("//p[@itemprop='inLanguage']");
                if (language != null)
                {
                    var matchLanguage = Regex.Match(language.InnerText, @"Язык(и|):(?<lang>.*)", RegexOptions.Singleline);
                    importEdition.Language = matchLanguage.Groups["lang"].Value.Trim().ToLowerInvariant();
                }
                else
                {
                    var lang = document.DocumentNode.SelectSingleNode("//span[@itemprop='inLanguage']");
                    importEdition.Language = lang?.InnerText.Trim().ToLowerInvariant();
                }

                // cover type
                var coverType = document.DocumentNode.SelectSingleNode("//span[@itemprop='bookFormat']");
                if (coverType != null)
                {
                    string cover = coverType.InnerText.Trim().ToLowerInvariant();

                    if (cover == "твердый переплет")
                    {
                        importEdition.CoverType = EditionCoverType.Hardcover;
                    }
                    else if (cover == "мягкая обложка")
                    {
                        importEdition.CoverType = EditionCoverType.Paperback;
                    }
                    else if (cover == "твердый переплет, суперобложка" || cover == "суперобложка")
                    {
                        importEdition.CoverType = EditionCoverType.Hardcover;
                        importEdition.SuperCover = true;
                    }
                }

                // serie
                var matchSerie = Regex.Match(html, @"Серия:\s+<a.*?>(?<serie>.+?)<\/a>");
                importEdition.Serie = matchSerie.Groups["serie"].Value;

                // book format
                var matchFormat = Regex.Match(html, @"Формат.*?\<span.*?>(?<format>.*?)(\s|\<\/span>)", RegexOptions.Singleline);
                importEdition.Format = matchFormat.Groups["format"].Value;

                // published count
                var matchCount = Regex.Match(html, @"Тираж.*?(?<count>[0-9]+) экз\.", RegexOptions.Singleline);
                if (matchCount.Success)
                    importEdition.Count = int.Parse(matchCount.Groups["count"].Value);

                // TODO: cover
                var matchCoverUri = Regex.Match(html, @"link rel=\""image_src\"".*?\/multimedia\/books_covers\/c300\/(?<coveruri>\d+).jpg\""");
                if (matchCoverUri.Success)
                {
                    string ozonId = matchCoverUri.Groups["coveruri"].Value;
                    importEdition.CoverUri = new Uri($"http://static.ozone.ru/multimedia/books_covers/{ozonId}.jpg");
                }
            
                // book type
                importEdition.Type = EditionType.Normal;
            
                var matchAntology = Regex.Match(html, @"<p\s*>\s*Антология", RegexOptions.Singleline);
                if (matchAntology.Success)
                    importEdition.Type = EditionType.Antology;

                var matchCollection = Regex.Match(html, @"<p\s*>\s*Авторский сборник", RegexOptions.Singleline);
                if (matchCollection.Success)
                    importEdition.Type = EditionType.Collection;

                // annotation
                var matchAnnotation = Regex.Match(html, @"<\!-- Data\[ANNOTATION\] --\>(?<annotation>.+?)<\/td>");
                importEdition.Annotation = matchAnnotation?.Groups["annotation"].Value.Trim();
            }
            catch (Exception ex)
            {
                var canUrl = document.DocumentNode.SelectSingleNode("//link[@rel='canonical']");
                var canonicalUrl = canUrl.Attributes["href"].Value;

                string message = $"Can't parse Ozon book (book url: {canonicalUrl})";

                _logger.LogError(message);
                throw new SerializationException(message, ex);
            }

#endif

            return importEdition;
        }
    }
}
