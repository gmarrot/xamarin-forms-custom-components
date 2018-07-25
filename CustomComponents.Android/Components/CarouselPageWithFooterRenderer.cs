using System;
using Android.Content;
using CustomComponents.Components;
using CustomComponents.Droid.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CarouselPageWithFooter), typeof(CarouselPageWithFooterRenderer))]
namespace CustomComponents.Droid.Components {
    public class CarouselPageWithFooterRenderer : CarouselPageRenderer {

        IVisualElementRenderer _footerViewRenderer;

        CarouselPageWithFooter FormsElement => Element as CarouselPageWithFooter;

        public CarouselPageWithFooterRenderer(Context context) : base(context) {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CarouselPage> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateFooterView();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            base.OnLayout(changed, l, t, r, b);

            double x = FormsElement.Padding.Left;
            double y = FormsElement.Padding.Top;
            double w = Context.FromPixels(ViewGroup.MeasuredWidth) - FormsElement.Padding.HorizontalThickness;
            double h = Context.FromPixels(ViewGroup.MeasuredHeight) - FormsElement.Padding.VerticalThickness;

            if (_footerViewRenderer != null) {
                VisualElement footerView = _footerViewRenderer.Element;
                SizeRequest sizeRequest = footerView.Measure(w, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                _footerViewRenderer.Element.Layout(new Rectangle(0, Context.FromPixels(ViewGroup.MeasuredHeight) - heightRequest, w, heightRequest));

                _footerViewRenderer.UpdateLayout();
                _footerViewRenderer.Tracker.UpdateLayout();
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing && _footerViewRenderer != null) {
                if (_footerViewRenderer.Element != null) {
                    _footerViewRenderer.Element.SizeChanged -= HandleOnFooterViewSizeChanged;
                }
                DisposeRenderer(_footerViewRenderer);
                _footerViewRenderer = null;
            }

            base.Dispose(disposing);
        }

        #region Footer methods

        void UpdateFooterView() {
            DisposeRenderer(_footerViewRenderer);
            if (FormsElement != null && FormsElement.FooterView != null) {
                VisualElement footerView = FormsElement.FooterView;
                SizeRequest sizeRequest = footerView.Measure(Context.FromPixels(ViewGroup.MeasuredWidth), double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                _footerViewRenderer = Platform.CreateRendererWithContext(FormsElement.FooterView, Context);
                AddView(_footerViewRenderer.View, 1);
                SetBottomPadding(heightRequest);

                footerView.SizeChanged += HandleOnFooterViewSizeChanged;
            }
        }

        void HandleOnFooterViewSizeChanged(object sender, EventArgs args) {
            if (_footerViewRenderer != null) {
                double height = _footerViewRenderer.Element.Height;
                if (height >= 0) {
                    SetBottomPadding(height);
                }
            }
        }

        #endregion

        void SetBottomPadding(double bottom) {
            double left = FormsElement.Padding.Left;
            double top = FormsElement.Padding.Top;
            double right = FormsElement.Padding.Right;

            var padding = new Thickness { Left = left, Top = top, Right = right, Bottom = bottom };
            FormsElement.Padding = padding;
        }

        void DisposeRenderer(IVisualElementRenderer renderer) {
            if (renderer != null) {
                renderer.View.RemoveFromParent();
                renderer.Dispose();
            }
        }

    }
}
