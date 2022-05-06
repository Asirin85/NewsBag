using System;
using System.IO;
using System.Linq;
using System.Threading;
using NewsBag.Localization;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace NewsBagUITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }
        [Test]
        public void WrongInputLoginScreen()
        {
            app.WaitForElement(x => x.Marked(AppResources.TabBarSettings));
            app.Tap(c => c.Marked(AppResources.TabBarSettings));
            app.Tap(c => c.Marked(AppResources.SettingsNotLoggedLabel));
            app.Tap(c => c.Marked("EntryUsernameLogin"));
            app.EnterText("");
            app.Tap(c => c.Marked("EntryPasswordLogin"));
            app.EnterText("123");
            app.Tap(c => c.Marked("LoginButton"));
            app.Tap(c => c.Marked("LoginButton"));
            var res = app.WaitForElement(c => c.Marked("OK").Parent().Class("AlertDialogLayout"), $"Timed out waiting for AlertDialogLayout", TimeSpan.FromSeconds(5));
            Assert.IsTrue(res.Any());
        }
        [Test]
        public void OpenListOfNews()
        {
            var res = app.WaitForElement(c => c.Marked("ListOfNews"), $"Timed out waiting for ListOfNews", TimeSpan.FromSeconds(5));
            Assert.IsTrue(res.Any());
        }
        [Test]
        public void OpenListOfBookmarks()
        {
            app.Tap(c => c.Marked(AppResources.TabBarBookmarks));
            var res = app.WaitForElement(c => c.Marked("BookmarksList"), $"Timed out waiting for BookmarksList", TimeSpan.FromSeconds(5));
            Assert.IsTrue(res.Any());
        }
    }
}
