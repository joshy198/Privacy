using Privacy.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class AboutView : Page
    {
        public AboutView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((App)Application.Current).OnBackRequested += OnOnBackRequested;
            base.OnNavigatedTo(e);
            VM.LoadData();
        }
        /// <summary>
        /// Function is called when it's navigated away from this page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ((App)Application.Current).OnBackRequested -= OnOnBackRequested;

            base.OnNavigatingFrom(e);
        }
        /// <summary>
        /// function is called when the device's back button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            VM.GoBackRequest();
        }
        private AboutViewModel VM => DataContext as AboutViewModel;
    }
}
