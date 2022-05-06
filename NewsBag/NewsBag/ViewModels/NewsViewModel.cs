using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Services;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        private NewsItem _selectedItem;
        public ObservableCollection<NewsItem> NewsItems { get; set; }
        public Dictionary<string, ObservableCollection<NewsItem>> itemDict { get; set; }
        public Command LoadItemsCommand { get; }
        private readonly OneParser _parser = GlobalNewsConstants.parser;
        public Command<NewsItem> ItemTapped { get; }
        public NewsViewModel()
        {
            ItemTapped = new Command<NewsItem>(OnItemSelected);
            NewsItems = new ObservableCollection<NewsItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        private async Task UpdateSources()
        {
            var dict = itemDict;
            if (itemDict is null)
                dict = new Dictionary<string, ObservableCollection<NewsItem>>();
            var all = AppResources.SourceAll.ToLowerInvariant();
            var sources = AppResources.Sources.ToLowerInvariant().Split(' ');
            if (!dict.ContainsKey(all))
                dict.Add(all, new ObservableCollection<NewsItem>());
            else dict[all] = new ObservableCollection<NewsItem>();
            foreach (var source in sources)
            {
                if (Preferences.Get(source, true))
                {
                    if (!dict.ContainsKey(source))
                        dict.Add(source, new ObservableCollection<NewsItem>());
                    else dict[source] = new ObservableCollection<NewsItem>();
                }
                else
                    dict.Remove(source);
            }
            itemDict = dict;
        }
        private async Task GetNewsBySourceAsync(string source)
        {
            switch (source.ToLowerInvariant())
            {
                case "all":
                    var sources1 = AppResources.Sources.ToLowerInvariant().Split(' ');
                    await GetAll(sources1);
                    await AddNewNews();
                    return;
                case "все":
                    var sources = AppResources.Sources.ToLowerInvariant().Split(' ');
                    await GetAll(sources);
                    await AddNewNews();
                    return;
                case "lenta.ru":
                    await _parser.GetNews(itemDict[source.ToLowerInvariant()], source.ToLowerInvariant(), GlobalNewsConstants.sourcesLinks[source.ToLowerInvariant()]);
                    await AddNewNews();
                    return;
                case "un.org":
                    await _parser.GetNews(itemDict[source.ToLowerInvariant()], source.ToLowerInvariant(), GlobalNewsConstants.sourcesLinks[source.ToLowerInvariant()]);
                    await AddNewNews();
                    return;
                case "rbc.ru":
                    await _parser.GetNews(itemDict[source.ToLowerInvariant()], source.ToLowerInvariant(), GlobalNewsConstants.sourcesLinks[source.ToLowerInvariant()]);
                    await AddNewNews();
                    return;
                default:
                    return;
            }

        }
        async Task GetAll(string[] sources)
        {
            itemDict[AppResources.SourceAll.ToLowerInvariant()].Clear();
            foreach (var source in sources)
            {
                var added = false;
                if (Preferences.Get(source, true))
                    switch (source)
                    {
                        case "rbc.ru":
                            await GetNewsBySourceAsync(source);
                            added = true;
                            break;
                        case "lenta.ru":
                            await GetNewsBySourceAsync(source);
                            added = true;
                            break;
                        case "un.org":
                            await GetNewsBySourceAsync(source);
                            added = true;
                            break;
                    }
                if (added)
                    itemDict[source.ToLowerInvariant()].ToList().ForEach(itemDict[AppResources.SourceAll.ToLowerInvariant()].Add);
            }
            return;
        }
        private async Task AddNewNews()
        {
            NewsItems.Clear();
            itemDict[GlobalNewsConstants.filter.ToLowerInvariant()].ToList().ForEach(NewsItems.Add);
            Sort(NewsItems);
            return;
        }
        public void Sort(ObservableCollection<NewsItem> collection)
        {
            List<NewsItem> sorted = collection.OrderByDescending(x => x.Date).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                await UpdateSources();
                await GetNewsBySourceAsync(GlobalNewsConstants.filter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public NewsItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(NewsItem item)
        {
            if (item == null)
                return;

            GlobalNewsConstants.SelectedItem = item;
            await Shell.Current.GoToAsync($"{nameof(NewsDetailPage)}");
        }

    }
}
