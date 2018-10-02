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

        UIColor _normalBackgroundColor;
        UIColor _pressedBackgroundColor;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateBackgroundColor();
                UpdatePressedBackgroundColor();
                UpdateTextColor();
                UpdatePressedTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PressedStateButton.InternalBackgroundColorProperty.PropertyName) {
                UpdateBackgroundColor();
            } else if (e.PropertyName == PressedStateButton.InternalPressedBackgroundColorProperty.PropertyName) {
                UpdatePressedBackgroundColor();
            } else if (e.PropertyName == PressedStateButton.InternalTextColorProperty.PropertyName) {
                UpdateTextColor();
            } else if (e.PropertyName == PressedStateButton.InternalPressedTextColorProperty.PropertyName) {
                UpdatePressedTextColor();
            }
        }

        protected override UIButton CreateNativeControl() {
            // Use custom type to allow the override of default highlight text color
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

        void UpdateBackgroundColor() {
            if (Element is PressedStateButton button) {
                _normalBackgroundColor = button.InternalBackgroundColor.ToUIColor();
                if (Control != null) {
                    SetBackgroundColorForState(Control.State);
                }
            }
        }

        void UpdatePressedBackgroundColor() {
            if (Element is PressedStateButton button) {
                _pressedBackgroundColor = button.InternalPressedBackgroundColor.ToUIColor();
                if (Control != null) {
                    SetBackgroundColorForState(Control.State);
                }
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

        void UpdateTextColor() {
            if (Element is PressedStateButton button && Control != null) {
                Control.SetTitleColor(button.InternalTextColor.ToUIColor(),
                                      UIControlState.Normal);
            }
        }

        void UpdatePressedTextColor() {
            if (Element is PressedStateButton button && Control != null) {
                Control.SetTitleColor(button.InternalPressedTextColor.ToUIColor(),
                                      UIControlState.Highlighted);
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
