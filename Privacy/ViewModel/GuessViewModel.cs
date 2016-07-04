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

        public string AnswerString { get { return SelectedAmmountOfPlayers + " of them have done that!"; } }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public bool ShowMenu { get; set; }
        public string Mode { get; set; }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#" + jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public string GameInfo { get { return Common.Mode.IsClient == Mode ? jvm.SystemGameID.ToString() : cvm.SystemGameID.ToString(); } }
        public int NumberOfPlayers { get; set; }
        public Profile UserProfile { get; set; }
        public int SelectedAmmountOfPlayers { get; set; }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly CategoryViewModel cvm;
        private readonly JoinGameViewModel jvm;
        private readonly MainViewModel mvm;
        private readonly QuestionViewModel qvm;
        public GuessViewModel(INavigationService navigationService, IDataService dataService, CategoryViewModel cvm, JoinGameViewModel jvm, MainViewModel mvm, QuestionViewModel qvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.jvm = jvm;
            this.cvm = cvm;
            this.mvm = mvm;
            this.qvm = qvm;
        }
        public async void NavigateToQuestion()
        {
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
        }
        public void GoBackRequest()
        {
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Join);
            else
                navigationService.NavigateTo(Common.Navigation.Category);
        }
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void NavigateToCentralMenu()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public async void LoadData()
        {
            ShowMenu = false;
            NumberOfPlayers = (await dataService.CountPlayersByGameId(Common.Mode.IsClient == Mode ? jvm.SystemGameID : cvm.SystemGameID));
            UserProfile = (await dataService.GetUserprofile(mvm.SystemUserId.Id));
    }
    }
}
