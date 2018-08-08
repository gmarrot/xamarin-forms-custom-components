using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace CustomComponents.iOS.Extensions {
    public static class VisualElementRendererExtensions {

        public static void DisposeRendererAndChildren(this IVisualElementRenderer renderer) {
            if (renderer == null) {
                return;
            }

            if (renderer.Element != null && Platform.GetRenderer(renderer.Element) == renderer) {
                Platform.SetRenderer(renderer.Element, null);
            }

            if (renderer.NativeView != null) {
                UIView[] subviews = renderer.NativeView.Subviews;
                for (int i = 0; i < subviews.Length; i++) {
                    if (subviews[i] is IVisualElementRenderer childRenderer) {
                        childRenderer.DisposeRendererAndChildren();
                    }
                }

                renderer.NativeView.RemoveFromSuperview();
            }
            renderer.Dispose();
        }

    }
}
