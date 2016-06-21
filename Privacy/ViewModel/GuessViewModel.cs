using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class GuessViewModel:ViewModelBase
    {
        public string Mode { get; set; }
        public int NumberOfPlayers { get; set; }
        public int SelectedAmmountOfPlayers { get; set; }
        private readonly INavigationService navigationService;
        public GuessViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            SelectedAmmountOfPlayers = 3;
            NumberOfPlayers = 8;
        }
        public void NavigateToQuestion()
        {
            if (Common.Mode.IsClient == Mode)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.ClientStatistic);
            else if (Common.Mode.IsHost == Mode)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostStatistic);
        }
    }
}
