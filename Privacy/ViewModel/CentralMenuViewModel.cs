using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class CentralMenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private bool showMenu;
        public int MenuWidth { get { if (showMenu) return 0;return 150; } }
        public CentralMenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void HambugerInteraction()
        {
            showMenu = !showMenu;
            System.Diagnostics.Debug.WriteLine("HambugerInteraction");
        }
    }
}
