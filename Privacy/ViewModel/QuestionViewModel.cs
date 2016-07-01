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
    public class QuestionViewModel :ViewModelBase
    {
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public string Mode { get; set; }
        public string QuestionContent { get { return Question.title+"\nMode: " + Mode; } }
        public Profile UserProfile { get { return dataService.GetUserprofile(mvm.SystemUserId); } }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#"+jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public Question Question { get; set; }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        private readonly JoinGameViewModel jvm;
        private readonly CategoryViewModel cvm;
        public bool Answer { get; set; }
        public QuestionViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm, JoinGameViewModel jvm, CategoryViewModel cvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
            this.jvm = jvm;
            this.cvm = cvm;
        }
        public void LoadNewQuestion()
        {
            Question = dataService.GetQuestionByUserAndGameId(mvm.SystemUserId, Mode == Common.Mode.IsClient ? jvm.SystemGameID : cvm.SystemGameID);
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void NavigateToYes()
        {
            Answer = true;
            NavigateToLobbyView();
        }
        public void NavigateToNo()
        {
            Answer = false;
            NavigateToLobbyView();
        }
        public void GoBackRequest()
        {
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Join);
            else
                navigationService.NavigateTo(Common.Navigation.Category);
        }
        public void NavigateToCentralMenu()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        private void NavigateToLobbyView()
        {
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.ClientWait);
            else if (Mode == Common.Mode.IsHost)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostWait);
        }
    }
}
