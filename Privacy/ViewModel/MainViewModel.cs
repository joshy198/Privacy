using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public ulong SystemUserId;

        public string UserName { get; set; }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        public MainViewModel(INavigationService navigationService, IDataService dataService)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            Languages = dataService.GetLanguages().ToList();
            if (SystemUserId > 0)
                navigationService.NavigateTo(Common.Navigation.CentralMenu);

        }
        public void FinishSetup()
        {
            if (UserName == String.Empty || UserName == null)
                UserName = "Windows Phone User";
            if (UserName.Length > 24)
                UserName=UserName.Replace(System.Environment.NewLine, " ").Remove(20);

            else
                SystemUserId = dataService.CreateUser(Languages.ElementAt(SelectedLanguage).id, UserName);
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public void GoBackRequest()
        {
            App.Current.Exit();
        }

    }
}
