using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using Meihana.Model;

namespace Meihana.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
