using System.Collections.Generic;
using System.Threading.Tasks;
using CustomComponents.Components;
using Xamarin.Forms;

namespace CustomComponents.Pages {
    public partial class CarouselPageWithFooterPage : CarouselPageWithFooter {

        public CarouselPageWithFooterPage() {
            InitializeComponent();
            ItemsSource = BuildItemsSource(3);

            Button1.Command = new Command(async (obj) => {
                await DisplayButtonClickedAlert("Button 1");
            });
            Button2.Command = new Command(async (obj) => {
                await DisplayButtonClickedAlert("Button 2");
            });
        }

        IEnumerable<string> BuildItemsSource(int count) {
            var items = new List<string>();
            for (int i = 1; i <= count; i++) {
                items.Add($"Page {i}");
            }

            return items;
        }

        Task DisplayButtonClickedAlert(string title) {
            return DisplayAlert(title, "Button has been clicked", "OK");
        }

    }
}
