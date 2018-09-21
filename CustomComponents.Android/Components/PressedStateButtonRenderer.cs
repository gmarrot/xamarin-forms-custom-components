using System;
using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using CustomComponents.Android.Components;
using CustomComponents.Android.Extensions;
using CustomComponents.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;
using AResource = Android.Resource;

[assembly: ExportRenderer(typeof(PressedStateButton), typeof(PressedStateButtonRenderer))]
namespace CustomComponents.Android.Components {
    public class PressedStateButtonRenderer : ButtonRenderer {

        const int DEFAULT_BACKGROUND_COLOR_RES_ID = AResource.Color.White;
        const double DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY = 0.5;

        const int DEFAULT_TEXT_COLOR_RES_ID = AResource.Color.Black;

        public PressedStateButtonRenderer(Context context) : base(context) {
        }

        PressedStateButton FormsElement => Element as PressedStateButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateTextColors();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PressedStateButton.PressedBackgroundColorProperty.PropertyName) {
                UpdateBackgroundColor();
            } else if (e.PropertyName == Button.TextColorProperty.PropertyName
                       || e.PropertyName == PressedStateButton.PressedTextColorProperty.PropertyName) {
                UpdateTextColors();
            }
        }

        protected override void UpdateBackgroundColor() {
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
                drawable.AddState(new int[] { AResource.Attribute.StatePressed }, new ColorDrawable(pressedBackgroundColor));
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
                    new int[] { AResource.Attribute.StatePressed },
                    new int[] {}
                }, new int[] {
                    pressedTextColor,
                    normalTextColor
                }));
            }
        }

    }
}
