using NewsBag.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsBookmarksPage : ContentPage
    {
        NewsBookmarksViewModel _viewModel;
        public NewsBookmarksPage()
        {
            Shell.SetNavBarIsVisible(this, true);
            InitializeComponent();
            BindingContext = _viewModel = new NewsBookmarksViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}