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

        #region variables

        #region public variables
        public bool ShowMenu { get; set;}
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public Profile UserProfile { get; set; }
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="dataService"></param>
        /// <param name="mvm"></param>
        public CentralMenuViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }

        /// <summary>
        /// Shows/Hides the Hamburger Menu
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }


        #region Navigation 

        /// <summary>
        /// Navigates to the JoinView
        /// </summary>
        public void NavigateToJoinView()
        {
            navigationService.NavigateTo(Common.Navigation.Join);
        }

        /// <summary>
        /// Navigates to the Category View
        /// </summary>
        public void NavigateToCategoryView()
        {
            navigationService.NavigateTo(Common.Navigation.Category);
        }

        /// <summary>
        /// navigates to the SettingsView
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }

        /// <summary>
        /// Navigates to the About page
        /// </summary>
        public void NavigateToAbout()
        {
            navigationService.NavigateTo(Common.Navigation.About);
        }

        /// <summary>
        /// Closes the Application
        /// </summary>
        public void GoBackRequest()
        {
            App.Current.Exit();
        }
        #endregion
 
        /// <summary>
        /// Loads the Data needed for this page
        /// Is allways called when navigated to this page
        /// </summary>
        public async void LoadData()
        {
            ShowMenu = false;
            while (UserProfile == null)
            {
                await Task.Delay(1000);
                UserProfile = mvm.SystemUserProfile;
            }
        }
    }
}
