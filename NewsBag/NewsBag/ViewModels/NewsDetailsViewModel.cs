using NewsBag.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
            Bookmarked = CheckIfExist(NewsItem);
            _toolbarItem = toolbarItem;
            BookmarkCommand = new Command(OnBookmarkClicked);
            NewsItem = GlobalNewsConstants.SelectedItem;
            if (NewsItem.ImageExist == 1) Visibility = true;

        }
        public bool CheckIfExist(NewsItem item)
        {
            return !Bookmarked;
        }
        public async void OnBookmarkClicked()
        {
            Bookmarked = !Bookmarked;
        }
    }
}
