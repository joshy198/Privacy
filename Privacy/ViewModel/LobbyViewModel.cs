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
    public class LobbyViewModel:ViewModelBase
    {
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public string Mode { get; set; }
        public string PlayersList { get; set; }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#" + jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public string ButtonText { get {
                if (Common.Mode.HostStart == Mode)
                    return "Start Game";
                else if (Common.Mode.ClientStatistic == Mode)
                    return "Continue";
                else if (Common.Mode.HostStatistic == Mode)
                    return "Continue";
                else if (Common.Mode.HostWait == Mode)
                    return "Ok";
                else if (Common.Mode.ClientWait == Mode)
                    return "Continue";
                else if (Common.Mode.IsClient == Mode)
                    return "Continue";
                    return "ERR";
            } }
        public string PlayersListHeader { get; set; }
        public Profile UserProfile { get { return dataService.GetUserprofile(mvm.SystemUserId); } }
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
        public void ReloadPlayers()
        {
            PlayersList = string.Empty;
            if (Common.Mode.HostStart == Mode)
            {
                PlayersListHeader = "Player\t @Game "+cvm.SystemGameID;
                foreach (var v in dataService.GetPlayersInGame(cvm.SystemGameID))
                {
                    PlayersList += v.title + "\n";
                }
            }
            else if (Common.Mode.ClientWait == Mode)
            {
                PlayersListHeader = "Player";
                foreach (var v in dataService.GetAnsweredUsers(jvm.SystemGameID))
                {
                    PlayersList += v.title + "\n";
                }
            }
            else if (Common.Mode.HostWait == Mode)
            {
                PlayersListHeader = "Player";
                foreach (var v in dataService.GetAnsweredUsers(cvm.SystemGameID))
                {
                    PlayersList += v.title + "\n";
                }
            }
            else if (Common.Mode.ClientStatistic == Mode)
            {
                PlayersListHeader = "Player\t\tDifference\tTotal";
                foreach (var v in dataService.GetStatisticByGameId(cvm.SystemGameID).OrderByDescending(x => x.points))
                {
                    PlayersList += String.Format("{0}\t\t{1,7}\t\t{2}\n", v.name, Math.Abs(v.guessed-v.yeses), v.points);
                    //PlayersList += ""+v.name + "\n\n";
                }
            }
            else if (Common.Mode.HostStatistic == Mode)
            {
                foreach (var v in dataService.GetStatisticByGameId(cvm.SystemGameID).OrderByDescending(x => x.points))
                {
                    PlayersList += String.Format("{0}\t\t{1,7}\t\t{2}\n", v.name, Math.Abs(v.guessed - v.yeses), v.points);
                    //PlayersList += ""+v.name + "\n\n";
                }
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
        public void NavigateToQuestionView()
        {
            if (Common.Mode.HostStart == Mode || Common.Mode.HostStatistic == Mode)
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsHost);
            else if (Common.Mode.ClientStatistic == Mode)
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsClient);
            else if (Common.Mode.HostWait == Mode)
                navigationService.NavigateTo(Common.Navigation.Guess, Common.Mode.IsHost);
            else if (Common.Mode.ClientWait == Mode)
                navigationService.NavigateTo(Common.Navigation.Guess, Common.Mode.IsClient);
            else if (Common.Mode.IsClient == Mode)
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsClient);
        }
    }
}
