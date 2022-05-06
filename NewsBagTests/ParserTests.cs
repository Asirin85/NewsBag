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
            new string[]{"rbc.ru", "<rss xmlns:rbc_news=\"http://www.rbc.ru\" version=\"2.0\"><item><title>������ �������� ����������� �� ��� � 14 ���</title><link>https://www.rbc.ru/politics/06/05/2022/62754ea59a79479927ecb5a4</link> <pubDate>Fri, 06 May 2022 21:19:43 +0300</pubDate> <description>� 2021 ���� ������� ��������� 26,5 ��� ���., ��� � 14 ��� ������, ��� � 2020-�, ����� ��� ����� �������� 381,19 ��� ���.</description> <category>��������</category> <author>������ �������</author> <guid isPermaLink=\"false\">rssexport.rbc.ru::62754ea59a79479927ecb5a4</guid> <rbc_news:pdalink>https://www.rbc.ru/politics/06/05/2022/62754ea59a79479927ecb5a4</rbc_news:pdalink> <rbc_news:full-text>������ ����� ����� ������� �������� � 2021 ���� ��������� 26,5 ��� ���., ������� �� ���������� � �������, �������������� �� ��� �����. � 2020 ���� ����� �������� ��������� 381,19 ��� ���., ��� � 14,3 ���� ������, ��� � 2021-�. � ������������� � �������� ��� ��������� �������, ������� ������� ���������� 3668 � 28 361 ��. �, � ����� ����� ��� �������� 2344 ��. �, ������� �� ���������. �� �� ������� ������������� ���� ������� � ���������� �� 2020 ���. ���� �������� � 2021 ���� ���������� 1,52 ��� ���., ��� ���� ������, ��� � 2020-� (1,59 ��� ���.), ������� �� ����������. � ������������� � ������� ����� ����� ��������� �������� �������� 209,8 ��. �. ������ � �� ������������� ��������� � �������������. � ����� �������� ������ 10 ������������������ �����, ������� � ���������, ����� ����� ��� ����������� 11. ������ ������� �������� ���� ����� ����� � 2007 ����, �� ��������� �� ����� ��������� ������ ���� ������ ������������� ���������� ��������. � 2020 ���� ������� ���� ����� ������� ����� ���� ���������� ������������ � � ������� 381,1 ��� ���. ������ ����� ����� ����� ����� �� ��������� ��������� (84,7 ��� ���.), � ������ ������� ����� ����� ����� ���������� ����������� ������ �������� (79,8 ��� ���.). � ����� ��������� �� ������ 2020 ���� ����� �������� �� ������� �� ������� ����� ���������� ����� 378 ��� ���. ����� �������, �� 2020 ��� ������� ��������� ������ �� ����, ������ ������.</rbc_news:full-text> <rbc_news:anons>� 2021 ���� ������� ��������� 26,5 ��� ���., ��� � 14 ��� ������, ��� � 2020-�, ����� ��� ����� �������� 381,19 ��� ���.</rbc_news:anons> <rbc_news:news_id>62754ea59a79479927ecb5a4</rbc_news:news_id> <rbc_news:tag>������ �������</rbc_news:tag> <rbc_news:tag>����������</rbc_news:tag> <rbc_news:tag>������</rbc_news:tag> <rbc_news:type>article</rbc_news:type> <rbc_news:newsline>politics</rbc_news:newsline> <rbc_news:newsDate_timestamp>1651861183</rbc_news:newsDate_timestamp> <rbc_news:newsModifDate>Fri, 06 May 2022 21:35:30 +0300</rbc_news:newsModifDate> <enclosure url=\"https://s0.rbk.ru/v6_top_pics/media/img/3/06/756518563562063.jpg\" type=\"image/jpeg\" length=\"0\"/></item></rss>" },
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