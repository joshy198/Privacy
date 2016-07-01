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
    public class JoinGameViewModel : ViewModelBase
    {
        public int MenuSize { get{ return ShowMenu ? 200 : 0; }}
        public bool ShowMenu { get; set; }
        private string gameId="8";
        public string GameId { get { return gameId; }
            set {
                ulong result;
                if (ulong.TryParse(value, out result))
                    gameId = value;
                else
                    RaisePropertyChanged(nameof(GameId));                }
        }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        public ulong SystemGameID = 0;
        public Profile UserProfile { get { return dataService.GetUserprofile(mvm.SystemUserId); } }
        public JoinGameViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void NavigateToLobbyView()
        {
            if (ulong.TryParse(GameId, out SystemGameID))
            {

                if (dataService.JoinGame(mvm.SystemUserId, SystemGameID) == SystemGameID)
                    navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.IsClient);
            }
        }
        public void GoBackRequest()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
    }
}
