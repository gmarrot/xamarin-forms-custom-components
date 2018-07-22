using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using CustomComponents.Components;
using CustomComponents.Droid.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PressedStateButton), typeof(PressedStateButtonRenderer))]
namespace CustomComponents.Droid.Components {
    public class PressedStateButtonRenderer : ButtonRenderer {

        static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;
        const double DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY = 0.5;

        static readonly Color DEFAULT_TEXT_COLOR = Color.Black;

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
                Color normalBackgroundColor = (Element.BackgroundColor != Color.Default) ? Element.BackgroundColor : DEFAULT_BACKGROUND_COLOR;

                Color pressedBackgroundColor;
                if (FormsElement.PressedBackgroundColor != Color.Default) {
                    pressedBackgroundColor = FormsElement.PressedBackgroundColor;
                } else {
                    pressedBackgroundColor = normalBackgroundColor.MultiplyAlpha(DEFAULT_PRESSED_BACKGROUND_COLOR_OPACITY);
                }

                var drawable = new StateListDrawable();
                drawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(pressedBackgroundColor.ToAndroid()));
                drawable.AddState(new int[] { }, new ColorDrawable(normalBackgroundColor.ToAndroid()));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
                    Control.Background = drawable;
                } else {
                    Control.SetBackgroundDrawable(drawable);
                }
            }
        }

        void UpdateTextColors() {
            if (FormsElement != null && Control != null) {
                Color normalTextColor = (Element.TextColor != Color.Default) ? Element.TextColor : DEFAULT_TEXT_COLOR;
                Color pressedTextColor = (FormsElement.PressedTextColor != Color.Default) ? FormsElement.PressedTextColor : normalTextColor;

                Control.SetTextColor(new ColorStateList(new int[][] {
                    new int[] { Android.Resource.Attribute.StatePressed },
                    new int[] {}
                }, new int[] {
                    pressedTextColor.ToAndroid(),
                    normalTextColor.ToAndroid()
                }));
            }
        }

    }
}
