using NewsBag.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel _viewModel;
        public SettingsPage()
        {
            BindingContext = _viewModel = new SettingsViewModel();
            InitializeComponent();
        }
    }
}