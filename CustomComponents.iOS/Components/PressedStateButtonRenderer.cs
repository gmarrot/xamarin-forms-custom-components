using System;
using System.ComponentModel;
using CustomComponents.Components;
using CustomComponents.iOS.Components;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PressedStateButton), typeof(PressedStateButtonRenderer))]
namespace CustomComponents.iOS.Components {
    public class PressedStateButtonRenderer : ButtonRenderer {

        static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;
        const double DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY = 0.8;

        UIColor _normalBackgroundColor;
        UIColor _pressedBackgroundColor;

        PressedStateButton FormsElement => Element as PressedStateButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateBackgroundColors();
                UpdatePressedTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName
                || e.PropertyName == PressedStateButton.PressedBackgroundColorProperty.PropertyName) {
                UpdateBackgroundColors();
            } else if (e.PropertyName == PressedStateButton.PressedTextColorProperty.PropertyName) {
                UpdatePressedTextColor();
            }
        }

        protected override UIButton CreateNativeControl() {
            // Use custom type to allow the ovverride of default highlight text color
            UIButton button = new UIButton(UIButtonType.Custom);
            button.TouchDown += HandleOnButtonTouchedDown;
            button.TouchUpInside += HandleOnButtonTouchedUpInside;
            button.TouchUpOutside += HandleOnButtonTouchedUpOutside;
            button.TouchCancel += HandleOnButtonTouchCancelled;

            return button;
        }

        protected override void Dispose(bool disposing) {
            if (disposing && Control != null) {
                Control.TouchDown -= HandleOnButtonTouchedDown;
                Control.TouchUpInside -= HandleOnButtonTouchedUpInside;
                Control.TouchUpOutside -= HandleOnButtonTouchedUpOutside;
                Control.TouchCancel -= HandleOnButtonTouchCancelled;
            }

            base.Dispose(disposing);
        }

        #region Background colors

        void UpdateBackgroundColors() {
            if (FormsElement != null) {
                Color normalBackgroundColor = (Element.BackgroundColor != Color.Default) ? Element.BackgroundColor : DEFAULT_BACKGROUND_COLOR;

                Color pressedBackgroundColor;
                if (FormsElement.PressedBackgroundColor != Color.Default) {
                    pressedBackgroundColor = FormsElement.PressedBackgroundColor;
                } else {
                    pressedBackgroundColor = normalBackgroundColor.MultiplyAlpha(DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY);
                }

                _normalBackgroundColor = normalBackgroundColor.ToUIColor();
                _pressedBackgroundColor = pressedBackgroundColor.ToUIColor();
            }
        }

        void SetBackgroundColorForState(UIControlState state) {
            if (Control != null) {
                switch (state) {
                    case UIControlState.Highlighted:
                        Control.BackgroundColor = _pressedBackgroundColor;
                        break;
                    default:
                        Control.BackgroundColor = _normalBackgroundColor;
                        break;
                }
            }
        }

        #endregion

        #region Text colors

        void UpdatePressedTextColor() {
            if (FormsElement != null && Control != null) {
                Color pressedTextColor = (FormsElement.PressedTextColor != Color.Default) ? FormsElement.PressedTextColor : Element.TextColor;
                Control.SetTitleColor(pressedTextColor.ToUIColor(), UIControlState.Highlighted);
            }
        }

        #endregion

        #region Event handlers

        void HandleOnButtonTouchedDown(object sender, EventArgs e) {
            SetBackgroundColorForState(UIControlState.Highlighted);
        }

        void HandleOnButtonTouchedUpInside(object sender, EventArgs e) {
            SetBackgroundColorForState(UIControlState.Normal);
        }

        void HandleOnButtonTouchedUpOutside(object sender, EventArgs e) {
            SetBackgroundColorForState(UIControlState.Normal);
        }

        void HandleOnButtonTouchCancelled(object sender, EventArgs e) {
            SetBackgroundColorForState(UIControlState.Normal);
        }

        #endregion

    }
}
