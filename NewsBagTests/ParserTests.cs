using NewsBag.Models;
using NewsBag.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace NewsBagTests
{
    public class ParserTests
    {
        private static readonly string[][] dataForParser =
        {
            new string[]{"rbc.ru", "<rss xmlns:rbc_news=\"http://www.rbc.ru\" version=\"2.0\"><item><title>Доходы Кадырова сократились за год в 14 раз</title><link>https://www.rbc.ru/politics/06/05/2022/62754ea59a79479927ecb5a4</link> <pubDate>Fri, 06 May 2022 21:19:43 +0300</pubDate> <description>В 2021 году Кадыров заработал 26,5 млн руб., что в 14 раз меньше, чем в 2020-м, когда его доход составил 381,19 млн руб.</description> <category>Политика</category> <author>Кирилл Соколов</author> <guid isPermaLink=\"false\">rssexport.rbc.ru::62754ea59a79479927ecb5a4</guid> <rbc_news:pdalink>https://www.rbc.ru/politics/06/05/2022/62754ea59a79479927ecb5a4</rbc_news:pdalink> <rbc_news:full-text>Доходы главы Чечни Рамзана Кадырова в 2021 году составили 26,5 млн руб., следует из декларации о доходах, опубликованной на его сайте. В 2020 году доход Кадырова составлял 381,19 млн руб., что в 14,3 раза больше, чем в 2021-м. В собственности у Кадырова два земельных участка, площадь которых составляет 3668 и 28 361 кв. м, а также жилой дом площадью 2344 кв. м, следует из документа. Те же объекты собственности были указаны в декларации за 2020 год. Жена Кадырова в 2021 году заработала 1,52 млн руб., что чуть меньше, чем в 2020-м (1,59 млн руб.), следует из декларации. В собственности у супруги главы Чечни находится квартира площадью 209,8 кв. м. Данные о ее собственности совпадают с прошлогодними. В семье Кадырова растут 10 несовершеннолетних детей, указано в документе, годом ранее там упоминались 11. Рамзан Кадыров занимает пост главы Чечни с 2007 года, он находится на своей должности дольше всех других руководителей российских регионов. В 2020 году Кадыров стал самым богатым среди всех российских губернаторов — с доходом 381,1 млн руб. Второе место занял глава Марий Эл Александр Евстифеев (84,7 млн руб.), в тройку лидеров тогда также вошел губернатор Подмосковья Андрей Воробьев (79,8 млн руб.). В общей сложности по итогам 2020 года главы регионов со второго по десятое место заработали около 378 млн руб. Таким образом, за 2020 год Кадыров заработал больше их всех, вместе взятых.</rbc_news:full-text> <rbc_news:anons>В 2021 году Кадыров заработал 26,5 млн руб., что в 14 раз меньше, чем в 2020-м, когда его доход составил 381,19 млн руб.</rbc_news:anons> <rbc_news:news_id>62754ea59a79479927ecb5a4</rbc_news:news_id> <rbc_news:tag>Рамзан Кадыров</rbc_news:tag> <rbc_news:tag>декларация</rbc_news:tag> <rbc_news:tag>доходы</rbc_news:tag> <rbc_news:type>article</rbc_news:type> <rbc_news:newsline>politics</rbc_news:newsline> <rbc_news:newsDate_timestamp>1651861183</rbc_news:newsDate_timestamp> <rbc_news:newsModifDate>Fri, 06 May 2022 21:35:30 +0300</rbc_news:newsModifDate> <enclosure url=\"https://s0.rbk.ru/v6_top_pics/media/img/3/06/756518563562063.jpg\" type=\"image/jpeg\" length=\"0\"/></item></rss>" },
        };



        [TestCaseSource(nameof(dataForParser))]
        public async Task TestOnCorrectParsing(string source, string data)
        {
            var result = new ObservableCollection<NewsItem>();
            using var stream = GenerateStreamFromString(data);
            await OneParser.GetNews(result, source, stream);
            // Assert
            Assert.That(result.Count > 0);
            var titleOrDescriptionIsNull = string.IsNullOrEmpty(result[0].Title) || string.IsNullOrEmpty(result[0].Description);
            // Assert
            Assert.AreEqual(false, titleOrDescriptionIsNull);
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}