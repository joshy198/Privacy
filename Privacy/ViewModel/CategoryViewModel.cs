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
        public List<Group> Groups { get { return dataService.GetQuestionGroupsByUserId(mvm.SystemUserId).ToList(); } }
        public int SelectedGroup { get; set; }
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        public Profile UserProfile { get { return dataService.GetUserprofile(mvm.SystemUserId); } }
        public ulong SystemGameID { get; set; }
        public List<ID> QuestionIDs { get; set; }
        public CategoryViewModel(INavigationService navigationService, IDataService dataService,MainViewModel mvm)
        {
            //Groups = new List<Group> { new Group { id = 1, title = "Group 1" }, new Group { id = 2, title = "Group 2" } };
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }
        public void NavigateToLobby()
        {
            var rnd = new Random();
            QuestionIDs =dataService.GetQuestionIdsByGroupId(Groups.ElementAt(SelectedGroup).id).OrderBy(item => rnd.Next()).ToList();
            SystemGameID=dataService.NewGame(mvm.SystemUserId, QuestionIDs.First().id);
            navigationService.NavigateTo(Common.Navigation.Lobby,Common.Mode.HostStart);
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

    }
}
