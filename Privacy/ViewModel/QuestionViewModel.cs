using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class QuestionViewModel :ViewModelBase
    {
        public string Mode { get; set; }
        private readonly INavigationService navigationService;
        public QuestionViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void NavigateToYes()
        {
            NavigateToLobbyView();
        }
        public void NavigateToNo()
        {
            NavigateToLobbyView();
        }
        private void NavigateToLobbyView()
        {
            if(Mode==Common.Mode.IsClient)
            navigationService.NavigateTo(Common.Navigation.Lobby,Common.Mode.ClientWait);
            else if (Mode == Common.Mode.IsHost)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostWait);
        }
    }
}
