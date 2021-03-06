﻿using GalaSoft.MvvmLight;
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
    public class QuestionViewModel :ViewModelBase
    {
        #region variables
        #region public variables
        public bool LoadingActive { get; set; }
        public bool ShowMenu { get; set; }
        public int MenuSize { get { return ShowMenu ? 200 : 0; } }
        public string Mode { get; set; }
        public Profile UserProfile { get; set; }
        public string DisplayGameID { get { return Mode == Common.Mode.IsClient ? "#"+jvm.SystemGameID : "#" + cvm.SystemGameID; } }
        public Question Question { get; set; }
        public bool Answer { get; set; }
        public bool isActive { get; set; }
        public LangPCK LanguagePackage { get; set; }
        #endregion
        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly MainViewModel mvm;
        private readonly JoinGameViewModel jvm;
        private readonly CategoryViewModel cvm;
        private readonly LobbyViewModel lvm;
        public bool AdvancedInformation { get { return mvm.AdvancedInformation; } }
        #endregion
        #endregion

        /// <summary>
        /// Constructor of the QuestionViewModel
        /// Sets the input variables to the private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of a Implementation of GalaSoft's INavigationService Interface</param>
        /// <param name="dataService">Instance of a Implementation of the IDataService Interface</param>
        /// <param name="mvm">Instance of the MainViewModel</param>
        /// <param name="jvm">Instance of the JoinVewModel</param>
        /// <param name="cvm">Instance of the CategoryViewModel</param>
        public QuestionViewModel(INavigationService navigationService, IDataService dataService, MainViewModel mvm, JoinGameViewModel jvm, CategoryViewModel cvm,LobbyViewModel lvm)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.mvm = mvm;
            this.jvm = jvm;
            this.cvm = cvm;
            this.lvm = lvm;
        }
        
        #region HambugerMenuAction
        /// <summary>
        /// This function displays/hides the Hamburger Menu
        /// </summary>
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        #endregion

        #region Navigation
        /// <summary>
        /// Sends the user's answer (in this case 'YES') to the server and navigates to the next page
        /// </summary>
        public async void NavigateToYes()
        {
            LoadingActive = true;
            Answer = true;
            if(await dataService.AnswerQuestion(mvm.SystemUserId.Id, Mode == Common.Mode.IsClient ? jvm.SystemGameID : cvm.SystemGameID,Question.ID,true,null))
            NavigateToLobbyView();
            LoadingActive = false;
        }
        /// <summary>
        /// Sends the user's answer (in this case 'NO') to the server and navigates to the next page
        /// </summary>
        public async void NavigateToNo()
        {
            LoadingActive = true;
            Answer = false;
            if (await dataService.AnswerQuestion(mvm.SystemUserId.Id, Mode == Common.Mode.IsClient ? jvm.SystemGameID : cvm.SystemGameID, Question.ID, true, null))
                NavigateToLobbyView();
            else
            {
                if (await dataService.IsGameExisting(Mode == Common.Mode.IsClient ? jvm.SystemGameID : cvm.SystemGameID))
                {
                    if (isActive)
                    {
                        var dialog = new MessageDialog("Seems like the game has ended, you will be taken to the Main Menu");
                        dialog.Title = "Notification";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        var res = await dialog.ShowAsync();
                        if ((int)res.Id == 0)
                        {
                            navigationService.NavigateTo(Common.Navigation.CentralMenu);
                        }
                    }
                }
            }
            LoadingActive = false;
        }
        /// <summary>
        /// Quits the game and navigates back to the JoinView/Category view, depending on if the user is working als gameleader or as client
        /// </summary>
        public void GoBackRequest()
        {
            lvm.ClearQuit();   
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Join);
            else
                navigationService.NavigateTo(Common.Navigation.Category);
        }
        /// <summary>
        /// Quits the gae and navigates back to the central Menu Page
        /// </summary>
        public void NavigateToCentralMenu()
        {
            lvm.ClearQuit();
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }
        /// <summary>
        /// navigates to the Settings Page
        /// </summary>
        public void NavigateToSettings()
        {
            navigationService.NavigateTo(Common.Navigation.Settings);
        }
        /// <summary>
        /// navigates to the Lobby view and handing over the current Mode
        /// </summary>
        private void NavigateToLobbyView()
        {
            if (Mode == Common.Mode.IsClient)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.ClientWait);
            else if (Mode == Common.Mode.IsHost)
                navigationService.NavigateTo(Common.Navigation.Lobby, Common.Mode.HostWait);
        }
        #endregion

        #region Loading
        /// <summary>
        /// Loads the data needed to be displayed on this page
        /// </summary>
        public async void LoadData()
        {
            LoadingActive = true;
            LanguagePackage = mvm.LanguagePackage;
            ShowMenu = false;
            mvm.ReloadUserProfile();
            UserProfile = mvm.SystemUserProfile;
            var qtn = await dataService.GetQuestionByUserAndGameId(mvm.SystemUserId.Id, Mode == Common.Mode.IsClient ? jvm.SystemGameID : cvm.SystemGameID);
            if (Question == null)
                Question = qtn;
            else
            {
                Question.ID = qtn.ID;
                Question.Title = qtn.Title;
                RaisePropertyChanged(nameof(Question));
                if(Question.ID==0)
                    if (isActive)
                    {
                        var dialog = new MessageDialog("Seems like there is a Problem with your Internet connection, you're now taken to the Main Menu");
                        dialog.Title = "Notification";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        var res = await dialog.ShowAsync();
                        if ((int)res.Id == 0)
                        {
                            navigationService.NavigateTo(Common.Navigation.CentralMenu);
                        }
                    }
            }
            LoadingActive = false;
        }
        #endregion
    }
}
