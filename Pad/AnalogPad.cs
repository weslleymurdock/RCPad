using Pad.Args;
using Pad.Enums;
using System;
using Xamarin.Forms;
using Constraint = Xamarin.Forms.Constraint;

namespace Pad
{
    public partial class AnalogPad : ContentView

    {
        private const string animationName = "CenterAnimation";
        private readonly Frame _button;
        private readonly Func<double, double, double> _offset;
        private double previousX = 0.0;
        private double previousY = 0.0;

        private readonly Frame _background;

        public AnalogPad()
        {
            _offset = Device.RuntimePlatform == Device.Android ? (Func<double, double, double>)((a, b) => a) : (a, b) => a - b;

            var root = new RelativeLayout();

            _background = new Frame { BackgroundColor = Color.LightBlue };
            root.Children.Add(
                    _background,
                    Constraint.RelativeToParent(p => p.Width / 2.0 - Math.Min(p.Width, p.Height) / 2.0),
                    Constraint.RelativeToParent(p => p.Height / 2.0 - Math.Min(p.Width, p.Height) / 2.0),
                    Constraint.RelativeToParent(p =>
                    {
                        _background.CornerRadius = (float)(Math.Min(p.Width, p.Height) / 2.0);
                        return Math.Min(p.Width, p.Height);
                    }
                        ),
                    Constraint.RelativeToParent(p => Math.Min(p.Width, p.Height))
                );

            _button = new Frame { BackgroundColor = Color.FromHex("#DC143D") };
            var dragGesture = new PanGestureRecognizer();
            dragGesture.PanUpdated += OnThumbDragged;
            _button.GestureRecognizers.Add(dragGesture);
            root.Children.Add(
                    _button,
                    Constraint.RelativeToParent(p => p.Width / 2.0 - (0.1 * Math.Min(p.Width, p.Height))),
                    Constraint.RelativeToParent(p => p.Height / 2.0 - (0.1 * Math.Min(p.Width, p.Height))),
                    Constraint.RelativeToParent(p =>
                    {
                        var width = 0.2 * Math.Min(p.Width, p.Height);

                        _button.CornerRadius = (float)(width / 2.0);
                        return width;
                    }),
                    Constraint.RelativeToParent(p => 0.2 * Math.Min(p.Width, p.Height))
                );

            this.Content = root;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        private void OnThumbDragged(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:

                    this.AbortAnimation(animationName);
                    previousX = e.TotalX;
                    previousY = e.TotalY;
                    _button.Scale = 1.2;
                    TappedCommand?.Execute(Direction.NONE);
                    DirectionSwipedCommand?.Execute(Direction.NONE);

                    break;

                case GestureStatus.Running:
                    var x = _button.TranslationX + _offset(e.TotalX, previousX);
                    var y = _button.TranslationY + _offset(e.TotalY, previousY);

                    var radius = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    var theta = Math.Atan2(y, x);

                    radius = Math.Min(_background.Width / 2.0, radius);

                    _button.TranslationX = radius * Math.Cos(theta);
                    _button.TranslationY = radius * Math.Sin(theta);

                    Update();

                    previousX = e.TotalX;
                    previousY = e.TotalY;
                    break;

                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    _button.Scale = 1.0;
                    Animate();
                    OnThumbTapped(sender, new PadTappedEventArgs(Direction.NONE, sender));
                    break;
            }
        }

        private void OnThumbTapped(object sender, PadTappedEventArgs e) => TappedCommand?.Execute(e.Direction);

        private void Animate() => new Animation(d =>
            {
                _button.TranslationX = d * _button.TranslationX;
                _button.TranslationY = d * _button.TranslationY;
            }, 1.0, 0.0, Easing.CubicInOut)
               .Commit(this, animationName, length: 150);

        private void Update()
        {
            var constant = Math.Min(_background.Width, _background.Height) / 2.0;
            XAxis = _button.TranslationX / constant;
            YAxis = _button.TranslationY / constant;

            Direction flow = GetDirection(XAxis, YAxis);
            DirectionSwipedCommand?.Execute(flow);
        }

        private Direction GetDirection(double x, double y)
        {
            if (y > 0.4)
                return (x >= -0.25 && x <= 0.25) ? Direction.CENTER_TO_UP : (x >= -0.26) ? Direction.CENTER_TO_UP_LEFT : Direction.CENTER_TO_UP_RIGHT;
            if (y <= 0.3 && y >= -0.4)
                return (x >= -0.25 && x <= 0.25) ? Direction.NONE : (x >= -0.26) ? Direction.CENTER_TO_UP_LEFT : Direction.CENTER_TO_UP_RIGHT;
            if (y >= -0.5)
                return (x >= -0.25 && x <= 0.25) ? Direction.CENTER_TO_DOWN : (x >= -0.26) ? Direction.CENTER_TO_DOWN_LEFT : Direction.CENTER_TO_DOWN_RIGHT;
            return Direction.NONE;
        }
    }
}
