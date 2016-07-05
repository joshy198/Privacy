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
    public class GuessViewModel:ViewModelBase
    {
        #region variables

        #region public variables
        public bool LoadingActive { get; set; }
        public string AnswerString { get { return SelectedAmmountOfPlayers + " of them have done that!"; } }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public bool ShowMenu { get; set; }
        public string Mode { get; set; }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#" + jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public string GameInfo { get { return Common.Mode.IsClient == Mode ? jvm.SystemGameID.ToString() : cvm.SystemGameID.ToString(); } }
        public int NumberOfPlayers { get; set; }
        public Profile UserProfile { get; set; }
        public int SelectedAmmountOfPlayers { get; set; }
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly CategoryViewModel cvm;
        private readonly JoinGameViewModel jvm;
        private readonly MainViewModel mvm;
        private readonly QuestionViewModel qvm;
        #endregion

        #endregion

        /// <summary>
        /// Constructor of the GuessViewModel
        /// Sets the given parameters to private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of GalaSoft's INavigationService Interface</param>
        /// <param name="dataService">Instance of an implementation of the IDataService Interface</param>
        /// <param name="cvm">Instance of the CategoryViewModel</param>
        /// <param name="jvm">Instance of the JoinViewModel</param>
        /// <param name="mvm">Instance of the MainViewModel</param>
        /// <param name="qvm">Instance of the QuestionViewModel</param>
        public GuessViewModel(INavigationService navigationService, IDataService dataService, CategoryViewModel cvm, JoinGameViewModel jvm, MainViewModel mvm, QuestionViewModel qvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.jvm = jvm;
            this.cvm = cvm;
            this.mvm = mvm;
            this.qvm = qvm;
        }

        /// <summary>
        /// Hides/Displays the Hamburger Menu
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }

        #region Navigation

        /// <summary>
        /// Checks if the Game is Existing, if so, Navigates to the QuestionViewModel and Handing over the Mode, which depends on this pages's Mode
        /// </summary>
        public async void NavigateToQuestion()
        {
            LoadingActive = true;
            if (await dataService.IsGameExisting(Common.Mode.IsClient == Mode ? jvm.SystemGameID : cvm.SystemGameID))
            {
                if (await dataService.AnswerQuestion(mvm.SystemUserId.Id, Common.Mode.IsClient == Mode ? jvm.SystemGameID : cvm.SystemGameID, qvm.Question.ID, qvm.Answer, SelectedAmmountOfPlayers))
                {
                    if (Common.Mode.IsClient == Mode)
                        navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.ClientStatistic);
                    else if (Common.Mode.IsHost == Mode)
                        navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostStatistic);
                }
                else
                    navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.ClientStatistic == Mode ? Common.Mode.IsClient : Common.Mode.IsHost);
            }
            else
                NavigateToCentralMenu();
            LoadingActive = false;
        }

        /// <summary>
        /// Navigates to the Settings Page
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        
        /// <summary>
        /// Quits the Game and Navigates to the Central Menu
        /// </summary>
        public void NavigateToCentralMenu()
        {
            dataService.QuitGame(mvm.SystemUserId.Id);
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

        /// <summary>
        /// Quits the Game and navigates back to the JoinView or the CategoryView depending on the Mode
        /// </summary>
        public void GoBackRequest()
        {
            dataService.QuitGame(mvm.SystemUserId.Id);
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Join);
            else
                navigationService.NavigateTo(Common.Navigation.Category);
        }
        #endregion

        /// <summary>
        /// Loading the Data needed for this page
        /// Allways called when navigated to this Page
        /// </summary>
        public async void LoadData()
        {
            LoadingActive = true;
            ShowMenu = false;
            NumberOfPlayers = (await dataService.CountPlayersByGameId(Common.Mode.IsClient == Mode ? jvm.SystemGameID : cvm.SystemGameID));
            UserProfile = (await dataService.GetUserprofile(mvm.SystemUserId.Id));
            LoadingActive = false;
    }
    }
}
