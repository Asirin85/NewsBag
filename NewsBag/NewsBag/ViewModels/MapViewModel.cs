using NewsBag.Localization;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NewsBag.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private readonly Xamarin.Forms.Maps.Map _map;
        public MapViewModel(Xamarin.Forms.Maps.Map map)
        {
            _map = map;
            ToCurrentLocation();
            map.MapElements.Add(path);
        }
        private async void ToCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude),
                                             Distance.FromKilometers(1)));
        }
        bool _activated = false;
        public bool Activated
        {
            get
            {
                return _activated;
            }
            set
            {
                SetProperty(ref _activated, value);
            }
        }
        private Polyline path = new Polyline();
        public async void TrackLocation()
        {
            Activated = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                OnPropertyChanged(nameof(Activated));
            });

            path.Geopath.Clear();
            _map.Pins.Clear();
            var res = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMinutes(1)));
            var position = new Position(res.Latitude, res.Longitude);
            var pin = new Pin();
            pin.Label = AppResources.StartLocationPin;
            pin.Position = position;
            _map.Pins.Add(pin);
            path.Geopath.Add(position);
            while (Activated)
            {
                res = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMinutes(1)));
                position = new Position(res.Latitude, res.Longitude);
                path.Geopath.Add(position);
                await Task.Delay(500);
            }
            Activated = false;
        }
        public async void StopTrack()
        {
            var res = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMinutes(1)));
            var position = new Position(res.Latitude, res.Longitude);
            var pin = new Pin();
            pin.Label = AppResources.EndLocationPin;
            pin.Position = position;
            _map.Pins.Add(pin);
            Activated = false;
        }
    }
}
