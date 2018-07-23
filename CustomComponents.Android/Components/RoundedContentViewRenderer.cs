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

        int _width;
        int _height;
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

            _width = w;
            _height = h;
            UpdateClipPath();
        }

        protected override void DispatchDraw(Canvas canvas) {
            if (_clipPath != null) {
                int savedCanvas = canvas.Save();

                canvas.ClipPath(_clipPath);
                base.DispatchDraw(canvas);

                canvas.RestoreToCount(savedCanvas);
            } else {
                base.DispatchDraw(canvas);
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

                UpdateClipPath();
            }
        }

        void UpdateClipPath() {
            if (_clipPath != null && _width > 0 && _height > 0) {
                using (var bounds = new RectF(PaddingLeft, PaddingTop, _width - PaddingRight, _height - PaddingBottom)) {
                    bounds.Inset(_borderWidth, _borderWidth);
                    using (Path.Direction direction = Path.Direction.Cw) {
                        _clipPath.Reset();
                        _clipPath.AddRoundRect(bounds, _cornerRadius, _cornerRadius, direction);
                        _clipPath.Close();
                    }
                }
            }
        }

    }
}
