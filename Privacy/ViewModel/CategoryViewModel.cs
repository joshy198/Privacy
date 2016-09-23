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
        #region variables

        #region public variables
        public bool LoadingActive { get; set; }
        public List<Group> Groups { get; set; }
        public int SelectedGroup { get; set; }
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public Profile UserProfile { get; set; }
        public ulong SystemGameID { get; set; }
        public LangPCK LanguagePackage { get; set; }
        public bool AdvancedInformation { get { return mvm.AdvancedInformation; } }
        #endregion

        #region private variables
        private List<ID> QuestionIDs { get; set; }
        private int pos = 0;
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        #endregion

        #endregion

        /// <summary>
        /// Constructor of the CategoryViewModel
        /// Sets the given parameters to private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of GalaSoft's INavigationService Interface</param>
        /// <param name="dataService">Instance of an Implementation of the IDataService Interface</param>
        /// <param name="mvm">Instance of the MainViewModel</param>
        public CategoryViewModel(INavigationService navigationService, IDataService dataService,MainViewModel mvm)
        {
            SelectedGroup = -1;
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
        }

        /// <summary>
        /// sets the next question on the server
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GoToNextQuestion()
        {
            pos++;
            if (QuestionIDs.Count > pos)
                if (await dataService.ForceNextQuestion(mvm.SystemUserId.Id, SystemGameID, QuestionIDs.ElementAt(pos).Id))
                    return true;
            return false;
        }

        /// <summary>
        /// Hides/shows the hamburger menu
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }

        #region Navigation
        /// <summary>
        /// Loads All the Questions of the selected Group
        /// creates a new game
        /// navigates to the LobbyView
        /// </summary>
        public async void NavigateToLobby()
        {
            try
            {
                LoadingActive = true;
                QuestionIDs = (await dataService.GetQuestionIdsByGroupId(Groups.ElementAt(SelectedGroup).ID)).ToList();
                SystemGameID = (await dataService.NewGame(mvm.SystemUserId.Id, QuestionIDs.First().Id)).Id;
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostStart);
                LoadingActive = false;
            }
            catch (Exception ex)
            {
                LoadingActive = false;
                navigationService.NavigateTo(Common.Navigation.CentralMenu);
            }
        }
        
        /// <summary>
        /// Navigates to the Settings Page
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        
        /// <summary>
        /// Navigates to the CentralMenu Page
        /// </summary>
        public void GoBackRequest()
        {
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        #endregion

        /// <summary>
        /// Loads the Data Needed for this Page
        /// </summary>
        public async void LoadData()
        {
            LoadingActive = true;
            LanguagePackage = mvm.LanguagePackage;
            ShowMenu = false;
            SelectedGroup = -1;
            pos = 0;
            if(Groups==null)
            Groups = (await dataService.GetQuestionGroupsByUserId(mvm.SystemUserId.Id)).ToList();
            UserProfile = mvm.SystemUserProfile;
            SelectedGroup = 0;
            LoadingActive = false;
        }

    }
}
