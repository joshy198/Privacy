using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
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
        public String SelectedLanguage { set; get; }

        public string UserName { get; set; }
        private readonly INavigationService navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Languages = new List<Language>{ new Language { id = 1, title = "English" }, new Language { id = 2, title = "Deutsch" } };
        }
        public void FinishSetup()
        {
            //if(SelectedLanguage!=String.Empty&&UserName!=String.Empty)
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

    }
}
