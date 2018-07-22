using Xamarin.Forms;

namespace CustomComponents.Pages {
    public partial class RoundedContentViewPage : ContentPage {

        public RoundedContentViewPage() {
            InitializeComponent();
            BindingContext = new RoundedContentViewPageModel();
        }

    }
}
