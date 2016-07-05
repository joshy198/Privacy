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
        #region variables

        #region public variables
        public bool LoadingActive { get; set; }
        public int MenuSize { get{ return ShowMenu ? 200 : 0; }}
        public bool ShowMenu { get; set; }
        public string GameId { get { return gameId; }
            set {
                ulong result;
                if (ulong.TryParse(value, out result))
                    gameId = value;
                else
                    RaisePropertyChanged(nameof(GameId));                }
        }
        public string NotificationContent { get; set; }
        public ulong SystemGameID = 0;
        public Profile UserProfile { get; set; }
        #endregion

        #region private variables
        private string gameId = "7";
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        #endregion

        #endregion

        /// <summary>
        /// Constructor of the JoinGameViewModel, sets the given parameters to readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of GalaSoft's INavigationService Interface</param>
        /// <param name="dataService">Instance of an Implementation of the IDataService Interface</param>
        /// <param name="mvm">Instance of the MainViewModel</param>
        public JoinGameViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }

        /// <summary>
        /// Shows/Hides the Hamburger Menu on this page
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }

        #region Navigation

        /// <summary>
        /// Navigates to the Settings Page
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }

        /// <summary>
        /// Trys to join the Game given by the ID which can be modifyd by the user
        /// if it succeeds, it navigates to the LobbyView and hands over the Mode Parameter
        /// if not, it's showing a Message on the page
        /// </summary>
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

        /// <summary>
        /// When called, it navigates to the CentralMenu Page
        /// </summary>
        public void GoBackRequest()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        #endregion

        /// <summary>
        /// Loads the Data needed for the Page
        /// Allways called when Navigated to the Page
        /// </summary>
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
