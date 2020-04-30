using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MeihanaUI.Views.Ecommerce
{
    /// <summary>
    /// Page to show the cart list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartPage" /> class.
        /// </summary>
        public CartPage()
        {
            this.InitializeComponent();
        }
    }
}