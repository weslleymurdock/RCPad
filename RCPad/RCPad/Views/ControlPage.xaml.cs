using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RCPad.Views
{
    public partial class ControlPage : ContentPage
    {
        public ControlPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ControlsViewModel();
        }
    }
}