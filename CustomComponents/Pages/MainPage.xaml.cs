using System;
using System.Collections.Generic;
using CustomComponents.Models;
using Xamarin.Forms;

namespace CustomComponents.Pages {
    public partial class MainPage : ContentPage {

        public MainPage(IEnumerable<SampleItem> items) {
            InitializeComponent();

            PagesListView.ItemsSource = items;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            if (args?.SelectedItem != null) {
                if (args.SelectedItem is SampleItem item) {
                    var selectedPage = Activator.CreateInstance(item.PageType) as Page;
                    if (selectedPage != null) {
                        Navigation.PushAsync(selectedPage, true);
                    }
                }

                if (sender is ListView listView) {
                    listView.SelectedItem = null;
                }
            }
        }

    }
}
