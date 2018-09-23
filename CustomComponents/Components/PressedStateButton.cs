using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CustomComponents.Components {
    public class PressedStateButton : Button {

        static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;
        const double DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY = 0.8;

        static readonly Color DEFAULT_TEXT_COLOR = Color.Black;

        #region Background bindable properties

        public static readonly BindableProperty PressedBackgroundColorProperty =
            BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        static readonly BindablePropertyKey InternalBackgroundColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(InternalBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty InternalBackgroundColorProperty = InternalBackgroundColorPropertyKey.BindableProperty;

        static readonly BindablePropertyKey InternalPressedBackgroundColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(InternalPressedBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty InternalPressedBackgroundColorProperty = InternalPressedBackgroundColorPropertyKey.BindableProperty;

        #endregion

        #region Text bindable properties

        public static readonly BindableProperty PressedTextColorProperty =
            BindableProperty.Create(nameof(PressedTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        static readonly BindablePropertyKey InternalTextColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(InternalTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty InternalTextColorProperty = InternalTextColorPropertyKey.BindableProperty;

        static readonly BindablePropertyKey InternalPressedTextColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(InternalPressedTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty InternalPressedTextColorProperty = InternalPressedTextColorPropertyKey.BindableProperty;

        #endregion

        public PressedStateButton() {
            UpdateInternalBackgroundColors();
            UpdateInternalTextColors();
        }

        #region Background properties

        public Color PressedBackgroundColor {
            get => (Color)GetValue(PressedBackgroundColorProperty);
            set => SetValue(PressedBackgroundColorProperty, value);
        }

        public Color InternalBackgroundColor {
            get => (Color)GetValue(InternalBackgroundColorProperty);
            private set => SetValue(InternalBackgroundColorPropertyKey, value);
        }

        public Color InternalPressedBackgroundColor {
            get => (Color)GetValue(InternalPressedBackgroundColorProperty);
            private set => SetValue(InternalPressedBackgroundColorPropertyKey, value);
        }

        #endregion

        #region Text properties

        public Color PressedTextColor {
            get => (Color)GetValue(PressedTextColorProperty);
            set => SetValue(PressedTextColorProperty, value);
        }

        public Color InternalTextColor {
            get => (Color)GetValue(InternalTextColorProperty);
            private set => SetValue(InternalTextColorPropertyKey, value);
        }

        public Color InternalPressedTextColor {
            get => (Color)GetValue(InternalPressedTextColorProperty);
            private set => SetValue(InternalPressedTextColorPropertyKey, value);
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BackgroundColorProperty.PropertyName
                || propertyName == PressedBackgroundColorProperty.PropertyName) {
                UpdateInternalBackgroundColors();
            } else if (propertyName == TextColorProperty.PropertyName
                       || propertyName == PressedTextColorProperty.PropertyName) {
                UpdateInternalTextColors();
            }
        }

        void UpdateInternalBackgroundColors() {
            InternalBackgroundColor = (BackgroundColor != Color.Default) ? BackgroundColor : DEFAULT_BACKGROUND_COLOR;
            InternalPressedBackgroundColor = (PressedBackgroundColor != Color.Default)
                ? PressedBackgroundColor
                : InternalBackgroundColor.MultiplyAlpha(DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY);
        }

        void UpdateInternalTextColors() {
            InternalTextColor = (TextColor != Color.Default) ? TextColor : DEFAULT_TEXT_COLOR;
            InternalPressedTextColor = (PressedTextColor != Color.Default) ? PressedTextColor : InternalTextColor;
        }

    }
}
