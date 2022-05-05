using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
namespace NewsBag.Services
{
    public class AvailableSources
    {
        public static void GetSources()
        {
            var sour = AppResources.Sources.Split(' ');
            var _sources = new List<string>(sour);
            var loopSources = new List<string>(sour);
            foreach (var source in loopSources)
            {
                if (!Preferences.Get(source, true))
                {
                    _sources.Remove(source);
                    var foundShell = GlobalNewsConstants.TabNewsItems.Where(x => x.Title.Equals(source)).FirstOrDefault();
                    GlobalNewsConstants.TabNews.Items.Remove(foundShell);
                    GlobalNewsConstants.TabNewsItems.Remove(foundShell);
                    Routing.UnRegisterRoute(nameof(source));
                }
                else
                {
                    var shell = new ShellContent
                    {
                        Content = new NewsPage(),
                        Title = source,
                        Route = source
                    };
                    GlobalNewsConstants.TabNewsItems.Add(shell);
                    GlobalNewsConstants.TabNews.Items.Add(shell);
                    Routing.RegisterRoute(nameof(source), typeof(NewsPage));
                }
            }
            return;
        }
        public static void ChangeSource(string source, bool add)
        {
            if (add)
            {
                var shell = new ShellContent
                {
                    Content = new NewsPage(),
                    Title = source,
                    Route = source
                };
                GlobalNewsConstants.TabNewsItems.Add(shell);
                GlobalNewsConstants.TabNews.Items.Add(shell);
                Routing.RegisterRoute(nameof(source), typeof(NewsPage));
            }
            else
            {
                var foundShell = GlobalNewsConstants.TabNewsItems.Where(x => x.Title.Equals(source)).FirstOrDefault();
                GlobalNewsConstants.TabNews.Items.Remove(foundShell);
                GlobalNewsConstants.TabNewsItems.Remove(foundShell);
                Routing.UnRegisterRoute(nameof(source));
            }
        }
    }
}
