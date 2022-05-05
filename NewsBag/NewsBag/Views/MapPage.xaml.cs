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
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
        }
    }
}