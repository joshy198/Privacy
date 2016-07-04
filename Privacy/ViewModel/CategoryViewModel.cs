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
    public class CategoryViewModel:ViewModelBase
    {
        public List<Group> Groups { get; set; }
        public int SelectedGroup { get; set; }
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        public Profile UserProfile { get; set; }
        public ulong SystemGameID { get; set; }
        private List<ID> QuestionIDs { get; set; }
        private int pos=0;
        public CategoryViewModel(INavigationService navigationService, IDataService dataService,MainViewModel mvm)
        {
            SelectedGroup = -1;
            //Groups = new List<Group> { new Group { id = 1, title = "Group 1" }, new Group { id = 2, title = "Group 2" } };
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        public async void NavigateToLobby()
        {
            var rnd = new Random();
            QuestionIDs =(await dataService.GetQuestionIdsByGroupId(Groups.ElementAt(SelectedGroup).ID)).OrderBy(item => rnd.Next()).ToList();
            SystemGameID=(await dataService.NewGame(mvm.SystemUserId.Id, QuestionIDs.First().Id)).Id;
            navigationService.NavigateTo(Common.Navigation.Lobby,Common.Mode.HostStart);
        }
        public async Task<bool> GoToNextQuestion()
        {
            pos++;
            if (QuestionIDs.Count > pos)
                if (await dataService.ForceNextQuestion(mvm.SystemUserId.Id, SystemGameID, QuestionIDs.ElementAt(pos).Id))
                    return true;
            return false;
        }
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void GoBackRequest()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        public async void LoadData()
        {
            ShowMenu = false;
            SelectedGroup = -1;
            pos = 0;
            Groups = (await dataService.GetQuestionGroupsByUserId(mvm.SystemUserId.Id)).ToList();
            UserProfile = (await dataService.GetUserprofile(mvm.SystemUserId.Id));
        }

    }
}
