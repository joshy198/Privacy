using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class CentralMenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public bool ShowMenu { get; set;}
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public CentralMenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
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
    }
}
