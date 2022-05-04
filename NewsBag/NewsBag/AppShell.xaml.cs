using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.ViewModels;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public List<string> _sources;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewsDetailPage), typeof(NewsDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            GetSourcesList();
        }
        private void GetSourcesList()
        {
            var sour = AppResources.Sources.Split(' ');
            _sources = new List<string>(sour);
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
                    if (TabNews.Items.Contains(shell)) TabNews.Items.Remove(shell);
                    Routing.UnRegisterRoute(nameof(shell.Title));
                }
                else
                {
                    if (!TabNews.Items.Contains(shell)) TabNews.Items.Add(shell);
                    Routing.RegisterRoute(nameof(shell.Title), typeof(NewItemPage));
                }
            }
        }
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            var found = false;
            var all = AppResources.SourceAll.ToLowerInvariant();
            Console.WriteLine(args.Target.Location.OriginalString.ToLowerInvariant());
            if (args.Target.Location.OriginalString.ToLowerInvariant().Contains(all))
            {
                found = true;
                GlobalNewsConstants.filter = all;
            }
            var variants = AppResources.Sources.ToLowerInvariant().Split(' ');
            for (int i = 0; i < variants.Length && !found; i++)
            {
                if (args.Target.Location.OriginalString.ToLowerInvariant().Contains(variants[i]))
                {
                    found = true;
                    GlobalNewsConstants.filter = variants[i];
                }
            }
        }
    }
}
