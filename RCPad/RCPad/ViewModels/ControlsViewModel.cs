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
        public ICommand TappedACommand { get; }
        public ICommand TappedBCommand { get; }

        public ControlsViewModel()
        {
            Title = "Controls";
            TappedACommand = new Command(ExecuteTappedACommand);
            TappedBCommand = new Command(ExecuteDirectionSwipedCommand);
        }

        private async void ExecuteDirectionSwipedCommand(object obj)
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