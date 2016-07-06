using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Model;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        #region variables
       
        #region public variables
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public ID SystemUserId;
        public Profile SystemUserProfile { get; set; }
        public bool UserControlsEnabled { get; set; }
        public bool StartScreenVisible { get { return !UserControlsEnabled; } }
        public bool NameInfoAvailable { get { return UserName == null || UserName == String.Empty; } }
        public bool LangInfoAvailable { get { return SelectedLanguage == -1; } }
        public string UserName { get; set; }
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly IStorageService storageService;
        #endregion
       
        #endregion

        /// <summary>
        /// Constructor of the Navigationviewmodel, which saves the given parameters to private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of Galasoft's INavigationService</param>
        /// <param name="dataService">Instance of an implementation of the IDataService</param>
        /// <param name="storageService">Instance of an implementation of the IStorageService</param>
        public MainViewModel(INavigationService navigationService, IDataService dataService, IStorageService storageService)
        {
            SelectedLanguage = -1;
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.storageService = storageService;
            SystemUserId = this.storageService.Read<ID>(nameof(SystemUserId), new ID { Id=0});
        }

        /// <summary>
        /// Called when the User has entered all the Information
        /// After a view pre-checks the Data is sent to the server to create an user and returning the USER's ID
        /// The Function stores the ID Permanently on the device
        /// </summary>
        public async void FinishSetup()
        {
            if (UserName == String.Empty || UserName == null)
                UserName = "Windows Phone User";
            if (UserName.Length > 24)
                UserName = UserName.Replace(System.Environment.NewLine, " ").Remove(20);
            if (SelectedLanguage < 0)
                SelectedLanguage = 0;
            SystemUserId = await dataService.CreateUser(Languages.ElementAt(SelectedLanguage).Id, UserName);
            storageService.Write(nameof(SystemUserId), SystemUserId);
            ReloadUserProfile();
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

        /// <summary>
        /// Called when navigated to the MainPage, loads the Data needed, before displaying the Usercontrols
        /// If a user has already been created it's redirecting to the central Menu Page
        /// </summary>
        public async void LoadData()
        {
            UserControlsEnabled = false;
            if (SystemUserId.Id > 0)
                if (await dataService.IsUserExisting(SystemUserId.Id))
                {
                    navigationService.NavigateTo(Common.Navigation.CentralMenu);
                    ReloadUserProfile();
                    return;
                }
            Languages = (await dataService.GetLanguages()).ToList();
            UserControlsEnabled = true;

        }

        /// <summary>
        /// When called reloads the users profile, if a profile with the user's id is not existent, he will be directed to the main page
        /// </summary>
        public async void ReloadUserProfile()
        {
            if (await dataService.IsUserExisting(SystemUserId.Id))
            {
                
                var prof = await dataService.GetUserprofile(SystemUserId.Id);
                if (SystemUserProfile == null)
                    SystemUserProfile = prof;
                else
                {
                    SystemUserProfile.ID = prof.ID;
                    SystemUserProfile.Lang.Id = prof.Lang.Id;
                    SystemUserProfile.Lang.Title = prof.Lang.Title;
                    SystemUserProfile.Name = prof.Name;
                    SystemUserProfile.Points = prof.Points;
                }
            }
            else
                navigationService.NavigateTo(Common.Navigation.Main);
        }

        /// <summary>
        /// Closes the Application
        /// </summary>
        public void GoBackRequest()
        {
            App.Current.Exit();
        }

    }
}
