using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using CustomComponents.Components;
using CustomComponents.Droid.Components;
using CustomComponents.Droid.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(PressedStateButton), typeof(PressedStateButtonRenderer))]
namespace CustomComponents.Droid.Components {
    public class PressedStateButtonRenderer : ButtonRenderer {

        const int DEFAULT_BACKGROUND_COLOR_RES_ID = Android.Resource.Color.White;
        const double DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY = 0.5;

        const int DEFAULT_TEXT_COLOR_RES_ID = Android.Resource.Color.Black;

        public PressedStateButtonRenderer(Context context) : base(context) {
        }

        PressedStateButton FormsElement => Element as PressedStateButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateBackgroundColors();
                UpdateTextColors();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName
                || e.PropertyName == PressedStateButton.PressedBackgroundColorProperty.PropertyName) {
                UpdateBackgroundColors();
            } else if (e.PropertyName == Button.TextColorProperty.PropertyName
                       || e.PropertyName == PressedStateButton.PressedTextColorProperty.PropertyName) {
                UpdateTextColors();
            }
        }

        protected override void UpdateBackgroundColor() {
            // Do nothing to override default behavior
        }

        void UpdateBackgroundColors() {
            if (FormsElement != null && Control != null) {
                AColor normalBackgroundColor = Element.BackgroundColor.ToAndroid(DEFAULT_BACKGROUND_COLOR_RES_ID, Context);

                AColor pressedBackgroundColor;
                if (FormsElement.PressedBackgroundColor != Color.Default) {
                    pressedBackgroundColor = FormsElement.PressedBackgroundColor.ToAndroid();
                } else {
                    int alpha = AColor.GetAlphaComponent(normalBackgroundColor);
                    alpha = Convert.ToInt32(Math.Round(alpha * DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY));

                    int red = AColor.GetRedComponent(normalBackgroundColor);
                    int green = AColor.GetGreenComponent(normalBackgroundColor);
                    int blue = AColor.GetBlueComponent(normalBackgroundColor);

                    pressedBackgroundColor = AColor.Argb(alpha, red, green, blue);
                }

                var drawable = new StateListDrawable();
                drawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(pressedBackgroundColor));
                drawable.AddState(new int[] { }, new ColorDrawable(normalBackgroundColor));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
                    Control.Background = drawable;
                } else {
                    Control.SetBackgroundDrawable(drawable);
                }
            }
        }

        void UpdateTextColors() {
            if (FormsElement != null && Control != null) {
                AColor normalTextColor = Element.TextColor.ToAndroid(DEFAULT_TEXT_COLOR_RES_ID, Context);
                AColor pressedTextColor = FormsElement.PressedTextColor.ToAndroid(normalTextColor);

                Control.SetTextColor(new ColorStateList(new int[][] {
                    new int[] { Android.Resource.Attribute.StatePressed },
                    new int[] {}
                }, new int[] {
                    pressedTextColor,
                    normalTextColor
                }));
            }
        }

    }
}
