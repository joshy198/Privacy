using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private readonly INavigationService navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void FinishSetup()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

    }
}
