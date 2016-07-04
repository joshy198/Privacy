﻿using GalaSoft.MvvmLight;
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
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public bool UserControlsEnabled { get; set; }
        public bool LoadingActive { get { return !UserControlsEnabled; } }
        public string Username { get; set; }
        private  Profile profile;
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        public SettingsViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        public void GoBackRequest()
        {
            navigationService.GoBack();
        }

        public async void SaveSettings()
        {
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
        }
        public async void LoadData()
        {
            SelectedLanguage = -1;
            UserControlsEnabled = false;
            Languages = (await dataService.GetLanguages()).ToList();
            profile = (await dataService.GetUserprofile(mvm.SystemUserId.Id));
            SelectedLanguage = Languages.IndexOf(Languages.Where(x => x.Id == profile.Lang.Id).FirstOrDefault());
            Username = profile.Name;
            UserControlsEnabled = true;
        }
    }
}
