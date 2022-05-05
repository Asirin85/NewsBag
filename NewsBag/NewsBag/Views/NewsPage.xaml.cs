using NewsBag.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        NewsViewModel _viewModel;
        public NewsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewsViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}