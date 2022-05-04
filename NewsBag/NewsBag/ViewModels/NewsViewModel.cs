﻿using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Services;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public Command<ToolbarItem> OnToolbar { get; }
        private readonly OneParser _parser = GlobalNewsConstants.parser;
        public Command<NewsItem> ItemTapped { get; }
        public NewsViewModel()
        {
            UpdateSources();
            ItemTapped = new Command<NewsItem>(OnItemSelected);
            NewsItems = new ObservableCollection<NewsItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        private void UpdateSources()
        {
            var dict = itemDict;
            if (itemDict is null)
                dict = new Dictionary<string, ObservableCollection<NewsItem>>();
            var all = AppResources.SourceAll.ToLowerInvariant();
            var sources = AppResources.Sources.ToLowerInvariant().Split(' ');
            dict.Add(all, new ObservableCollection<NewsItem>());
            foreach (var source in sources)
            {
                if (Preferences.Get(source, true))
                    dict.Add(source, new ObservableCollection<NewsItem>());
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
                    await _parser.GetNews(itemDict[source.ToLowerInvariant()], source.ToLowerInvariant(), GlobalNewsConstants.sourcesLinks[source.ToLowerInvariant()]);
                    await AddNewNews();
                    return;
                case "все":
                    var sources = AppResources.Sources.ToLowerInvariant().Split(' '); 
                    await GetAllRu(sources);
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
        async Task GetAllRu(string[] sources)
        {
            itemDict[AppResources.SourceAll.ToLowerInvariant()].Clear();
            foreach (var source in sources)
            {
                var added = false;
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
            // This will push the ItemDetailPage onto the navigation stack
            
            GlobalNewsConstants.SelectedItem = item;
            await Shell.Current.GoToAsync($"{nameof(NewsDetailPage)}");
        }

    }
}
