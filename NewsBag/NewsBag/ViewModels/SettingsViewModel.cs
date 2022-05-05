using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public ObservableCollection<SettingsModel> Settings { get; set; }
        public string Login { get; set; }
        public Command LoginTapped { get; }
        public Command MapTapped { get; }
        public SettingsViewModel()
        {
            LoginTapped = new Command(OnLoginTapped);
            MapTapped = new Command(OnMapTapped);
            Settings = new ObservableCollection<SettingsModel>();
            Login = AppResources.SettingsNotLoggedLabel;
            GetSettings();
        }
        async void OnLoginTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        async void OnMapTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(MapPage)}");
        }
        public void GetSettings()
        {
            Settings.Clear();
            var sources = AppResources.Sources.Split(' ');
            foreach (var source in sources)
            {
                var setting = new SettingsModel();
                setting.Source = source;
                setting.IsOn = Preferences.Get(source, true);
                Settings.Add(setting);
            }
        }
    }
}
