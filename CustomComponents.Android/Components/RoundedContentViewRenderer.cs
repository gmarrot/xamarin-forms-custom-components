using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using CustomComponents.Components;
using CustomComponents.Droid.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace CustomComponents.Droid.Components {
    public class RoundedContentViewRenderer : VisualElementRenderer<RoundedContentView> {

        Path _clipPath;
        int _borderWidth;
        float _cornerRadius;

        public RoundedContentViewRenderer(Context context) : base(context) {
            _clipPath = new Path();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedContentView> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateBackground();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName
                || e.PropertyName == RoundedContentView.BorderWidthProperty.PropertyName
                || e.PropertyName == RoundedContentView.BorderColorProperty.PropertyName
                || e.PropertyName == RoundedContentView.CornerRadiusProperty.PropertyName) {
                UpdateBackground();
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (_clipPath != null) {
                using (var bounds = new RectF(0, 0, w, h)) {
                    using (Path.Direction direction = Path.Direction.Cw) {
                        _clipPath.Reset();
                        _clipPath.AddRoundRect(bounds, _cornerRadius, _cornerRadius, direction);
                        _clipPath.Close();
                    }
                }
            }
        }

        public override void Draw(Canvas canvas) {
            if (_clipPath != null) {
                canvas.Save();
                canvas.ClipPath(_clipPath);
                base.Draw(canvas);
                canvas.Restore();
            } else {
                base.Draw(canvas);
            }
        }

        protected override void UpdateBackgroundColor() {
            // Do nothing to override default behavior
        }

        protected override void Dispose(bool disposing) {
            if (disposing && _clipPath != null) {
                _clipPath.Dispose();
                _clipPath = null;
            }

            base.Dispose(disposing);
        }

        void UpdateBackground() {
            if (Element != null) {
                var drawable = new GradientDrawable();
                drawable.SetColor(Element.BackgroundColor.ToAndroid());

                _cornerRadius = Context.ToPixels(Math.Max(0, Element.CornerRadius));
                drawable.SetCornerRadius(_cornerRadius);

                _borderWidth = Convert.ToInt32(Context.ToPixels(Math.Max(0, Element.BorderWidth)));
                drawable.SetStroke(_borderWidth, Element.BorderColor.ToAndroid());

                if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean) {
                    Background = drawable;
                } else {
                    SetBackgroundDrawable(drawable);
                }
            }
        }

    }
}
