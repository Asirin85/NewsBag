using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Services;
using NewsBag.Services.Parsers;
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
        public ObservableCollection<NewsItem> NewsItems { get; }
        public Command LoadItemsCommand { get; }
        public Command<ToolbarItem> OnToolbar { get; }
        public Command<NewsItem> ItemTapped { get; }
        public NewsViewModel()
        {
            NewsItems = new ObservableCollection<NewsItem>();
            OnToolbar = new Command<ToolbarItem>(OnToolBarClick);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        private async Task<IEnumerable<NewsItem>> GetNewsBySourceAsync(string source)
        {
            List<NewsItem> list = new List<NewsItem>();
            switch (source.ToLowerInvariant())
            {
                case "all":
                    return await UnParser.GetNews();
                case "все":
                    return await GetAllRu();
                case "lenta.ru":
                    return await LentaParser.GetNews();
                case "un.org":
                    return await UnParser.GetNews();
                case "rbc.ru":
                    return await RbcParser.GetNews();
                default:
                    return null;
            }
        }
        async Task<List<NewsItem>> GetAllRu()
        {
            var Rbc = await RbcParser.GetNews();
            var Lenta = await LentaParser.GetNews();
            Rbc.AddRange(Lenta);
            var result = Rbc.OrderByDescending(x => x.Date).ToList();
            return result;
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            var items = await GetNewsBySourceAsync(GlobalNewsFilter.filter);
            try
            {
                NewsItems.Clear();
                if (items != null)
                    foreach (var item in items)
                    {
                        NewsItems.Add(item);
                    }
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
        public void OnToolBarClick(ToolbarItem item)
        {
            item.Order = ToolbarItemOrder.Primary;
        }
        async void OnItemSelected(NewsItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

    }
}
