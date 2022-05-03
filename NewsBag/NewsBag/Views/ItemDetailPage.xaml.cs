using NewsBag.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NewsBag.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}