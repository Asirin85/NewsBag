using NewsBag.Database;
using NewsBag.Models;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        public NewsItem NewsItem { get; set; }
        public bool Visibility { get; set; } = false;
        public Command BookmarkCommand { get; }
        private bool _isBookmarked = false;
        private ToolbarItem _toolbarItem;
        private NewsRepository _newsRepository;
        public bool Bookmarked
        {
            get
            {
                return _isBookmarked;
            }
            set
            {
                _isBookmarked = value;
                if (_toolbarItem != null)
                    if (_isBookmarked) _toolbarItem.IconImageSource = "icon_bookmarkfilled.png";
                    else _toolbarItem.IconImageSource = "icon_bookmark.png";
            }
        }
        public NewsDetailsViewModel(ToolbarItem toolbarItem)
        {
            NewsItem = GlobalNewsConstants.SelectedItem;
            SetupRepo();
            _toolbarItem = toolbarItem;
            CheckIfExist(NewsItem);
            BookmarkCommand = new Command(OnBookmarkClicked);
            if (NewsItem.ImageExist == 1) Visibility = true;

        }
        private async void SetupRepo()
        {
            var connection = await DatabaseConnection.GetConnection();
            _newsRepository = new NewsRepository(connection);
            CheckIfExist(NewsItem);
        }
        public async void CheckIfExist(NewsItem item)
        {
            if (_newsRepository != null)
                Bookmarked = await _newsRepository.ItemExists(item.ID);
        }
        public async void OnBookmarkClicked()
        {
            if (_newsRepository != null)
            {
                Bookmarked = !Bookmarked;
                if (Bookmarked) await _newsRepository.AddItemAsync(NewsItem);
                else await _newsRepository.DeleteItemAsync(NewsItem);
            }
        }
    }
}
