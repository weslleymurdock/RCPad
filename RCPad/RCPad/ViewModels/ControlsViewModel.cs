using Pad.Enums;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace RCPad.ViewModels
{
    public class ControlsViewModel : BaseViewModel
    {
        public ICommand TappedCommand { get; }
        public ICommand DirectionSwipedCommand { get; }
        public ControlsViewModel()
        {
            Title = "Controls";
            TappedCommand = new Command(ExecuteTappedACommand);
            DirectionSwipedCommand = new Command(ExecuteTappedBCommand);
        }

        private async void ExecuteTappedBCommand(object obj)
        {
            await Shell.Current.DisplayAlert("title", "message", "OK");
        }

        private async void ExecuteTappedACommand(object obj)
        {
            var direction = (Direction)obj;
            await Shell.Current.DisplayAlert("direction", direction.ToString(), "ok");
        }
    }
}