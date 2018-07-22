using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomComponents.Pages {
    public class RoundedContentViewPageModel : INotifyPropertyChanged {

        const double EPSILON = 0.0001;

        double _borderWidth;
        double _cornerRadius;

        public event PropertyChangedEventHandler PropertyChanged;

        public RoundedContentViewPageModel() {
            BorderWidth = 1;
            CornerRadius = 10;
        }

        public double BorderWidth {
            get => _borderWidth;
            set {
                if (Math.Abs(_borderWidth - value) > EPSILON) {
                    _borderWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        public double CornerRadius {
            get => _cornerRadius;
            set {
                if (Math.Abs(_cornerRadius - value) > EPSILON) {
                    _cornerRadius = value;
                    OnPropertyChanged();
                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
