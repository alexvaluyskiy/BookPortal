using System;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Xunit;

namespace BookPortal.Web.Tests.Services
{
    public class ImporterServiceTests
    {
        private readonly ImportersService _service;

        public ImporterServiceTests()
        {
            _service = new ImportersService();
        }

        [Fact]
        public void BookNameParseSuccessTest()
        {
            string html = @"<h1 itemprop=""name"">Книжный вор</h1>";
            string expectedString = "Книжный вор";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Name);
            Assert.Equal(BookType.Normal, importEdition.BookType);
        }

        [Fact]
        public void BookAuthorParseSuccessTest()
        {
            string html = @"<p itemprop=""author"">Автор: <a>Эдгар Аллан По</a></p>";
            string expectedString = "Эдгар Аллан По";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Authors);
        }

        [Fact]
        public void BookAuthorManyParseSuccessTest()
        {
            string html = @"<p itemprop=""author"">Автор: <a>Эдгар Аллан По</a>, <a>Джон Сноу</a></p>";
            string expectedString = "Эдгар Аллан По, Джон Сноу";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Authors);
        }

        [Fact]
        public void BookPublisherParseSuccessTest()
        {
            string html = @"<p itemprop=""publisher"">Издательство: <a>Рипол Классик</a></p>";
            string expectedString = "Рипол Классик";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Publishers);
        }

        [Fact]
        public void BookIsbnYearParseSuccessTest()
        {
            string html = @"<p itemprop=""isbn"">ISBN 978-5-386-05405-2; 2013 г.</p>";
            string expectedIsbn = "978-5-386-05405-2";
            int expectedYear = 2013;

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedIsbn, importEdition.Isbn);
            Assert.Equal(expectedYear, importEdition.Year);
        }

        [Fact]
        public void BookPagesParseSuccessTest()
        {
            string html = @"<span itemprop=""numberOfPages"">224</span>";
            int expectedString = 224;

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Pages);
        }

        [Theory]
        [InlineData(@"<p itemprop=""inLanguage"">Язык: Русский</p>")]
        [InlineData(@"<p itemprop=""inLanguage"">Языки: Русский</p>")]
        [InlineData(@"<span itemprop=""inLanguage"">Русский</span>")]
        public void BookLanguageParseSuccessTest(string html)
        {
            string expectedString = "русский";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Language);
        }

        [Fact]
        public void BookCoverTypeHardcoverParseSuccessTest()
        {
            string html = @"<span itemprop=""bookFormat"">Твердый переплет</span>";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(CoverType.Hardcover, importEdition.CoverType);
            Assert.False(importEdition.SuperCover);
        }

        [Fact]
        public void BookCoverTypePaperbackParseSuccessTest()
        {
            string html = @"<span itemprop=""bookFormat"">Мягкая обложка</span>";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(CoverType.Paperback, importEdition.CoverType);
            Assert.False(importEdition.SuperCover);
        }

        [Fact]
        public void BookCoverTypeHardcoverSuperParseSuccessTest()
        {
            string html = @"<span itemprop=""bookFormat"">Твердый переплет, суперобложка</span>";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(CoverType.Hardcover, importEdition.CoverType);
            Assert.True(importEdition.SuperCover);
        }

        [Theory]
        [InlineData(@"<p>Серия: <a href='/context/detail/id/25193909/' title='Коллекция Метаморфозы'>Коллекция Метаморфозы</a></p>")]
        [InlineData(@"<p>Серия: <a>Коллекция Метаморфозы</a></p>")]
        public void BookSerieParseSuccessTest(string html)
        {
            string expectedString = "Коллекция Метаморфозы";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Serie);
        }

        [Fact]
        public void BookFormatParseSuccessTest()
        {
            string html = @"<div><span>Формат</span></div><div><span>60x90/16 (145х215 мм)</span></div>";
            string expectedString = "60x90/16";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Format);
        }

        [Fact]
        public void BookCountParseSuccessTest()
        {
            string html = @"<div><span>Тираж</span></div><div><span>27004 экз.</span></div>";
            int expectedString = 27004;

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Count);
        }

        [Fact]
        public void BookAnnotationParseSuccessTest()
        {
            string html = @"<!-- Data[ANNOTATION] --> Сорок лет назад это считалось фантастикой. Сорок лет назад это читалось как фантастика.</td>";
            string expectedString = "Сорок лет назад это считалось фантастикой. Сорок лет назад это читалось как фантастика.";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedString, importEdition.Annotation);
        }

        [Fact]
        public void BookTypeCollectionParseSuccessTest()
        {
            string html = @"<p>Авторский сборник</p>";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(BookType.Collection, importEdition.BookType);
        }

        [Fact]
        public void BookTypeAntologyParseSuccessTest()
        {
            string html = @"<p>Антология</p>";

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(BookType.Antology, importEdition.BookType);
        }

        [Fact]
        public void BookCoverParseSuccessTest()
        {
            string html = @"<link rel=""image_src"" href=""//static1.ozone.ru/multimedia/books_covers/c300/1009507913.jpg""></link>";
            Uri expectedUri = new Uri("http://static.ozone.ru/multimedia/books_covers/1009507913.jpg");

            ImportEditionDto importEdition = _service.ParseOzonPage(html);

            Assert.Equal(expectedUri, importEdition.CoverUri);
        }
    }
}
