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
    public class SettingsViewModel : ViewModelBase
    {
        #region variables
        #region pulic variables
        public bool LoadingActive { get; set; }
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public string Username { get; set; }
        private  Profile profile;
        #endregion
        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        #endregion
        #endregion

        /// <summary>
        /// Constructor of the SettingsViewModel, sets the given arguments
        /// </summary>
        /// <param name="navigationService">Navigationservice, configured and created at the ViewModelLocator</param>
        /// <param name="dataService">Takes an instance of an implementation of the IDataService</param>
        /// <param name="mvm">Takes the instance of the MainViewModel</param>
        public SettingsViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        /// <summary>
        /// Sends the data after a view checks to the database
        /// </summary>
        public async void SaveSettings()
        {
            LoadingActive = true;
            if (Username == String.Empty || Username == null)
                Username = "Windows Phone User";
            if (Username.Length > 24)
                Username = Username.Replace(System.Environment.NewLine, " ").Remove(20);
            if (SelectedLanguage < 0)
                SelectedLanguage = 0;
            if (profile.Name != Username)
                await dataService.ChangeUserName(mvm.SystemUserId.Id,Username);
            if (profile.Lang.Id != Languages.ElementAt(SelectedLanguage).Id)
                await dataService.ChangeLanguage(mvm.SystemUserId.Id, Languages.ElementAt(SelectedLanguage).Id);
            GoBackRequest();
            mvm.ReloadUserProfile();
            LoadingActive = false;
        }
        /// <summary>
        /// This function is allways called, when navigated to this page, it's loading the data needed on the page
        /// </summary>
        public async void LoadData()
        {
            LoadingActive = true;
            SelectedLanguage = -1;
            Languages = (await dataService.GetLanguages()).ToList();
            profile = mvm.SystemUserProfile;
            SelectedLanguage = Languages.IndexOf(Languages.Where(x => x.Id == profile.Lang.Id).FirstOrDefault());
            Username = profile.Name;
            LoadingActive = false;
        }
        /// <summary>
        /// This function is called, to navigate back to the previous page
        /// </summary>
        public void GoBackRequest()
        {
            navigationService.GoBack();
        }
    }
}
