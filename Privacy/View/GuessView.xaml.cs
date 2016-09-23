using Privacy.ViewModel;
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
    public sealed partial class GuessView : Page
    {
        public GuessView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VM.Mode = e.Parameter as string;
            ((App)Application.Current).OnBackRequested += OnOnBackRequested;
            base.OnNavigatedTo(e);
            VM.LoadData();
            VM.isActive = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ((App)Application.Current).OnBackRequested -= OnOnBackRequested;
            base.OnNavigatingFrom(e);
            VM.isActive = false;
        }
        private async void OnOnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            var dialog = new MessageDialog(VM.LanguagePackage.QuitMsg);
            dialog.Title = VM.LanguagePackage.ConfirmationTranslation;
            dialog.Commands.Add(new UICommand { Label = VM.LanguagePackage.YesTranslation, Id = 0 });
            dialog.Commands.Add(new UICommand { Label = VM.LanguagePackage.NoTranslation, Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 0)
            {
                VM.GoBackRequest();
            }
        }
        private GuessViewModel VM => DataContext as GuessViewModel;
    }
}
