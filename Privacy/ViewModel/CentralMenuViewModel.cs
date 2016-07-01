using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Model;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class CentralMenuViewModel : ViewModelBase
    {
        public bool ShowMenu { get; set;}
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        public Profile UserProfile{get{ return dataService.GetUserprofile(mvm.SystemUserId); }}
        public CentralMenuViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void NavigateToJoinView()
        {
            navigationService.NavigateTo(Common.Navigation.Join);
        }
        public void NavigateToCategoryView()
        {
            navigationService.NavigateTo(Common.Navigation.Category);
        }
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        public void GoBackRequest()
        {
            App.Current.Exit();
        }
    }
}
