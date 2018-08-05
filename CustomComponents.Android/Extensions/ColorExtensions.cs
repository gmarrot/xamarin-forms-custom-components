using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;

namespace CustomComponents.Droid.Extensions {
    public static class ColorExtensions {

        public static AColor ToAndroid(this Color color, AColor defaultColor) {
            if (color == Color.Default) {
                return defaultColor;
            }

            return color.ToAndroid();
        }

    }
}
