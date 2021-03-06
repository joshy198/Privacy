﻿using Privacy.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Privacy.View
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class CentralMenuView : Page
    {
        public CentralMenuView()
        {
            this.InitializeComponent();
        }
        private CentralMenuViewModel VM => DataContext as CentralMenuViewModel;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((App)Application.Current).OnBackRequested += OnOnBackRequested;
            base.OnNavigatedTo(e);
            VM.LoadData();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ((App)Application.Current).OnBackRequested -= OnOnBackRequested;

            base.OnNavigatingFrom(e);
        }
        private async void OnOnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            var dialog = new MessageDialog("Do you want to quit?");
            dialog.Title = "Confirmation";
            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 0)
            {
                VM.GoBackRequest();
            }
        }
    }
}
