using NewsBag.Database;
using NewsBag.Models;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class NewsBookmarksViewModel : BaseViewModel
    {
        private NewsItem _selectedItem;
        public ObservableCollection<NewsItem> NewsItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command<ToolbarItem> OnToolbar { get; }
        public Command<NewsItem> ItemTapped { get; }
        private NewsRepository _newsRepository;

        public NewsBookmarksViewModel()
        {
            SetupRepo();
            ItemTapped = new Command<NewsItem>(OnItemSelected);
            NewsItems = new ObservableCollection<NewsItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }
        private async void SetupRepo()
        {
            var connection = await DatabaseConnection.GetConnection();
            _newsRepository = new NewsRepository(connection);
            await ExecuteLoadItemsCommand();
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                await GetBookmarkedNews();
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
        public async Task GetBookmarkedNews()
        {
            if (_newsRepository != null)
            {
                var list = await _newsRepository.GetItemsAsync();
                NewsItems.Clear();
                list.ForEach(NewsItems.Add);
                Sort(NewsItems);
            }
        }
        public void Sort(ObservableCollection<NewsItem> collection)
        {
            List<NewsItem> sorted = collection.OrderByDescending(x => x.Date).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
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
