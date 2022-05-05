using NewsBag.Services;
using NewsBag.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewsBag.Models
{
    public class SettingsModel : BaseViewModel
    {
        public string Source { get; set; }
        private bool isOn = false;
        public bool IsOn
        {
            get {
                return isOn;
            }
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    OnPropertyChanged(nameof(ThumbColor));
                    if (Preferences.Get(Source, true) != isOn)
                    {
                        Preferences.Set(Source, isOn);
                        AvailableSources.ChangeSource(Source, isOn);
                    }
                }
            }
        }
        public Color ThumbColor
        {
            get
            {
                if (IsOn)
                    return Color.MediumPurple;
                else return Color.LightGray;
            }
        }
    }
}
