﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Model;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// <summary>
        /// Boolean, auf das von der UI Zugegriffen wird, ist true, wenn Daten Vor bzw. nach der Navigation geladen werden
        /// </summary>
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
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#" + jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public string ButtonText { get {
                if (Common.Mode.HostStart == Mode)
                {
                    NextAvailable = true;
                    return LanguagePackage.StartTranslation;
                }
                else if (Common.Mode.ClientStatistic == Mode)
                    return LanguagePackage.ContinueTranslation;
                else if (Common.Mode.HostStatistic == Mode)
                {
                    NextAvailable = true;
                    return LanguagePackage.ContinueTranslation;
                }
                else if (Common.Mode.HostWait == Mode)
                {
                    //NextAvailable = true;
                    return "Ok";
                }
                else if (Common.Mode.ClientWait == Mode)
                    return LanguagePackage.ContinueTranslation;
                else if (Common.Mode.IsClient == Mode)
                {
                    return LanguagePackage.StartTranslation;
                }
                return "ERR";
            } }
        public string AdminText
        {
            get
            {
                if (Common.Mode.HostStatistic == Mode)
                {
                    return LanguagePackage.AllowStatisticTranslation;
                }
                else if (Common.Mode.HostWait == Mode)
                {
                    return LanguagePackage.AllowGuessingTranslation;
                }
                return "ERR";
            }
        }
        public bool StatisticVisible { get { return Common.Mode.HostStatistic == Mode || Common.Mode.ClientStatistic == Mode; } }
        public bool PlayersVisible { get { return !StatisticVisible; } }
        public string PlayersListHeader { get; set; }
        public Profile UserProfile { get; set; }
        public ObservableCollection<Statistic> Statistics { get; set; }
        public ObservableCollection<Player> Players { get; set; }
        public LangPCK LanguagePackage { get; set; }
        public string LobbyInformation
        {
            get
            {
                if (Common.Mode.HostStart == Mode|| Common.Mode.IsClient == Mode)
                    return LanguagePackage.LobbyInfo1;//MEssage: Here you see all the players, who already joined this game. Select "Start" to get to the first question.
                else if (Common.Mode.ClientStatistic == Mode)
                    return LanguagePackage.LobbyInfo2;
                else if (Common.Mode.HostWait == Mode)
                    return LanguagePackage.LobbyHostInfo2;
                else if (Common.Mode.ClientWait == Mode)
                    return LanguagePackage.LobbyInfo1;
                return "ERR";
            }
        }
        public bool AdvancedInformation { get { return mvm.AdvancedInformation; } }
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
            Players = new ObservableCollection<Player>();
            Statistics = new ObservableCollection<Statistic>();
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
            {
                ClearQuit();
                NavigateToCentralMenu();
            }
            LoadingActive = false;
        }
        public void ClearQuit()
        {
            Players.Clear();
            Statistics.Clear();
            dataService.QuitGame(mvm.SystemUserId.Id);
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
            try
            {
                if (Common.Mode.HostStart == Mode)
                {
                    PlayersListHeader = LanguagePackage.GameIdTranslation+" " + cvm.SystemGameID;
                    var data = await dataService.GetPlayersInGame(cvm.SystemGameID);
                    if (Players.Count != 0)
                        foreach (var v in Players)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Title == v.Title).Count() == 0)
                                Players.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Players.Where(x => x.ID == v.ID).Count() == 0)
                            Players.Add(v);
                    }
                }
                else if (Common.Mode.IsClient == Mode)
                {
                    PlayersListHeader = LanguagePackage.PlayersTranslation;

                    var data = await dataService.GetPlayersInGame(jvm.SystemGameID);
                    if (Players.Count != 0)
                        foreach (var v in Players)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Title == v.Title).Count() == 0)
                                Players.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Players.Where(x => x.ID == v.ID).Count() == 0)
                            Players.Add(v);
                    }
                    NextAvailable = true;
                }
                else if (Common.Mode.ClientWait == Mode)
                {
                    PlayersListHeader = LanguagePackage.AnsweredPlayersTranslation;
                    NextAvailable = await dataService.IsContinueAllowed(jvm.SystemGameID);
                    var data = await dataService.GetAnsweredUsers(jvm.SystemGameID);
                    if (Players.Count != 0)
                        foreach (var v in Players)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Title == v.Title).Count() == 0)
                                Players.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Players.Where(x => x.ID == v.ID).Count() == 0)
                            Players.Add(v);
                    }
                }
                else if (Common.Mode.HostWait == Mode)
                {
                    PlayersListHeader = LanguagePackage.AnsweredPlayersTranslation;
                    var data = await dataService.GetAnsweredUsers(cvm.SystemGameID);
                    if (Players.Count != 0)
                        foreach (var v in Players)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Title == v.Title).Count() == 0)
                                Players.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Players.Where(x => x.ID == v.ID).Count() == 0)
                            Players.Add(v);
                    }
                }
                else if (Common.Mode.ClientStatistic == Mode)
                {
                    NextAvailable = !await dataService.IsContinueAllowed(jvm.SystemGameID);
                    var data = await dataService.GetStatisticByGameId(jvm.SystemGameID);
                    if (Statistics.Count != 0)
                        foreach (var v in Statistics)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Name == v.Name).Count() == 0)
                                Statistics.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Statistics.Where(x => x.ID == v.ID).Count() == 0)
                            Statistics.Add(v);
                    }
                    PlayersListHeader = LanguagePackage.YesVoteText+" " + Statistics.FirstOrDefault().Yesses;
                }
                else if (Common.Mode.HostStatistic == Mode)
                {
                    var data = await dataService.GetStatisticByGameId(cvm.SystemGameID);
                    if (Statistics.Count != 0)
                        foreach (var v in Statistics)
                        {
                            if (data.Where(x => x.ID == v.ID && x.Name == v.Name).Count() == 0)
                                Statistics.Remove(v);
                        }
                    foreach (var v in data)
                    {
                        if (Statistics.Where(x => x.ID == v.ID).Count() == 0)
                            Statistics.Add(v);
                    }
                    PlayersListHeader = LanguagePackage.YesVoteText+" " + Statistics.FirstOrDefault().Yesses;
                }
            }
            catch (Exception ex)
            {
                if (Mode == Common.Mode.ClientStatistic || Mode == Common.Mode.ClientWait || Mode == Common.Mode.IsClient)
                {
                    if (!await dataService.IsGameExisting(jvm.SystemGameID))
                    {
                        if (isActive)
                        {
                            var dialog = new MessageDialog(LanguagePackage.LobbyMsg1);
                            dialog.Title = LanguagePackage.NotificationTranslation;
                            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                            var res = await dialog.ShowAsync();
                            if ((int)res.Id == 0)
                            {
                                navigationService.NavigateTo(Common.Navigation.CentralMenu);
                            }
                        }
                    }
                }
                else
                {
                    if (!await dataService.IsGameExisting(cvm.SystemGameID))
                    {
                        if (isActive)
                        {
                            var dialog = new MessageDialog(LanguagePackage.LobbyMsg1);
                            dialog.Title = LanguagePackage.NotificationTranslation;
                            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                            var res = await dialog.ShowAsync();
                            if ((int)res.Id == 0)
                            {
                                navigationService.NavigateTo(Common.Navigation.CentralMenu);
                            }
                        }
                    }
                }
            }
        }

        #region Navigation

        /// <summary>
        /// Quits the Game and navigates to the Central Menu
        /// </summary>
        public void NavigateToCentralMenu()
        {
            ClearQuit();
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
                    var dialog = new MessageDialog(LanguagePackage.LobbyMsg2);
                    dialog.Title = LanguagePackage.CongratulationsTranslation;
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
            ClearQuit();
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
            LanguagePackage = mvm.LanguagePackage;
            ShowMenu = false;
            NextAvailable = false;
            UserProfile = mvm.SystemUserProfile;
            PlayersListHeader = string.Empty;
            Players.Clear();
            Statistics.Clear();
            LoadingActive = false;
            DataLoading();
        }
    }
}
