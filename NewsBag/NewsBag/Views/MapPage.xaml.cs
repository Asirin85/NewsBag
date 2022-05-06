using NewsBag.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private MapViewModel _viewModel;
        public MapPage()
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            BindingContext = _viewModel = new MapViewModel(map);
        }
        bool active = false;
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (active)
            {
                active = false;
                MapButton.Text = "✓";
                _viewModel.StopTrack();
            }
            else
            {
                active = true;
                MapButton.Text = "✗";
                _viewModel.TrackLocation();
            }
        }
    }
}