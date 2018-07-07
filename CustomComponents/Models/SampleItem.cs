using System;
using Xamarin.Forms;

namespace CustomComponents.Models {
    public class SampleItem {

        SampleItem(string title, Type pageType) {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            PageType = pageType ?? throw new ArgumentNullException(nameof(pageType));
        }

        public string Title { get; }

        public Type PageType { get; }

        public static SampleItem For<T>(string title = null) where T : Page, new() {
            Type pageType = typeof(T);
            string pageTitle = title ?? pageType.Name;

            return new SampleItem(pageTitle, pageType);
        }

    }
}
