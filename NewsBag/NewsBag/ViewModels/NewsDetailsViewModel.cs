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
            _toolbarItem = toolbarItem;
            CheckIfExist(NewsItem);
            BookmarkCommand = new Command(OnBookmarkClicked);
            if (NewsItem.ImageExist == 1) Visibility = true;
        }
        public async void CheckIfExist(NewsItem item)
        {
            NewsDatabase database = await NewsDatabase.Instance;
            Bookmarked = await database.ItemExists(item.ID);
        }
        public async void OnBookmarkClicked()
        {
            Bookmarked = !Bookmarked;
            NewsDatabase database = await NewsDatabase.Instance;
            if (Bookmarked) await database.AddItemAsync(NewsItem);
            else await database.DeleteItemAsync(NewsItem);
        }
    }
}
