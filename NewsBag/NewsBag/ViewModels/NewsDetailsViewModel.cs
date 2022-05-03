using NewsBag.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsBag.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        public NewsItem NewsItem { get; set; }
        public NewsDetailsViewModel()
        {
            
            NewsItem = GlobalNewsConstants.SelectedItem;
        }
    }
}
