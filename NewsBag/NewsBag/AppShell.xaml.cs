using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Services;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NewsBag
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public List<string> _sources;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(NewsDetailPage), typeof(NewsDetailPage));
            GlobalNewsConstants.TabNews = TabNews;
            AvailableSources.GetSources();
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
