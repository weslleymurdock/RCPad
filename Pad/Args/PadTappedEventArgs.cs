using Pad.Enums;
using System;
using Xamarin.Forms;

namespace Pad.Args
{
    public class PadTappedEventArgs : TappedEventArgs
    {
        private bool _isTapped;
        private Direction _direction;
        private object paramenter;

        public bool IsTapped => _isTapped;
        public Direction Direction => _direction;



        public PadTappedEventArgs(object parameter) : base(parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            paramenter = parameter;
            _direction = Direction.NONE;
            _isTapped = false;
        }

        public PadTappedEventArgs(Direction flow, object parameter) : base(parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            paramenter = parameter;
            _direction = flow;
            _isTapped = flow == Direction.NONE ? false : true;
        }
    }
}
