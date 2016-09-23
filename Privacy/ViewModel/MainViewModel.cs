using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Privacy.Model;
using Privacy.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        #region variables
       
        #region public variables
        public List<Language> Languages { get; set; }
        public int SelectedLanguage { set; get; }
        public ID SystemUserId;
        public Profile SystemUserProfile { get; set; }
        public bool UserControlsEnabled { get; set; }
        public bool StartScreenVisible { get { return !UserControlsEnabled; } }
        public bool NameInfoAvailable { get { return UserName == null || UserName == String.Empty; } }
        public bool LangInfoAvailable { get { return SelectedLanguage == -1; } }
        public string UserName { get; set; }
        public LangPCK LanguagePackage { get; set; }
        public bool AdvancedInformation { get; set; }
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;
        private readonly IStorageService storageService;
        #endregion
       
        #endregion

        /// <summary>
        /// Constructor of the Navigationviewmodel, which saves the given parameters to private readonly fields
        /// </summary>
        /// <param name="navigationService">Instance of an implementation of Galasoft's INavigationService</param>
        /// <param name="dataService">Instance of an implementation of the IDataService</param>
        /// <param name="storageService">Instance of an implementation of the IStorageService</param>
        public MainViewModel(INavigationService navigationService, IDataService dataService, IStorageService storageService)
        {
            SelectedLanguage = -1;
            this.navigationService = navigationService;
            this.dataService = dataService;
            this.storageService = storageService;
            SystemUserId = this.storageService.Read<ID>(nameof(SystemUserId), new ID { Id=0});
            DefaultLanguage();
        }

        /// <summary>
        /// Called when the User has entered all the Information
        /// After a view pre-checks the Data is sent to the server to create an user and returning the USER's ID
        /// The Function stores the ID Permanently on the device
        /// </summary>
        public async void FinishSetup()
        {
            if (UserName == String.Empty || UserName == null)
                UserName = "Windows Phone User";
            if (UserName.Length > 24)
                UserName = UserName.Replace(System.Environment.NewLine, " ").Remove(20);
            if (SelectedLanguage < 0)
                SelectedLanguage = 0;
            SystemUserId = await dataService.CreateUser(Languages.ElementAt(SelectedLanguage).Id, UserName);
            storageService.Write(nameof(SystemUserId), SystemUserId);
            ReloadUserProfile();
            navigationService.NavigateTo(Common.Navigation.CentralMenu);
        }

        /// <summary>
        /// Called when navigated to the MainPage, loads the Data needed, before displaying the Usercontrols
        /// If a user has already been created it's redirecting to the central Menu Page
        /// </summary>
        public async void LoadData()
        {
            UserControlsEnabled = false;
            if (SystemUserId.Id > 0)
                if (await dataService.IsUserExisting(SystemUserId.Id))
                {
                    LoadSettings();
                    if (storageService.Read<string>("SystemLangName", "DEFAULT") == "DEFAULT")
                    {
                        LoadLanguagePackage();
                    }
                    else
                    {
                        var pckname = storageService.Read<string>("SystemLangName");
                        var LanguagePackage = storageService.Read<LangPCK>(pckname);
                        if ((await dataService.GetLangVersion(LanguagePackage.Id, LanguagePackage.Version)).VersionNr != LanguagePackage.Version)
                        {
                            LoadLanguagePackage();
                        }

                    }
                    navigationService.NavigateTo(Common.Navigation.CentralMenu);
                    ReloadUserProfile();
                    return;
                }
            Languages = (await dataService.GetLanguages()).ToList();
            UserControlsEnabled = true;
            DefaultLanguage();
        }

        /// <summary>
        /// When called reloads the users profile, if a profile with the user's id is not existent, he will be directed to the main page
        /// </summary>
        public async void ReloadUserProfile()
        {
            if (await dataService.IsUserExisting(SystemUserId.Id))
            {
                if (LanguagePackage.Id == 0)
                {
                    try
                    {
                        LoadLanguagePackage();
                    }
                    catch (Exception ex)
                    {
                        DefaultLanguage();
                    }
                }
                var prof = await dataService.GetUserprofile(SystemUserId.Id);
                if (SystemUserProfile == null)
                    SystemUserProfile = prof;
                else
                {
                    SystemUserProfile.ID = prof.ID;
                    SystemUserProfile.Lang.Id = prof.Lang.Id;
                    SystemUserProfile.Lang.Title = prof.Lang.Title;
                    SystemUserProfile.Name = prof.Name;
                    SystemUserProfile.Points = prof.Points;
                }
                if (SystemUserProfile.Lang.Id != LanguagePackage.LanguageID)
                    LoadLanguagePackage();
            }
            else
                navigationService.NavigateTo(Common.Navigation.Main);
        }

        /// <summary>
        /// Closes the Application
        /// </summary>
        public void GoBackRequest()
        {
            App.Current.Exit();
        }

        public async void LoadLanguagePackage()
        {
                var lpg = await dataService.GetLanguagePack(SystemUserId.Id);
            if (LanguagePackage == null)
                    LanguagePackage = lpg;
                else
                {
                LanguagePackage.AboutInfo = lpg.AboutInfo;
                LanguagePackage.ApplyTranslation = lpg.ApplyTranslation;
                LanguagePackage.BackTranslation = lpg.BackTranslation;
                LanguagePackage.CategoryText = lpg.CategoryText;
                LanguagePackage.CreateGameTranslation = lpg.CreateGameTranslation;
                LanguagePackage.CreateInfo = lpg.CreateInfo;
                LanguagePackage.DiscardTranslation = lpg.DiscardTranslation;
                LanguagePackage.GuessInfo = lpg.GuessInfo;
                LanguagePackage.GuessTranslation = lpg.GuessTranslation;
                LanguagePackage.HomeTranslation = lpg.HomeTranslation;
                LanguagePackage.Id = lpg.Id;
                LanguagePackage.JoiniInfo = lpg.JoiniInfo;
                LanguagePackage.JoinTranslation = lpg.JoinTranslation;
                LanguagePackage.LanguageID = lpg.LanguageID;
                LanguagePackage.LanguageTranslation = lpg.LanguageTranslation;
                LanguagePackage.LobbyInfo1 = lpg.LobbyInfo1;
                LanguagePackage.LobbyInfo2 = lpg.LobbyInfo2;
                LanguagePackage.LobbyInfo3 = lpg.LobbyInfo3;
                LanguagePackage.LobbyTranslation = LanguagePackage.LobbyTranslation;
                LanguagePackage.MainInfo = lpg.MainInfo;
                LanguagePackage.MenuTranslation = LanguagePackage.MenuTranslation;
                LanguagePackage.NameTranslation = lpg.NameTranslation;
                LanguagePackage.NoTranslation = lpg.NoTranslation;
                LanguagePackage.PackageName = lpg.PackageName;
                LanguagePackage.PlayersTranslation = lpg.PlayersTranslation;
                LanguagePackage.PointsTranslation = lpg.PointsTranslation;
                LanguagePackage.ProfileTranslation = lpg.ProfileTranslation;
                LanguagePackage.QuestionInfo = lpg.QuestionInfo;
                LanguagePackage.QuestionTranslation = lpg.QuestionTranslation;
                LanguagePackage.QuitMsg = lpg.QuitMsg;
                LanguagePackage.QuitTranslation = lpg.QuitTranslation;
                LanguagePackage.SettingsInfo = lpg.SettingsInfo;
                LanguagePackage.SettingsTranslation = lpg.SettingsTranslation;
                LanguagePackage.SetupInfo = lpg.SetupInfo;
                LanguagePackage.SetupTranslation = lpg.SetupTranslation;
                LanguagePackage.StartTranslation = lpg.StartTranslation;
                LanguagePackage.Version = lpg.Version;
                LanguagePackage.YesTranslation = lpg.YesTranslation;
                LanguagePackage.CreditTranslation = lpg.CreditTranslation;
                LanguagePackage.GameIdTranslation = lpg.GameIdTranslation;
                LanguagePackage.GameTranslation = lpg.GameTranslation;
                LanguagePackage.AnsweredPlayersTranslation = lpg.AnsweredPlayersTranslation;
                LanguagePackage.YesVoteText = lpg.YesVoteText;
                LanguagePackage.NotificationTranslation = lpg.NotificationTranslation;
                LanguagePackage.LobbyMsg1 = lpg.LobbyMsg1;
                LanguagePackage.LobbyMsg2 = lpg.LobbyMsg2;
                LanguagePackage.LobbyMsg3 = lpg.LobbyMsg3;
                LanguagePackage.CongratulationsTranslation = lpg.CongratulationsTranslation;
                LanguagePackage.CategoryTranslation = lpg.CategoryTranslation;
                LanguagePackage.GuessText = lpg.GuessText;
                LanguagePackage.CategoryText = lpg.CategoryText;
                LanguagePackage.ConfirmationTranslation = lpg.ConfirmationTranslation;
                LanguagePackage.LoadingTranslation = lpg.LoadingTranslation;
                LanguagePackage.LoadMsg1 = lpg.LoadMsg1;
                LanguagePackage.LoadMsg2 = lpg.LoadMsg2;
                LanguagePackage.LoadMsg3 = lpg.LoadMsg3;
                LanguagePackage.PickLanguageTranslation=lpg.PickLanguageTranslation;
                LanguagePackage.YourNameTranslation = lpg.YourNameTranslation;
                LanguagePackage.AdditionalInformationTranslation = lpg.AdditionalInformationTranslation;
                LanguagePackage.AllowGuessingTranslation = lpg.AllowGuessingTranslation;
                LanguagePackage.AllowStatisticTranslation = lpg.AllowStatisticTranslation;
                LanguagePackage.ContinueTranslation = lpg.ContinueTranslation;
                LanguagePackage.SaveTranslation = lpg.SaveTranslation;
                }
            storageService.Write(lpg.PackageName, lpg);
            storageService.Write("SystemLangName", lpg.PackageName);
        }

        private void LoadSettings()
        {
            if (storageService.Read<string>("LocalSettings", "DEFAULT") == "DEFAULT")
            {
                WriteSettings(true);
            }
            else
            {
                AdvancedInformation = storageService.Read<bool>("LocalSettings");
            }
        }
        public void WriteSettings(bool advancedInfo)
        {
            storageService.Write("LocalSettings", advancedInfo);
            AdvancedInformation = advancedInfo;
        }

        private void DefaultLanguage()
        {
            LanguagePackage = new LangPCK
            {
                Id = 0,
                LanguageID = 0,
                Version = 1.0,
                PackageName = "DEFAULT",
                YesTranslation = "Yes",
                NoTranslation = "No",
                MenuTranslation = "Menu",
                SettingsTranslation = "Settings",
                ProfileTranslation = "Profile",
                PointsTranslation = "Points",
                LanguageTranslation = "Language",
                NameTranslation = "Name",
                QuitTranslation = "Quit",
                HomeTranslation = "Home",
                QuestionTranslation = "Question",
                ApplyTranslation = "Apply",
                DiscardTranslation = "Discard",
                BackTranslation = "Back",
                CreateGameTranslation = "Create Game",
                CategoryTranslation = "Category",
                JoinTranslation = "Join",
                LobbyTranslation = "Lobby",
                PlayersTranslation = "Players",
                StartTranslation = "Start",
                SetupTranslation = "Setup",
                GuessTranslation = "Guess",
                QuitMsg = "Do you really want to quit the current game?",
                SetupInfo = "Before we can start, we need some information. Please enter a name you want to use and select your prefered language.",
                MainInfo = "If you want to join a running game, select join, if you want to start a new one, select create. Press the Button in the left corner for more options.",
                CreateInfo = "Select a category. Each category contains a set of questions suitable to the category.",
                JoiniInfo = "Enter the ID of the game you want to join.",
                QuestionInfo = "Please answer the queston truthfully. We won\t tell your answer anyone ;)",
                SettingsInfo = "Here you can change your language and your username. Also you can enable or disable additional information like this.",
                AboutInfo = "Translation of the about text",
                GuessInfo = "Guess how many of your friends have answered the question bevore with 'YES'.",
                LobbyInfo1 = "Infotext lobby 1",
                LobbyInfo2 = "infotext lobby 2",
                LobbyInfo3 = "infotext lobby 3",
                AnsweredPlayersTranslation = "Answered Players",
                ConfirmationTranslation = "Confirmation",
                CongratulationsTranslation = "Congratulations",
                CreditTranslation = "Credits",
                GameIdTranslation = "Game ID",
                GameTranslation = "Game",
                LoadingTranslation = "Loading...",
                NotificationTranslation = "Notification",
                WrongIdTranslation = "Wrong Game ID",
                YesVoteText = "Number of yes-votes",
                CategoryText = "Select a category",
                GuessText = "of them have done that!",
                LoadMsg1 = "Loading Data ...",
                LobbyMsg1 = "Seems like the game has ended, you will be taken to the Main Menu.",
                LobbyMsg2 = "You've played through all the questions.",
                LobbyMsg3 = "Lobby_MSG3",
                AdditionalInformationTranslation="Additional Information",
                PickLanguageTranslation="Pick a language",
                YourNameTranslation="Your name",
                LoadMsg2="LoadMsg2",
                LoadMsg3="LoadMsg3",
                AllowGuessingTranslation="Unlock guessing",
                AllowStatisticTranslation="Unlock statistic",
                ContinueTranslation="Continue",
                SaveTranslation="Save",
                LobbyHostInfo1= "Select 'Unlock guessing' when all the players have given their answer",
                LobbyHostInfo2= "Select 'Unlock statistic' when all the players have voted",
                LobbyHostInfo3="Host info 3"
            };
        }
    }
}
