using System.Linq;
using System.Text.RegularExpressions;
using BookPortal.Web.Models;
using HtmlAgilityPack;

namespace BookPortal.Web.Services
{
    public class ImporterService
    {
        public ImportEditionDto ParseOzonPage(string html)
        {
            ImportEditionDto importEdition = new ImportEditionDto();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            // book name
            var name = document.DocumentNode.SelectSingleNode("//h1[@itemprop='name']");
            importEdition.Name = name.InnerText.Trim();

            // book authors
            var authors = document.DocumentNode.SelectNodes("//p[@itemprop='author']/a");
            importEdition.Authors = string.Join(", ", authors.Select(c => c.InnerText));

            // publishers
            var publishers = document.DocumentNode.SelectNodes("//p[@itemprop='publisher']/a");
            importEdition.Publishers = string.Join(", ", publishers.Select(c => c.InnerText));

            // ISBN and year
            var isbns = document.DocumentNode.SelectSingleNode("//p[@itemprop='isbn']");
            var matchYear = Regex.Match(isbns.InnerText, @"ISBN(.+?);(.*)г\.", RegexOptions.Singleline);
            if (matchYear.Groups.Count > 0)
            {
                importEdition.Isbn = matchYear.Groups[1].Value.Trim();
                importEdition.Year = matchYear.Groups[2].Value.Trim();
            }

            // pages
            var pages = document.DocumentNode.SelectSingleNode("//span[@itemprop='numberOfPages']");
            importEdition.Pages = pages.InnerText.Trim();

            // language
            var language = document.DocumentNode.SelectSingleNode("//p[@itemprop='inLanguage']");
            var matchLanguage = Regex.Match(language.InnerText, @"Язык:(.*)", RegexOptions.Singleline);
            if (matchLanguage.Groups.Count > 0)
                importEdition.Language = matchLanguage.Groups[1].Value.Trim().ToLowerInvariant();

            // book format
            var coverType = document.DocumentNode.SelectSingleNode("//span[@itemprop='bookFormat']");
            importEdition.CoverType = coverType.InnerText.Trim().ToLowerInvariant();

            // serie
            var matchSerie = Regex.Match(html, @"Серия:\s+<a href=\'\/context\/detail\/id\/([0-9]+)\/\'[^>]+?>(.+?)<\/a>");
            if (matchSerie.Groups.Count > 0)
                importEdition.Serie = matchSerie.Groups[2].Value;

            // format
            var matchFormat = Regex.Match(html, @"Формат.*?\<span.*?>(.*?)(\s|\<\/span>)", RegexOptions.Singleline);
            if (matchFormat.Groups.Count > 0)
                importEdition.Format = matchFormat.Groups[1].Value;

            // published count
            var matchCount = Regex.Match(html, @"Тираж.*?([0-9]+) экз\.", RegexOptions.Singleline);
            if (matchCount.Groups.Count > 0)
                importEdition.Count = matchCount.Groups[1].Value;

            return importEdition;
        }
    }
}
