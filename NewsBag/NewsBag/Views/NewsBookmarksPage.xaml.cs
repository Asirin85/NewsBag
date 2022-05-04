using NewsBag.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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