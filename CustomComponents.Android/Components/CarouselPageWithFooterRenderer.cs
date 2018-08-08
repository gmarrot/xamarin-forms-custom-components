using System;
using Android.Content;
using CustomComponents.Android.Components;
using CustomComponents.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CarouselPageWithFooter), typeof(CarouselPageWithFooterRenderer))]
namespace CustomComponents.Android.Components {
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

            if (_footerViewRenderer != null) {
                double pageWidth = Context.FromPixels(ViewGroup.MeasuredWidth);
                double pageHeight = Context.FromPixels(ViewGroup.MeasuredHeight);

                VisualElement footerView = _footerViewRenderer.Element;
                SizeRequest sizeRequest = footerView.Measure(pageWidth, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double heightRequest = sizeRequest.Request.Height;

                footerView.Layout(new Rectangle(0, pageHeight - heightRequest, pageWidth, heightRequest));
                _footerViewRenderer.UpdateLayout();
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
            VisualElement footerView = FormsElement?.FooterView;
            if (footerView != null) {
                _footerViewRenderer = GetOrCreateRenderer(footerView, Context);
                AddView(_footerViewRenderer.View);

                SizeRequest sizeRequest = footerView.Measure(Context.FromPixels(ViewGroup.MeasuredWidth), double.PositiveInfinity, MeasureFlags.IncludeMargins);
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

        IVisualElementRenderer GetOrCreateRenderer(VisualElement element, Context context) {
            IVisualElementRenderer renderer = Platform.GetRenderer(element);
            if (renderer == null) {
                renderer = Platform.CreateRendererWithContext(element, context);
                Platform.SetRenderer(element, renderer);
            }

            return renderer;
        }

        void DisposeRenderer(IVisualElementRenderer renderer) {
            if (renderer == null) {
                return;
            }

            if (renderer.Element != null && Platform.GetRenderer(renderer.Element) == renderer) {
                Platform.SetRenderer(renderer.Element, null);
            }

            renderer.View?.RemoveFromParent();
            renderer.Dispose();
        }

    }
}
