using Pad.Enums;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pad
{
    public partial class AnalogPad
    {
        public static readonly BindableProperty XAxisProperty = BindableProperty.Create(
            nameof(XAxis),
            typeof(double),
            typeof(AnalogPad),
            0.0
        );

        public double XAxis { get => (double)GetValue(XAxisProperty); set => SetValue(XAxisProperty, value); }

        public static readonly BindableProperty YAxisProperty = BindableProperty.Create(
            nameof(YAxis),
            typeof(double),
            typeof(AnalogPad),
            0.0
        );

        public double YAxis { get => (double)GetValue(YAxisProperty); set => SetValue(YAxisProperty, value); }

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(AnalogPad), default(ICommand));

        public ICommand TappedCommand { get =>(ICommand)GetValue(TappedCommandProperty); set => SetValue(TappedCommandProperty, value); }
        

        public static readonly BindableProperty DirectionSwipedProperty =
           BindableProperty.Create(nameof(DirectionSwipedCommand), typeof(ICommand), typeof(Direction), default(ICommand));

        public ICommand DirectionSwipedCommand
        {
            get { return (ICommand)GetValue(DirectionSwipedProperty); }
            set { SetValue(DirectionSwipedProperty, value); }
        }
    }
}
