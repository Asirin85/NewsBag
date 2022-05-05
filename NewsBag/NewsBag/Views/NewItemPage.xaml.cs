using NewsBag.Models;
using NewsBag.ViewModels;
using Xamarin.Forms;

namespace NewsBag.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}