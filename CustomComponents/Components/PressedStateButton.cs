using Xamarin.Forms;

namespace CustomComponents.Components {
    public class PressedStateButton : Button {

        public static readonly BindableProperty PressedBackgroundColorProperty =
            BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty PressedTextColorProperty =
            BindableProperty.Create(nameof(PressedTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public Color PressedBackgroundColor {
            get => (Color)GetValue(PressedBackgroundColorProperty);
            set => SetValue(PressedBackgroundColorProperty, value);
        }

        public Color PressedTextColor {
            get => (Color)GetValue(PressedTextColorProperty);
            set => SetValue(PressedTextColorProperty, value);
        }

    }
}
