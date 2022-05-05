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
        string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                SetProperty(ref _login, value);
            }
        }
        public Command LoginTapped { get; }
        public Command MapTapped { get; }
        public SettingsViewModel()
        {
            LoginTapped = new Command(OnLoginTapped);
            MapTapped = new Command(OnMapTapped);
            Settings = new ObservableCollection<SettingsModel>();
            GetLogin();
            GetSettings();
            MessagingCenter.Subscribe<Object, string>(this, "UsernameSettings", (snd, arg) =>
            {
                Login = arg;
            });
        }
        public async void GetLogin()
        {
            Login = await SecureStorage.GetAsync("username");
            if (Login == null) Login = AppResources.SettingsNotLoggedLabel;
        }

        async void OnLoginTapped()
        {
            if (await SecureStorage.GetAsync("username") != null && await SecureStorage.GetAsync("token") != null)
                await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
            else
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
