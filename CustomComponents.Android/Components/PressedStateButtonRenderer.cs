using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using CustomComponents.Android.Components;
using CustomComponents.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;
using AResource = Android.Resource;

[assembly: ExportRenderer(typeof(PressedStateButton), typeof(PressedStateButtonRenderer))]
namespace CustomComponents.Android.Components {
    public class PressedStateButtonRenderer : ButtonRenderer {

        public PressedStateButtonRenderer(Context context) : base(context) {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateBackgroundColors();
                UpdateTextColors();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PressedStateButton.InternalBackgroundColorProperty.PropertyName
                || e.PropertyName == PressedStateButton.InternalPressedBackgroundColorProperty.PropertyName) {
                UpdateBackgroundColors();
            } else if (e.PropertyName == PressedStateButton.InternalTextColorProperty.PropertyName
                       || e.PropertyName == PressedStateButton.InternalPressedTextColorProperty.PropertyName) {
                UpdateTextColors();
            }
        }

        void UpdateBackgroundColors() {
            if (Element is PressedStateButton button && Control != null) {
                AColor normalBackgroundColor = button.InternalBackgroundColor.ToAndroid();
                AColor pressedBackgroundColor = button.InternalPressedBackgroundColor.ToAndroid();

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
            if (Element is PressedStateButton button && Control != null) {
                AColor normalTextColor = button.InternalTextColor.ToAndroid();
                AColor pressedTextColor = button.InternalPressedTextColor.ToAndroid();

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
