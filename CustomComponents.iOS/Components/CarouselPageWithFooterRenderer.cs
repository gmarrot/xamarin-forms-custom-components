using System;
using CustomComponents.Components;
using CustomComponents.iOS.Components;
using CustomComponents.iOS.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CarouselPageWithFooter), typeof(CarouselPageWithFooterRenderer))]
namespace CustomComponents.iOS.Components {
    public class CarouselPageWithFooterRenderer : CarouselPageRenderer {

        IVisualElementRenderer _footerViewRenderer;

        CarouselPageWithFooter FormsElement => Element as CarouselPageWithFooter;

        protected override void OnElementChanged(VisualElementChangedEventArgs e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                UpdateFooterView();
            }
        }

        public override void ViewDidLayoutSubviews() {
            base.ViewDidLayoutSubviews();

            if (_footerViewRenderer != null) {
                VisualElement footerView = _footerViewRenderer.Element;
                SizeRequest sizeRequest = footerView.Measure(View.Frame.Width, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                _footerViewRenderer.SetElementSize(new Size(View.Frame.Width, heightRequest));
                _footerViewRenderer.NativeView.Frame = new CoreGraphics.CGRect(0, View.Frame.Height - heightRequest, View.Frame.Width, heightRequest);
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing && _footerViewRenderer != null) {
                if (_footerViewRenderer.Element != null) {
                    _footerViewRenderer.Element.SizeChanged -= HandleOnFooterViewSizeChanged;
                }
                _footerViewRenderer.DisposeRendererAndChildren();
                _footerViewRenderer = null;
            }

            base.Dispose(disposing);
        }

        #region Footer methods

        void UpdateFooterView() {
            _footerViewRenderer?.DisposeRendererAndChildren();
            if (FormsElement != null && FormsElement.FooterView != null) {
                VisualElement footerView = FormsElement.FooterView;
                SizeRequest sizeRequest = footerView.Measure(View.Frame.Width, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                _footerViewRenderer = Platform.CreateRenderer(footerView);
                View.InsertSubview(_footerViewRenderer.NativeView, 1);
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
            var left = FormsElement.Padding.Left;
            var top = FormsElement.Padding.Top;
            var right = FormsElement.Padding.Right;

            var padding = new Thickness { Left = left, Top = top, Right = right, Bottom = bottom };
            FormsElement.Padding = padding;
        }

    }
}
