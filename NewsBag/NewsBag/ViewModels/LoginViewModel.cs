using NewsBag.Localization;
using NewsBag.Models;
using NewsBag.Views;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command SignUpCommand { get; }
        public User User { get; set; }
        public LoginViewModel()
        {
            User = new User();
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUp);
        }

        private async void OnLoginClicked(object obj)
        {
            if (string.IsNullOrEmpty(User.username)) await Application.Current.MainPage.DisplayAlert(AppResources.ErrorInput, AppResources.BadUsername, "OK");
            else if (string.IsNullOrEmpty(User.password)) await Application.Current.MainPage.DisplayAlert(AppResources.ErrorInput, AppResources.BadPassword, "OK");
            else
            {
                string jsonData = JsonConvert.SerializeObject(User);
                Console.WriteLine(jsonData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await GlobalNewsConstants.httpClient.PostAsync(GlobalNewsConstants.apiLogin, content);
                var username = AppResources.SettingsNotLoggedLabel;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                    ResponseToken token = JsonConvert.DeserializeObject<ResponseToken>(result);

                    if (token != null)
                    {
                        await SecureStorage.SetAsync("token", token.token);
                        await SecureStorage.SetAsync("username", token.username);
                        username = await SecureStorage.GetAsync("username");
                    }
                    MessagingCenter.Send<Object, string>(this, "UsernameSettings", username);
                    await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}/{nameof(ProfilePage)}");
                }
                else await Application.Current.MainPage.DisplayAlert(AppResources.BadReqError, AppResources.BadReqTextLogin, "OK");
            }
        }
        private async void OnSignUp(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(SettingsPage)}/{nameof(RegisterPage)}");
        }
    }
}
