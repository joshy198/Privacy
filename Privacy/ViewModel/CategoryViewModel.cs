using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class CategoryViewModel:ViewModelBase
    {
        private readonly INavigationService navigationService;
        public CategoryViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void NavigateToLobby()
        {
            navigationService.NavigateTo(Common.Navigation.Lobby);
        }
    }
}
