using RCPad.ViewModels;
using RCPad.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RCPad
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
