using System.Collections.Generic;
using CustomComponents.Components;

namespace CustomComponents.Pages {
    public partial class CarouselPageWithFooterPage : CarouselPageWithFooter {

        public CarouselPageWithFooterPage() {
            InitializeComponent();
            ItemsSource = BuildItemsSource(3);
        }

        IEnumerable<string> BuildItemsSource(int count) {
            var items = new List<string>();
            for (int i = 1; i <= count; i++) {
                items.Add($"Page {i}");
            }

            return items;
        }

    }
}
