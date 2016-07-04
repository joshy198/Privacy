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
        public bool LoadingActive { get; set; }
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
                    //NextAvailable = true;
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
                    NextAvailable = true;
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
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly JoinGameViewModel jvm;
        private readonly CategoryViewModel cvm;
        private readonly MainViewModel mvm;
        public LobbyViewModel(INavigationService navigationService, IDataService dataService, JoinGameViewModel jvm, CategoryViewModel cvm, MainViewModel mvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.jvm = jvm;
            this.cvm = cvm;
            this.mvm = mvm;
        }
        public async void LoadData()
        {
            LoadingActive = true;
            ShowMenu = false;
            NextAvailable = false;
            UserProfile = await dataService.GetUserprofile(mvm.SystemUserId.Id);
            ReloadPlayers();
            LoadingActive = false;
        }
        public async void EnableNext()
        {
            LoadingActive = true;
            if (await dataService.IsGameExisting(cvm.SystemGameID))
            {
                if (Common.Mode.HostWait == Mode)
                    NextAvailable = await dataService.AllowCounting(mvm.SystemUserId.Id, cvm.SystemGameID);
                else if (Common.Mode.HostStatistic == Mode)
                    NextAvailable = await dataService.AllowStatistics(mvm.SystemUserId.Id, cvm.SystemGameID);
            }
            else
            NavigateToCentralMenu();
            LoadingActive = false;
        }
        
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void GoBackRequest()
        {
            if (Mode == Common.Mode.HostStart || Mode == Common.Mode.HostStatistic || Mode == Common.Mode.HostWait)
                navigationService.NavigateTo(Common.Navigation.Category);
            else
                navigationService.NavigateTo(Common.Navigation.Join);
        }
        public async void ReloadPlayers()
        {
            PlayersList = string.Empty;
            if (Common.Mode.HostStart == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    PlayersListHeader = "Game ID " + cvm.SystemGameID;
                    foreach (var v in await dataService.GetPlayersInGame(cvm.SystemGameID))
                    {
                        PlayersList += v.Title + "\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.IsClient == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    PlayersListHeader = "Players";
                    foreach (var v in await dataService.GetPlayersInGame(jvm.SystemGameID))
                    {
                        PlayersList += v.Title + "\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.ClientWait == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    NextAvailable = await dataService.IsContinueAllowed(jvm.SystemGameID);
                    PlayersListHeader = "Answered Players";
                    foreach (var v in await dataService.GetAnsweredUsers(jvm.SystemGameID))
                    {
                        PlayersList += v.Title + "\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.HostWait == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    PlayersListHeader = "Answered Players";
                    foreach (var v in await dataService.GetAnsweredUsers(cvm.SystemGameID))
                    {
                        PlayersList += v.Title + "\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.ClientStatistic == Mode)
            {
                if (await dataService.IsGameExisting(jvm.SystemGameID))
                {
                    PlayersListHeader = "Player\t\tDifference\tTotal";
                    foreach (var v in (await dataService.GetStatisticByGameId(jvm.SystemGameID)).OrderByDescending(x => x.Points))
                    {
                        if (v.Name.Length > 15)
                            v.Name = v.Name.Remove(15);
                        PlayersList += v.Name.PadRight(15, ' ');
                        if (v.Name.Length < 10)
                            PlayersList += "\t";
                        PlayersList += String.Format("{0,9}\t\t{1}\n", Math.Abs(v.Guessed - v.Yeses), v.Points);
                        //PlayersList += ""+v.name + "\n\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
            else if (Common.Mode.HostStatistic == Mode)
            {
                if (await dataService.IsGameExisting(cvm.SystemGameID))
                {
                    PlayersListHeader = "Player\t\tDifference\tTotal";
                    foreach (var v in (await dataService.GetStatisticByGameId(cvm.SystemGameID)).OrderByDescending(x => x.Points))
                    {
                        if (v.Name.Length > 15)
                            v.Name = v.Name.Remove(15);
                        PlayersList += v.Name.PadRight(15, ' ');
                        if (v.Name.Length < 10)
                            PlayersList += "\t";
                        PlayersList += String.Format("{0,9}\t\t{1}\n", Math.Abs(v.Guessed - v.Yeses), v.Points);
                        //PlayersList += ""+v.name + "\n\n";
                    }
                }
                else
                    NavigateToCentralMenu();
            }
        }

        public void NavigateToCentralMenu()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
            public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
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

        private async void DataLoading()
        { }

    }
}
