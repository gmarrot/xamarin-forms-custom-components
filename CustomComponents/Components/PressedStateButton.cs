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

        static readonly BindablePropertyKey ActualBackgroundColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(ActualBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty ActualBackgroundColorProperty = ActualBackgroundColorPropertyKey.BindableProperty;

        static readonly BindablePropertyKey ActualPressedBackgroundColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(ActualPressedBackgroundColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty ActualPressedBackgroundColorProperty = ActualPressedBackgroundColorPropertyKey.BindableProperty;

        #endregion

        #region Text bindable properties

        public static readonly BindableProperty PressedTextColorProperty =
            BindableProperty.Create(nameof(PressedTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        static readonly BindablePropertyKey ActualTextColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(ActualTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty ActualTextColorProperty = ActualTextColorPropertyKey.BindableProperty;

        static readonly BindablePropertyKey ActualPressedTextColorPropertyKey =
            BindableProperty.CreateReadOnly(nameof(ActualPressedTextColor), typeof(Color), typeof(PressedStateButton), Color.Default);

        public static readonly BindableProperty ActualPressedTextColorProperty = ActualPressedTextColorPropertyKey.BindableProperty;

        #endregion

        public PressedStateButton() {
            UpdateActualBackgroundColors();
            UpdateActualTextColors();
        }

        #region Background properties

        public Color PressedBackgroundColor {
            get => (Color)GetValue(PressedBackgroundColorProperty);
            set => SetValue(PressedBackgroundColorProperty, value);
        }

        public Color ActualBackgroundColor {
            get => (Color)GetValue(ActualBackgroundColorProperty);
            private set => SetValue(ActualBackgroundColorPropertyKey, value);
        }

        public Color ActualPressedBackgroundColor {
            get => (Color)GetValue(ActualPressedBackgroundColorProperty);
            private set => SetValue(ActualPressedBackgroundColorPropertyKey, value);
        }

        #endregion

        #region Text properties

        public Color PressedTextColor {
            get => (Color)GetValue(PressedTextColorProperty);
            set => SetValue(PressedTextColorProperty, value);
        }

        public Color ActualTextColor {
            get => (Color)GetValue(ActualTextColorProperty);
            private set => SetValue(ActualTextColorPropertyKey, value);
        }

        public Color ActualPressedTextColor {
            get => (Color)GetValue(ActualPressedTextColorProperty);
            private set => SetValue(ActualPressedTextColorPropertyKey, value);
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BackgroundColorProperty.PropertyName
                || propertyName == PressedBackgroundColorProperty.PropertyName) {
                UpdateActualBackgroundColors();
            } else if (propertyName == TextColorProperty.PropertyName
                       || propertyName == PressedTextColorProperty.PropertyName) {
                UpdateActualTextColors();
            }
        }

        void UpdateActualBackgroundColors() {
            ActualBackgroundColor = (BackgroundColor != Color.Default) ? BackgroundColor : DEFAULT_BACKGROUND_COLOR;
            ActualPressedBackgroundColor = (PressedBackgroundColor != Color.Default)
                ? PressedBackgroundColor
                : ActualBackgroundColor.MultiplyAlpha(DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY);
        }

        void UpdateActualTextColors() {
            ActualTextColor = (TextColor != Color.Default) ? TextColor : DEFAULT_TEXT_COLOR;
            ActualPressedTextColor = (PressedTextColor != Color.Default) ? PressedTextColor : ActualTextColor;
        }

    }
}
