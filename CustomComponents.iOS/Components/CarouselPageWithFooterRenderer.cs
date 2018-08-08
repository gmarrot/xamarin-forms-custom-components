using System;
using CoreGraphics;
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
                double pageWidth = View.Frame.Width;
                double pageHeight = View.Frame.Height;

                VisualElement footerView = _footerViewRenderer.Element;
                SizeRequest sizeRequest = footerView.Measure(pageWidth, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                _footerViewRenderer.SetElementSize(new Size(View.Frame.Width, heightRequest));
                _footerViewRenderer.NativeView.Frame = new CGRect(0, pageHeight - heightRequest, View.Frame.Width, heightRequest);
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
            VisualElement footerView = FormsElement?.FooterView;
            if (footerView != null) {
                _footerViewRenderer = GetOrCreateRenderer(footerView);
                View.AddSubview(_footerViewRenderer.NativeView);

                SizeRequest sizeRequest = footerView.Measure(View.Frame.Width, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;
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

            FormsElement.Padding = new Thickness(left, top, right, bottom);
        }

        IVisualElementRenderer GetOrCreateRenderer(VisualElement element) {
            IVisualElementRenderer renderer = Platform.GetRenderer(element);
            if (renderer == null) {
                renderer = Platform.CreateRenderer(element);
                Platform.SetRenderer(element, renderer);
            }

            return renderer;
        }

    }
}
