using Xamarin.Forms;

namespace CustomComponents.Components {
    public class CarouselPageWithFooter : CarouselPage {

        public static readonly BindableProperty FooterViewProperty =
            BindableProperty.Create(nameof(FooterView), typeof(VisualElement), typeof(CarouselPageWithFooter), default(VisualElement),
                                    propertyChanged: (bindable, oldValue, newValue) => {
                                        if (oldValue is VisualElement oldFooterView) {
                                            oldFooterView.Parent = null;
                                        }
                                        if (newValue is VisualElement newFooterView) {
                                            newFooterView.Parent = bindable as CarouselPageWithFooter;
                                        }
                                    });

        public VisualElement FooterView {
            get { return (VisualElement)GetValue(FooterViewProperty); }
            set { SetValue(FooterViewProperty, value); }
        }

    }
}
