using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        public SettingsViewModel(INavigationService navigationService, IDataService dataService)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
        }
        public void GoBackRequest()
        {
            navigationService.GoBack();
        }
    }
}
