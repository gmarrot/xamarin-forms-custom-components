using Xamarin.Forms;

namespace CustomComponents.Components {
    public class CarouselPageWithFooter : CarouselPage {

        public static readonly BindableProperty FooterViewProperty =
            BindableProperty.Create(nameof(FooterView), typeof(VisualElement), typeof(CarouselPageWithFooter), default(VisualElement),
                                    propertyChanged: (bindable, oldValue, newValue) => {
                                        ((CarouselPageWithFooter)bindable).OnFooterViewChanged(oldValue, newValue);
                                    });

        public VisualElement FooterView {
            get { return (VisualElement)GetValue(FooterViewProperty); }
            set { SetValue(FooterViewProperty, value); }
        }

        void OnFooterViewChanged(object oldValue, object newValue) {
            if (oldValue is VisualElement oldFooterView) {
                oldFooterView.Parent = null;
            }

            if (newValue is VisualElement newFooterView) {
                newFooterView.Parent = this;
            }
        }

    }
}
