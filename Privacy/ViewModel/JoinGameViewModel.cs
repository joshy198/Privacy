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
        public bool LoadingActive { get; set; }
        public int MenuSize { get{ return ShowMenu ? 200 : 0; }}
        public bool ShowMenu { get; set; }
        private string gameId="7";
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
        public string NotificationContent { get; set; }
        public ulong SystemGameID = 0;
        public Profile UserProfile { get; set; }
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
        public async void NavigateToLobbyView()
        {
            LoadingActive = true;
            if (ulong.TryParse(GameId, out SystemGameID))
            {
                NotificationContent = String.Empty;

                if ((await dataService.JoinGame(mvm.SystemUserId.Id, SystemGameID)).Id == SystemGameID)
                    navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.IsClient);
                else
                    NotificationContent = "Wrong Game ID";
            }
            LoadingActive = false;
        }
        public void GoBackRequest()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public async void LoadData()
        {
            LoadingActive = true;
            ShowMenu = false;
            NotificationContent = String.Empty;
            UserProfile = (await dataService.GetUserprofile(mvm.SystemUserId.Id));
            LoadingActive = false;
        }
    }
}
