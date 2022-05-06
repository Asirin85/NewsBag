using NewsBag.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}