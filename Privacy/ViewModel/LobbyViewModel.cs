using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Model;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Privacy.ViewModel
{
    public class LobbyViewModel:ViewModelBase
    {
        #region variables
        #region public variables
        public bool LoadingActive { get; set; }
        public bool isActive;
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public string Mode { get;
            set; }
        public bool NextAvailable { get; set; }
        public bool AdminAvailable { get {
                if (NextAvailable || Common.Mode.IsClient == Mode || Common.Mode.ClientStatistic == Mode || Common.Mode.ClientWait == Mode)
                    return false;
                return true;
            } }
        public string PlayersList { get; set; }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#" + jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public string ButtonText { get {
                if (Common.Mode.HostStart == Mode)
                {
                    NextAvailable = true;
                    return "Start Game";
                }
                else if (Common.Mode.ClientStatistic == Mode)
                    return "Continue";
                else if (Common.Mode.HostStatistic == Mode)
                {
                    NextAvailable = true;
                    return "Continue";
                }
                else if (Common.Mode.HostWait == Mode)
                {
                    //NextAvailable = true;
                    return "Ok";
                }
                else if (Common.Mode.ClientWait == Mode)
                    return "Continue";
                else if (Common.Mode.IsClient == Mode)
                {
                    return "Start";
                }
                return "ERR";
            } }
        public string AdminText
        {
            get
            {
                if (Common.Mode.HostStatistic == Mode)
                {
                    return "Allow Statistic";
                }
                else if (Common.Mode.HostWait == Mode)
                {
                    return "Allow Guessing";
                }
                return "ERR";
            }
        }
        public string PlayersListHeader { get; set; }
        public Profile UserProfile { get; set; }
        #endregion
        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly JoinGameViewModel jvm;
        private readonly CategoryViewModel cvm;
        private readonly MainViewModel mvm;
        #endregion
        #endregion

        /// <summary>
        /// Constructor of the LobbyViewModel
        /// Sets the given parameters to private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of Gaasoft's INavigationService Interface</param>
        /// <param name="dataService">Instance of an implementation of the IDataServiceInterface</param>
        /// <param name="jvm">Instance of of the JoinViewModel</param>
        /// <param name="cvm">Instance of of the CategoryViewModel</param>
        /// <param name="mvm">Instance of of the MainViewModel</param>
        public LobbyViewModel(INavigationService navigationService, IDataService dataService, JoinGameViewModel jvm, CategoryViewModel cvm, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.jvm = jvm;
            this.cvm = cvm;
            this.mvm = mvm;
        }

        /// <summary>
        /// Enables the Guessing Area as an Game Master
        /// </summary>
        public async void EnableNext()
        {
            LoadingActive = true;
            if (await dataService.IsGameExisting(cvm.SystemGameID))
            {
                if (Common.Mode.HostWait == Mode)
                    NextAvailable = await dataService.AllowContinue(mvm.SystemUserId.Id, cvm.SystemGameID);
            }
            else
            NavigateToCentralMenu();
            LoadingActive = false;
        }
        
        /// <summary>
        /// Shows/Hides the Hamburger Menu
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        /// <summary>
        /// Loads and displays the list of Information depending on the current Mode
        /// </summary>
        public async void ReloadPlayers()
        {
            if (Common.Mode.HostStart == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    PlayersListHeader = "Game ID " + cvm.SystemGameID;
                    string data = "";
                    foreach (var v in await dataService.GetPlayersInGame(cvm.SystemGameID))
                    {
                        data += v.Title + "\n";
                    }
                    PlayersList = data;
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.IsClient == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    string data = "";
                    PlayersListHeader = "Players";
                    foreach (var v in await dataService.GetPlayersInGame(jvm.SystemGameID))
                    {
                        data += v.Title + "\n";
                    }
                    PlayersList = data;
                    NextAvailable = true;
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.ClientWait == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    string data = "";
                    NextAvailable = await dataService.IsContinueAllowed(jvm.SystemGameID);
                    PlayersListHeader = "Answered Players";
                    foreach (var v in await dataService.GetAnsweredUsers(jvm.SystemGameID))
                    {
                        data += v.Title + "\n";
                    }
                    PlayersList = data;
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.HostWait == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    string data = "";
                    PlayersListHeader = "Answered Players";
                    foreach (var v in await dataService.GetAnsweredUsers(cvm.SystemGameID))
                    {
                        data += v.Title + "\n";
                    }
                    PlayersList = data;

                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.ClientStatistic == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    string data = "";
                    NextAvailable = !await dataService.IsContinueAllowed(jvm.SystemGameID);
                    PlayersListHeader = "Player\t\tDifference\tTotal";
                    foreach (var v in (await dataService.GetStatisticByGameId(jvm.SystemGameID)).OrderByDescending(x => x.Points))
                    {
                        if (v.Name.Length > 15)
                            v.Name = v.Name.Remove(15);
                        data += v.Name.PadRight(15, ' ');
                        if (v.Name.Length < 10)
                            data += "\t";
                        data += String.Format("{0,9}\t\t{1}\n", v.Difference, v.Points);
                        //PlayersList += ""+v.name + "\n\n";
                    }
                    PlayersList = data;
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.HostStatistic == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    string data = "";
                    PlayersListHeader = "Player\t\tDifference\tTotal";
                    foreach (var v in (await dataService.GetStatisticByGameId(cvm.SystemGameID)).OrderByDescending(x => x.Points))
                    {
                        if (v.Name.Length > 15)
                            v.Name = v.Name.Remove(15);
                        PlayersList += v.Name.PadRight(15, ' ');
                        if (v.Name.Length < 10)
                            PlayersList += "\t";
                        PlayersList += String.Format("{0,9}\t\t{1}\n", v.Difference, v.Points);
                        //PlayersList += ""+v.name + "\n\n";
                    }
                    PlayersList = data;
                }
                else
                    NavigateToCentralMenu();
            }
        }

        #region Navigation

        /// <summary>
        /// Quits the Game and navigates to the Central Menu
        /// </summary>
        public void NavigateToCentralMenu()
        {
            dataService.QuitGame(mvm.SystemUserId.Id);
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

        /// <summary>
        /// Navigates to the Settings View
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }

        /// <summary>
        /// Navigating to the QuestionView
        /// Depending on the State it's handing over parameters and
        /// sets the next question ID
        /// if there are no more questions it's giving a feedback to the User and navigates to the 
        /// CategoryView
        /// </summary>
        public async void NavigateToQuestionView()
        {
            LoadingActive = true;
            if (Common.Mode.HostStart == Mode)
            {
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsHost);
            }
            else if (Common.Mode.HostStatistic == Mode)
            {
                if (await cvm.GoToNextQuestion())
                    navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsHost);
                else
                {
                    var dialog = new MessageDialog("You've played through all the questions");
                    dialog.Title = "Congratulations";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    var res = await dialog.ShowAsync();
                    if ((int)res.Id == 0)
                    {
                        GoBackRequest();
                    }
                }

            }
            else if (Common.Mode.ClientStatistic == Mode)
            {
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsClient);
            }
            else if (Common.Mode.HostWait == Mode)
            {
                navigationService.NavigateTo(Common.Navigation.Guess, Common.Mode.IsHost);
            }
            else if (Common.Mode.ClientWait == Mode)
            {
                navigationService.NavigateTo(Common.Navigation.Guess, Common.Mode.IsClient);
            }
            else if (Common.Mode.IsClient == Mode)
            {
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsClient);
            }
            LoadingActive = false;
        }

        /// <summary>
        /// Navigates Back to the CategoryView or the JoinView depending on the current Mode
        /// </summary>
        public void GoBackRequest()
        {
            dataService.QuitGame(mvm.SystemUserId.Id);
            if (Mode == Common.Mode.HostStart || Mode == Common.Mode.HostStatistic || Mode == Common.Mode.HostWait)
                navigationService.NavigateTo(Common.Navigation.Category);
            else
                navigationService.NavigateTo(Common.Navigation.Join);
        }
        #endregion

        /// <summary>
        /// Task, that's reloading the List of Data (Players or Statistics) while the user is on the page
        /// stops as soon as the User leaves this page
        /// </summary>
        /// <returns>returns nothing</returns>
        private async Task DataLoading()
        {
            while (isActive)
            {
                await Task.Delay(4000);
                if (isActive)
                    ReloadPlayers();
            }
        }

        /// <summary>
        /// Loads the data needed on the page
        /// and sets some parameters
        /// This function is called when navigated to this Page
        /// </summary>
        public async void LoadData()
        {
            LoadingActive = true;
            ShowMenu = false;
            NextAvailable = false;
            UserProfile = await dataService.GetUserprofile(mvm.SystemUserId.Id);
            PlayersList = string.Empty;
            PlayersListHeader = string.Empty;
            LoadingActive = false;
            DataLoading();
        }
    }
}
