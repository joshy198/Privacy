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
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public ID SystemUserId;
        public bool UserControlsEnabled { get; set; }
        public bool StartScreenVisible { get { return !UserControlsEnabled; } }
        public bool NameInfoAvailable { get { return UserName == null || UserName == String.Empty; } }
        public bool LangInfoAvailable { get { return SelectedLanguage == -1; } }

        public string UserName { get; set; }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly IStorageService storageService;
        public MainViewModel(INavigationService navigationService, IDataService dataService, IStorageService storageService)
        {
            SelectedLanguage = -1;
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.storageService = storageService;
            SystemUserId = this.storageService.Read<ID>(nameof(SystemUserId), new ID { Id=0});

        }
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
                navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public async void LoadData()
        {
            UserControlsEnabled = false;
            if (SystemUserId.Id > 0)
                if (await dataService.IsUserExisting(SystemUserId.Id))
                {
                    navigationService.NavigateTo(Common.Navigation.CentralMenu);
                    return;
                }
            Languages = (await dataService.GetLanguages()).ToList();
            UserControlsEnabled = true;

        }
        public void GoBackRequest()
        {
            App.Current.Exit();
        }

    }
}
