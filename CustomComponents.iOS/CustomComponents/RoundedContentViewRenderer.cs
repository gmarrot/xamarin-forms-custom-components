using System;
using System.ComponentModel;
using CustomComponents.Components;
using CustomComponents.iOS.CustomComponents;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace CustomComponents.iOS.CustomComponents {
    public class RoundedContentViewRenderer : VisualElementRenderer<RoundedContentView> {

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedContentView> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                Layer.MasksToBounds = true;

                UpdateBorderWidth();
                UpdateBorderColor();
                UpdateCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedContentView.BorderWidthProperty.PropertyName) {
                UpdateBorderWidth();
            } else if (e.PropertyName == RoundedContentView.BorderColorProperty.PropertyName) {
                UpdateBorderColor();
            } else if (e.PropertyName == RoundedContentView.CornerRadiusProperty.PropertyName) {
                UpdateCornerRadius();
            }
        }

        void UpdateBorderWidth() {
            if (Element != null) {
                Layer.BorderWidth = Math.Max(0, Convert.ToSingle(Element.BorderWidth));
            }
        }

        void UpdateBorderColor() {
            if (Element != null && Element.BorderColor != Color.Default) {
                Layer.BorderColor = Element.BorderColor.ToCGColor();
            }
        }

        void UpdateCornerRadius() {
            if (Element != null) {
                Layer.CornerRadius = Math.Max(0, Convert.ToSingle(Element.CornerRadius));
            }
        }

    }
}
