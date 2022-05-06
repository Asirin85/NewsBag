using NewsBag.Localization;
using NewsBag.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public Command LogOutCommand { get; }
        public ProfileViewModel()
        {
            GetLogin();
            LogOutCommand = new Command(OnLogOut);
        }
        async void GetLogin()
        {
            Login = await SecureStorage.GetAsync("username");
        }
        async void OnLogOut()
        {
            SecureStorage.Remove("username");
            SecureStorage.Remove("token");
            MessagingCenter.Send<Object, string>(this, "UsernameSettings", AppResources.SettingsNotLoggedLabel);
            await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}");
        }
    }
}
