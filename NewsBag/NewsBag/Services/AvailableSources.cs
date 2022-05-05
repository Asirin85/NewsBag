using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
                var shell = new ShellContent();
                shell.Content = new NewsPage();
                shell.Title = source;
                shell.Route = source;
                if (!Preferences.Get(source, true))
                {
                    _sources.Remove(source);
                    if (GlobalNewsConstants.TabNews.Items.Contains(shell)) GlobalNewsConstants.TabNews.Items.Remove(shell);
                    Routing.UnRegisterRoute(nameof(shell.Title));
                }
                else
                {
                    if (!GlobalNewsConstants.TabNews.Items.Contains(shell)) GlobalNewsConstants.TabNews.Items.Add(shell);
                    Routing.RegisterRoute(nameof(shell.Title), typeof(NewsPage));
                }
            }
            return;
        }
        public static void ChangeSource(string source, bool add)
        {
            var shell = new ShellContent();
            shell.Content = new NewsPage();
            shell.Title = source;
            shell.Route = source;
            if (add)
            {
                if (!GlobalNewsConstants.TabNews.Items.Contains(shell)) GlobalNewsConstants.TabNews.Items.Add(shell);
                Routing.RegisterRoute(nameof(source), typeof(NewsPage));
            }
            else
            {
                if (GlobalNewsConstants.TabNews.Items.Contains(shell)) GlobalNewsConstants.TabNews.Items.Remove(shell);
                Routing.UnRegisterRoute(nameof(source));
            }
        }
    }
}
